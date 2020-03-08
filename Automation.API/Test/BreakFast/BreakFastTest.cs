using Automation.API.Model;
using Automation.API.Utility;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace Automation.API
{
    public class BreakFastTest
    {
        StandardHttpClient _standardHttpClient;
        [SetUp]
        public void Setup()
        {
            var baseUrl = DataProvider.GetConfiguration("ApiEndpoint");
            _standardHttpClient = new StandardHttpClient(new HttpClient(), baseUrl);
        }

        [Test]
        public void Test1()
        {
            var x = _standardHttpClient.GetRequestXml<BreakfastMenu>("xml/simple.xml");
            Assert.IsTrue(x.Response.Food.Count > 0);
        }

        [Test]
        public void CheckResponseCode()
        {
            var x = _standardHttpClient.GetRequestXml<BreakfastMenu>("xml/simple.xml");
            Assert.IsTrue(x.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Test]
        public void TestwithAutodata()
        {
            
            //Test using moq data
            var y = DataProvider.MoqData<BreakfastMenu>();
            //Get Data from sheet
            var data = DataProvider.ReadExcelFile<BreakfastMenu>();
            var x = _standardHttpClient.GetRequestXml<BreakfastMenu>("xml/simple.xml");
            Assert.IsTrue(x.Response.Food.Count > 0);
        }
    }
}




    