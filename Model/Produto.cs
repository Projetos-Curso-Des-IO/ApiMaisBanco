using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFuncional.Model
{
	public class Produto
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string? Nome { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Range(0.01, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Preco { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public int QuantidadeEstoque { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string? Descricao { get; set; }

	}
}
