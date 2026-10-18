namespace Section02;

// =====================================================================
// PART 3 - Validation with Predicate<Order>.
// A Predicate<T> is a delegate that answers a YES/NO question about T.
// =====================================================================
public static class OrderValidator
{
    // Returns true if the order satisfies the supplied rule.
    public static bool ValidateOrder(Order order, Predicate<Order> validationRule)
    {
        return validationRule(order);
    }

    // Used in Part 7: an order is valid only if ALL rules pass.
    // (LINQ All(...) replaces any if/else chain.)
    public static bool ValidateAll(Order order, IEnumerable<Predicate<Order>> rules)
    {
        return rules.All(rule => ValidateOrder(order, rule));
    }
}
