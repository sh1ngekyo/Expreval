using Expreval.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.CLI.TruthTable.Functions
{
    class BooleanImplication : IBinaryFunction<bool, bool, bool>
    {
        private BooleanImplication() { }

        private static Lazy<IBinaryFunction<bool, bool, bool>> instanse => new Lazy<IBinaryFunction<bool, bool, bool>>(() => new BooleanImplication());

        public static IBinaryFunction<bool, bool, bool> Instanse => instanse.Value;

        public bool Call(bool left, bool right)
            => (!left) | right;
    }
}
