using Expreval.Core.Interfaces;
using System.Collections.Generic;

namespace Expreval.Core.Models
{
    public class Expression : IConfigurable
    {
        public Expression(string representation)
        {
            Representation = representation;
            IsConfigrured = false;
        }

        public IReadOnlyDictionary<char, IFunction> Functions { get; private set; }
        public IReadOnlyDictionary<string, dynamic> Variables { get; private set; }
        public string Representation { get; }
        public bool IsConfigrured { get; private set; }

        public void Configure(IConfiguration configuration)
        {
            if (!IsConfigrured)
            {
                Functions = configuration.Functions;
                Variables = configuration.Variables;
                IsConfigrured = true;
            }
            //configuration.
        }
    }
}
