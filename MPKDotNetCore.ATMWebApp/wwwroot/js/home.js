var signinModal = new bootstrap.Modal(document.getElementById('signinModal'));
var registerModal = new bootstrap.Modal(document.getElementById('registerModal'));
var depositModal = new bootstrap.Modal(document.getElementById('depositModal'));
var withdrawModal = new bootstrap.Modal(document.getElementById('withdrawModal'));

function showSigninModal() {
    signinModal.show();
}

function showRegisterModal() {
    registerModal.show();
}

function showDepositModal() {
    depositModal.show();
}

function showWithdrawModal() {
    withdrawModal.show();
}

    $('#btnRegister').click(function (e) {
        e.preventDefault();

        var name = $('#txtAddName').val();
        var phone = $('#txtAddPhone').val();
        var nrcno = $('#txtAddNRCNo').val();
        var pin = $('#txtAddPin').val();

        if (name == "" || name == undefined || name == null) {
            validateMsg("Name can't be empty");
            return;
        }


        if (phone == "" || phone == undefined || phone == null) {
            validateMsg("Phone can't be empty");
            return;
        }


        if (nrcno == "" || nrcno == undefined || nrcno == null) {
            validateMsg("Nrc No can't be empty");
            return;
        }

        if (pin == "" || pin == undefined || pin == null) {
            validateMsg("Pin can't be empty");
            return;
        }

        var _reqmodel = {
            Name: name,
            Phone: phone,
            NRCNo: nrcno,
            Pin:pin
        };

        var l = Ladda.create(this);
        l.start();

        $.ajax({
            url: '/Home/Register',
            type: 'POST',
            data: {
                reqmodel: _reqmodel
            },
            success: function (response) {
                l.stop();
                showMsg(response);
                Clear("Register");
                registerModal.hide();
            },
            error: function (request, status, error) {
                l.stop();
                errorMsgNotiflix("Account creating fail!");
                Clear("Register");
                console.log({ request, status, error });
            }
        });
    });


$('#btnSignin').click(function (e) {
    e.preventDefault();

    var accountno = $('#txtAccountNo').val();
    var pin = $('#txtPin').val();

    if (accountno == "" || accountno == undefined || accountno == null) {
        validateMsg("Account No can't be empty");
        return;
    }


    if (pin == "" || pin == undefined || pin == null) {
        validateMsg("Pin can't be empty");
        return;
    }

    var _reqmodel = {
        AccountNo:accountno,
        Pin: pin
    };
    $.ajax({
        url: '/Home/Signin',
        type: 'POST',
        data: {
            reqmodel: _reqmodel
        },
        success: function (response) {
            sucessMsgNotiflix(response);
            setTimeout(function () {
                location.href = '/';
            }, 2000);
            Clear("Signin");
            signinModal.hide();
        },
        error: function (request, status, error) {
            errorMsgNotiflix("Sign in fail!");
            setTimeout(function () {
                location.href = '/';
            }, 2000);
            Clear("Signin");
            console.log({ request, status, error });
        }
    });
});

$('#btnBalance').click(function (e) {
    e.preventDefault();
    var l = Ladda.create(this);
    l.start();
    $.ajax({
        url: '/Home/CheckBalance',
        type: 'POST',
        success: function (response) {
            showInfoMessage(response,'/');
        },
        error: function (request, status, error) {
            l.stop();
            errorMsgNotiflix("Something went wrong!");
            console.log({ request, status, error });
        }
    });
});


$('#btnDeposit').click(function (e) {
    e.preventDefault();

    var _amount = $('#txtDeposit').val();

    if (_amount == "" || _amount == undefined || _amount == null) {
        validateMsg("Deposit amount can't be empty");
        return;
    }
    var l = Ladda.create(this);
    l.start();

    $.ajax({
        url: '/Home/Deposit',
        type: 'POST',
        data: {
            amount: _amount
        },
        success: function (response) {
            l.stop();
            sucessMsgNotiflix(response);
            Clear("Deposit");
            depositModal.hide();
        },
        error: function (request, status, error) {
            l.stop();
            errorMsgNotiflix("Something went wrong");
            Clear("Deposit");
            console.log({ request, status, error });
        }
    });
});



$('#btnWithdraw').click(function (e) {
    e.preventDefault();

    var _amount = $('#txtWithdraw').val();

    if (_amount == "" || _amount == undefined || _amount == null) {
        validateMsg("Withdraw amount can't be empty");
        return;
    }
    var l = Ladda.create(this);
    l.start();

    $.ajax({
        url: '/Home/Withdraw',
        type: 'POST',
        data: {
            amount: _amount
        },
        success: function (response) {
            l.stop();
            sucessMsgNotiflix(response);
            Clear("Withdraw");
            withdrawModal.hide();
        },
        error: function (request, status, error) {
            l.stop();
            errorMsgNotiflix("Something went wrong");
            Clear("Withdraw");
            console.log({ request, status, error });
        }
    });
});

function Clear(status) {
    if (status == "Register") {
        $('#txtAddName').val("");
        $('#txtAddPhone').val("");
        $('#txtAddNRCNo').val("");
        $('#txtAddPin').val("");
    }
    else if (status == "Signin") {
        $('#txtAccountNo').val("");
        $('#txtPin').val("");
    }
    else if (status == "Deposit") {
        $('#txtDeposit').val("");
    }
    else if (status == "Withdraw") {
        $('#txtWithdraw').val("");
    }
    
};

function closeSignin() {
    Clear('Signin');
    signinModal.hide();
}

function closeRegister() {
    Clear('Register');
    registerModal.hide();
}

function closeDeposit() {
    Clear('Deposit');
    depositModal.hide();
}

function closeWithdraw() {
    Clear('Withdraw');
    withdrawModal.hide();
}

