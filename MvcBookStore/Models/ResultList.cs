using System.Collections.Generic;

namespace MvcBookStore.Models
{
    public class ResultList<T>
    {
        public List<T> Result { get; set; }
        public QueryOption QueryOption { get; set; }
    }
}