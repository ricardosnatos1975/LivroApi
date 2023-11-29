using System;
using System.Threading.Tasks;
using AutoMapper;
using LivroApi.Api.Models;
using LivroApi.Api.Services;
using LivroApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Polly.CircuitBreaker;
using Xunit;

namespace LivroApi.Tests
{
    public class LivroServiceTests
    {
        private readonly Mock<ILivrariaContext> _contextMock = new Mock<ILivrariaContext>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<ILogger<LivroService>> _loggerMock = new Mock<ILogger<LivroService>>();

        [Fact]
        public async Task ObterLivroPorIdAsync_ShouldReturnSuccess()
        {

            var LivroId = 1;
            var LivroService = CreateLivroService();

            _contextMock.Setup(ctx => ctx.Livros.FindAsync(LivroId)).ReturnsAsync(new Livro { LivroID = LivroId });

            var result = await LivroService.ObterLivroPorIdAsync(LivroId);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ObterLivroPorIdAsync_ShouldReturnError()
        {
            var LivroId = 1;
            var LivroService = CreateLivroService();

           
            _contextMock.Setup(ctx => ctx.Livros.FindAsync(LivroId)).ReturnsAsync((Livro)null);

            
            var result = await LivroService.ObterLivroPorIdAsync(LivroId);

            
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.NotNull(result.ErrorMessage);
        }

        private LivroService CreateLivroService()
        {
            
            return new LivroService(_contextMock.Object, _mapperMock.Object, _loggerMock.Object);
        }
    }
}
