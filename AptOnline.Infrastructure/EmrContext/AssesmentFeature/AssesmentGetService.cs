using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.EmrContext.AssesmentFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EmrContext.AssesmentFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EmrContext.AssesmentFeature;

public class AssesmentGetService : IAssesmentGetService
{
    private readonly IDbConnection _conn;
    private readonly IRegGetService _regGetService;

    public AssesmentGetService(IOptions<DatabaseOptions> opt, IRegGetService regGetService)
    {
        _regGetService = regGetService;
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    public MayBe<AssesmentModel> Execute(IRegKey reg)
    {
        var register = _regGetService.Execute(reg);
        if (register is null) 
            return MayBe<AssesmentModel>.None;

        var listAssesmentConcept = ListAssesmentConcept(reg);
        if (!listAssesmentConcept.HasValue) 
            return MayBe<AssesmentModel>.None;

        var result = new AssesmentModel(register.ToRefference());
        foreach (var item in listAssesmentConcept.Value)
        {
            var concept = new ConceptType(item.ConceptId, item.ConceptName, item.Prompt);
            result.AddConcept(item.AssesmentId, item.AssesmentDate, concept, item.AssValue);
        }
        return MayBe.From(result);
    }

    private MayBe<IEnumerable<AssesmentConceptDto>> ListAssesmentConcept(IRegKey req)
    {
        const string sql = @"
            SELECT 
                aa.ConceptId, aa.Prompt, aa.AssValue,
                bb.RegId, bb.AssesmentId, bb.AssesmentDate, 
                ISNULL(cc.ConceptName,'-') ConceptName 
            FROM
                SMASS_AssesmentConcept aa
                INNER JOIN SMASS_Assesment bb ON aa.AssesmentId = bb.AssesmentId
                LEFT JOIN SMASS_Concept cc ON aa.ConceptId = cc.ConceptId
            WHERE
                bb.RegId = @regId";
        
        var dp = new DynamicParameters();
        dp.Add("@regId", req.RegId, DbType.String, ParameterDirection.Input);
        var result = _conn.Read<AssesmentConceptDto>(sql, dp);
        result = result?.Where(x => !string.IsNullOrWhiteSpace(x.AssValue)).ToList();

        return MayBe.From(result);
    }

    public record AssesmentConceptDto(string ConceptId, string Prompt, string AssValue, 
        string RegId, string AssesmentId, DateTime AssesmentDate, string ConceptName);
}