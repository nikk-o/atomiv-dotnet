﻿using System.Threading.Tasks;

namespace Atomiv.Core.Common.Http
{
    public interface IObjectClient
    {
        Task<ObjectClientResponse<TResponse>> GetAsync<TResponse>(string uri, HeaderDictionary headers = null);

        // TODO: VC: DELETE this + impl
        // Task<ClientResponse> GetAsync(string uri);

        Task<ObjectClientResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, TRequest request, HeaderDictionary headers = null);

        Task<ObjectClientResponse<TResponse>> PostAsync<TResponse>(string uri, HeaderDictionary headers = null);

        Task<ClientResponse> PostNoResponseAsync<TRequest>(string uri, TRequest request, HeaderDictionary headers = null);

        Task<ObjectClientResponse<TResponse>> PutAsync<TRequest, TResponse>(string uri, TRequest request, HeaderDictionary headers = null);

        Task<ClientResponse> PutNoResponseAsync<TRequest>(string uri, TRequest request, HeaderDictionary headers = null);

        Task<ObjectClientResponse<TResponse>> DeleteAsync<TResponse>(string uri, HeaderDictionary headers = null);

        // TODO: VC: DELETE this + impl
        // Task<ClientResponse> DeleteAsync(string uri);
    }
}