namespace SupermarketReceipt;

public class ShoppingCart
{
    private readonly List<ICartItem> _items = new();

    public IEnumerable<ICartItem> Items => this._items.AsReadOnly();

    public void AddItem(Product product, int quantity = 1) => this._items.Add(new CartItem(product, quantity));
        
    public void AddItem(WeightProduct product, double weight) => this._items.Add(new WeightCartItem(product, weight));
}