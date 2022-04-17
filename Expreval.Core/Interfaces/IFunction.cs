using Expreval.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{

    public interface IFunction
    {
        FunctionType Type { get; set; }
    }
}
