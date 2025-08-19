using System.Net.Http.Json;
using BeautySoft.WebUI.Models;

namespace BeautySoft.WebUI.Services
{
    public class CitasApiClient
    {
        private readonly HttpClient _http;
        public CitasApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("Api");

        public Task<List<CitaVm>?> GetAllAsync()
            => _http.GetFromJsonAsync<List<CitaVm>>("/api/citas");

        public Task<CitaVm?> GetAsync(int id)
            => _http.GetFromJsonAsync<CitaVm>($"/api/citas/{id}");

        public async Task<bool> CreateAsync(CitaVm vm)
        {
            var res = await _http.PostAsJsonAsync("/api/citas", vm);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(CitaVm vm)
        {
            var res = await _http.PutAsJsonAsync($"/api/citas/{vm.CitaId}", vm);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"/api/citas/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
