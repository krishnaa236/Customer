

$('#ChooseImage').click(function () {
    $('#UserImage').click();
});

$("#UserImage").change(function () {
    var fileInput = document.getElementById('UserImage');
    if (fileInput.files.length === 0) {
        toastr.error('Please select a file');
        return;
    }
    var file = fileInput.files[0];
    var formData = new FormData();
    formData.append('avatar', file);
    formData.append('id', 1);
   

    $.ajax({
        url: '/User/changeUserProfile',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {

            var oFReader = new FileReader();
            oFReader.readAsDataURL($("#UserImage")[0].files[0]);

            oFReader.onload = function (oFREvent) {
                $(".Customer_Detail_image").attr("src", oFREvent.target.result)
            };


            window.location.reload();
            setInterval(toastr.success('Avatar uploaded successfully'), 100000);
        },
        error: function () {
            toastr.error('Avatar upload failed');
        }
    });
})
