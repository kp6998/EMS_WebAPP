using System;
using System.Collections.Generic;
using EMS_WebAPI.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EMS_WebAPP.Model
{
    public class APIHandler
    {
        public static RQRS.ResponseData APICallMethod<T>(T input, string ApiEndPoint, string Type)
        {
            RQRS.ResponseData response = new RQRS.ResponseData();
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

                string apiUrl = configuration.GetSection("MySettings")["ApiUrl"];
                var client = new RestClient(apiUrl);
                var method = (Type == "POST") ? Method.Post : Method.Get;
                var request = new RestRequest(ApiEndPoint, method);
                request.AddHeader("Content-Type", "application/json");
                string jsonBody = JsonConvert.SerializeObject(input);
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                var apiResponse = client.Execute<JObject>(request);
                response = JsonConvert.DeserializeObject<RQRS.ResponseData>(apiResponse.Content);
            }
            catch (Exception ex)
            {
                response.strStatus = "00";
                response.strResponse = $"Unable to connect remote service: {ex.Message}";
            }
            return response;
        }
    }
}
