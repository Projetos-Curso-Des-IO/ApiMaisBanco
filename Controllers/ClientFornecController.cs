using ApiFuncional.Data;
using ApiFuncional.Errors;
using ApiFuncional.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace ApiFuncional.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/clientes")]
    public class ClientFornecController : ControllerBase
    {

		private readonly ErrorsRespons _erros;
		private readonly Service.ClientesService _service;

		public ClientFornecController(ErrorsRespons errorsRespons, Service.ClientesService service)
		{
			_service = service;
			_erros = errorsRespons;
		}




		[HttpGet]
		[ProducesResponseType(typeof(ClienteFornec), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<IEnumerable<ClienteFornec>>> GetClientes()
        {
			try
			{
				var _listaDeClientes = await _service.ListarCliente();
				if (_listaDeClientes != null && _listaDeClientes.Any())
				{
					return Ok(_listaDeClientes);
				}

				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Mensagem = "Ocorreu um erro interno ao processar a requisição.",
					Detalhes = ex.Message
				});
			}
		}






		[AllowAnonymous]
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Produto>> GetCliente(int id)
		{
			try
			{
				var cliente = await _service.BuscarClienteById(id);
				if (cliente == null)
					return NotFound("Cliente não encontrado!");

				return Ok(cliente);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Mensagem = "Ocorreu um erro interno ao processar a requisição.",
					Detalhes = ex.Message
				});
			}
		}







		[HttpGet("{cnpj}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<ClienteFornec>> GetClienteCnpj(string cnpj)
		{
			try
			{
				var _dadosDoclientes = await _service.BuscarDadosPorCnpjAsync(cnpj);
				if (_dadosDoclientes != null)
				{
					return Ok(_dadosDoclientes);
				}
				else
				{
					return NotFound("CNPJ não encontrado na API externa.");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Mensagem = "Ocorreu um erro interno ao processar a requisição.",
					Detalhes = ex.Message
				});
			}
		}




		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<ClienteFornec>> PostCliente(ClienteFornec cliente)
		{

			try
			{
				var _errorsRespons = _erros.ValidarCliente(cliente);

				if (_errorsRespons.Result is ObjectResult result &&
					result?.StatusCode.HasValue == true &&
					result.StatusCode != StatusCodes.Status200OK)
				{
					return BadRequest(result.Value);
				}
	
				var _dadosDoCliente = await _service.CriarCliente(cliente);
				if (_dadosDoCliente != null)
				{
					return Created("", cliente);
				}

				return Problem("Erro ao salvar o cliente no banco de dados!");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Mensangem = "Erro interno identificado!",
					Detalhes = ex.Message
				});
			}
		}
	}
}
