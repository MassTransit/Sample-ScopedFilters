namespace WebApi.Contracts
{
    public record InventoryStatus
    {
        public string Sku { get; init; }
        public int QuantityOnHand { get; init; }
    }
}