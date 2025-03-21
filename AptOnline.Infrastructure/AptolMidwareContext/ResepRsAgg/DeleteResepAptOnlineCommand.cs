using AptOnline.Api.Helpers;
using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
using AptOnline.Api.Workers;
using AptOnline.Infrastructure.LocalContext.ResepRsAgg;
using Mapster;
using MediatR;
using Newtonsoft.Json;
using Nuna.Lib.DataTypeExtension;
using System;
using System.Text.RegularExpressions;

namespace AptOnline.Api.Usecases
{
    public class DeleteResepAptolCommand : IRequest<DeleteResepAptolCommandResponse>
    {
        public string PenjualanId { get; set; }
    }
    public class DeleteResepAptolCommandResponse
    {
        public bool Success { get; set; }
        public string? PenjualanId { get; set; }
        public string? BpjsReffId { get; set; }
        public string? Message { get; set; }
    }
    public class DeleteResepAptolCommandHandler : IRequestHandler<DeleteResepAptolCommand,
        DeleteResepAptolCommandResponse>
    {
        private readonly IResepDal _resepDal;
        private readonly IDeleteResepBpjsService _deleteResepBpjsService;
        public DeleteResepAptolCommandHandler(IResepDal resepDal, 
            IDeleteResepBpjsService deleteResepBpjsService)
        {
            _resepDal = resepDal;
            _deleteResepBpjsService = deleteResepBpjsService;
        }
        public Task<DeleteResepAptolCommandResponse> Handle(DeleteResepAptolCommand request,
            CancellationToken cancellationToken)
        {
            // BUILD
            var savedResep = _resepDal.GetData(request.PenjualanId);
            if (savedResep != null)
            {
                var reqDto = new DeleteResepBpjsReqDto 
                { 
                    noresep = savedResep.PenjualanId, 
                    nosjp = savedResep.ReffId, 
                    refasalsjp = savedResep.FaskesAsal 
                };
                _ = _deleteResepBpjsService.Execute(reqDto);

                //if (resp.metaData.code == "200")
                   
            }
            return Task.FromResult(new DeleteResepAptolCommandResponse
            {
                BpjsReffId = savedResep.ReffId,
                Success = true,
                PenjualanId = savedResep.PenjualanId
            });
        }
    }
}