using NUnit.Framework;
using ApiTest.API.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.ApiTests
{
    public class Test
    {
        [Category("Test")]
        [Test]
        public async Task TestApiCall()
        {
            ApiTest.API.Request.API api = new ApiTest.API.Request.API();
            var response = await api.testRequest();
        }
    }
}
