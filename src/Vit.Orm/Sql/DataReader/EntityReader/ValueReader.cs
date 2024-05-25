﻿using System;

using Vit.Linq.ExpressionTree.ComponentModel;

namespace Vit.Orm.Sql.DataReader
{

    class ValueReader : SqlFieldReader, IArgReader
    {
        public string argName { get; set; }

        public string argUniqueKey { get; set; }

        public Type argType { get => valueType; }

        public ValueReader(EntityReader entityReader, ExpressionNode_Member member, string argUniqueKey, string argName, string sqlFieldName)
             : base(entityReader.sqlFields, member.Member_GetType(), sqlFieldName)
        {
            this.argUniqueKey = argUniqueKey;
            this.argName = argName;
        }
    }


}
