class AuthorFormViewModel {
    saveCompleted = ko.observable(false);
    sending = ko.observable(false);
    isCreating = false;
    author = {
        __RequestVerificationToken: "",
        id: 0,
        firstName: ko.observable(),
        lastName: ko.observable(),
        biography: ko.observable(),
    };

    constructor(author: Author) {
        this.author.id = author.id;
        this.author.firstName(author.firstName);
        this.author.lastName(author.lastName);
        this.author.biography(author.biography);

        this.isCreating = author.id === 0;
    }

    validateAndSave(form: HTMLFormElement) {
        if (!$(form).valid()) {
            return false;
        }

        this.sending(true);

        this.author.__RequestVerificationToken = form[0].value;

        $.ajax({
            url: (this.isCreating) ? 'Create' : 'Edit',
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
        setTimeout(() => {
            if (this.isCreating)
                location.href = "./";
            else
                location.href = "../";
        }, 1000);
    }
    private errorSave() {

        $('.body-content').prepend('<div class="alert alert-danger">' +
            '<strong>Error!</strong> There was an error saving the author.</div>');
    }

}