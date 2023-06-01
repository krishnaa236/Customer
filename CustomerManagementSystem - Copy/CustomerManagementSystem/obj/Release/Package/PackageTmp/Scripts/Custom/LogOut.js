
function LogOut() {
    $.ajax({
        url: '/Account/SignOut',
        success: function (result) {         
                location.href = "https://localhost:44319/";            
        }
    })

}