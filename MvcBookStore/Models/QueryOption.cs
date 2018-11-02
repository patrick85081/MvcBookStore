using System;

namespace MvcBookStore.Models
{
    public class QueryOption : ICloneable
    {
        public int CurrentPage { get; set; } = 1;
        //public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public string SortField { get; set; } = "Id";
        public SortOrder SortOrder { get; set; }
        public string Sort => $"{SortField} {SortOrder}";
        public int TotalPages { get; set; }

        object ICloneable.Clone() => this.MemberwiseClone();

        public QueryOption Clone() => (this as ICloneable).Clone() as QueryOption;
    }
}