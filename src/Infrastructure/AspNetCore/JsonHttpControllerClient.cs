﻿using Atomiv.Core.Common.Http;
using Atomiv.Core.Common.Reflection;
using Atomiv.Core.Common.Serialization;
using Atomiv.Infrastructure.System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Atomiv.Infrastructure.AspNetCore
{
    public class JsonHttpControllerClient : IControllerClient
    {
        public JsonHttpControllerClient(HttpClient httpClient, IJsonSerializer jsonSerializer, string controllerUri)
        {
            Client = new WebClient(httpClient);
            ObjectClient = new JsonClient(Client, jsonSerializer);
            PropertyFactory = new PropertyMapper();
            ControllerUri = controllerUri;
        }

        public IClient Client { get; private set; }

        public IObjectClient ObjectClient { get; private set; }

        public IPropertyMapper PropertyFactory { get; private set; }

        public string ControllerUri { get; private set; }

        public Task<ObjectClientResponse<TResponse>> GetAsync<TRequest, TResponse>(TRequest request, HeaderDictionary headers = null)
        {
            var queryString = GetQueryString(request);
            var relativeUri = GetRelativeByQuery(queryString);
            return ObjectClient.GetAsync<TResponse>(relativeUri, headers);
        }

        public Task<ObjectClientResponse<TResponse>> GetAsync<TRequest, TResponse>(string uri, TRequest request, HeaderDictionary headers = null)
        {
            var queryString = GetQueryString(request);
            var relativeUri = GetRelativeByPathQuery(uri, queryString);
            return ObjectClient.GetAsync<TResponse>(relativeUri, headers);
        }

        public Task<ObjectClientResponse<TResponse>> GetAsync<TResponse>(HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath();
            return ObjectClient.GetAsync<TResponse>(relativeUri, headers);
        }

        public Task<ClientResponse> GetNoResponseAsync(HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath();
            return Client.GetAsync(relativeUri, headers);
        }

        public Task<ObjectClientResponse<TResponse>> GetAsync<TResponse>(string uri, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return ObjectClient.GetAsync<TResponse>(relativeUri, headers);
        }

        public Task<ObjectClientResponse<TResponse>> GetByIdAsync<TId, TResponse>(TId id, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeUriById(id);
            return ObjectClient.GetAsync<TResponse>(relativeUri, headers);
        }

        public Task<ClientResponse> GetByIdNoResponseAsync<TId>(TId id, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeUriById(id);
            return Client.GetAsync(relativeUri, headers);
        }

        public Task<ObjectClientResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, TRequest request, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return ObjectClient.PostAsync<TRequest, TResponse>(relativeUri, request, headers);
        }

        public Task<ObjectClientResponse<TResponse>> PostAsync<TResponse>(string uri, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return ObjectClient.PostAsync<TResponse>(relativeUri, headers);
        }

        public Task<ClientResponse> PostNoResponseAsync<TRequest>(string uri, TRequest request, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return ObjectClient.PostNoResponseAsync(relativeUri, request, headers);
        }

        public Task<ObjectClientResponse<TResponse>> PostAsync<TRequest, TResponse>(TRequest request, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath();
            return ObjectClient.PostAsync<TRequest, TResponse>(relativeUri, request, headers);
        }

        public Task<ClientResponse> PostNoResponseAsync<TRequest>(TRequest request, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath();
            return ObjectClient.PostNoResponseAsync(relativeUri, request, headers);
        }

        public Task<ObjectClientResponse<TResponse>> PostSubAsync<TRequest, TResponse>(string uri, TRequest request, HeaderDictionary headers = null)
        {
            throw new NotImplementedException();
        }

        public Task<ObjectClientResponse<TResponse>> PutAsync<TRequest, TResponse>(string uri, TRequest request, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return ObjectClient.PutAsync<TRequest, TResponse>(relativeUri, request, headers);
        }

        public Task<ClientResponse> PutNoResponseAsync<TRequest>(string uri, TRequest request, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return ObjectClient.PutNoResponseAsync(relativeUri, request, headers);
        }

        public Task<ObjectClientResponse<TResponse>> PutByIdAsync<TId, TRequest, TResponse>(TId id, TRequest request, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeUriById(id);
            return ObjectClient.PutAsync<TRequest, TResponse>(relativeUri, request, headers);
        }

        public Task<ClientResponse> PutByIdNoResponseAsync<TId, TRequest>(TId id, TRequest request, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeUriById(id);
            return ObjectClient.PutNoResponseAsync(relativeUri, request, headers);
        }

        public Task<ObjectClientResponse<TResponse>> DeleteAsync<TResponse>(string uri, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return ObjectClient.DeleteAsync<TResponse>(relativeUri, headers);
        }

        public Task<ObjectClientResponse<TResponse>> DeleteByIdAsync<TId, TResponse>(TId id, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeUriById(id);
            return ObjectClient.DeleteAsync<TResponse>(relativeUri, headers);
        }

        public Task<ClientResponse> DeleteByIdNoResponseAsync<TId>(TId id, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeUriById(id);
            return Client.DeleteAsync(relativeUri, headers);
        }

        public Task<ClientResponse> GetAsync(string uri, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return Client.GetAsync(relativeUri, headers);
        }

        public Task<ClientResponse> PostAsync(string uri, string content, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return Client.PostAsync(relativeUri, content, headers);
        }

        public Task<ClientResponse> PutAsync(string uri, string content, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return Client.PutAsync(relativeUri, content, headers);
        }

        public Task<ClientResponse> DeleteAsync(string uri, HeaderDictionary headers = null)
        {
            var relativeUri = GetRelativeByPath(uri);
            return Client.DeleteAsync(relativeUri, headers);
        }

        #region Helper

        private string GetRelativeUriById<TId>(TId id)
        {
            return $"{ControllerUri}/{id}";
        }

        /*
        private string GetPathSuffix(string path)
        {
            return $"/{path}";
        }

        private string GetQuerySuffix(string query)
        {
            return $"?{query}";
        }

        private string GetControllerUri()
        {
            return ControllerUri;
        }

        private string GetControllerUri(string uri)
        {
            return $"{ControllerUri}{uri}";
        }
        */

        private string GetRelativeByQuery(string query)
        {
            return $"{ControllerUri}?{query}";
        }

        private string GetRelativeByPathQuery(string path, string query)
        {
            return $"{ControllerUri}/{path}?{query}";
        }

        private string GetRelativeByPath(string path)
        {
            return $"{ControllerUri}/{path}";
        }

        private string GetRelativeByPath()
        {
            return ControllerUri;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private string GetQueryString<TRequest>(TRequest request)
        {
            var propertyValues = PropertyFactory.GetObjectProperties(request);

            var queryParameters = propertyValues
                .Where(e => e.Value != null)
                .Select(e => QueryParameter.From(e))
                .ToList();

            var queryString = QueryString.From(queryParameters);

            return queryString;
        }

        private class QueryParameter
        {
            public QueryParameter(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; set; }

            public string Value { get; set; }

            public override string ToString()
            {
                return $"{Name}={Value}";
            }

            public static QueryParameter From(IObjectProperty property)
            {
                var name = property.TypeProperty.Name;
                var value = property.Value;
                var stringValue = value.ToString();
                var encodedValue = HttpUtility.UrlEncode(stringValue);

                return new QueryParameter(name, encodedValue);
            }
        }

        private class QueryString
        {
            public static string From(IEnumerable<QueryParameter> queryParameters)
            {
                var queryParameterStrings = queryParameters.Select(e => e.ToString()).ToList();
                var queryString = string.Join("&", queryParameterStrings);
                return queryString;
            }
        }

        #endregion Helper
    }
}
