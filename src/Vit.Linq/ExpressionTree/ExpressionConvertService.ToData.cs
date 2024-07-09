﻿using System;
using System.Linq;
using System.Linq.Expressions;

using Vit.Linq.ExpressionTree.ComponentModel;

namespace Vit.Linq.ExpressionTree
{
    public partial class ExpressionConvertService
    {
        public ExpressionNode ConvertToLambdaData(Expression expression, bool autoReduce = true, bool reduceConvertExpression = true, Func<object, Type, bool> isArgument = null)
        {
            return ConvertToLambdaData(expression, out _, autoReduce: autoReduce, reduceConvertExpression: reduceConvertExpression, isArgument: isArgument);
        }
        public ExpressionNode ConvertToLambdaData(Expression expression, out ParameterInfo[] parameters, bool autoReduce = true, bool reduceConvertExpression = true, Func<object, Type, bool> isArgument = null)
        {
            var arg = new DataConvertArgument { convertService = this, autoReduce = autoReduce, reduceConvertExpression = reduceConvertExpression, isArgument = isArgument };
            return ConvertToLambdaData(expression, out parameters, arg);
        }
        public ExpressionNode ConvertToLambdaData(Expression expression, out ParameterInfo[] parameters, DataConvertArgument arg)
        {
            arg.convertService ??= this;

            ExpressionNode body = ConvertToData(arg, expression);

            arg.GenerateGlobalParameterName();
            parameters = arg.globalParameters?.ToArray();
            var parameterNames = parameters?.Select(m => m.parameterName).ToArray();
            return ExpressionNode.Lambda(parameterNames: parameterNames, body: body);
        }

        public ExpressionNode ConvertToData(DataConvertArgument arg, Expression expression)
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
