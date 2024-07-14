﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vit.Linq.Filter.ComponentModel;

namespace Vit.Linq.Filter.FilterConvertor.ConditionConvertor
{
    public class NotAnd : IConditionConvertor
    {
        public int priority { get; set; } = 10;
        public (bool success, Expression expression) ConvertToCode(FilterConvertArgument arg, IFilterRule filter, string condition)
        {
            if (RuleCondition.NotAnd.Equals(condition, arg.comparison))
            {
                if (filter.rules?.Any() != true)
                    return (true, null);

                return (true, Expression.Not(And.ConvertAnd(arg, filter.rules, isAnd: true)));
            }
            return default;
        }

    }
}
