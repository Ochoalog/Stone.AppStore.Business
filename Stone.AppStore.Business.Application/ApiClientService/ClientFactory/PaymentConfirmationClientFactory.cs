using Microsoft.Extensions.Options;
using Stone.AppStore.Business.Application.ApiClientService.Configs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Stone.AppStore.Business.Application.ApiClientService.ClientFactory
{
    public class PaymentConfirmationClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UrlConfig _urls;

        public PaymentConfirmationClientFactory(IHttpClientFactory httpClientFactory, IOptions<UrlConfig> config)
        {
            _httpClientFactory = httpClientFactory;
            _urls = config.Value;
        }

        public PaymentConfirmationClient BuildInBoundConfirmationSAPApiClient()
        {
            return new PaymentConfirmationClient(_httpClientFactory, _urls);
        }
    }
}
