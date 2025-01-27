namespace CarCheckup.Domain.Core.Entities.Configs;

public class SiteSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = null!;
    public Limitation Limitation { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
}
