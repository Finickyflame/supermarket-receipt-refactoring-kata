namespace SupermarketReceipt;

/// <summary>
/// Items will simply be added to the list.
/// </summary>
public class AddReceiptItemStrategy : IAddReceiptItemStrategy
{
    public void AddReceiptItem(ReceiptItems items, ReceiptItem item) => items.Add(item);

    public void AddReceiptItem(ReceiptItems items, WeightReceiptItem item) => items.Add(item);
}