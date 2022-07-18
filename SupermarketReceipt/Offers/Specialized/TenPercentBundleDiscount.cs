namespace SupermarketReceipt;

public record TenPercentBundleDiscount(params Product[] Products) : BundlePercentDiscountOffer(10, Products);