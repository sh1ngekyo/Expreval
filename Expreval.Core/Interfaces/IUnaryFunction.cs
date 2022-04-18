using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{
    public interface IUnaryFunction<TResult, TParam> : IFunction
    {
        TResult Call(TParam var);
    }
}
