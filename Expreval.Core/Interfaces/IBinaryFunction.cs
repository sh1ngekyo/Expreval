using Expreval.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{
    public interface IBinaryFunction<TResult, TParamLeft, TParamRight> : IFunction
    {
        FunctionType IFunction.Type => FunctionType.Binary;

        TResult Call(TParamLeft left, TParamRight right);
    }
}
