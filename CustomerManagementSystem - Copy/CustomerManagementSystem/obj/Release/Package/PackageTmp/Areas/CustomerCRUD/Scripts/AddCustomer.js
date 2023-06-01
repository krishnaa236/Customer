$(".AddUserEvent").click(function () {
    let firstName = $(".FirstName").val();
    let lastName = $(".LastName").val();
    let email = $(".Email").val();
    let phoneNumber = $(".PhoneNumber").val();
    let userName = $(".UserName").val();
    let password = $(".Password").val();
    let city = $(".City").val();
    let country = $(".Country").val();
    let status = $(".Status").val();
    $.ajax({
        url: '/Customer/AddCustomerDetail',
        data: {
            firstName, lastName, email, phoneNumber, userName, password, city, country,status
        },
        success: function (result) {
            toastr.success("Data Added Successfully")
        }
    })
})

function UpdateUserEvent(customer_id) {
    let firstName = $(".FirstName").val();
    let lastName = $(".LastName").val();
    let email = $(".Email").val();
    let phoneNumber = $(".PhoneNumber").val();
    let userName = $(".UserName").val();
    let password = $(".Password").val();
    let city = $(".City").val();
    let country = $(".Country").val();
    let status = $(".Status").val();
    $.ajax({
        url: '/Customer/UpdateCustomerDetails',
        data: {
            firstName, lastName, email, phoneNumber, userName, password, city, country, status, customer_id
        },
        success: function (result) {
            toastr.success("Data Updated Successfully")
        }
    })
}