using System;
using System.ComponentModel.DataAnnotations;

namespace LivroApi.Api.Models
{
    public class Livro
    {
        public int LivroID { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Título deve ter no máximo {1} caracteres.")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "O campo Data de Publicação é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Formato inválido para a Data de Publicação.")]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo Valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo AutorID é obrigatório.")]
        public int AutorID { get; set; }
        public Autor? Autor { get; set; }

        [Required(ErrorMessage = "O campo AssuntoID é obrigatório.")]
        public int AssuntoID { get; set; }
        public Assunto? Assunto { get; set; }
        public int Id { get; internal set; }
    }
}
