﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Vit.Core.Util.ComponentModel.Query;
using Vit.Linq;

namespace Vit.Extensions.Linq_Extensions
{

    public static partial class Queryable_Sort_ByReflection_Extensions
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> Sort_ByReflection<T>(this IQueryable<T> query, IEnumerable<SortItem> sort)
            where T : class
        {
            if (query == null || sort == null) return query;

            var sortCount = sort.Count();
            if (sortCount == 0) return query;

            IOrderedQueryable<T> orderedQuery = null;
            foreach (var item in sort)
            {
                var keySelector = LinqHelp.GetFieldExpression_ByReflection<T>(item.field);

                if (keySelector == null)
                {
                    continue;
                }
                if (item.asc)
                {
                    if (orderedQuery == null)
                    {
                        orderedQuery = query.OrderBy(keySelector);
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenBy(keySelector);
                    }
                }
                else
                {
                    if (orderedQuery == null)
                    {
                        orderedQuery = query.OrderByDescending(keySelector);
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenByDescending(keySelector);
                    }
                }
            }
            return orderedQuery ?? query;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="field"></param>
        /// <param name="asc"> whether sort by asc</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> Sort_ByReflection<T>(this IQueryable<T> query, string field, bool asc = true)
           where T : class
        {
            if (query == null) return query;

            var keySelector = LinqHelp.GetFieldExpression_ByReflection<T>(field);

            if (keySelector == null)
            {
                return query;
            }

            if (asc)
            {
                query = query.OrderBy(keySelector);
            }
            else
            {
                query = query.OrderByDescending(keySelector);
            }

            return query;
        }

    }
}