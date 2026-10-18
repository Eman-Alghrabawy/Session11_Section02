namespace Section02;

// =====================================================================
// PART 1 - Named methods that MATCH the PriceCalculator delegate
// (take an Order, return a decimal), so they can be passed as delegates.
// =====================================================================
public static class PricingMethods
{
    // Price x Quantity
    public static decimal CalculateTotal(Order order)
    {
        return order.Price * order.Quantity;
    }

    // Price x Quantity - discount (here: a 10% discount)
    public static decimal CalculateTotalWithDiscount(Order order)
    {
        decimal total = order.Price * order.Quantity;
        decimal discount = total * 0.10m;
        return total - discount;
    }
}
