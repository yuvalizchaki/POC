using System.Collections.Generic;

namespace POC.Infrastructure.Models.CrmSearchQuery
{
    public sealed class FilterParameter
    {
        public FilterParameter()
        {
            GroupingOperation = BooleanOperation.And;    
            Operation = FilterOperation.Ct;
            IsNegated = false;
        }

        public BooleanOperation GroupingOperation { get; set; }

        public FilterOperation Operation { get; set; }

        public bool IsNegated { get; set; }

        public IEnumerable<string> Values { get; set; }
    }
}