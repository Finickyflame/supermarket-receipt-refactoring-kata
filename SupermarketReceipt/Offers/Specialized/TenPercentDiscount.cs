namespace SupermarketReceipt;

public record TenPercentDiscount(IProduct Product): ProductPercentDiscountOffer(Product, 10);