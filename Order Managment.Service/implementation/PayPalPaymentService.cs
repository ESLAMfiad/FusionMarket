using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
//using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.implementation
{
	//public class PayPalPaymentService : IPayPalPaymentService
	//{
	//	private readonly HttpClient _httpClient;
	//	private readonly string _clientId;
	//	private readonly string _clientSecret;
	//	private readonly string _apiUrl;
	//	public PayPalPaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
	//	{
	//		_httpClient = httpClientFactory.CreateClient();
	//		_clientId = configuration["PayPal:ClientId"];
	//		_clientSecret = configuration["PayPal:ClientSecret"];
	//		_apiUrl = configuration["PayPal:ApiUrl"];
	//	}
	//	public async Task<string> ProcessPayment(decimal amount, string currency, string paymentMethod, Dictionary<string, string> paymentDetails)
	//	{
	//		var token = await GetAccessTokenAsync();

	//		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

	//		var payment = new
	//		{
	//			intent = "sale",
	//			transactions = new[]
	//			{
	//			new
	//			{
	//				amount = new
	//				{
	//					total = amount.ToString("F2"),
	//					currency = currency
	//				}
	//			}
	//		},
	//			payer = new
	//			{
	//				payment_method = paymentMethod
	//			},
	//			redirect_urls = new
	//			{
	//				cancel_url = "https://example.com/cancel",
	//				return_url = "https://example.com/return"
	//			}
	//		};

	//		var content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");
	//		var response = await _httpClient.PostAsync($"{_apiUrl}/v1/payments/payment", content);
	//		response.EnsureSuccessStatusCode();

	//		var result = await response.Content.ReadAsStringAsync();
	//		dynamic jsonResponse = JsonConvert.DeserializeObject(result);

	//		return jsonResponse.id;
	//	}

	//	private async Task<string> GetAccessTokenAsync()
	//	{
	//		var authToken = Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}");

	//		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

	//		var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
	//		var response = await _httpClient.PostAsync($"{_apiUrl}/v1/oauth2/token", content);
	//		response.EnsureSuccessStatusCode();

	//		var result = await response.Content.ReadAsStringAsync();
	//		dynamic jsonResponse = JsonConvert.DeserializeObject(result);

	//		return jsonResponse.access_token;
	//	}
	//}
}
