
$(function () {
    $("#loader").hide();
});

function ImageValidation(input)
{
    
    var count = input.files.length;
    var isImage = true;
    for (var i = 0; i < count; i++)
    {
        var file = input.files[i];
        var fileType = file["type"];
        var validImageTypes = ["image/gif", "image/jpeg", "image/png", "application/pdf","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"];
        if ($.inArray(fileType, validImageTypes) < 0)
        {
            isImage = false;
            alert("Not Valid File!!");
            $("#imgupload").val(''); 
            break;
            
        }
    }
    return isImage;
}
// file upload click function
function openFileUpload() {
 
    $("#displayImage").css("background-image", "none");
    $("#imgupload").trigger("click");

}
// show selected image on upload click
function readURL(input)
{

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgHolder').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function ValidateSize(file)
{
    
    var FileSize = file.files[0].size / 1500 / 1500; // in MB
    if (FileSize > 1500000)
    {
        alert('File size exceeds 1.5MB');
        $(file).val(''); //for clearing with Jquery
    }
    else {
        readURL(file);
    }
}

function ImageValidation1(input) {

    var count = input.files.length;
    var isImage = true;
    for (var i = 0; i < count; i++) {
        var file1 = input.files[i];
        var fileType = file1["type"];
        var validImageTypes = ["image/gif", "image/jpeg", "image/png", "application/pdf", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"];
        if ($.inArray(fileType, validImageTypes) < 0) {
            isImage = false;
            alert("Not Valid File!!");
            $("#idImageUpload").val('');
            break;

        }
    }
    return isImage;
}
// file upload click function
function FileUpld() {

    $("#displayImage").css("background-image", "none");
    $("#idImageUpload").trigger("click");

}
// show selected image on upload click
function readURL1(input) {

    if (input.files && input.files[0]) {
        var reader1 = new FileReader();

        reader1.onload = function (e) {
            $('#imgHolder2').attr('src', e.target.result);
        };

        reader1.readAsDataURL(input.files[0]);
    }
}

function ValidateSize1(file)
{

    var FileSize1 = file.files[0].size / 1500 / 1500; // in MB
    if (FileSize1 > 1500000) {
        alert('File size exceeds 1.5MB');
        $(file).val(''); //for clearing with Jquery
    }
    else {
        readURL1(file);
    }
}





