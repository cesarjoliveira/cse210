using System;
using System.Collections.Generic;

namespace FoundationProgram2
{
    public class Address
    {
        private string _street;
        private string _city;
        private string _state;
        private string _country;

        public Address(string street, string city, string state, string country)
        {
            _street = street;
            _city = city;
            _state = state;
            _country = country;
        }

        public bool IsInUSA()
        {
            return _country.Equals("USA", StringComparison.OrdinalIgnoreCase);
        }

        public string GetFullAddress()
        {
            return $"{_street}, {_city}, {_state}, {_country}";
        }
    }

    public class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public bool IsInUSA()
        {
            return _address.IsInUSA();
        }

        public string GetCustomerDetails()
        {
            return $"{_name}, Address: {_address.GetFullAddress()}";
        }
    }

    public class Product
    {
        private string _name;
        private string _productId;
        private decimal _price;
        private int _quantity;

        public Product(string name, string productId, decimal price, int quantity)
        {
            _name = name;
            _productId = productId;
            _price = price;
            _quantity = quantity;
        }

        public decimal GetTotalCost()
        {
            return _price * _quantity;
        }
    }

    public class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _products = new List<Product>();
            _customer = customer;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public decimal CalculateTotalCost()
        {
            decimal total = 0;
            foreach (var product in _products)
            {
                total += product.GetTotalCost();
            }
            return total;
        }

        public string GetPackingLabel()
        {
            return $"Packing Label: \nCustomer: {_customer.GetCustomerDetails()}";
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label: \nTo: {_customer.GetCustomerDetails()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
            Customer customer1 = new Customer("John Doe", address1);
            Order order1 = new Order(customer1);
            order1.AddProduct(new Product("Laptop", "P001", 1200.00m, 1));
            order1.AddProduct(new Product("Mouse", "P002", 25.00m, 2));

            Address address2 = new Address("456 Elm St", "Othertown", "TX", "USA");
            Customer customer2 = new Customer("Jane Smith", address2);
            Order order2 = new Order(customer2);
            order2.AddProduct(new Product("Monitor", "P003", 300.00m, 1));
            order2.AddProduct(new Product("Keyboard", "P004", 40.00m, 1));

            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine($"Total Cost: {order1.CalculateTotalCost():C}");
            Console.WriteLine(order1.GetShippingLabel());

            Console.WriteLine();

            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine($"Total Cost: {order2.CalculateTotalCost():C}");
            Console.WriteLine(order2.GetShippingLabel());
        }
    }
}