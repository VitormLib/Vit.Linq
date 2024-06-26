﻿using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Vit.Core.Module.Serialization;
using Vit.Extensions.Linq_Extensions;
using Vit.Linq.Filter;

namespace Vit.Linq.MsTest
{
    [TestClass]
    public class QueryableBuilder_Test
    {
        [TestMethod]
        public void Test()
        {
            {
                Func<Expression, Type, object> QueryExecutor = (exp, type) =>
                {
                    var queryAction = new FilterRuleConvert().ConvertToQueryAction(exp);
                    var strQuery = Json.Serialize(queryAction);

                    var exp2 = new FilterService().ToExpression<Person>(queryAction.filter);
                    var query2 = new FilterRuleConvert().ConvertToQueryAction(exp2);
                    var strQuery2 = Json.Serialize(query2);


                    var list = DataSource.GetQueryable().Where(queryAction.filter);
                    list = list.OrderBy(queryAction.orders);


                    if (queryAction.skip.HasValue)
                        list = list.Skip(queryAction.skip.Value);
                    if (queryAction.take.HasValue)
                        list = list.Take(queryAction.take.Value);

                    switch (queryAction.method)
                    {
                        case "First": return list.First();
                        case "FirstOrDefault": return list.FirstOrDefault();
                        case "Last": return list.Last();
                        case "LastOrDefault": return list.LastOrDefault();
                        case "Count": return list.Count();
                    }

                    // ToList
                    return list;
                };

                var query = QueryableBuilder.Build<Person>(QueryExecutor);

                query = query
                    .Where(m => m.id >= 10)
                    .Where(m => m.id < 20)
                    .Where(m => !m.name.Contains("8"))
                    .Where(m => !m.job.name.Contains("6"))
                    .OrderBy(m => m.job.name)
                    .OrderByDescending(m => m.id)
                    .ThenBy(m => m.job.departmentId)
                    .ThenByDescending(m => m.name)
                    .Skip(1)
                    .Take(5);

                var list = query.ToList();
                var count = query.Count();
             

                Assert.AreEqual(5, list.Count);
                Assert.AreEqual(17, list[0].id);
                Assert.AreEqual(15, list[1].id);
            }


        }




    }
}
