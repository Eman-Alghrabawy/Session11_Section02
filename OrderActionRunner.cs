namespace Section02;

// =====================================================================
// PART 4 - ProcessOrder with Action<Order>.
// An Action<T> is a delegate that DOES something and returns nothing (void).
// Different behaviors can be passed WITHOUT editing this method.
// =====================================================================
public static class OrderActionRunner
{
    public static void ProcessOrder(Order order, Action<Order> action)
    {
        Console.WriteLine($"  [Runner] Processing order {order.Id}...");

        // The supplied behavior runs AFTER the order has been processed.
        action(order);
    }
}
