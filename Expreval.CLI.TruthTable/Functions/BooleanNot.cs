using Expreval.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.CLI.TruthTable.Functions
{
    public sealed class BooleanNot : IUnaryFunction<bool, bool>
    {
        private BooleanNot() { }

        private static Lazy<IUnaryFunction<bool, bool>> instanse => new Lazy<IUnaryFunction<bool, bool>>(() => new BooleanNot());

        public static IUnaryFunction<bool, bool> Instanse => instanse.Value;

        public bool Call(bool var)
            => !var;
    }
}
