namespace SupermarketReceipt;

public interface IReceiptItem
{
    IProduct Product { get; }

    public double TotalPrice { get; }

    void Accept(IReceiptPrinter printer);
}