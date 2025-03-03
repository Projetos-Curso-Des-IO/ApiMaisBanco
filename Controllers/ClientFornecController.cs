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
        private readonly Service.ClientesService service = new Service.ClientesService();
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
			var _dadosDoclientes =  await service.BuscarDadosPorCnpjAsync(cnpj);

			if (_dadosDoclientes != null)
			{
				return Ok(_dadosDoclientes);
			}
			else
			{
                return NotFound("CNPJ não encontrado na API externa.");
            }
		}

	}
}
