using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Flurl.Http;
using TelcobrightUtil;
using System.Threading.Tasks;

namespace SmsGateway
{
    public interface IHttpEndPoint
    {
        string ApiKey { get; set; }
        string ClientId { get; set; }
        string BaseUrl { get; set; }
        Task<object> Post(object requestBody, string requestUrlSuffix = "");
        Task<object> Get(object requestBody, string requestUrlSuffix = "");
    }
    public interface IHttpConfig
    {
        string BaseUrl { get; set; }
        string ApiKey { get; set; }
        string ClientId { get; set; }
    }
    public class HttpConfig : IHttpConfig
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ClientId { get; set; }
    }
    public abstract class BaseEndPoint : IHttpEndPoint
    {
        public string ApiKey { get; set; }
        public string ClientId { get; set; }
        public string BaseUrl { get; set; }
        Dictionary<string, object> RequestBodyTemplate { get; }
        public BaseEndPoint(IHttpConfig config)
        {
            BaseUrl = config.BaseUrl;
            RequestBodyTemplate = new Dictionary<string, object>
            {
                { "ApiKey", config.ApiKey },
                { "ClientId", config.ClientId }
            };
        }
        public Dictionary<string, object> CreateRequestBody()
        {
            return new Dictionary<string, object>(RequestBodyTemplate);
        }
        public string RequestUrl(string EndpointUri)
        {
            return $"{BaseUrl}{EndpointUri}";
        }
        public abstract Task<object> Post(object requestBody, string requestUrlSuffix);
        public abstract Task<object> Get(object requestBody, string requestUrlSuffix);
    }
    public class EndPoint : BaseEndPoint
    {
        public EndPoint(IHttpConfig config) : base(config) { }
        public override async Task<object> Post(object requestBody, string requestUrlSuffix)
        {
            var response = await RequestUrl(requestUrlSuffix).PostJsonAsync(requestBody);
            return await response.GetJsonAsync<object>();
        }
        public override Task<object> Get(object requestBody, string requestUrlSuffix)
        {
            throw new NotImplementedException();
        }
    }
    public class SmsProviderHttp : ISmsProvider
    {
        public EndPoint EndPoint { get; }
        public SmsProviderHttp(EndPoint endPoint)
        {
            this.EndPoint = endPoint;
        }
        public async Task<string> SendSms(SmsTask smsTask)
        {
            var requestBody = EndPoint.CreateRequestBody();
            var requestUrlSuffix = "/SendSMS";

            var taskDict = smsTask.ToDictionary();
            taskDict.ForEach(item => requestBody.Add(item.Key, item.Value));

            var result = await EndPoint.Post(requestBody, requestUrlSuffix);
            return result.ToString();
        }
    }
}
