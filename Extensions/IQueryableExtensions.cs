using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using marioProgetto.Models;
using marioProgettoRepos.Core.Models;

namespace marioProgettoRepos.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Veichle> ApplyFiltering(this IQueryable<Veichle> query, VeichleQuery queyObj)
        {
            if (queyObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queyObj.MakeId.Value);
            if (queyObj.ModelId.HasValue)
                query = query.Where(v => v.ModelId == queyObj.ModelId.Value);

            return query;
        }


        public static IQueryable<T> ApplyOrding<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columsMap)
        {
            if (String.IsNullOrEmpty(queryObj.SortBy) || !columsMap.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query = query.OrderBy(columsMap[queryObj.SortBy]);
            else
                return query = query.OrderByDescending(columsMap[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 10;

            return query = query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}