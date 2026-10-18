namespace Section02;

// =====================================================================
// PART 5 - OrderService with an EVENT.
// BONUS   - It receives the pricing behavior through a Func delegate,
//           so it never knows HOW the price is calculated.
//
// Responsibility: process an order and announce that it is done.
// It does NOT print, email, or audit - subscribers do that.
// =====================================================================
public class OrderService
{
    private readonly Func<Order, decimal> _pricingStrategy;

    // BONUS: behavior is injected through the constructor.
    public OrderService(Func<Order, decimal> pricingStrategy)
    {
        _pricingStrategy = pricingStrategy;
    }

    // PART 5: the event. Outside code can only += and -=.
    // It cannot call it or replace it (see README Q5 / Q8).
    public event Action<Order>? OrderProcessed;

    public void ProcessOrder(Order order)
    {
        // Calculate price using the supplied strategy (no if/else needed).
        order.Total = _pricingStrategy(order);

        // Raise the event. '?.' means: only if someone is subscribed.
        // All subscribers run in the order they subscribed (multicast).
        OrderProcessed?.Invoke(order);
    }
}
