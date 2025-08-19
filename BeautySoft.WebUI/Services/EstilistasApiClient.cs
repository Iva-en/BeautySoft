using System.Net.Http.Json;
using BeautySoft.WebUI.Models;

namespace BeautySoft.WebUI.Services
{
    public class EstilistasApiClient
    {
        private readonly HttpClient _http;
        public EstilistasApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("Api");

        public Task<List<EstilistaVm>?> GetAllAsync()
            => _http.GetFromJsonAsync<List<EstilistaVm>>("/api/estilistas");
    }
}
