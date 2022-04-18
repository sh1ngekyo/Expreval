using Expreval.Core.Enums;
using Expreval.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Test.Dummy
{
    public sealed class Function
    {
        private Function() { }

        public class BooleanAnd : IBinaryFunction<bool, bool, bool>
        {
            public FunctionType Type { get; set; }

            public bool Call(bool left, bool right)
            {
                return left & right;
            }
        }
        public class BooleanOr : IBinaryFunction<bool, bool, bool>
        {
            public FunctionType Type { get; set; }

            public bool Call(bool left, bool right)
            {
                return left | right;
            }
        }
        public class BooleanNot : IUnaryFunction<bool, bool>
        {
            public FunctionType Type { get; set; }

            public bool Call(bool var)
            {
                return !var;
            }
        }

        public class IEnumerableConcat<TResult, TLeft, TRight> 
            : IBinaryFunction<IEnumerable<TResult>, IEnumerable<TLeft>, IEnumerable<TRight>>
        {
            public FunctionType Type { get; set; }

            public IEnumerable<TResult> Call(IEnumerable<TLeft> left, IEnumerable<TRight> right)
            {
                return (IEnumerable<TResult>)left.Concat((IEnumerable<TLeft>)right);
            }
        }
    }
}
