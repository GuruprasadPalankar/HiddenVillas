window.ShowToastr = (type, message) => {
    if (type === "success")
    {
        toastr.success(message, "Operation Successful", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 10000 });
    }
}

window.SweetAlert2 = (type, message) => {
    if (type === "success")
    {
        Swal.fire({
            icon: 'success',
            title: 'Success!',
            text: message
        })
    }
    if (type === "error") {
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            text: message
        })
    }
}

function ShowDeleteConfirmationModel() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModel() {
    $('#deleteConfirmationModal').modal('hide');
}