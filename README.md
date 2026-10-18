# Section 02 - Delegates & Events: Answers

## Part 2 question - What problem does `Func<>` solve?
Without it, every method shape needs its own custom `delegate` declaration
(`PriceCalculator`, `TaxCalculator`, ...). `Func<Order, decimal>` already
describes "takes an Order, returns a decimal", so no new type is needed. Less
code, and any code that uses `Func` is compatible with the rest of .NET.

## Part 3 question - Why is `Predicate<Order>` more expressive than `Func<Order, bool>`?
Both have the same shape, but the name says the intent: *"this is a rule that
tests an Order"*. A reader sees `Predicate<Order> validationRule` and
immediately knows it is a yes/no condition. `Func<Order, bool>` could be
anything that happens to return a bool.

## Q1 - `PriceCalculator` vs `Func<Order, decimal>`
Same signature. `PriceCalculator` is a custom named delegate type (more
descriptive name, but must be declared and is a different type). `Func<Order, decimal>`
is built in. They are NOT interchangeable without conversion, even though
they look the same.

## Q2 - `Action<Order>` vs `Func<Order, decimal>`
`Action<Order>` returns nothing (void): it performs a side effect (print, log,
send). `Func<Order, decimal>` returns a decimal: it computes a value.

## Q3 - Why does `Predicate<T>` return `bool`?
It models a condition/test: "does this T satisfy a rule?" The answer is yes
or no. Used for validation, filtering and searching (e.g. `List<T>.FindAll`).

## Q4 - Delegate vs event
A delegate is a type that holds references to methods. An event is a member
declared with a delegate type that restricts access: outside code can only
subscribe (`+=`) and unsubscribe (`-=`).

## Q5 - Why can't external code invoke an event of another class?
The compiler only lets the declaring class raise the event. Otherwise any
class could fake "order processed" notifications and break the publisher's
control over when the event fires.

## Q6 - Multiple handlers on one event
The event's invocation list holds all of them (multicast delegate). When
raised, they run one after another in the order they subscribed, all receiving
the same argument.

## Q7 - `orderService.OrderProcessed += HandleOrderProcessed;`
- `orderService.OrderProcessed` : the event on that object (the publisher)
- `+`/`=` (`+=`) : add to the invocation list; it does not replace existing handlers
- `HandleOrderProcessed` : the method to call when the event is raised; its
  signature must match `Action<Order>`

## Q8 - `Action<Order>` vs `event Action<Order>`
A public `Action<Order>` field/property can be called by anyone, overwritten
with `=` (wiping other subscribers) or set to null. With `event`, outside code
can only `+=` and `-=`; only the owning class can invoke it. This protects
encapsulation.

## Bonus
`OrderService` takes `Func<Order, decimal>` in its constructor. See
`PricingStrategies.cs` (Normal, 10%, 20%, VIP) and the BONUS block in `Program.cs`.
`OrderService` never changes when a new strategy is added.
