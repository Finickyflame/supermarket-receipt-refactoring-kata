namespace SupermarketReceipt;

public record WeightReceiptItem(WeightProduct Product, double Weight) : IReceiptItem
{
    public double TotalPrice => this.Weight * this.Product.Price;

    IProduct IReceiptItem.Product => this.Product;


    public void Accept(IReceiptPrinter printer) => printer.Print(this);
    
    public virtual bool Equals(WeightReceiptItem other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return Equals(this.Product, other.Product);
    }

    public override int GetHashCode() => this.Product.GetHashCode();
}