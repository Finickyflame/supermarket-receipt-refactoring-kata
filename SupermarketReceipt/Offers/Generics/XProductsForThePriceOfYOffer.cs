namespace SupermarketReceipt;

/// <example>
/// 3 products for the price of 3.99
/// <code>
/// new XProductsForThePriceOfYOffer(product, 3, 3.99);
/// </code>
/// </example>
public record XProductsForThePriceOfYOffer(IProduct Product, int Count, double NewPrice) : IOffer
{
    public IEnumerable<Discount> GetDiscounts(IReadOnlyList<ICartItem> cartItems)
    {
        int quantity = QuantityCounter.GetQuantity(cartItems.Where(item => item.Product == this.Product));
        if (quantity >= this.Count)
        {
            int productCountRatio = quantity / this.Count;
            double total = this.NewPrice * productCountRatio + quantity % this.Count * this.Product.Price;
            double discountTotal = this.Product.Price * quantity - total;
            yield return new Discount($"{this.Count} for {this.NewPrice}({this.Product.Name})", discountTotal);
        }
        else
        {
            // This branch doesn't really make sense, how can we apply a discount of quantity on weight?! (5 apples != 5kg)
            // I left it here to keep the existing behavior...
            double weight = WeightCounter.GetWeight(cartItems.Where(item => item.Product == this.Product));
            if (weight >= this.Count)
            {
                int weightPerProduct = (int)weight / this.Count;
                double discountTotal = this.Product.Price * weight - (this.NewPrice * weightPerProduct + (int)weight % this.Count * this.Product.Price);
                yield return new Discount($"{this.Count} for {this.NewPrice}({this.Product.Name})", discountTotal);
            }
        }
    }
}