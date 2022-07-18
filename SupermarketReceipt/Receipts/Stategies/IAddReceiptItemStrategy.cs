namespace SupermarketReceipt;

internal interface IAddReceiptItemStrategy
{
    void AddReceiptItem(ReceiptItems items, ReceiptItem item);
    
    void AddReceiptItem(ReceiptItems items, WeightReceiptItem item);
}