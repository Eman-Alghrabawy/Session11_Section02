namespace Section02;

// =====================================================================
// BONUS - Pricing strategies as Func<Order, decimal> lambdas.
// OrderService never sees these formulas; the caller picks one.
// =====================================================================
public static class PricingStrategies
{
    public static readonly Func<Order, decimal> Normal =
        order => order.Price * order.Quantity;

    public static readonly Func<Order, decimal> TenPercentDiscount =
        order => order.Price * order.Quantity * 0.90m;

    public static readonly Func<Order, decimal> TwentyPercentDiscount =
        order => order.Price * order.Quantity * 0.80m;

    // VIP: 30% off
    public static readonly Func<Order, decimal> Vip =
        order => order.Price * order.Quantity * 0.70m;
}
