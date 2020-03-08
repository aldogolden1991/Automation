using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Automation.API.Utility
{
    public class StandardHttpClient : IHttpClient
    {
        private readonly HttpClient _client;
        public StandardHttpClient(HttpClient client, string baseUrl)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(baseUrl);
        }

        public ResponseModel<T> GetRequestXml<T>(string url) where T : class
        {
            ResponseModel<T> responseModel = new ResponseModel<T>();
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = _client.SendAsync(request).Result;
            var returnedXml = response.Content.ReadAsStringAsync().Result;
            responseModel.Response = Serializer.DeserializeXml<T>(returnedXml);
            responseModel.StatusCode = response.StatusCode;
            return responseModel;
        }

        public ResponseModel<T>  PutRequestXml<T>(string url, T content) where T : class
        {
            ResponseModel<T> responseModel = new ResponseModel<T>();
            var xml = Serializer.SerializeXml<T>(content);
            var data = new StringContent(xml, Encoding.UTF8, "text/xml");
            var response = _client.PutAsync(url, data).Result;
            var returnedXml = response.Content.ReadAsStringAsync().Result;
            responseModel.Response = Serializer.DeserializeXml<T>(returnedXml);
            responseModel.StatusCode = response.StatusCode;
            return responseModel;
        }

        public ResponseModel<T> PostRequestXml<T>(string url, T content) where T : class
        {
            ResponseModel<T> responseModel = new ResponseModel<T>();
            var xml = Serializer.SerializeXml<T>(content);
            var data = new StringContent(xml, Encoding.UTF8, "text/xml");
            var response = _client.PostAsync(url, data).Result;
            var returnedXml = response.Content.ReadAsStringAsync().Result;
            responseModel.Response = Serializer.DeserializeXml<T>(returnedXml);
            responseModel.StatusCode = response.StatusCode;
            return responseModel;
        }
    }

    public interface IHttpClient
    {
        ResponseModel<T> GetRequestXml<T>(string url) where T : class;
        ResponseModel<T> PostRequestXml<T>(string url, T content) where T : class;
        ResponseModel<T> PutRequestXml<T>(string url, T content) where T : class;
    }

    public class ResponseModel<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Response { get; set; }
    }
}
