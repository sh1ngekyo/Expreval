using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{
    public interface IConfigurable
    {
        IReadOnlyDictionary<char, IFunction> Functions { get; }

        IReadOnlyDictionary<string, dynamic> Variables { get; }

        bool IsConfigrured { get; }

        string Representation { get; }

        void Configure(IConfiguration configuration);
    }
}
