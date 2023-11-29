using System.Threading.Tasks;
using LivroApi.Api.Data.Repositories;
using LivroApi.Api.Models;
using LivroApi.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AutorApi.Tests
{
    public class AssuntoRepositoryTests
    {
        [Fact]
        public async Task GetAssuntoByIdAsync_DeveRetornarAssunto_QuandoAssuntoExiste()
        {
            var assuntoId = 1;
            var mockContext = new Mock<LivrariaContext>();
            var mockAssuntoDbSet = new Mock<DbSet<Assunto>>();
            var expectedAssunto = new Assunto { AssuntoID = assuntoId, Descricao = "Descrição Teste" };

            mockAssuntoDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(expectedAssunto);

            mockContext.Setup(c => c.Assuntos)
                .Returns(mockAssuntoDbSet.Object);

            var assuntoRepository = new AssuntoRepository(mockContext.Object);

            // Act
            var result = await assuntoRepository.ObterAssuntoPorIdAsync(assuntoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssunto.Descricao, result.Descricao);
        }

        [Fact]
        public async Task GetAssuntoByIdAsync_DeveRetornarNull_QuandoAssuntoNaoExiste()
        {
            var assuntoId = 2;
            var mockContext = new Mock<LivrariaContext>();
            var mockAssuntoDbSet = new Mock<DbSet<Assunto>>();

            mockAssuntoDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((Assunto)null);

            mockContext.Setup(c => c.Assuntos)
                .Returns(mockAssuntoDbSet.Object);

            var assuntoRepository = new AssuntoRepository(mockContext.Object);

            var result = await assuntoRepository.ObterAssuntoPorIdAsync(assuntoId);

            Assert.Null(result);
        }
    }
}
