using Order_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orders_Management.Core.Specifications
{
	public interface ISpecificationcs<T>  where T : class
	{
		//for the where condition
		Expression<Func<T, bool>> Criteria { get; }
		//for the includes
		List<Expression<Func<T, object>>> Includes { get; }
		Expression<Func<T, object>> OrderBy { get; }
		Expression<Func<T, object>> OrderByDescending { get; }
		int Take { get; }
		int Skip { get; }
		bool IsPagingEnabled { get; }
	}
}
