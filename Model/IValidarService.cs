using Microsoft.AspNetCore.Mvc;

namespace ApiFuncional.Model
{
	public interface IValidarService
	{
		Task<ActionResult> ValidarCliente(ClienteFornec cliente);
	}
}
