using System.ComponentModel.DataAnnotations;
public class Autor
{
    public int AutorID { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo {1} caracteres.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de e-mail válido.")]
    public string? Email { get; set; }
}