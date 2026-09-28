namespace EfCoreQueryOptimizationLab.Models;

public class Order
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = string.Empty;

    public string InternalNotes { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; }
        = new List<Payment>();
}