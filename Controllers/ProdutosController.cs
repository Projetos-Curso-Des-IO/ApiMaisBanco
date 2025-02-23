using ApiFuncional.Data;
using ApiFuncional.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiFuncional.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly ApiDbContext _context;

		public ProdutosController(ApiDbContext context)
		{
			_context = context;
		}



		[HttpGet]
		[ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
		{
			return await _context.Produtos.ToListAsync();
		}


		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Produto>> GetProduto(int id)
		{
			var produto = await _context.Produtos.FindAsync(id);
			if (produto == null) return NotFound();
			
			return produto;
		}



		[HttpPost]
		[ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(Produto), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Produto>> PostProduto(Produto produto)
		{
			if (produto == null) return BadRequest();
	
			_context.Produtos.Add(produto);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetProduto), new {id=produto.Id}, produto);
		}



		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(Produto), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Produto>> PutProduto(int id, Produto produto)
		{
			if (id != produto.Id) return ValidationProblem(ModelState);
			var produtoPesquisado = await _context.Produtos.FindAsync(id);
			if (produtoPesquisado == null) return BadRequest();

			//_context.Produtos.Update(produto);
			_context.Entry(produtoPesquisado).CurrentValues.SetValues(produto);
			await _context.SaveChangesAsync();

			return NoContent();
		}




		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Produto>> DeleteProduto(int id)
		{
			var produto = await _context.Produtos.FindAsync(id);
			if (produto==null) return NotFound();

			_context.Produtos.Remove(produto);
			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}
