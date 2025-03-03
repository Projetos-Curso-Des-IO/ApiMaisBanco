using ApiFuncional.Model;

namespace ApiFuncional.Service
{
    public class ClientesService
    {

        public async Task<ClienteFornec> BuscarDadosPorCnpjAsync(string cnpj)
        {

            string _cnpjLimpo = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            var _apiExterna = "https://www.receitaws.com.br/v1/cnpj/";
            var _url = $"{_apiExterna}{_cnpjLimpo}";

            using (var httpClient = new HttpClient())
            {

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(_url);

                    if (response.IsSuccessStatusCode)
                    {
                        var _jsonString = await response.Content.ReadAsStringAsync();

                        //Converto o json para objeto cliente
                        var _apiResponse = System.Text.Json.JsonSerializer.Deserialize<ClienteFornec>(_jsonString, new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true // Ignora diferença entre maiúsculas e minúsculas
                        });


                        if (_apiResponse != null)
                        {
                            ClienteFornec clienteFornecedoresApi = new ClienteFornec
                            {
                                Cnpj = _apiResponse.Cnpj,
                                Nome = _apiResponse.Nome,
                                Fantasia = _apiResponse.Fantasia,
                                Email = _apiResponse.Email,
                                Telefone = _apiResponse.Telefone,
                                Logradouro = _apiResponse.Logradouro,
                                Bairro = _apiResponse.Bairro,
                                Municipio = _apiResponse.Municipio,
                                Uf = _apiResponse.Uf,
                                Cep = _apiResponse.Cep
                            };

                            return clienteFornecedoresApi;
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Erro na requisição: {ex.Message}");
                    return null;
                }
            }

            return null;
        }
    }
}

