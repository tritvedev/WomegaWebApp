var app = new Vue({
    el: '#app',
    data: {
        userModel: {
            firstName: "a",
            lastName: "b",
            userName: "c",
            gstNumber: "29AADCB2230M1ZP",
            companyName: "xyz inc",
            email: "a@gmail.com",
            password: "1",
            isWholesaleAccount: true
        }
    },
    mounted() {
        // Todo: get all the users 
    },
    methods: {

        createUser() {
            axios.post('/users', this.userModel)
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                });
        }
    }
})