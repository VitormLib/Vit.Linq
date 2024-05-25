﻿using System;
using System.Collections.Generic;
using System.Text;

using Vit.Linq.Filter;

 

namespace Vit.Linq.ExpressionTree.ComponentModel
{

    public interface ExpressionNode_MethodCall : IExpressionNode
    {
        /// <summary>
        ///     the System.Linq.Expressions.Expression that represents the instance for instance method calls or null for static method calls.
        /// </summary>
        public ExpressionNode @object { get;}

        public string typeName { get; }
        public string methodName { get;  }

        public ExpressionNode[] arguments { get; }
    }

    public partial class ExpressionNode : ExpressionNode_MethodCall
    {

        /// <summary>
        ///     the System.Linq.Expressions.Expression that represents the instance for instance method calls or null for static method calls.
        /// </summary>
        public ExpressionNode @object { get; set; }

        public string typeName { get; set; }
        public string methodName { get; set; }

        public ExpressionNode[] arguments { get; set; }

        public static ExpressionNode MethodCall(string typeName = null, string methodName = null, ExpressionNode @object = null, ExpressionNode[] arguments = null)
        {
            return new ExpressionNode
            {
                nodeType = NodeType.MethodCall,
                typeName = typeName,
                methodName = methodName,
                @object = @object,
                arguments = arguments,
            };
        }
    }
}
