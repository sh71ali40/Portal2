
namespace Portal.Statics
{
    public class Variable
    {
        public const int MainPageId = 1;

        public const int PageSize = 10;
        public enum PageSectionName
        {
            Header,
            Footer
        }

        public static class CacheKeys
        {
            public static string InPageModules
            {
                get { return "_InPageModules"; }
            }
        }
    }
}
