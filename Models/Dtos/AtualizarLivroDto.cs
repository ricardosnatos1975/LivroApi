using System;
using System.ComponentModel.DataAnnotations;

namespace LivroApi.Api.Models.Dtos
{
    public class AtualizarLivroDto
    {
        [StringLength(100, ErrorMessage = "O campo Título deve ter no máximo {1} caracteres.")]
        public string? Titulo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Formato inválido para a Data de Publicação.")]
        public DateTime DataPublicacao { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O campo Valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo AutorID deve ser maior que zero.")]
        public int AutorID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo AssuntoID deve ser maior que zero.")]
        public int AssuntoID { get; set; }
    }
}
