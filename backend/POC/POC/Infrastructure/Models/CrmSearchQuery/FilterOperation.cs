namespace POC.Infrastructure.Models.CrmSearchQuery
{
    public enum FilterOperation
    {
        /// <summary>Equal</summary>
        Eq,
        /// <summary>Great then</summary>
        Gt,
        /// <summary>Less then</summary>
        Lt,
        /// <summary>Start with</summary>
        Sw,
        /// <summary>Contain</summary>
        Ct,
        /// <summary>In</summary>
        In
    }
}