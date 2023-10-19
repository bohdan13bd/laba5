using System;
using System.Collections.Generic;
using System.Linq;

// Клас для товару
class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public double Rating { get; set; }

    public Product(string name, double price, string description, string category, double rating)
    {
        Name = name;
        Price = price;
        Description = description;
        Category = category;
        Rating = rating;
    }
}

// Клас для користувача
class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Order> PurchaseHistory { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        PurchaseHistory = new List<Order>();
    }
}

// Клас для замовлення
class Order
{
    public List<Product> Products { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }

    public Order(List<Product> products, int quantity, double totalPrice, string status)
    {
        Products = products;
        Quantity = quantity;
        TotalPrice = totalPrice;
        Status = status;
    }
}

// Інтерфейс для пошуку товарів
interface ISearchable
{
    List<Product> SearchByCategory(string category);
    List<Product> SearchByPriceRange(double minPrice, double maxPrice);
    List<Product> SearchByRating(double minRating);
}

// Клас для магазину
class Store : ISearchable
{
    public List<Product> Products { get; set; }
    public List<User> Users { get; set; }
    public List<Order> Orders { get; set; }

    public Store()
    {
        Products = new List<Product>();
        Users = new List<User>();
        Orders = new List<Order>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void PlaceOrder(User user, List<Product> products, int quantity)
    {
        double totalPrice = products.Sum(p => p.Price) * quantity;
        Order order = new Order(products, quantity, totalPrice, "Pending");
        user.PurchaseHistory.Add(order);
        Orders.Add(order);
    }

    public List<Product> SearchByCategory(string category)
    {
        return Products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Product> SearchByPriceRange(double minPrice, double maxPrice)
    {
        return Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
    }

    public List<Product> SearchByRating(double minRating)
    {
        return Products.Where(p => p.Rating >= minRating).ToList();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Store store = new Store();

        User user1 = new User("user1", "password1");
        User user2 = new User("user2", "password2");

        store.AddUser(user1);
        store.AddUser(user2);

        Product product1 = new Product("Product 1", 10.99, "Description 1", "Category A", 4.5);
        Product product2 = new Product("Product 2", 25.49, "Description 2", "Category B", 4.0);
        Product product3 = new Product("Product 3", 5.99, "Description 3", "Category A", 3.8);

        store.AddProduct(product1);
        store.AddProduct(product2);
        store.AddProduct(product3);

        List<Product> user1OrderProducts = new List<Product> { product1, product2 };
        store.PlaceOrder(user1, user1OrderProducts, 2);

        List<Product> user2OrderProducts = new List<Product> { product3 };
        store.PlaceOrder(user2, user2OrderProducts, 5);

        // Приклади пошуку товарів
        List<Product> categoryResults = store.SearchByCategory("Category A");
        List<Product> priceResults = store.SearchByPriceRange(5.0, 15.0);
        List<Product> ratingResults = store.SearchByRating(4.0);

        Console.WriteLine("Category Search Results:");
        foreach (var product in categoryResults)
        {
            Console.WriteLine(product.Name);
        }

        Console.WriteLine("\nPrice Range Search Results:");
        foreach (var product in priceResults)
        {
            Console.WriteLine(product.Name);
        }

        Console.WriteLine("\nRating Search Results:");
        foreach (var product in ratingResults)
        {
            Console.WriteLine(product.Name);
        }
    }
}
