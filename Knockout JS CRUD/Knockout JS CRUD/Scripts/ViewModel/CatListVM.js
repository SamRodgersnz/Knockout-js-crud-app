var urlPath = window.location.pathname;
$(function () {
    ko.applyBindings(CatListVM);
    CatListVM.getCats();
});

//View Model
var CatListVM = {
    Cats: ko.observableArray([]),
    getCats: function () {
        var self = this;
        $.ajax({
            type: "GET",
            url: '/Cats/FetchCats',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Cats(data); //Put the response in ObservableArray
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });
    },
};

self.editCat = function (cat) {
    window.location.href = '/Cats/Edit/' + cat.id;
};
self.deleteCat = function (cat) {
    window.location.href = '/Cats/Delete/' + cat.id;
};

//Model
function Cats(data) {
    this.name = ko.observable(data.name);
    this.breed = ko.observable(data.breed);
    this.sex = ko.observable(data.sex);
    this.age = ko.observable(data.age);
}