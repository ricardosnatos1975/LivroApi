using System.Threading.Tasks;
using LivroApi.Api.Data.Repositories;
using LivroApi.Api.Models;
using LivroApi.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AutorApi.Tests
{
    public class AutorRepositoryTests
    {
        [Fact]
        public async Task GetAutorByIdAsync_DeveRetornarAutor_QuandoAutorExiste()
        {
            var autorId = 1;
            var mockContext = new Mock<LivrariaContext>();
            var mockAutorDbSet = new Mock<DbSet<Autor>>();
            var expectedAutor = new Autor { AutorID = autorId, Nome = "Nome Teste" };

            mockAutorDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(expectedAutor);

            mockContext.Setup(c => c.Autores)
                .Returns(mockAutorDbSet.Object);

            var autorRepository = new AutorRepository(mockContext.Object);

            // Act
            var result = await autorRepository.ObterAutorPorIdAsync(autorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAutor.Nome, result.Nome);
        }

        [Fact]
        public async Task GetAutorByIdAsync_DeveRetornarNull_QuandoAutorNaoExiste()
        {
            var autorId = 2;
            var mockContext = new Mock<LivrariaContext>();
            var mockAutorDbSet = new Mock<DbSet<Autor>>();

            mockAutorDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((Autor)null);

            mockContext.Setup(c => c.Autores)
                .Returns(mockAutorDbSet.Object);

            var autorRepository = new AutorRepository(mockContext.Object);

            var result = await autorRepository.ObterAutorPorIdAsync(autorId);

            Assert.Null(result);
        }
    }
}
