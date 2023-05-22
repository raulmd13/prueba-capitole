using NLog;
using NLog.Web;
using RaulPruebaTecnica.Models;
using RaulPruebaTecnica.Models.DTO;
using RaulPruebaTecnica.Repository;
using System.Text.Json;

namespace RaulPruebaTecnica.Service
{
    public class TransactionsService : ITransactionsService
    {
        private readonly string UrlTransactions;
        private readonly string UrlConversions;
        private readonly HttpClient HttpClient;
        private readonly NLog.Logger Logger;
        private readonly ITransactionRepository _TransactionRepository;
        private readonly IConversionRepository _ConversionRepository;

        private readonly string EuroString = "EUR";

        public TransactionsService(ExternalServices externalServices, ITransactionRepository transactionRepository, IConversionRepository conversionRepository)
        {
            UrlTransactions = externalServices.Transactions;
            UrlConversions = externalServices.ConversionRates;
            HttpClient = new HttpClient();
            Logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            _TransactionRepository = transactionRepository;
            _ConversionRepository = conversionRepository;
        }

        public virtual List<ConversionRateDTO> GetConversions()
        {
            try
            {
                var responce = HttpClient.GetStringAsync(UrlConversions).Result;
                var Conversions = JsonSerializer.Deserialize<List<ConversionRateDTO>>(responce.ToString());

                if (Conversions != null || Conversions.Count > 0)
                {
                    _ConversionRepository.UpdateAll(Conversions);
                    return Conversions;
                }
            }
            catch (AggregateException ex)
            {
                if (ex.GetBaseException() is HttpRequestException httpRequestException)
                {
                    //El servicio esta caido, registramos el fallo y tratamos de apoyarnos en registros previamente guardados
                    Logger.Error(ex);
                }
                else
                {
                    throw ex;
                }
            }

            return _ConversionRepository.GetAll().ToList();
        }

        public virtual List<TransactionDTO> GetTransactions()
        {
            try
            {
                var responce = HttpClient.GetStringAsync(UrlTransactions).Result;
                var Transactions = JsonSerializer.Deserialize<List<TransactionDTO>>(responce.ToString());

                if (Transactions != null || Transactions.Count > 0)
                {
                    _TransactionRepository.UpdateAll(Transactions);
                    return Transactions;
                }
            }
            catch (AggregateException ex)
            {
                if (ex.GetBaseException() is HttpRequestException httpRequestException)
                {
                    //El servicio esta caido, registramos el fallo y tratamos de apoyarnos en registros previamente guardados
                    Logger.Error(ex);
                }
                else
                {
                    throw ex;
                }
            }

            return _TransactionRepository.GetAll().ToList();
        }

        public TransactionsSumDTO GetTransactions(string sku)
        {
            List<TransactionDTO> transactions = GetTransactions();
            List<ConversionRateDTO> conversions = GetConversions();

            TransactionsSumDTO result = new TransactionsSumDTO();

            foreach (TransactionDTO transaction in transactions)
            {
                if (transaction.Sku == sku)
                {
                    Decimal rate = 1;

                    if (transaction.Currency != EuroString)
                    {
                        rate = Convert.ToDecimal(GetConversionRateFromAToB(conversions, transaction.Currency, EuroString));
                    }

                    result.Total += transaction.Amount * rate;
                    result.Transactions.Add(transaction);
                }
            }

            result.Total = Math.Round(result.Total, 2);

            return result;
        }

        private double GetConversionRateFromAToB(List<ConversionRateDTO> list, string currencyA, string currencyB)
        {
            foreach (ConversionRateDTO conversionRate in list)
            {
                if (conversionRate.From == currencyA && conversionRate.To == currencyB)
                {
                    return conversionRate.Rate;
                }
                if (conversionRate.To == currencyA && conversionRate.From == currencyB)
                {
                    return (1 / conversionRate.Rate);
                }
            }

            throw new Exception(message: $"A correction rate has not been found for currencies {currencyA} and {currencyB}");
        }
    }
}