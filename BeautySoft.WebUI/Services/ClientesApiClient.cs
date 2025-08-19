using System.Net.Http.Json;
using BeautySoft.WebUI.Models;

namespace BeautySoft.WebUI.Services
{
    public class ClientesApiClient
    {
        private readonly HttpClient _http;
        public ClientesApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("Api");

        public Task<List<ClienteVm>?> GetAllAsync()
            => _http.GetFromJsonAsync<List<ClienteVm>>("/api/clientes");
    }
}
