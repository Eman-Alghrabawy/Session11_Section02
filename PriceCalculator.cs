namespace Section02;

// =====================================================================
// PART 1 - USER-DEFINED DELEGATE
// A delegate is a type-safe "pointer to a method".
// This one describes any method that takes an Order and returns a decimal.
// =====================================================================
public delegate decimal PriceCalculator(Order order);
