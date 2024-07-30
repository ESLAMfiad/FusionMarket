using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders_Management.Core.Specifications
{
	public static class SpecEvaluator<T> where T : class
	{
		//Fun to build Query
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecificationcs<T> spec)
		{
			var query = inputQuery; //_db.context.Set<T>()

			if (spec.Criteria is not null)
			{
				query = query.Where(spec.Criteria);
			}

			if (spec.OrderBy is not null)
			{
				query = query.OrderBy(spec.OrderBy);
			}

			if (spec.OrderByDescending is not null)
			{
				query = query.OrderByDescending(spec.OrderByDescending);
			}

			if (spec.IsPagingEnabled)
			{
				query = query.Skip(spec.Skip).Take(spec.Take);
			}

			query = spec.Includes.Aggregate(query, (CurrentQuery, include) => CurrentQuery.Include(include));

			return query;
		}
	}
}
