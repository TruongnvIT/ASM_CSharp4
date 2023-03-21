// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Load ảnh trong tạo mới đối tượng
function previewImage(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#preview').attr('src', e.target.result);
            $('#preview').show();
        };
        reader.readAsDataURL(input.files[0]);
    }
}
function increaseValue(max) {
    var value = parseInt(document.getElementById('number').value, 10);
    value = isNaN(value) ? 0 : value;
    if (value < max) {
        value++;
        document.getElementById('number').value = value;
    }

}

function decreaseValue() {
    var value = parseInt(document.getElementById('number').value, 10);
    value = isNaN(value) ? 1 : value;
    value < 1 ? value = 1 : '';
    if (value > 1) {
        value--;
    }
    document.getElementById('number').value = value;
}


