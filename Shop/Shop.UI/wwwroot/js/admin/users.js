var app = new Vue({
    el: '#app',
    data: {
        userModel: {
            firstName: "a",
            lastName: "b",
            userName: "c",
            gstNumber: "29AADCB2230M1ZP",
            email: "a@gmail.com",
            password: "1"
        },
        msg: []
    },
    mounted() {
        // Todo: get all the users 
    },
    computed: {
        firstName() {
            return this.userModel.firstName;
        },
        lastName() {
            return this.userModel.lastName;
        },
        userName() {
            return this.userModel.userName;
        },
        email() {
            return this.userModel.email;
        },
        gst() {
            return this.userModel.gstNumber;
        },
        password() {
            return this.userModel.password;
        },
        isDisabled: function () {
            //Todo: Implement disabling submit button
        }
    },
    watch: {
        firstName() {
            this.validate('firstName');
        },
        lastName() {
            this.validate('lastName');
        },
        userName() {
            this.validate('userName');
        },
        email() {
            this.validate('email');
        },
        gst() {
            this.validate('gst');
        },
        password() {
            this.validate('password');
        }
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
        },

        validate(prop) {
            switch (prop) {
                case "firstName":
                    var reg = null;
                    if (this.userModel.firstName != "") {
                        this.msg['firstName'] = '';
                    } else {
                        this.msg['firstName'] = 'First Name cannot be empty';
                    }
                    break;

                case "lastName":
                    var reg = null;
                    if (this.userModel.lastName != "") {
                        this.msg['lastName'] = '';
                    } else {
                        this.msg['lastName'] = 'Last Name cannot be empty';
                    }
                    break;

                case "userName":
                    var reg = null;
                    if (this.userModel.userName != "") {
                        this.msg['userName'] = '';
                    } else {
                        this.msg['userName'] = 'User Name cannot be empty';
                    }
                    break;

                case "gst":
                    var reg = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;
                    if (this.userModel.gstNumber != "") {
                        if (!reg.test(this.userModel.gstNumber)) {
                            this.msg['gst'] = 'Invalid Gst Number';
                        } else {
                            this.msg['gst'] = '';
                        }
                    } else {
                        this.msg['gst'] = 'Gst Number cannot be empty';
                    }
                    break;

                case "email":
                    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                    if (this.userModel.email != "") {
                        if (!reg.test(this.userModel.email)) {
                            this.msg['email'] = 'Invalid Email Address';
                        } else {
                            this.msg['email'] = '';
                        }
                    } else {
                        this.msg['email'] = 'Email Address cannot be empty';
                    }
                    break;

                case "password":
                    var reg = null;
                    if (this.userModel.password != "") {
                        this.msg['password'] = '';
                    } else {
                        this.msg['password'] = 'Password cannot be empty';
                    }
                    break;
            }
        }

    }
})