namespace Section02;

// =====================================================================
// PARTS 5, 6, 7 - Event handlers (subscribers of OrderProcessed).
// They are NAMED methods (not lambdas) because we need to be able to
// unsubscribe them later with -=.
// Signature must match Action<Order>: void Method(Order order).
// =====================================================================
public static class OrderHandlers
{
    // Handler1: print a message
    public static void Handler1(Order order)
    {
        Console.WriteLine($"  [Handler1] Order {order.Id} completed. Total = {order.Total:C}");
    }

    // Handler2: send a notification
    public static void Handler2(Order order)
    {
        Console.WriteLine($"  [Handler2] Notification sent to {order.CustomerName}.");
    }

    // Handler3: write an audit message
    public static void Handler3(Order order)
    {
        Console.WriteLine($"  [Handler3] AUDIT: order {order.Id} recorded.");
    }
}
