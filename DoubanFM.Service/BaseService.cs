﻿using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace DoubanFM.Service
{
    public abstract class BaseService
    {
        public const string baseUrl = "http://www.douban.com/";
        public const string LoginRequstPath = "j/app/login";
        public const string UserInfoRequestPath = "/j/app/radio/user_info";
        public const string ChannelRequestPath = "j/app/radio/channels";
        public const string SongRequestPath= "j/app/radio/people";


        public BaseService()
        {

        }

        protected async Task<T> Get<T>(string path, BaseSvcParams param)
        {
            var restClient = new RestClient(baseUrl);
            var request = new RestRequest(path, Method.GET);
            GetParameters(param).ForEach(p => request.AddParameter(p.Name, p.GetValue(param)));
            var response = await restClient.ExecuteTaskAsync(request);
            return Serialize<T>(response);
        }


        protected async Task<T> Post<T>(string path, BaseSvcParams param)
        {
            var restClient = new RestClient(baseUrl);
            var request = new RestRequest(path, Method.POST);
            GetParameters(param).ForEach(p => request.AddParameter(p.Name, p.GetValue(param)));
            var response= await restClient.ExecuteTaskAsync(request);
            return Serialize<T>(response);
        }

        private T Serialize<T>(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;
                return JObject.Parse(content).ToObject<T>();
            }

            return default(T);
        }

        private List<PropertyInfo> GetParameters(BaseSvcParams param)
        {
            var type = param.GetType();
            var props = type.GetProperties();
            return props.Where(p => p.GetValue(param) != null).ToList();
        }

    }
}
