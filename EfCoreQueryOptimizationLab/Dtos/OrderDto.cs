namespace EfCoreQueryOptimizationLab.Dtos;

public class OrderDto
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = string.Empty;

    public int ItemCount { get; set; }

    public double TotalPaid { get; set; }

    public List<string> ProductNames { get; set; }
        = new List<string>();
}