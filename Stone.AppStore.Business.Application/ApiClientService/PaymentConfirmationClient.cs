using Newtonsoft.Json;
using Stone.AppStore.Business.Application.ApiClientService.Configs;
using Stone.AppStore.Business.Application.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Stone.AppStore.Business.Application.ApiClientService
{
    public class PaymentConfirmationClient : IPaymentConfirmationClient
    {
        private readonly IHttpClientFactory _apiClientFactory;
        private readonly UrlConfig _urls;

        public PaymentConfirmationClient(IHttpClientFactory apiClientFactory, UrlConfig urls)
        {
            _apiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
            _urls = urls ?? throw new ArgumentNullException(nameof(urls));
        }

        public async Task<ResponsePaymentConfirmation> PostPaymentConfirmation(PaymentModel command)
        {
            var _apiClient = _apiClientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(command);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _apiClient.PostAsync(_urls.PaymentConfirmationAPI, content);

            response.EnsureSuccessStatusCode();

            var responseString = response.Content.ReadAsStringAsync().Result;

            var jsonConvert = JsonConvert.DeserializeObject<ResponsePaymentConfirmation>(responseString);

            return jsonConvert;
        }
    }
}
