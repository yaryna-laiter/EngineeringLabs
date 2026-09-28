namespace EfCoreQueryOptimizationLab.Models;

public class Payment
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public DateTime PaidAt { get; set; }

    public string Method { get; set; } = string.Empty;

    public string ProviderReference { get; set; } = string.Empty;

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}