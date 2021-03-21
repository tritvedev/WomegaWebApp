// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var app = new Vue({
    el: '#app', 
    data: {
        //price: 0,
        //howPrice: true,
        loading: false,
        productModel: {
            name: "Product Name",
            description: "Product Description",
            price: 1.99,
            value: 1    // value not relevant but let be as it is, to override the error
        },
        products: []
    },
    methods: {
        /*togglePrice: function () {
            this.showPrice = !this.showPrice
        },
        alert(v) {
            alert(v);
        },*/        
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                })
        },
        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                })
        }
    },
    computed: {
        /*formatPrice: function () {
            return "€" + this.price;
        }*/
    }
});