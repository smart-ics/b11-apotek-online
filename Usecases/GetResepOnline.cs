using MassTransit;
using MediatR;
using MyHospital.MsgContract.Pharmacy.SalesEvents;
using AptOnline.Api.Services;
using AptOnline.Api.Models;
using Mapster;
using System.Text.RegularExpressions;
using Nuna.Lib.DataTypeExtension;
using Newtonsoft.Json.Linq;

namespace AptOnline.Api.Usecases;
public class GetResepOnlineListener : IConsumer<string>
{
    public GetResepOnlineListener()
    {
    }

    public Task Consume(ConsumeContext<string> context)
    {
        var x = context.Message;
        // BUILD
        //if (context.Headers.TryGetHeader("ConsumerType", out var headerValue) && headerValue?.ToString() == "TypeA")
        //{
        //    var x = headerValue.ToString();
        //    //_logger.LogInformation(" [NameConsumer] Message received context: {conetxt} ", JsonSerializer.Serialize(context));
        //}
        //return Task.CompletedTask;

        // RETURN
        return Task.FromResult(Unit.Value);
    }

}