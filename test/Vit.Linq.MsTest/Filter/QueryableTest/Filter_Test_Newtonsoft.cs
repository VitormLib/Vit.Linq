﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;
using Vit.Extensions.Linq_Extensions;
using Vit.Linq.Filter;
using Queryable = System.Linq.IQueryable<Vit.Linq.MsTest.Person>;
using Vit.Linq.Filter.ComponentModel;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Vit.Linq.MsTest.Filter.QueryableTest
{
    [TestClass]
    public class Filter_Test_Newtonsoft : Filter_TestBase<Queryable>
    {

        [TestMethod]
        public void Test_FilterRule()
        {
            base.TestFilterRule();
        }

        public override FilterRule GetRule(string filterRule)
        {
            return JsonConvert.DeserializeObject<FilterRule>(filterRule);
        }

        public override Queryable ToQuery(IQueryable<Person> query) => query;

        public override List<Person> Filter(Queryable query, IFilterRule rule)
        {
            return query.Where(rule, GetService()).ToList<Person>();
        }

        public virtual FilterService GetService()
        {
            FilterService service = new FilterService();
            service.getPrimitiveValue = GetPrimitiveValue_Newtonsoft;
            return service;
        }

      

        static object GetPrimitiveValue_Newtonsoft(object value)
        {
            if (value is JValue jv)
            {
                return jv.Value;
            }
            if (value is JArray ja)
            {
                return ja.Select(token => (token as JValue)?.Value).ToList();
            }
            return value;
        }
         


    }
}
