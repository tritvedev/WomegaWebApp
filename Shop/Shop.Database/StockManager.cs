using Microsoft.EntityFrameworkCore;
using Shop.Domain.Cart;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Database.Cart
{
    public class StockManager : IStockManager
    {
        private ApplicationDbContext _ctx;

        public StockManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateStock(Stock stock)
        {
            _ctx.Stock.Add(stock);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> DeleteStock(int id)
        {
            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == id);

            _ctx.Stock.Remove(stock);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> UpdateStockRange(List<Stock> stockList)
        {
            _ctx.Stock.UpdateRange(stockList);

            return _ctx.SaveChangesAsync();
        }

        public bool EnoughStock(int stockId, int qty)
        {
            // check if we have enough stock to do the following operation
            return _ctx.Stock.FirstOrDefault(x => x.Id == stockId).Qty >= qty;
        }

        public Stock GetStockWithProduct(int stockId)
        {
            return _ctx.Stock.Include(x => x.Product)
                    .FirstOrDefault(x => x.Id == stockId);
        }

        public Task PutStockOnHold(int stockId, int qty, string sessionId)
        {
            /*
                * Database Responsibility: to update the stock
                * */

            // begin transaction

            // update stock set qty = qty + {0} where id = {1}

            _ctx.Stock.FirstOrDefault(x => x.Id == stockId).Qty -= qty;


            var stockOnHold = _ctx.StocksOnHold
                                .AsEnumerable()
                                .Where(x => x.SessionId == sessionId)
                                .ToList();

            // select count (*) from stockonhold where  stockId = {0} and sessionId = {1}

            // if item exists in the stockOnHold, then increment it's quantity otherwise add it
            if (stockOnHold.Any(x => x.StockId == stockId)) // if there is any item in the list
            {
                // update stockonhold set qty = qty + {0}
                // where stockId = {1} and sessionId = {2}
                // add to stock on hold
                stockOnHold.Find(x => x.StockId == stockId).Qty += qty;
            }
            else
            {
                // insert into StockOnHold (StockId, SessionId, Qty, ExpiryDate)
                // values {0}, {1}, {2}, {3}
                _ctx.StocksOnHold.Add(new StocksOnHold
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Qty = qty,
                    ExpiryDate = DateTime.Now.AddMinutes(20)        // hold it for 20 mins
                });
            }

            // update stockOnHold set expiry date = {0} where sessionId = {1}

            foreach (var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            }

            // commit transaction

            return _ctx.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(string sessionId)
        {
            var stockOnHold = _ctx.StocksOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();

            _ctx.StocksOnHold.RemoveRange(stockOnHold);

            return _ctx.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(int stockId, int qty, string sessionId)
        {
            var stockOnHold = _ctx.StocksOnHold
                .FirstOrDefault(x => x.StockId == stockId
                                && x.SessionId == sessionId);

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == stockId);

            stock.Qty += qty;
            stockOnHold.Qty -= qty;

            if (stockOnHold.Qty <= 0)
            {
                // remove the stock on hold from the database
                _ctx.Remove(stockOnHold);
            }

            return _ctx.SaveChangesAsync();

        }

        public Task RetrieveExpiredStockOnHold()
        {
            var stocksOnHold = _ctx.StocksOnHold.Where(x => x.ExpiryDate < DateTime.Now).ToList();

            if (stocksOnHold.Count > 0)
            {
                var stockToReturn = _ctx.Stock
                    .AsEnumerable()
                    .Where(x => stocksOnHold.Any(y => y.StockId == x.Id)).ToList();

                foreach (var stock in stockToReturn)
                {
                    stock.Qty = stock.Qty + stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }

                _ctx.StocksOnHold.RemoveRange(stocksOnHold);

                return _ctx.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
