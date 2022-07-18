namespace SupermarketReceipt;

public sealed class ReceiptItems : List<IReceiptItem>
{
    public void AddOrSet<T>(T newItem, Func<T, T> existingAction) where T : IReceiptItem
    {
        int index = this.IndexOf(newItem);
        if (index >= 0)
        {
            var existingItem = (T)this[index];
            this[index] = existingAction(existingItem);
        }
        else
        {
            this.Add(newItem);
        }
    }
}