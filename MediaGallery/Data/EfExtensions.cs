﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaGallery.Data
{
    public static class EfExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            if (page == 2)
            {
                skip -= pageSize / 2;
            }
            else if (page == 3)
            {
                skip -= (pageSize / 2) - 1;
            }

            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
