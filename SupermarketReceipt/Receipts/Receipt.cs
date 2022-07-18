namespace SupermarketReceipt;

public class Receipt
{
    private readonly List<Discount> _discounts = new();
    private readonly ReceiptItems _items = new();
    private readonly IAddReceiptItemStrategy _strategy;

    public Receipt(ReceiptItemsBehavior behavior = ReceiptItemsBehavior.Combined)
    {
        this._strategy = GetStrategy(behavior);
    }

    public IEnumerable<IReceiptItem> Items => this._items;

    public IEnumerable<Discount> Discounts => this._discounts.AsReadOnly();
    
    public double TotalPrice => this.Items.Sum(item => item.TotalPrice) - this.Discounts.Sum(discount => discount.Amount);

    public void AddProduct(Product product, int quantity)
    {
        this._strategy.AddReceiptItem(this._items, new ReceiptItem(product, quantity));
    }

    public void AddProduct(WeightProduct product, double weight)
    {
        this._strategy.AddReceiptItem(this._items, new WeightReceiptItem(product, weight));
    }

    public void AddDiscount(Discount discount) => this._discounts.Add(discount);

    private static IAddReceiptItemStrategy GetStrategy(ReceiptItemsBehavior behavior) => behavior switch
    {
        ReceiptItemsBehavior.Single => new AddReceiptItemStrategy(),
        ReceiptItemsBehavior.Combined => new AddOrCombineReceiptItemStrategy(),
        _ => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null)
    };
}