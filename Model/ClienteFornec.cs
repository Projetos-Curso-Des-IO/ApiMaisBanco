using System.ComponentModel.DataAnnotations;

namespace ApiFuncional.Model
{
	public class ClienteFornec
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Cnpj { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Fantasia { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Email { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Telefone { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Logradouro { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Bairro { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Municipio { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Uf { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Cep { get; set; }
	}
}
