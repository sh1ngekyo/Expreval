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
            public bool Call(bool left, bool right) => left & right;
        }
        public class BooleanOr : IBinaryFunction<bool, bool, bool>
        {
            public bool Call(bool left, bool right) => left | right;
        }
        public class BooleanNot : IUnaryFunction<bool, bool>
        {
            public bool Call(bool var) => !var;
        }

        public class IEnumerableConcat<TResult, TLeft, TRight> 
            : IBinaryFunction<IEnumerable<TResult>, IEnumerable<TLeft>, IEnumerable<TRight>>
        {
            public IEnumerable<TResult> Call(IEnumerable<TLeft> left, IEnumerable<TRight> right) 
                => (IEnumerable<TResult>)left.Concat((IEnumerable<TLeft>)right);
        }
    }
}
