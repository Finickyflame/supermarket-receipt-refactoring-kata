namespace SupermarketReceipt;

/// <example>
/// product is 10% off
/// <code>
/// new ProductPercentDiscountOffer(product, 10);
/// </code>
/// </example> 
public record ProductPercentDiscountOffer(IProduct Product, int DiscountPercentage) : IOffer
{
    public IEnumerable<Discount> GetDiscounts(IReadOnlyList<ICartItem> cartItems)
    {
        double sum = cartItems.Where(item => item.Product == this.Product).Sum(item => item.Product.Price);
        yield return new Discount($"{this.DiscountPercentage}% off({this.Product.Name})", sum * this.DiscountPercentage / 100.0);
    }
}