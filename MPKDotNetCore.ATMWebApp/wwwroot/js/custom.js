function successMessage(message, url) {

    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    }).then((result) => {
        if (result.isConfirmed && url != undefined) {
            location.href = url;
        }
    });
}

function successMsg(message) {

    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    });
}

function infoMessage(message,url) {

    Swal.fire({
        title: "Balance Check",
        text: message,
        icon: "info",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    }).then((result) => {
        if (result.isConfirmed && url != undefined) {
            location.href = url;
        }
    });
}
function confirmMessage(message) {
    return new Promise((resolve, reject) => {
        Swal.fire({
            title: "Confirm",
            text: message,
            icon: "warning",
            showCancelButton: true,
        }).then((result) => {
            // return result.isConfirmed;
            resolve(result.isConfirmed)
        });
    });
}


function warningMessage(message) {
    Swal.fire({
        title: "Warning!",
        text: message,
        icon: "warning",
    });
}

function errorMessage(message) {
    Swal.fire({
        title: "Error!",
        text: message,
        icon: "error",
    });
}

function showMessage(data, url) {
    if (data.isSuccess) {
        successMessage(data.message, url);
    }
    else {
        errorMessage(data.message);
    }
}

function showMsg(data) {
    if (data.isSuccess) {
        successMsg(data.message);
    }
    else {
        errorMessage(data.message);
    }
}

function showInfoMessage(data,url){
    if (data.isSuccess) {
        infoMessage(data.message,url);
    }
    else {
        errorMessage(data.message);
    }

}

function validateMsg(message) {
    Notiflix.Notify.warning(message);
}

function sucessMsgNotiflixWithUrl(data, url) {
    if (data.isSuccess) 
        Notiflix.Notify.success(data.message);
    
    else 
        Notiflix.Notify.failure(data.message);
    

    if(url != undefined) 
        location.href = url;
    
}

function sucessMsgNotiflix(data) {
    if (data.isSuccess)
        Notiflix.Notify.success(data.message);

    else
        Notiflix.Notify.failure(data.message);

}

function errorMsgNotiflix(message) {
    Notiflix.Notify.failure(message);
}


