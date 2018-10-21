class AuthorFormViewModel {
    saveCompleted = ko.observable(false);
    sending = ko.observable(false);
    author = {
        __RequestVerificationToken: "",
        firstname: ko.observable(),
        lastName: ko.observable(),
        biography: ko.observable(),
    };

    validateAndSave(form: HTMLFormElement) {
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
            success: () => this.successfulSave(),
            error: () => this.errorSave(),
            complete: () => this.sending(false)
        });
        
    }

    private successfulSave() {
        this.saveCompleted(true);

        $('.body-content').prepend('<div class="alert alert-success">' +
            '<strong>Success!</strong> The new author has been saved.</div>');
        setTimeout(() => location.href = "./", 1000);
    }
    private errorSave() {

        $('.body-content').prepend('<div class="alert alert-success">' +
            '<strong>Success!</strong> There was an error creating the author.</div>');
    }

}