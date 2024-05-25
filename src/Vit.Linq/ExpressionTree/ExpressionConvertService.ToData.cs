﻿using System;
using System.Linq;
using System.Linq.Expressions;

using Vit.Linq.ExpressionTree.ComponentModel;

namespace Vit.Linq.ExpressionTree
{
    public partial class ExpressionConvertService
    {
        public ExpressionNode ConvertToData(Expression expression, bool autoReduce = false)
        {
            return ConvertToData(expression, out _, autoReduce);
        }

        public ExpressionNode ConvertToData(Expression expression, out ParamterInfo[] parameters, bool autoReduce = false)
        {
            var arg = new DataConvertArgument { convertService = this, autoReduce = autoReduce };

            ExpressionNode body = ConvertToData(arg, expression);

            arg.GenerateGlobalParameterName();
            parameters = arg.globalParameters?.ToArray();
            var parameterNames = parameters?.Select(m => m.parameterName).ToArray();
            return ExpressionNode.Lambda(parameterNames: parameterNames, body: body);
        }

        public  ExpressionNode ConvertToData(DataConvertArgument arg, Expression expression)
        {
            foreach (var expressionConvertor in expressionConvertors)
            {
                var node = expressionConvertor.ConvertToData(arg, expression);
                if (node != null) return node;
            }

            throw new NotSupportedException($"Unsupported expression type: {expression.GetType()}");
        }



    }
}
