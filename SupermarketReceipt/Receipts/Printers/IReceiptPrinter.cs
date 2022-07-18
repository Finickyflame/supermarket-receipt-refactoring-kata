namespace SupermarketReceipt;

public interface IReceiptPrinter
{
    string PrintReceipt(Receipt receipt);

    void Print(Discount discount);

    void Print(WeightReceiptItem item);

    void Print(ReceiptItem item);
}