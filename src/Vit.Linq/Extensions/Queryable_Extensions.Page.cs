﻿using System.Linq;

using Vit.Linq.ComponentModel;

namespace Vit.Linq
{

    public static partial class Queryable_Extensions
    {

        public static IQueryable<T> Page<T>(this IQueryable<T> query, PageInfo page)
        {
            return query?.Range(page?.ToRange());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"> the index of the page (starting from 1) </param>
        /// <returns></returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageSize, int pageIndex = 1)
        {
            return Page(query, new PageInfo(pageSize: pageSize, pageIndex: pageIndex));
        }
    }
}