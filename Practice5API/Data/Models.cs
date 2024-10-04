namespace Practice5API.Data
{
    public class Product : IEntity
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Id { get => ProductID; set => ProductID = value; }
    }

    public class Sale
    {
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }

    public class Purchase
    {
        public int PurchaseID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class Inventory
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
