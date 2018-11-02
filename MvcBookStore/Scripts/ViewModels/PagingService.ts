class PagingService<T> {
    entities = ko.observableArray<T>([]);
    queryOption = {
        currentPage: ko.observable<number>(),
        totalPages: ko.observable<number>(),
        pageSize: ko.observable<number>(),
        sortField: ko.observable<string>(),
        sortOrder: ko.observable<string>(),
    }

    constructor(resultList: ResultList<T>) {
        this.updateResultList(resultList);
    }

    updateResultList(resultList: ResultList<T>) {
        this.queryOption.currentPage(resultList.queryOption.currentPage);
        this.queryOption.totalPages(resultList.queryOption.totalPages);
        this.queryOption.pageSize(resultList.queryOption.pageSize);
        this.queryOption.sortField(resultList.queryOption.sortField);
        this.queryOption.sortOrder(resultList.queryOption.sortOrder);

        this.entities(resultList.result);
        console.log(resultList);
    }

    sortEntitiesBy = (data, event) => {
        let sortField = $(event.target).data('sortField');

        if (sortField === this.queryOption.sortField() &&
            this.queryOption.sortOrder() === "ASC") {
            this.queryOption.sortOrder("DESC");
        } else {
            this.queryOption.sortOrder("ASC");
        }

        this.queryOption.sortField(sortField);
        this.queryOption.currentPage(1);

        this.fetchEntities(event);
    };

    previousPage = (data, event) => {
        if (this.queryOption.currentPage() > 1) {
            this.queryOption.currentPage(this.queryOption.currentPage() - 1);

            this.fetchEntities(event);
        }
    }

    nextPage = (data, event) => {
        if (this.queryOption.currentPage() < this.queryOption.totalPages()) {
            this.queryOption.currentPage(this.queryOption.currentPage() + 1);

            this.fetchEntities(event);
        }
    }

    fetchEntities = (event) => {
        let url = `/api/${$(event.target).attr('href')}?
sortField=${this.queryOption.sortField()}&
sortOrder=${this.queryOption.sortOrder()}&
currentPage=${this.queryOption.currentPage()}&
pageSize=${this.queryOption.pageSize()}`;

        $.ajax({
            dataType: 'json',
            url: url,
            success: (data) => this.updateResultList(data),
            error: () => {
                $('.body-content').prepend(`<div class="alert alert-danger">
<strong>Error !</strong> There was an error fetchig the data.
</div>`);
            }
        });
    };

    buildSortIcon = (sortField) => {
        return ko.pureComputed(() => {
            let sortIcon = 'sort';

            if (this.queryOption.sortField() === sortField) {
                sortIcon += '-by-alphabet';
                if (this.queryOption.sortOrder() === 'DESC') {
                    sortIcon += '-alt';
                }
            }
            return `glyphicon glyphicon-${sortIcon}`;
        });
    }

    buildPreviousClass = () => {
        return ko.pureComputed(() => {
            let className = 'previous';

            if (this.queryOption.currentPage() === 1) {
                className += ' disabled';
            }

            return className;
        });
    }

    buildNextClass = () => {
        return ko.pureComputed(() => {
            let className = 'next';

            if (this.queryOption.currentPage() >= this.queryOption.totalPages()) {
                className += ' disabled';
            }

            return className;
        });
    }
}