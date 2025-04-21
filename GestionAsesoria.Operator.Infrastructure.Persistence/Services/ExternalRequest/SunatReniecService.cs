using GestionAsesoria.Operator.Application.DTOs.SunatReniec.Response;
using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Shared.Wrapper;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Services.ExternalRequest
{
    public class SunatReniecService : ISunatReniecService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token = "apis-token-12450.ELtQRN0umaDF8XYopQ41KNXFPCL8fYoF";
        public SunatReniecService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<SunatReniecResponse>> GetCompanyData(string documentType, string documentNumber)
        {
            try
            {
                string entidad = documentType.ToLower() switch
                {
                    "ruc" => "sunat",
                    "dni" => "reniec",
                    _ => throw new ArgumentException("Entidad no soportada.")
                };

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                var response = await _httpClient.GetAsync($"https://api.apis.net.pe/v2/{entidad}/{documentType}?numero={documentNumber}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SunatReniecResponse>(content);
                return Result<SunatReniecResponse>.Success(data);
            }
            catch (HttpRequestException ex)
            {
                return Result<SunatReniecResponse>.Fail($"Error de red: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<SunatReniecResponse>.Fail($"Error: {ex.Message}");
            }
        }
    }
}
