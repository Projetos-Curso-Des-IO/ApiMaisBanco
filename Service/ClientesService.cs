using ApiFuncional.Data;
using ApiFuncional.Errors;
using ApiFuncional.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;
using System.Web;

namespace ApiFuncional.Service
{
    public class ClientesService
    {
		private readonly ApiDbContext _context;
        private readonly ErrorsRespons _errors;

		public ClientesService(ErrorsRespons erros, ApiDbContext context)
		{
            _context = context;
            _errors = erros;
		}

		public async Task<ClienteFornec> BuscarDadosPorCnpjAsync(string cnpj)
        {

            string _cnpjLimpo = Regex.Replace(HttpUtility.UrlDecode(cnpj), @"[^0-9]", "");

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
                        var _apiResponse = System.Text.Json.JsonSerializer.Deserialize<ClienteFornec>
                                           (_jsonString, new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true // Ignora diferença entre maiúsculas e minúsculas
                        });


                        if (_apiResponse != null &&
                            !string.IsNullOrEmpty(_apiResponse.Cnpj) &&
                            !string.IsNullOrEmpty(_apiResponse.Nome))
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







        public async Task<ClienteFornec> CriarCliente(ClienteFornec cliente)
        {
			var clientePesquisado = await _context.Clientes.FindAsync(cliente.Cnpj);           
			if (clientePesquisado != null) 
            {
				throw new BadRequestException("Já existe um cliente cadastrado com esse CNPJ.");
			}			
			if (cliente != null)
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
       
            return null;
        }





		public async Task<IEnumerable<ClienteFornec>> ListarCliente()
		{
            var errorResponse = _errors.ValidarBancoModels();
            if (errorResponse != null)
            {                
                return await _context.Clientes.ToListAsync();
            }

            return null;
		}




        
        public async Task<ActionResult<ClienteFornec>> BuscarClienteById(int id)
        {
            if (id < 1 || id == null)
            {
                throw new NotFoundException("ID inexistente!");
			}
			if(_context.Clientes == null) throw new NotFoundException("Erro, entre em contato com suporte.");

			var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
                return cliente;
            
            return null;
        }









	}
}

