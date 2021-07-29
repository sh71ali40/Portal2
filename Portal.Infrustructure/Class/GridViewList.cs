using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Portal.Infrustructure.Class
{
    public class GridViewList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public GridViewList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public List<int> PageItems
        {
            get
            {
                var pages = new List<int>();
                var startPageIndex = PageIndex - 5 <= 1 ? 1 : PageIndex - 5;
                var endPageIndex = PageIndex + 5 >= TotalPages ? TotalPages : PageIndex + 5;
                for (var i = startPageIndex; i <= endPageIndex; i++)
                {
                    pages.Add(i);
                }
                return pages;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<GridViewList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            
            var count = await source.CountAsync();
            if (pageIndex<1)
            {
                return new GridViewList<T>(new List<T>(),0,1,1 );
            }
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new GridViewList<T>(items, count, pageIndex, pageSize);
        }
    }
}
