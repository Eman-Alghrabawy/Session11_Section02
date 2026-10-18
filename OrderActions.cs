namespace Section02;

// =====================================================================
// PART 4 - Ready-made Action<Order> behaviors, written as lambdas.
// Each one is a different behavior for the SAME ProcessOrder method.
// =====================================================================
public static class OrderActions
{
    // 1) Print order information
    public static readonly Action<Order> PrintOrder = order =>
    {
        Console.WriteLine($"  [Action] Order {order.Id} for {order.CustomerName} processed.");
    };

    // 2) Send a confirmation message
    public static readonly Action<Order> SendConfirmation = order =>
    {
        Console.WriteLine($"  [Action] Confirmation sent to {order.CustomerName} for order {order.Id}.");
    };

    // 3) Write an audit message
    public static readonly Action<Order> WriteAudit = order =>
    {
        Console.WriteLine($"  [Action] AUDIT: order {order.Id} processed at {DateTime.Now:HH:mm:ss}.");
    };
}
