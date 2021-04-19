using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using log4net;
using System.Reflection;

namespace Outsourcing.Core.Payment
{
    public static class PaymentCryptoApi
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static string urlAdress = "https://api.cryptoapis.io/";
        static string apiKey = "c9f3046cf9ac48f774f7d5dbe188a7b5dac6e9e3";
        public static string getBalance(string strAddress)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                HttpResponseMessage response = client.GetAsync("v1/bc/eth/mainnet/address/" + strAddress + "/balance").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }
        public static string getAssetDetail() //Lấy tỉ giá coin
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                HttpResponseMessage response = client.GetAsync("v1/assets/").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }
        public static string getMainnetTransactionByAddressEndpoind(string strAddress) //Lấy lịch sử giao dịch
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                HttpResponseMessage response = client.GetAsync("v1/bc/eth/mainnet/address/" + strAddress + "/transactions").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }

        public static string getMainnetGeneratorAddressEndpoind()   //Tao tài khoản mới với private key
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("","")
                });
                HttpResponseMessage response = client.PostAsync("v1/bc/eth/mainnet/address", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }

        public static string MainnetNonceEndPiond(string strAddress)    //giống với getMainnetTransactionByAddressEndpoind
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                HttpResponseMessage response = client.GetAsync("v1/bc/eth/mainnet/address/" + strAddress + "/transactions").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }

        public static string MainnetTransactionHashEndPiond(string strHash) // Chi tiết transaction
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                HttpResponseMessage response = client.GetAsync("v1/bc/eth/mainnet/txs/hash/" + strHash).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }

        public static string MainnetPendingTransactionsEndPiond()    //lấy các giao dịch đang chờ xử lý
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                HttpResponseMessage response = client.GetAsync("v1/bc/eth/mainnet/txs/pending").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }

        public static string MainnetQueuedTransactionsEndPiond()    //lấy các giao dịch đang đợi xử lí
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                HttpResponseMessage response = client.GetAsync("v1/bc/eth/mainnet/txs/queued").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }

        public static string MainnetEstimateTransactionsGaspriceEndPiond()    //Lấy giá gas (giống MainnetQueuedTransactionsEndPiond)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                HttpResponseMessage response = client.GetAsync("v1/bc/eth/mainnet/txs/queued").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }

        public static async Task<string> MainnetInternalTransactionsEndPiond()    //
        {
            string respone = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                using (HttpResponseMessage response = await client.GetAsync("v1/bc/eth/mainnet/txs/queued"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        respone = await response.Content.ReadAsStringAsync();
                    }
                }                   
                return respone;
            }
        }
        #region Nghia by code
        public static string GenerateAccountEndpoint(string _password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
                //start truyển tham số vào body tại đây
                var jsonObject = new { password = _password };
                //end truyển tham số vào body tại đây
                var myContent = JsonConvert.SerializeObject(jsonObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("v1/bc/eth/mainnet/account", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
        }
        #endregion

    }
}
