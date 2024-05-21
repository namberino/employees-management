// custom.js
window.FocusInputElement = function () {
    document.getElementById('ThangTinhCong').focus();
};

function scrollToTop() {
    window.scrollTo(0, 0);
}

// Icon 3 gạch hiển thị menu
$(document).ready(function () {
    "use strict";

    // Sidebar Toggler
    $('.sidebar-toggler').click(function () {
        console.log("Clicked");
        $('.sidebar, .content').toggleClass("open");
        return false;
    });
});

window.chooseFile = async () => {
    const file = await new Promise((resolve) => {
        const input = document.createElement('input');
        input.type = 'file';
        input.onchange = (e) => resolve(e.target.files[0].name); // Trả về tên của file
        input.click();
    });
    return file;
};





