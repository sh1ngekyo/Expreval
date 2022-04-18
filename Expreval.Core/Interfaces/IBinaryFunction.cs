﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{
    public interface IBinaryFunction<TResult, TParamLeft, TParamRight> : IFunction
    {
        TResult Call(TParamLeft left, TParamRight right);
    }
}