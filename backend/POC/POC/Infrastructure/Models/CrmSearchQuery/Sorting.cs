namespace POC.Infrastructure.Models.CrmSearchQuery
{
    public sealed class Sorting
    {
        public Sorting()
        {
            InverseOrder = false;
        }

        public Sorting(string fieldName, bool isInverseOrder)
        {
            SortBy = fieldName;
            InverseOrder = isInverseOrder;
        }

        public string SortBy { get; set; }

        public bool InverseOrder { get; set; }
    }
}