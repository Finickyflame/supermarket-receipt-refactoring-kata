namespace SupermarketReceipt;

public record BundlePercentDiscountOffer(int DiscountPercentage, params Product[] Products) : IOffer
{
    public IEnumerable<Discount> GetDiscounts(IReadOnlyList<ICartItem> cartItems)
    {
        CartItem[] eligibleCartItems = GetEligibleCartItems(cartItems, this.Products);
        if (eligibleCartItems.Length < this.Products.Length)
        {
            yield break;
        }
        string description = $"{this.DiscountPercentage}% Bundle({string.Join('+', this.Products.Select(p => p.Name))})";
        double discount = this.Products.Sum(product => product.Price) * this.DiscountPercentage / 100.0;
        int discountCount = eligibleCartItems.Min(cartItem => cartItem.Quantity);
        yield return new Discount(description, discount * discountCount);
    }

    private static CartItem[] GetEligibleCartItems(IEnumerable<ICartItem> cartItems, Product[] products)
    {
        return (from cartItem in cartItems
                let product = cartItem.Product as Product
                where product != null && products.Contains(product)
                group cartItem by product
                into items
                select new CartItem(items.Key, QuantityCounter.GetQuantity(items))).ToArray();
    }
}