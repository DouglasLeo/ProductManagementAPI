namespace ProductManagement.Domain.Entities;

public class Product : Entity
{
    private Product()
    {
    }

    public Product(string name, string description, decimal price, int quantity, bool active)
    {
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
        Active = active;
    }

    #region Properties

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public bool Active { get; private set; }

    #endregion

    #region Public Methods

    public void Update(string name, string description, decimal price, int quantity, bool active)
    {
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
        Active = active;
    }

    #endregion
}