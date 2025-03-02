using ApiFuncional.Data;
using ApiFuncional.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFuncional.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/clientes")]
    public class ClientFornecController : ControllerBase
    {

        private readonly ApiDbContext _context;
		public ClientFornecController(ApiDbContext context)
		{
            _context = context;
		}


		[HttpGet]
		[ProducesResponseType(typeof(ClienteFornec), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<IEnumerable<ClienteFornec>>> GetClientes()
        {
			if (_context.Clientes == null) return NotFound("Cliente não encontrado com esse CNPJ");

			return await _context.Clientes.ToListAsync();
        }




		[HttpGet("{cnpj}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<ClienteFornec>> GetClienteCnpj(string cnpj)
		{
			string cnpjLimpo = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
			var apiExterna = "https://www.receitaws.com.br/v1/cnpj/";
			var url = $"{apiExterna}{cnpjLimpo}";

			using (var httpClient = new HttpClient())
			{
				HttpResponseMessage response = await httpClient.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var jsonString = await response.Content.ReadAsStringAsync();

					//Converto o json para objeto cliente
					var apiResponse = System.Text.Json.JsonSerializer.Deserialize<ClienteFornec>(jsonString, new System.Text.Json.JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true // Ignora diferença entre maiúsculas e minúsculas
					});


					if (apiResponse != null)
					{
						ClienteFornec clienteFornecedoresApi = new ClienteFornec
						{
							Cnpj = apiResponse.Cnpj,
							Nome = apiResponse.Nome,
							Fantasia = apiResponse.Fantasia,
							Email = apiResponse.Email,
							Telefone = apiResponse.Telefone,
							Logradouro = apiResponse.Logradouro,
							Bairro = apiResponse.Bairro,
							Municipio = apiResponse.Municipio,
							Uf = apiResponse.Uf,
							Cep = apiResponse.Cep
						};

						return Ok(clienteFornecedoresApi);
					}
				}
				else
				{
					return NotFound("CNPJ não encontrado na API externa.");
				}
			}
			return NotFound("CNPJ não encontrado na API externa.");
		}

	}
}
