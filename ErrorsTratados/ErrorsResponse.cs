using ApiFuncional.Data;
using ApiFuncional.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiFuncional.Errors
{
	public class ErrorsRespons : ControllerBase, IValidarService
	{
		private readonly ApiDbContext _context;
		private ClienteFornec _cliente;
		public ErrorsRespons( ApiDbContext context)
		{
			_context = context;			
		}

		

		public async Task<ActionResult> ValidarCliente(ClienteFornec cliente)
		{
			if (_context.Clientes == null)
				return Problem("Erro ao processar cliente, contate suporte!");

			if (cliente == null)
				return BadRequest("Um ou mais campos estão vazios. Verifique!");

			if (!ModelState.IsValid)
				return ValidationProblem(new ValidationProblemDetails(ModelState)
				{ 
					Title = "Um ou mais erros de validação ocorreram!" 
				});

			return Ok();
		}


		public async Task<ActionResult> ValidarBancoModels()
		{
			if (_context.Clientes == null)
				return NotFound("Erro, cliente não encontrado, contate suporte!");

			if (!ModelState.IsValid)
				return ValidationProblem(new ValidationProblemDetails(ModelState)
				{
					Title = "Um ou mais erros de validação ocorreram!"
				});

			return Ok();
		}



	}
}
