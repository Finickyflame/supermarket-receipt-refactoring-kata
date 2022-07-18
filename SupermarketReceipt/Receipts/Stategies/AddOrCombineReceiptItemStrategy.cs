namespace SupermarketReceipt;

/// <summary>
/// Items will be added to the list if they are not there, otherwise their Quantity/Weight will be combined in the exising entry.
/// </summary>
public class AddOrCombineReceiptItemStrategy : IAddReceiptItemStrategy
{
    public void AddReceiptItem(ReceiptItems items, ReceiptItem item)
    {
        items.AddOrSet(item, existingItem => existingItem with
        {
            Quantity = existingItem.Quantity + item.Quantity
        });
    }

    public void AddReceiptItem(ReceiptItems items, WeightReceiptItem item)
    {
        items.AddOrSet(item, existingItem => existingItem with
        {
            Weight = existingItem.Weight + item.Weight
        });
    }
}