using System;
using System.Threading.Tasks;
using AutoMapper;
using LivroApi.Api.Services;
using LivroApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Polly.CircuitBreaker;
using Xunit;

namespace LivroApi.Tests
{
    public class AutorServiceTests
    {
        private readonly Mock<ILivrariaContext> _contextMock = new Mock<ILivrariaContext>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<ILogger<AutorService>> _loggerMock = new Mock<ILogger<AutorService>>();

        [Fact]
        public async Task ObterAutorPorIdAsync_ShouldReturnSuccess()
        {

            var autorId = 1;
            var autorService = CreateAutorService();

            _contextMock.Setup(ctx => ctx.Autores.FindAsync(autorId)).ReturnsAsync(new Autor { AutorID = autorId });

            var result = await autorService.ObterAutorPorIdAsync(autorId);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ObterAutorPorIdAsync_ShouldReturnError()
        {
            var autorId = 1;
            var autorService = CreateAutorService();

           
            _contextMock.Setup(ctx => ctx.Autores.FindAsync(autorId)).ReturnsAsync((Autor)null);

            
            var result = await autorService.ObterAutorPorIdAsync(autorId);

            
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.NotNull(result.ErrorMessage);
        }

        private AutorService CreateAutorService()
        {
            
            return new AutorService(_contextMock.Object, _mapperMock.Object, _loggerMock.Object);
        }
    }
}
