namespace Section02;

// =====================================================================
// PART 2 - Same idea as Part 1, but with the BUILT-IN Func<Order, decimal>.
// No custom delegate type needed: Func<input, output> already exists.
// (Kept in its own class so lambdas are not ambiguous with the
//  PriceCalculator overload from Part 1.)
// =====================================================================
public static class FuncPricing
{
    public static decimal CalculateOrderPrice(Order order, Func<Order, decimal> calculator)
    {
        return calculator(order);
    }
}
