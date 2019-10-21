# LinqToString

[![Build status](https://ci.appveyor.com/api/projects/status/gyky4c8et9oysuyg?svg=true)](https://ci.appveyor.com/project/mccj/expressiontodynamiclinq)
[![MyGet](https://img.shields.io/myget/mccj/vpre/LinqToString.svg)](https://myget.org/feed/mccj/package/nuget/LinqToString)
[![NuGet](https://img.shields.io/nuget/v/LinqToString.svg)](https://www.nuget.org/packages/LinqToString)
![MIT License](https://img.shields.io/badge/license-MIT-orange.svg)

这是一个把 Linq 转换成字符串(string) 或者 可序列化对象()类型，方便在需要序列化的项目中传输 Linq 对象。
比如，把一个客户端的 Linq 查询对象推送至服务端。

```c#
        Expression<Func<Model, bool>> expression = f => (f.B2 > Convert.ToDateTime("2012-01-01"));
        var s1 = expression.ToStringExpression();
        //s1 = "((it).B2 > Convert.ToDateTime(\"2012-01-01\"))"


        Expression<Func<Model1, object>> expression1 = f => new { sss = "hhhhhh", Name = f.Name };
        var s1 = expression1.ToStringExpression();
        //s1= "new(\"hhhhhh\" as sss,(it).Name as Name)"
```