using System;
using System.ComponentModel.DataAnnotations;

namespace LivroApi.Api.Models.Dtos
{
    public class CriarLivroDto
    {
        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Título deve ter no máximo {1} caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Data de Publicação é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Formato inválido para a Data de Publicação.")]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo Valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo AutorID é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo AutorID deve ser maior que zero.")]
        public int AutorID { get; set; }

        [Required(ErrorMessage = "O campo AssuntoID é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo AssuntoID deve ser maior que zero.")]
        public int AssuntoID { get; set; }
    }
}

