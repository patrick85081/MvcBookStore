var AuthorFormViewModel = /** @class */ (function () {
    function AuthorFormViewModel() {
        this.saveCompleted = ko.observable(false);
        this.sending = ko.observable(false);
        this.author = {
            __RequestVerificationToken: "",
            firstname: ko.observable(),
            lastName: ko.observable(),
            biography: ko.observable(),
        };
    }
    AuthorFormViewModel.prototype.validateAndSave = function (form) {
        var _this = this;
        if (!$(form).valid()) {
            return false;
        }
        this.sending(true);
        this.author.__RequestVerificationToken = form[0].value;
        $.ajax({
            url: 'Create',
            type: 'post',
            contentType: 'application/x-www-form-urlencoded',
            data: ko.toJS(this.author),
            success: function () { return _this.successfulSave(); },
            error: function () { return _this.errorSave(); },
            complete: function () { return _this.sending(false); }
        });
    };
    AuthorFormViewModel.prototype.successfulSave = function () {
        this.saveCompleted(true);
        $('.body-content').prepend('<div class="alert alert-success">' +
            '<strong>Success!</strong> The new author has been saved.</div>');
        setTimeout(function () { return location.href = "./"; }, 1000);
    };
    AuthorFormViewModel.prototype.errorSave = function () {
        $('.body-content').prepend('<div class="alert alert-success">' +
            '<strong>Success!</strong> There was an error creating the author.</div>');
    };
    return AuthorFormViewModel;
}());
//# sourceMappingURL=AuthorFormViewModel.js.map