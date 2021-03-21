// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var app = new Vue({
    el: '#app', 
    data: {
        price: 0,
        showPrice: true,
    },
    methods: {
        togglePrice: function () {
            this.showPrice = !this.showPrice
        },
        alert(v) {
            alert(v);
        }
    },
    computed: {
        formatPrice: function () {
            return "€" + this.price;
        }
    }
});