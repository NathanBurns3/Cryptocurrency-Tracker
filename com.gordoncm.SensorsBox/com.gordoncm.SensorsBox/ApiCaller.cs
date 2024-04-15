using RestSharp;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Web;

namespace com.gordoncm.SensorsBox
{
    public class ApiCaller
    {
        public async Task<string> GetTokenFromTx(string contractAddress)
        {
            // 0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2
            var url = "https://api.etherscan.io/api?module=account&action=tokentx&contractaddress="+contractAddress+"" +
                "&apikey=HCM6XPJS2XHRWI4UATNGYA4ZK2EBKPWGAG+&page=1&offset=5";
            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var request = new RestRequest(""); 

            var client = new RestClient(options);
            var response = await client.GetAsync(request);

            return response.Content;
        }

        public static async Task<string> getListings()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";


            queryString["limit"] = "200";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();
            //note:
            //VS code was saying that WebClient was obsolete, so I changed it to Http client, method was made async Task rather than string


            //var client = new WebClient();
            //client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            //client.Headers.Add("Accepts", "application/json");
            //return client.DownloadString(URL.ToString());
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "beabbf43-6590-4d45-8e2f-09d1f219d80f"); //I think these are going to be universal so having them as default is fine ???
            client.DefaultRequestHeaders.Add("Accepts", "application/json");
            string page = await client.GetStringAsync(URL.ToString());

            return page;
        }

        public async Task<string> GetETHPortfolio(string walletAddress)
        {
            var url = "https://eth-mainnet.g.alchemy.com/v2/aUQWVOzm8LCfr0MDlN1cbNZUjZEhEw7z";

            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddJsonBody("{\"id\":1,\"jsonrpc\":\"2.0\",\"method\":\"alchemy_getTokenBalances\",\"params\":["+
            '"'+walletAddress+'"'+"]}", false);
            var response = await client.PostAsync(request);

            return response.Content; 
        }
    }
}
/** 
 * 
 * */