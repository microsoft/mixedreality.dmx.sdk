﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Net.Http.Headers;
using RESTFulSense.Clients;

namespace DMX.Sdk.Brokers.DmxApis
{
    public partial class DmxApiBroker : IDmxApiBroker
    {
        private readonly string apiUrl;
        private readonly IRESTFulApiFactoryClient apiClient;
        private readonly string token;

        public DmxApiBroker(
            string apiUrl,
            string token)
        {
            this.token = token;
            this.apiUrl = apiUrl;
            this.apiClient = GetApiClient(apiUrl);
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PostContentAsync<T>(relativeUrl, content);

        private async ValueTask<T> UpdateAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PutContentAsync<T>(relativeUrl, content);

        private async ValueTask<T> DeleteAsync<T>(string relativeUrl) =>
            await this.apiClient.DeleteContentAsync<T>(relativeUrl);

        private IRESTFulApiFactoryClient GetApiClient(string apiUrl)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", this.token);

            httpClient.BaseAddress = new Uri(apiUrl);

            return new RESTFulApiFactoryClient(httpClient);
        }
    }
}