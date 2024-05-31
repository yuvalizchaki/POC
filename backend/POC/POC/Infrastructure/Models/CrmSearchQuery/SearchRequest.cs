using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace POC.Infrastructure.Models.CrmSearchQuery
{
    public sealed class SearchRequest
    {
        public SearchRequest()
        {
            Framing = new Framing();
            Sorts = new List<Sorting>();
            Filters = new List<Filtering>();
        }

        public ICollection<Filtering>? Filters { get; set; }

        public ICollection<Sorting>? Sorts { get; set; }

        public Framing? Framing { get; set; }

        public bool? SkipCount { get; set; }

        public ICollection<string>? Fields { get; set; }
        
    }
}