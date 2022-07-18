namespace SupermarketReceipt;

public enum ReceiptItemsBehavior
{
    /// <summary>
    /// All items will be inserted as is
    /// </summary>
    Single,
    /// <summary>
    /// Same items will be combined together
    /// </summary> 
    Combined,
}