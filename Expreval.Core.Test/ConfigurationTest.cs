using Expreval.Core.Enums;
using System.Collections.Generic;
using Expreval.Core.Interfaces;
using Expreval.Core.Runtime.Extensions;
using Xunit;
using Expreval.Core.Exceptions;

namespace Expreval.Core.Test
{
    public class ConfigurationTest
    {
        [Fact]
        public void RegisterVariable_DefaultCase_AddVariable()
        {
            var config = new ExpressionConfiguration<int>();
            var expected = new { Name = "test", Value = 10 };

            config.RegisterVariable(expected.Name, expected.Value);

            Assert.Single(config.Variables);
            Assert.Equal(expected.Value, config.Variables[expected.Name]);
        }

        [Fact]
        public void RegisterVariable_DoubleRegisterCase_ThrowValueAlreadyExistException()
        {
            var config = new ExpressionConfiguration<int>();
            var input = new { Name = "test", Value = 10 };

            config.RegisterVariable(input.Name, input.Value);

            Assert.Throws<ValueAlreadyExistException>(() => { config.RegisterVariable(input.Name, input.Value); });
        }

        [Fact]
        public void RegisterFunction_DefaultCase_AddFunction()
        {
            var config = new ExpressionConfiguration<int>();
            var mock = new Moq.Mock<IFunction>();
            mock.SetupGet(func => func.Type).Returns(FunctionType.Binary);
            var input = new { Prefix = '*', Function = mock.Object };

            config.RegisterFunction(input.Prefix, input.Function);
            Assert.Single(config.Functions);
            Assert.Equal(input.Function.Type, config.Functions[input.Prefix].Type);
        }

        [Fact]
        public void RegisterFunction_DoubleRegisterCase_ThrowValueAlreadyExistException()
        {
            var config = new ExpressionConfiguration<int>();
            var mock = new Moq.Mock<IFunction>();
            mock.SetupGet(func => func.Type).Returns(FunctionType.Binary);
            var input = new { Prefix = '*', Function = mock.Object };

            config.RegisterFunction(input.Prefix, input.Function);
            Assert.Throws<ValueAlreadyExistException>(() => { config.RegisterFunction(input.Prefix, input.Function); });
        }
    }
}
