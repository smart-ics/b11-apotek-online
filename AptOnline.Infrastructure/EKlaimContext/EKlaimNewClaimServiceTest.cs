using System.Net;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Infrastructure.EKlaimContext.Shared;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext;

public class EKlaimNewClaimServiceTests
{
    private static EKlaimModel EklaimModelFaker()
        => EKlaimModel.Default;
    private static IOptions<EKlaimOptions> EklaimOptionsFaker()
    => Options.Create(new EKlaimOptions
    {
        Debug = "0",
        ApiKey = "dummy-key",
        BaseApiUrl = "http://fakeapi.com"
    });
    
    [Fact]
    public void UT1_GivenValidResponse_WhenExecute_ShouldReturnSuccess()
    {
        // Arrange
        var expectedJson = JsonConvert.SerializeObject(new EKlaimNewClaimResponseEnvelope
        {
            metadata = new EKlaimResponseMeta { code = "200", message = "OK" },
            response = new EKlaimNewClaimResponseDto { admission_id = "ADM123" }
        });

        var mockClient = new Mock<RestClient>("http://fakeapi.com") { CallBase = true };
        var mockResponse = new RestResponse
        {
            StatusCode = HttpStatusCode.OK,
            Content = expectedJson
        };
        mockClient.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(mockResponse);
        var mockFactory = new Mock<IRestClientFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockClient.Object);

        var sut = new EKlaimNewClaimService(EklaimOptionsFaker(), mockFactory.Object);
        var req = EklaimModelFaker();
        
        // Act
        var result = sut.Execute((EKlaimModel)req);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().Be("ADM123");
    }
    
    [Fact]
    public void UT2_GivenHttp400Response_WhenExecute_ShouldReturnSuccess()
    {
        // Arrange
        var expectedJson = JsonConvert.SerializeObject(new EKlaimNewClaimResponseEnvelope
        {
            metadata = new EKlaimResponseMeta { code = "400", message = "Bad Request" },
            response = new EKlaimNewClaimResponseDto { admission_id = "ADM123" }
        });

        var mockClient = new Mock<RestClient>("http://fakeapi.com") { CallBase = true };
        var mockResponse = new RestResponse
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = expectedJson
        };
        mockClient.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(mockResponse);
        var mockFactory = new Mock<IRestClientFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockClient.Object);

        var sut = new EKlaimNewClaimService(EklaimOptionsFaker(), mockFactory.Object);
        var req = EklaimModelFaker();
        
        // Act
        var result = sut.Execute((EKlaimModel)req);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Unknown HTTP error");
    }
}
