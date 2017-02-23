var urlPath = window.location.pathname;
$(function () {
    ko.applyBindings(CreateVM);
});

var CreateVM = {
    name: ko.observable(),
    breed: ko.observable(),
    sex: ko.observable(),
    age: ko.observable(),
    SaveCat: function () {
        $.ajax({
            url: '/Cats/Create',
            type: 'post',
            dataType: 'json',
            data: ko.toJSON(this),
            contentType: 'application/json',
            success: function (result) {
            },
            error: function (err) {
                if (err.responseText == "Creation Failed")
                { window.location.href = '/Cats/Index/'; }
                else {
                    alert("Status:" + err.responseText);
                    window.location.href = '/Cats/Index/';;
                }
            },
            complete: function () {
                window.location.href = '/Cats/Index/';
            }
        });
    }
};