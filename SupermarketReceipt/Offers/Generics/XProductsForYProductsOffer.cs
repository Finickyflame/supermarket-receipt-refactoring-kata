namespace SupermarketReceipt;

/// <example>
/// 3 products for the price of 2 products
/// <code>
/// new XForThePriceOfYOffer(product, 3, 2);
/// </code>
/// </example>
public record XProductsForYProductsOffer(IProduct Product, int Dividend, int Divisor) : IOffer
{
    public IEnumerable<Discount> GetDiscounts(IReadOnlyList<ICartItem> cartItems)
    {
        int quantity = QuantityCounter.GetQuantity(cartItems);
        if (quantity >= this.Dividend)
        {
            double discountAmount = quantity * this.Product.Price - (quantity / this.Dividend *  this.Divisor * this.Product.Price + quantity % this.Dividend * this.Product.Price);
            yield return new Discount($"{this.Dividend} for {this.Divisor}({this.Product.Name})", discountAmount);
        }
    }
}