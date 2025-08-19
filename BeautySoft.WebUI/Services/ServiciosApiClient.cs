using BeautySoft.Domain.Entities;
using BeautySoft.WebUI.Models;
using System.Net.Http.Json;

namespace BeautySoft.WebUI.Services
{
    public class ServiciosApiClient
    {
        private readonly HttpClient _http;

        public ServiciosApiClient(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        public Task<List<ServicioVm>?> GetAllAsync()
            => _http.GetFromJsonAsync<List<ServicioVm>>("/api/servicios");

        public Task<ServicioVm?> GetAsync(int id)
            => _http.GetFromJsonAsync<ServicioVm>($"/api/servicios/{id}");

        public async Task<bool> CreateAsync(ServicioVm vm)
        {
            var res = await _http.PostAsJsonAsync("/api/servicios", vm);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ServicioVm vm)
        {
            var res = await _http.PutAsJsonAsync($"/api/servicios/{vm.ServicioId}", vm);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"/api/servicios/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
