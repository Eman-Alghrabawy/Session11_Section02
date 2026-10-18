using Section02;

// Helper to create a fresh order for each demo
static Order NewOrder() => new Order
{
    Id = 1,
    CustomerName = "Eman",
    Price = 100m,
    Quantity = 3
};

// =====================================================================
// PART 1 - USER-DEFINED DELEGATE (PriceCalculator)
// Pass NAMED methods that match the delegate signature.
// =====================================================================
Console.WriteLine("=== PART 1: User-defined delegate ===");
Order order = NewOrder();
Console.WriteLine($"Total:               {DelegatePricing.CalculateOrderPrice(order, PricingMethods.CalculateTotal)}");
Console.WriteLine($"Total with discount: {DelegatePricing.CalculateOrderPrice(order, PricingMethods.CalculateTotalWithDiscount)}");

// =====================================================================
// PART 2 - Func<Order, decimal> with LAMBDAS
// No custom delegate needed and no named methods needed.
// =====================================================================
Console.WriteLine("\n=== PART 2: Func<Order, decimal> ===");
Console.WriteLine($"Normal:      {FuncPricing.CalculateOrderPrice(order, x => x.Price * x.Quantity)}");
Console.WriteLine($"15% off:     {FuncPricing.CalculateOrderPrice(order, x => x.Price * x.Quantity * 0.85m)}");

// =====================================================================
// PART 3 - Predicate<Order> validation rules (lambdas)
// =====================================================================
Console.WriteLine("\n=== PART 3: Predicate<Order> ===");
Predicate<Order> quantityRule = o => o.Quantity > 0;
Predicate<Order> priceRule    = o => o.Price > 0;
Predicate<Order> customerRule = o => !string.IsNullOrWhiteSpace(o.CustomerName);

Console.WriteLine($"Quantity > 0:      {OrderValidator.ValidateOrder(order, quantityRule)}");
Console.WriteLine($"Price > 0:         {OrderValidator.ValidateOrder(order, priceRule)}");
Console.WriteLine($"Has customer name: {OrderValidator.ValidateOrder(order, customerRule)}");

// An invalid order shows the rules really work
Order badOrder = new Order { Id = 99, CustomerName = "", Price = 10m, Quantity = 0 };
Console.WriteLine($"Bad order, Quantity > 0: {OrderValidator.ValidateOrder(badOrder, quantityRule)}");

// =====================================================================
// PART 4 - Action<Order>: pass different behaviors to the SAME method
// =====================================================================
Console.WriteLine("\n=== PART 4: Action<Order> ===");
OrderActionRunner.ProcessOrder(order, OrderActions.PrintOrder);
OrderActionRunner.ProcessOrder(order, OrderActions.SendConfirmation);
OrderActionRunner.ProcessOrder(order, OrderActions.WriteAudit);

// Multicast delegate: combine several actions with + and run them together
Console.WriteLine("  -- multicast Action (3 behaviors in one) --");
Action<Order> allActions = OrderActions.PrintOrder + OrderActions.SendConfirmation + OrderActions.WriteAudit;
OrderActionRunner.ProcessOrder(order, allActions);

// =====================================================================
// PART 5 - EVENTS: subscribe multiple handlers, then process
// =====================================================================
Console.WriteLine("\n=== PART 5: Events ===");
OrderService service = new OrderService(PricingStrategies.Normal);

service.OrderProcessed += OrderHandlers.Handler1;
service.OrderProcessed += OrderHandlers.Handler2;
service.OrderProcessed += OrderHandlers.Handler3;

service.ProcessOrder(NewOrder());   // all 3 handlers run

// =====================================================================
// PART 6 - UNSUBSCRIBING with -=
// =====================================================================
Console.WriteLine("\n=== PART 6: Unsubscribe Handler1 ===");
service.OrderProcessed -= OrderHandlers.Handler1;

service.ProcessOrder(NewOrder());   // only Handler2 and Handler3 run now
Console.WriteLine("(Handler1 did not run above - unsubscribe works)");

// =====================================================================
// PART 7 - FINAL APPLICATION
// Order -> Validate -> Calculate Price -> Process -> OrderProcessed -> Handlers
// =====================================================================
Console.WriteLine("\n=== PART 7: Final application ===");
List<Predicate<Order>> rules = new() { quantityRule, priceRule, customerRule };

OrderService finalService = new OrderService(PricingStrategies.TenPercentDiscount);
finalService.OrderProcessed += OrderHandlers.Handler1;   // multicast: 3 subscribers
finalService.OrderProcessed += OrderHandlers.Handler2;
finalService.OrderProcessed += OrderHandlers.Handler3;

foreach (Order o in new[] { NewOrder(), badOrder })
{
    // Step 1: validate (every rule must pass)
    bool isValid = OrderValidator.ValidateAll(o, rules);
    Console.WriteLine($"Order {o.Id} valid? {isValid}");

    // Steps 2-4: price + process + raise event. Only for valid orders.
    // (This guard only decides WHETHER to process, not which behavior to run.)
    if (isValid)
    {
        finalService.ProcessOrder(o);
    }
}

// =====================================================================
// BONUS - Pricing strategy chosen by the CALLER at runtime.
// OrderService is never modified: it just receives a Func<Order, decimal>.
// A dictionary lets us loop over strategies without any if/else.
// =====================================================================
Console.WriteLine("\n=== BONUS: Pricing strategies ===");
Dictionary<string, Func<Order, decimal>> strategies = new()
{
    ["Normal price"] = PricingStrategies.Normal,
    ["10% discount"] = PricingStrategies.TenPercentDiscount,
    ["20% discount"] = PricingStrategies.TwentyPercentDiscount,
    ["VIP discount"] = PricingStrategies.Vip,
};

foreach (KeyValuePair<string, Func<Order, decimal>> strategy in strategies)
{
    OrderService svc = new OrderService(strategy.Value);
    svc.OrderProcessed += o => Console.WriteLine($"  {strategy.Key,-13} -> total = {o.Total:C}");  // lambda subscriber
    svc.ProcessOrder(NewOrder());
}
