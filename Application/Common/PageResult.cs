using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Common
{
    public class PageResult<T>
    {
        public PageResult(List<T> items, int pageNumber , int totalCount , int pageSize)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
        public PageResult()
        {
        }
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage => TotalPages > PageNumber;
        public bool HasPreviousPage => PageNumber > 1;
        public async static Task<PageResult<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageResult<T>(items, pageNumber, totalCount, pageSize);
        }


    }
}
