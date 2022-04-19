# TruthTable

This example project is for constructing Nth truth tables from custom boolean expressions.

Example:
```
    Function  Description
        &       And;
        |       Or;
        !       Not;
        >       Imlication;
        ^       Xor;

Your expression (for example A & B): A & C | B > C ^ G

A       C       B       G       Result
0       0       0       0       0
0       0       0       1       0
0       0       1       0       0
0       0       1       1       0
0       1       0       0       0
0       1       0       1       0
0       1       1       0       0
0       1       1       1       0
1       0       0       0       1
1       0       0       1       1
1       0       1       0       0
1       0       1       1       1
1       1       0       0       1
1       1       0       1       1
1       1       1       0       1
1       1       1       1       1
```