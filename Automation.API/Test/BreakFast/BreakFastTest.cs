using Automation.API.Model;
using Automation.API.Utility;
using NUnit.Framework;
using System.Net.Http;

namespace Automation.API
{
    public class BreakFastTest
    {
        StandardHttpClient _standardHttpClient;

        [SetUp]
        public void Setup()
        {
            var baseUrl = "https://www.w3schools.com/";
            _standardHttpClient = new StandardHttpClient(new HttpClient(), baseUrl);
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
        public void Test1()
        {
            var x = _standardHttpClient.GetRequestXml<BreakfastMenu>("xml/simple.xml");
            Assert.IsTrue(x.Response.Food.Count > 0);
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
        public void CheckResponseCode()
        {
            var x = _standardHttpClient.GetRequestXml<BreakfastMenu>("xml/simple.xml");
            Assert.IsTrue(x.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
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




    