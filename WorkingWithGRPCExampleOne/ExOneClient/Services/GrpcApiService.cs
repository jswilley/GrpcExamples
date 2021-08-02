
using Ex1GrpcClient;
using Ex1Shared;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ex1GrpcClient
{
    public class GrpcApiService
    {
       
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiTokenInMemoryClient _apiTokenInMemoryClient;

        public GrpcApiService(
            IHttpClientFactory clientFactory,
            ApiTokenInMemoryClient apiTokenClient)
        {
           
            _clientFactory = clientFactory;
            _apiTokenInMemoryClient = apiTokenClient;
        }

        public async Task<string> GetGrpcGreetingsAsync()
        {
            try
            {
                var client = _clientFactory.CreateClient("grpc");
             

                var access_token = await _apiTokenInMemoryClient.GetApiToken(
                    "ProtectedGrpc",
                    "grpc_protected_scope",
                    "grpc_protected_secret"
                );

                var tokenValue = "Bearer " + access_token;
                var metadata = new Metadata
                {
                    { "Authorization", tokenValue }
                };

                CallOptions callOptions = new CallOptions(metadata);

              //  var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var baseUri = "https://localhost:44371";
                var channel = GrpcChannel.ForAddress(baseUri);

                var clientGrpc = new Ex1Shared.Greeter.GreeterClient(channel);

                var response = await clientGrpc.SayHelloAsync(
                 new HelloRequest { Name = "GreeterClient managed" }, callOptions);

                return  response.Message;
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception {e}");
            }
        }



        public async Task<IList<WeatherForecast>> GetGrpcWeatherDataAsync()
        {
            try
            {
                var client = _clientFactory.CreateClient("grpc");
              
                var access_token = await _apiTokenInMemoryClient.GetApiToken(
                    "ProtectedGrpc",
                    "grpc_protected_scope",
                    "grpc_protected_secret"
                );

                var tokenValue = "Bearer " + access_token;
                var metadata = new Metadata
                {
                    { "Authorization", tokenValue }
                };

                CallOptions callOptions = new CallOptions(metadata);

                  var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var baseUri = "https://localhost:44371";
                var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient });
                var clientGrpc = new Ex1Shared.WeatherForecasts.WeatherForecastsClient(channel);
                var response = await clientGrpc.GetWeatherAsync(new Ex1Shared.WeatherForecast(), callOptions);
                
                return response.Forecasts;
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception {e}");
            }
        }
    }
}
