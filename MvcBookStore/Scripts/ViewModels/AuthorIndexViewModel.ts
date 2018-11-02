class AuthorIndexViewModel {
    sending = ko.observable(false);

    pagingService: PagingService<Author>;

    constructor(resultList: ResultList<Author>) {
        this.pagingService = new PagingService(resultList);
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

interface QueryOption {
    currentPage: number,
    totalPages: number,
    pageSize: number,
    sortField: string,
    sortOrder: string,
}

interface ResultList<T> {
    result: T[],
    queryOption: QueryOption,
}