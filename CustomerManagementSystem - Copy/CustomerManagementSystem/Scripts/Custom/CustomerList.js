$(document).ready(function () {
    $('#CustomerList_table').dataTable({
        responsive: true,
        "lengthChange": false,
        "ordering": true,
        "info": true,

        language: { search: "" },
        language: { search: '', searchPlaceholder: "Search..." },

    });
});
function TransferCustomerId(customerId) {
    $(".Transfer_Customer_id_input").val(customerId);
}

function DeleteCustomer(customer_id) {
    $.ajax({
        url: '/CustomerList/DeleteCustomer',
        data: {
            customer_id
        },
        success: function (result) {
            window.location.reload();
        }
    })
}

