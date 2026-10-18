namespace Section02;

// =====================================================================
// PART 1 - Order: plain data class (SRP: it only holds data).
// =====================================================================
public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Filled in by OrderService after the pricing delegate runs.
    // Handlers read it so they can show the final price.
    public decimal Total { get; set; }
}
