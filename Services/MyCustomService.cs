using LivroApi.Api.Models;
using LivroApi.Api.Services;
using LivroApi.Data;

public class MyCustomService : IMyCustomService
{
    private readonly LivrariaContext _dbContext;

    public MyCustomService(LivrariaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void DoSomething()
    {
        Console.WriteLine("MyCustomService está fazendo algo...");

        // Exemplo: Adicionar um novo livro ao banco de dados
        var novoLivro = new Livro
        {
            Titulo = "Novo Livro",
            DataPublicacao = DateTime.Now,
            Valor = 29.99m,
            AutorID = 1, // Supondo que o autor com ID 1 já existe no banco de dados
            AssuntoID = 1 // Supondo que o assunto com ID 1 já existe no banco de dados
        };

        _dbContext.Livros.Add(novoLivro);
        _dbContext.SaveChanges();

        // Exemplo: Recuperar todos os livros do banco de dados
        var todosLivros = _dbContext.Livros.ToList();

        // Exemplo: Imprimir os títulos dos livros recuperados
        foreach (var livro in todosLivros)
        {
            Console.WriteLine($"Livro ID: {livro.LivroID}, Título: {livro.Titulo}");
        }

        Console.WriteLine("MyCustomService concluiu a operação.");
    }

    public Task<string> ObterInformacaoAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> RealizarOperacaoComplexaAsync(string parametro1, int parametro2)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidarDadosAsync(DadosValidacao dados)
    {
        throw new NotImplementedException();
    }
}
