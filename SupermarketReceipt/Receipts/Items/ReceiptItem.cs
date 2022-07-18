namespace SupermarketReceipt;

public record ReceiptItem(Product Product, int Quantity) : IReceiptItem
{

    public double TotalPrice => this.Quantity * this.Product.Price;

    IProduct IReceiptItem.Product => this.Product;


    public void Accept(IReceiptPrinter printer) => printer.Print(this);
    
    public virtual bool Equals(ReceiptItem other)
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