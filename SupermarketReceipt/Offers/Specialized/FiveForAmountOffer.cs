namespace SupermarketReceipt;

public record FiveForAmountOffer(IProduct Product, double NewPrice) : XProductsForThePriceOfYOffer(Product, 5, NewPrice);