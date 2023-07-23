using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineShop.Library.Options;
using OnlineShop.Library.UserManagmentService;
using System.Text;

namespace OnlineShop.Library.Clients.UserManagmentService
{
    public abstract class UserManagmentBaseClient : IDisposable
    {
        public HttpClient HttpClient { get; set; }

        public UserManagmentBaseClient(HttpClient httpClient, IOptions<ServiceAddressOptions> opts)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            HttpClient.BaseAddress = new Uri(opts.Value.UserManagmentService);
        }

        protected async Task<IdentityResult> SendPostRequest<TRequest>(TRequest request, string path)
        {
            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var requetResult = await HttpClient.PostAsync(path, httpContent);

            if (requetResult.IsSuccessStatusCode)
            {
                var response = await requetResult.Content.ReadAsStringAsync();
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = requetResult.StatusCode.ToString(),
                    Description = requetResult.ReasonPhrase
                });
        }

        protected async Task<UserManagmentServiceResponse<TResult>> SendGetRequest<TResult>(string request)
        {
            var requestResult = await HttpClient.GetAsync(request);

            var result = new UserManagmentServiceResponse<TResult>
            {
                Code = requestResult.StatusCode.ToString(),
                Description = requestResult.ReasonPhrase
            };

            var response = await requestResult.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(response))
            {
                var payload = JsonConvert.DeserializeObject<TResult>(response);
                result.Payload = payload;
            }

            return result;
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
