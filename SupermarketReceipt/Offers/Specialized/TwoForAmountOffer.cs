namespace SupermarketReceipt;

public record TwoForAmountOffer(IProduct Product, double NewPrice) : XProductsForThePriceOfYOffer(Product, 2, NewPrice);