using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Stone.AppStore.Business.Application.ApiClientService.ClientFactory;
using Stone.AppStore.Business.Application.Models;
using Stone.AppStore.Business.Domain.Entities;
using Stone.AppStore.Business.Domain.Enum;
using Stone.AppStore.Business.Domain.Interfaces;
using System;

namespace Stone.AppStore.Business.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly PaymentConfirmationClientFactory _paymentConfirmationClientFactory;

        public PaymentService(IMapper mapper, IServiceProvider serviceProvider, PaymentConfirmationClientFactory paymentConfirmationClientFactory)
        {
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _paymentConfirmationClientFactory = paymentConfirmationClientFactory;
        }

        public async void PaymentConfirmation(PaymentModel paymentModel)
        {
            Payment paymentEntity = new Payment();

            if (paymentModel == null)
            {
                Log.Logger.Error("Payment is empty!");
            }

            try
            {
                paymentEntity = _mapper.Map<Payment>(paymentModel);

                var paymentConfirmationClient = _paymentConfirmationClientFactory.BuildInBoundConfirmationSAPApiClient();

                ResponsePaymentConfirmation response = await paymentConfirmationClient.PostPaymentConfirmation(paymentModel);

                SetResultPaymentConfirmation(response, paymentEntity);

                paymentEntity.SetMessage(response.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(nameof(ex) + ": " + ex.ToString());

                paymentEntity.SetMessage(ex.Message);

                throw ex;
            }
            finally
            {
                if(paymentEntity.ResultConfirmation == ResultConfirmationEnum.Error)
                {
                    Log.Logger.Error("Credit card is not valid!");
                }
                else
                {
                    using var scope = _serviceProvider.CreateScope();
                    var paymentRepository = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();
                    await paymentRepository.CreateAsync(paymentEntity);
                    Log.Logger.Information("Payment confirmed with success!");
                }
            }
        }

        private void SetResultPaymentConfirmation(ResponsePaymentConfirmation response, Payment paymentEntity)
        {
            if (response.Result == "S")
                paymentEntity.SetResultConfirmation(ResultConfirmationEnum.Success);
            else
                paymentEntity.SetResultConfirmation(ResultConfirmationEnum.Error);
        }
    }
}
