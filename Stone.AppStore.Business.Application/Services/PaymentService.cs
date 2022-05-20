using AutoMapper;
using Serilog;
using Stone.AppStore.Business.Application.ApiClientService.ClientFactory;
using Stone.AppStore.Business.Application.Models;
using Stone.AppStore.Business.Domain;
using Stone.AppStore.Business.Domain.Enum;
using Stone.AppStore.Business.Domain.Interfaces;
using System;

namespace Stone.AppStore.Business.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly PaymentConfirmationClientFactory _paymentConfirmationClientFactory;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, PaymentConfirmationClientFactory paymentConfirmationClientFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _paymentConfirmationClientFactory = paymentConfirmationClientFactory ?? throw new ArgumentNullException(nameof(paymentConfirmationClientFactory));
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

                Log.Logger.Information("Payment confirmed with Success!");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(nameof(ex) + ": " + ex.ToString());

                paymentEntity.SetMessage(ex.Message);

                throw ex;
            }
            finally
            {
                await _paymentRepository.CreateAsync(paymentEntity);
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
