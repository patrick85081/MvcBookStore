class AuthorIndexViewModel {
    sending = ko.observable(false);

    constructor(public authors: Author[]) {
    }

    showDeleteModal = (data, event) => {
        this.sending(false);

        $.get($(event.target).attr('href'),
            html => {
                $('.body-content').prepend(html);
                $('#deleteModal').modal('show');

                ko.applyBindings(this, document.getElementById('deleteModal'));
            });
    }

    deleteAuthor(form: HTMLFormElement) {
        this.sending(true);
        return true;
    }
}