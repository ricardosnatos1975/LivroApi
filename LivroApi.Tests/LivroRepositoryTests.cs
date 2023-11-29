using System.Threading.Tasks;
using LivroApi.Api.Data.Repositories;
using LivroApi.Api.Models;
using LivroApi.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LivroApi.Tests
{
    public class LivroRepositoryTests
    {
        [Fact]
        public async Task GetLivroByIdAsync_DeveRetornarLivro_QuandoLivroExiste()
        {
            var livroId = 1;
            var mockContext = new Mock<LivrariaContext>();
            var mockLivroDbSet = new Mock<DbSet<Livro>>();
            var expectedLivro = new Livro { LivroID = livroId, Titulo = "Livro Teste" };

            mockLivroDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(expectedLivro);

            mockContext.Setup(c => c.Livros)
                .Returns(mockLivroDbSet.Object);

            var livroRepository = new LivroRepository(mockContext.Object);

            // Act
            var result = await livroRepository.ObterLivroPorIdAsync(livroId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLivro.Titulo, result.Titulo);
        }

        [Fact]
        public async Task GetLivroByIdAsync_DeveRetornarNull_QuandoLivroNaoExiste()
        {
            var livroId = 2;
            var mockContext = new Mock<LivrariaContext>();
            var mockLivroDbSet = new Mock<DbSet<Livro>>();

            mockLivroDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((Livro)null);

            mockContext.Setup(c => c.Livros)
                .Returns(mockLivroDbSet.Object);

            var livroRepository = new LivroRepository(mockContext.Object);

            var result = await livroRepository.ObterLivroPorIdAsync(livroId);

            Assert.Null(result);
        }
    }
}
