using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.MappingAndPaging
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; } = 0;
        public int TotalPage { get; set; } = 0;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex == 0 && pageSize == 0)
            {
                var allItems = source.ToList();
                return new PaginatedList<T>(allItems, allItems.Count, pageIndex, pageSize);
            }
            else
            {
                var count = source.Count();
                var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PaginatedList<T>(items, count, pageIndex, pageSize);
            }
        }
    }
}
