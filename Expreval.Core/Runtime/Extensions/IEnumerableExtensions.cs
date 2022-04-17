using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Runtime.Extensions
{
    public static class IEnumerableExtensions
    {
        public static string ToString<T>(this IEnumerable<T> collection)
        {
            var builder = new StringBuilder();
            foreach (var item in collection)
            {
                builder.Append(item);
            }
            return builder.ToString();
        }
    }
}
