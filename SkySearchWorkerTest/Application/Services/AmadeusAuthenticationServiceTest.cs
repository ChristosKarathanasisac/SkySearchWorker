using Azure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NSubstitute;
using SkySearchWorker.Application.DTOs.Amadeus.Authentication;
using SkySearchWorker.Application.Services;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Interfaces;
using SkySearchWorkerTest.Application.TestDataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorkerTest.Application.Services
{
    public class AmadeusAuthenticationServiceTest
    {
        private readonly ICustomHttpClient _mockHttpClient;
        private readonly IOptions<AppSettings> _mockAppSettings;
        private readonly ILogger<AmadeusAuthenticationService> _mockLogger;
        private readonly AppSettings _appSettings;


        public AmadeusAuthenticationServiceTest()
        {
            _mockHttpClient = Substitute.For<ICustomHttpClient>();
            _mockAppSettings = Substitute.For<IOptions<AppSettings>>();
            _mockLogger = Substitute.For<ILogger<AmadeusAuthenticationService>>();
            _appSettings = DummyTestData.GetAppSettings();
            _mockAppSettings.Value.Returns(_appSettings);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnTrue_WhenAuthenticationIsSuccessful()
        {
            // Arrange
            var response = new AuthenticationResponseDto
            {
                AccessToken = "test-access-token"
            };

            _mockHttpClient.PostUrlEncodedAsync<AuthenticationResponseDto>(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Dictionary<string, string>>())
                .Returns(response);

            var service = new AmadeusAuthenticationService(_mockLogger, _mockHttpClient, _mockAppSettings);

            // Act
            var result = await service.Authenticate();

            // Assert
            Assert.True(result);
            Assert.Equal("test-access-token", _appSettings.Credentials.AccessToken);
        }
    }

}
