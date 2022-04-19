# Expreval
Fast, Compact, Easy-To-Use Library for evaluating boolean (or your own) expressions.

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/sh1ngekyo/expreval/build?style=for-the-badge)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/sh1ngekyo/expreval/test?label=tests&style=for-the-badge)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/sh1ngekyo/expreval?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/sh1ngekyo/expreval?style=for-the-badge)

# Quick example:

A = true, B = false; 

```csharp
"!(A | B)".ToConfiguredExpression<bool>().Eval();
"((!B) & A) | B)".ToConfiguredExpression<bool>().Eval();
Convert.ToDouble(("!B".ToConfiguredExpression<bool>().Eval()));
```
Output: false, true, 1.0;

# Another example:
[Truth Table Builder](https://github.com/sh1ngekyo/Expreval/tree/example-branch/Expreval.CLI.TruthTable)

# Imports:
```csharp
using Expreval.Core;
using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Models;
```

# Define your functions with IBinaryFunction<TResult, TLeft, TRight> and IUnaryFunction<TResult, TVariable>

```csharp
// here we define function with two boolean params and boolean return type
class AndOperator : IBinaryFunction<bool, bool, bool>
{
    /* your function */
    public bool Call(bool left, bool right)
        => left & right;
}
```

# Create and configure your expression:

```csharp
var config = new ExpressionConfiguration<bool>();
config.RegisterVariables(("A", true), ("B", false));
config.RegisterFunctions('&', new AndOperator());
var expr = "B & A".ToExpression<bool>();
expr.Configure(config);
```

# Evaluate your expression:
```csharp
var result = expr.Eval();
```
Output for example: false

# Use your custom types as variables and functions:

```csharp
public class IEnumerableConcat<TResult, TLeft, TRight> 
    : IBinaryFunction<IEnumerable<TResult>, IEnumerable<TLeft>, IEnumerable<TRight>>
{
    public IEnumerable<TResult> Call(IEnumerable<TLeft> left, IEnumerable<TRight> right) 
        => (IEnumerable<TResult>)left.Concat((IEnumerable<TLeft>)right);
}
...

List<int> A = new() { 1, 2, 3 }, B = new() { 4, 5, 6 };

var config = new ExpressionConfiguration<IEnumerable<int>>();
config.RegisterVariables((nameof(A), A), (nameof(B), B));
config.RegisterFunction('+', new IEnumerableConcat<int, int, int>());
var result = "A+B".ToConfiguredExpression<IEnumerable<int>>(config).Eval().ToList();
```
Output: 1 2 3 4 5 6

# FAQ

1. > Can I use different types in an expression?
    * Yes, but you may have problems if there is no implicit type conversion. An option would be to explicitly specify a type conversion. Another option is to use a dynamic type.
2. > How does an expression get executed?
    * The lexical analyzer converts a string into a set of tokens. The tokens are then converted into a Polish notation. Then the parser builds an expression tree and gives the tree to be executed. Execution is done by recursive traversal of the tree.