using System.ComponentModel.DataAnnotations;

public class Assunto
{
    public int AssuntoID { get; set; }

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo Descrição deve ter no máximo {1} caracteres.")]
    public string? Descricao { get; set; }
}