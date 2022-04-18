# Expreval
Fast, Compact, Easy-To-Use Library for evaluating boolean (or your own) expressions.

[![Build](https://github.com/sh1ngekyo/Expreval/actions/workflows/build.yml/badge.svg?branch=master&event=workflow_run)](https://github.com/sh1ngekyo/Expreval/actions/workflows/build.yml) [![Test](https://github.com/sh1ngekyo/Expreval/actions/workflows/test.yml/badge.svg?branch=master&event=workflow_run)](https://github.com/sh1ngekyo/Expreval/actions/workflows/test.yml)

# Quick example:

A = true, B = false; 
```csharp
new Expression("!(A | B)").Eval<bool>();
new Expression("((!B) & A) | B)").Eval<bool>();
new Expression("!B").Eval<double>();
```
Output: false, true, 1.0;

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
    public FunctionType Type { get; set; }

    /* your function */
    public bool Call(bool left, bool right)
    {
        return left & right;
    }
}
```

# Create and configure your expression:

```csharp
var config = new ExpressionConfiguration() as IConfiguration;
config.RegisterVariable("A", true);
config.RegisterVariable("B", false);
config.RegisterFunction('&', new AndOperator() { Type = FunctionType.Binary });
var expr = new Expression("B & A");
expr.Configure(config);
```

# Evaluate your expression:
```csharp
//Eval<T> where T is your expected returned type
expr.Eval<bool>()
```

# Use your custom types as variables and functions:

```csharp
class ListPlusOperator<T> : IBinaryFunction<List<T>, List<T>, List<T>>
{
    public FunctionType Type { get; set; }

    public List<T> Call(List<T> left, List<T> right)
    {
        var result = new List<T>();
        result.AddRange(left);
        result.AddRange(right);
        return result;
    }
}

...

var Left = new List<int>() { 1, 2, 3 };
var Right = new List<int>() { 4, 5, 6 };

var config = new ExpressionConfiguration() as IConfiguration;
config.RegisterVariable(nameof(Left), Left);
config.RegisterVariable(nameof(Right), Right);
config.RegisterFunction('+', new ListPlusOperator<int>() { Type = FunctionType.Binary });

var expr = new Expression($"{nameof(Left)} + {nameof(Right)}");
expr.Configure(config);

expr.Eval<List<int>>();
```
Output: 1 2 3 4 5 6

# Quick example:

A = true, B = false; 
```csharp
new Expression("!(A | B)").Eval<bool>();
new Expression("((!B) & A) | B)").Eval<bool>();
new Expression("!B").Eval<double>();
```
Output: false, true, 1.0;

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
    public FunctionType Type { get; set; }

    /* your function */
    public bool Call(bool left, bool right)
    {
        return left & right;
    }
}
```

# Create and configure your expression:

```csharp
var config = new ExpressionConfiguration() as IConfiguration;
config.RegisterVariable("A", true);
config.RegisterVariable("B", false);
config.RegisterFunction('&', new AndOperator() { Type = FunctionType.Binary });
var expr = new Expression("B & A");
expr.Configure(config);
```

# Evaluate your expression:
```csharp
//Eval<T> where T is your expected returned type
expr.Eval<bool>()
```

# Use your custom types as variables and functions:

```csharp
class ListPlusOperator<T> : IBinaryFunction<List<T>, List<T>, List<T>>
{
    public FunctionType Type { get; set; }

    public List<T> Call(List<T> left, List<T> right)
    {
        var result = new List<T>();
        result.AddRange(left);
        result.AddRange(right);
        return result;
    }
}

...

var Left = new List<int>() { 1, 2, 3 };
var Right = new List<int>() { 4, 5, 6 };

var config = new ExpressionConfiguration() as IConfiguration;
config.RegisterVariable(nameof(Left), Left);
config.RegisterVariable(nameof(Right), Right);
config.RegisterFunction('+', new ListPlusOperator<int>() { Type = FunctionType.Binary });

var expr = new Expression($"{nameof(Left)} + {nameof(Right)}");
expr.Configure(config);

expr.Eval<List<int>>();
```
Output: 1 2 3 4 5 6