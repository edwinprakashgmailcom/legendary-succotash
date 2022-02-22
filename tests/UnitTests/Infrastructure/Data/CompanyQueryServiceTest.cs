using Companies.Core;
using Companies.Core.Exceptions;
using Companies.Infrastructure.Data;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Companies.UnitTests.Infrastructure.Data
{
    public class CompanyQueryServiceTest
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;

        public CompanyQueryServiceTest()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        }

        [Fact]
        public async Task NotReturnNullIfCompanyIsPresent()
        {
            var expectedId = 2;
            var expectedName = "Bob The Builder";
            var expectedDescription = "We always fix it!";
            var xmlResponse = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
                                <Data>
                                  <id>{expectedId}</id>
                                  <name>{expectedName}</name>
                                  <description>{expectedDescription}</description>
                                </Data>";

            ArrangeMockHttpResponse(HttpStatusCode.OK, xmlResponse);

            var sut = CreateSut();

            var result = await sut.GetByIdAsync(default);

            Assert.NotNull(result);
            Assert.Equal(expectedId, result?.Id);
            Assert.Equal(expectedName, result?.Name);
            Assert.Equal(expectedDescription, result?.Description);
        }

        [Fact]
        public async Task ReturnsNullIfCompanyIsNotPresent()
        {
            ArrangeMockHttpResponse(HttpStatusCode.NotFound);

            var sut = CreateSut();

            var result = await sut.GetByIdAsync(default);

            Assert.Null(result);
        }


        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.GatewayTimeout)]
        public void ThrowsExceptionOnError(HttpStatusCode statusCode)
        {
            ArrangeMockHttpResponse(statusCode);

            var sut = CreateSut();

            Assert.ThrowsAsync<FailedApiCallException>(() => sut.GetByIdAsync(default));
        }

        private CompanyQueryService CreateSut()
        {
            return new CompanyQueryService(new HttpClient(_mockHttpMessageHandler.Object), 
                Options.Create(new BaseUrlConfiguration() { CustomerXmlApiBase = "http://localhost" }));
        }

        private void ArrangeMockHttpResponse(HttpStatusCode statusCode, string content = null)
        {
            _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content ?? string.Empty)
            })
            .Verifiable();

        }
    }
}
