namespace SupermarketReceipt;

public class Teller
{
    private readonly ISupermarketCatalog _catalog;

    public Teller(ISupermarketCatalog catalog)
    {
        this._catalog = catalog;
    }

    public Receipt ChecksOutArticlesFrom(ShoppingCart cart)
    {
        var transaction = new Transaction();
        foreach (ICartItem item in cart.Items)
        {
            transaction.Add(item);
        }
        transaction.ApplyOffers(this._catalog.Offers);
        return transaction.CreateReceipt();
    }


    private sealed class Transaction : ICartItemHandler
    {
        private readonly Receipt _receipt = new();
        private readonly List<ICartItem> _items = new();

        public void Add(ICartItem item)
        {
            this._items.Add(item);
            item.Accept(this);
        }

        public void Handle(CartItem item)
        {
            this._receipt.AddProduct(item.Product, item.Quantity);
        }

        public void Handle(WeightCartItem item)
        {
            this._receipt.AddProduct(item.Product, item.Weight);
        }

        public Receipt CreateReceipt() => this._receipt;

        public void ApplyOffers(IEnumerable<IOffer> offers)
        {
            foreach (Discount discount in offers.SelectMany(offer => offer.GetDiscounts(this._items)))
            {
                this._receipt.AddDiscount(discount);
            }
        }
    }
}
