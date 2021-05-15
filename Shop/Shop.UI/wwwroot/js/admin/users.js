var app = new Vue({
    el: '#app',
    data: {
        userModel: {
            firstName: "",
            lastName: "",
            userName: "",
            password: "",
            email: ""
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
    },
    computed: {
        //Todo: confirm password implementation
    }
})