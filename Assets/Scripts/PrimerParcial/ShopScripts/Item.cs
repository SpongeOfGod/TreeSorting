public class Item
{
    public string Name { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }

    public Item(string name, float price, int quantity, string category)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
    }

    public Item Clone()
    {
        return new Item(Name, Price, Quantity, Category);
    }
}


