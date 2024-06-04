using System.Collections.Generic;

namespace POC.Infrastructure.Models.CrmSearchQuery
{
    public sealed class Filtering
    {
        public Filtering()
        {
            FilterParameters = new List<FilterParameter>();
            GroupingOperation = BooleanOperation.And;
        }

        public string PropertyName { get; set; }

        public BooleanOperation GroupingOperation { get; set; }

        public IEnumerable<FilterParameter> FilterParameters { get; set; }

        public static Filtering CreateFiltering(string propertyName, FilterOperation operation, params string[] values)
        {
            return new Filtering
            {
                PropertyName = propertyName,
                FilterParameters = new List<FilterParameter>
                {
                    new FilterParameter
                    {
                        Operation = operation,
                        Values = values
                    }
                }
            };
        }
    }
}