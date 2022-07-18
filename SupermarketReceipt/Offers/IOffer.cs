namespace SupermarketReceipt;

public interface IOffer
{
    IEnumerable<Discount> GetDiscounts(IReadOnlyList<ICartItem> cartItems);
}