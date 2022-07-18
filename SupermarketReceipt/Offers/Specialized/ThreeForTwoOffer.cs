namespace SupermarketReceipt;

public record ThreeForTwoOffer(IProduct Product):XProductsForYProductsOffer(Product, 3, 2);