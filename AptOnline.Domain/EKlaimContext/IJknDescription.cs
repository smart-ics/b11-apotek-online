namespace AptOnline.Domain.EKlaimContext;

public interface IJknRefference
{
    IEnumerable<JknRefference> ListRefference();
}

public record JknRefference(string Value, string Description);