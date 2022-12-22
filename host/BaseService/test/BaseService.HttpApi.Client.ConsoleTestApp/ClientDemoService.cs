using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BaseService.Samples;
using Volo.Abp.DependencyInjection;

namespace BaseService;

public class ClientDemoService : ITransientDependency
{
    private readonly ISampleAppService _sampleAppService;
    private readonly IConfiguration _configuration;

    public ClientDemoService(
        ISampleAppService sampleAppService,
        IConfiguration configuration)
    {
        _sampleAppService = sampleAppService;
        _configuration = configuration;
    }

    public async Task RunAsync()
    {
        await TestWithDynamicProxiesAsync();
        await TestWithHttpClientAndIdentityModelAuthenticationServiceAsync();
        await TestAllManuallyAsync();
    }

    /* Shows how to perform an HTTP request to the API using ABP's dynamic c# proxy
     * feature. It is just simple as calling a local service method.
     * Authorization and HTTP request details are handled by the ABP framework.
     */
    private async Task TestWithDynamicProxiesAsync()
    {
        Console.WriteLine();
        Console.WriteLine($"***** {nameof(TestWithDynamicProxiesAsync)} *****");

        var result = await _sampleAppService.GetAsync();
        Console.WriteLine("Result: " + result.Value);

        result = await _sampleAppService.GetAuthorizedAsync();
        Console.WriteLine("Result (authorized): " + result.Value);
    }

    /* Shows how to use HttpClient to perform a request to the HTTP API.
     * It uses ABP's IIdentityModelAuthenticationService to simplify obtaining access tokens.
     */
    private async Task TestWithHttpClientAndIdentityModelAuthenticationServiceAsync()
    {
        Console.WriteLine();
        Console.WriteLine($"***** {nameof(TestWithHttpClientAndIdentityModelAuthenticationServiceAsync)} *****");

        //Get access token using ABP's IIdentityModelAuthenticationService

        

        //Perform the actual HTTP request

        using (var httpClient = new HttpClient())
        {
            
            var url = _configuration["RemoteServices:BaseService:BaseUrl"] +
                      "api/BaseService/sample/authorized";

            var responseMessage = await httpClient.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseString = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine("Result: " + responseString);
            }
            else
            {
                throw new Exception("Remote server returns error code: " + responseMessage.StatusCode);
            }
        }
    }

    /* Shows how to use HttpClient to perform a request to the HTTP API.
     */
    private async Task TestAllManuallyAsync()
    {
        Console.WriteLine();
        Console.WriteLine($"***** {nameof(TestAllManuallyAsync)} *****");

        //Obtain access token from the IDS4 server

        // discover endpoints from metadata
        var client = new HttpClient();
        

        

    }
}
