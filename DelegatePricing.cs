namespace Section02;

// =====================================================================
// PART 1 - CalculateOrderPrice using the USER-DEFINED delegate.
// The method does not know HOW the price is calculated;
// the caller decides by passing a method.
// =====================================================================
public static class DelegatePricing
{
    public static decimal CalculateOrderPrice(Order order, PriceCalculator calculator)
    {
        return calculator(order);
    }
}
