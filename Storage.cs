using System;
using System.Collections.Generic;
using System.IO;

namespace HomeTask8_2
{
    class Storage
    {
        public List<Product> Products { get; private set; }

        public Product this[int index]
        {
            get
            {
                if (index >= 0 && index < Products.Count)
                    return Products[index];
                else throw new ArgumentNullException("Going beyond the array");
            }

            set
            {
                if (index >= 0 && index < Products.Count && value is Product)
                    Products[index] = (Product)value;
                else throw new ArgumentNullException("Going beyond the array");
            }
        }

        public Storage(List<Product> products)
        {
            Products = new List<Product>(products);
        }

        public Storage()
        {
            Products = new List<Product>();
        }

        public string PrintMenu()
        {
            return ("\n[1]Сreate an array of products\t[2] Add product" +
                "\n[3]Delete last product\t\t[4] Print Check\n" +
                "[5]Change price for all product\t[6] Find all meat products\n" +
                "[7]Delete spoiled Dairy Product\t[8]Read data from a file\n" +
                "[0] Exit\nSelect an action:");

        }

        public void DeleteSpoiledDairyProducts(string filePath)
        {
            DateTime now = DateTime.Now;

            StreamWriter writer = new StreamWriter(filePath);


            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i] is DairyProduct)
                {
                    if (Products[i].DateOfManufacture.AddDays(Products[i].ExpirationDate) < now)
                    {
                        writer.WriteLine(Products[i]);
                        Products.RemoveAt(i);
                    }
                }
            }

            writer.Close();
        }

        public void ReadDataFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            string[] data;

            using (StreamReader reader = new StreamReader(filePath))
            {
                data = reader.ReadToEnd().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (var item in data)
            {
                string[] worlds = item.Split();
                switch (worlds[0])
                {
                    case "Product":
                        AddProduct(item);
                        break;
                    case "Meat":
                        AddMeat(item);
                        break;
                    case "Dairy":
                        AddDairyProduct(item);
                        break;

                    default: throw new ArgumentException("Data is incorrect!!");
                }
            }

        }

        public void AddProduct(string data)
        {
            data = data.Substring(8);
            Products.Add(Product.Parse(data));
        }

        public void AddMeat(string data)
        {
            data = data.Substring(5);
            Products.Add(Meat.Parse(data));
        }

        public void AddDairyProduct(string data)
        {
            data = data.Substring(6);
            Products.Add(DairyProduct.Parse(data));
        }

        public void AddMeat(string name, double weight, double price, int expirationDate, DateTime dateOfManufacture, Category category, Kind kind)
        {
            Products.Add(new Meat(name, weight, price, expirationDate, dateOfManufacture, category, kind));
        }

        public void AddDairy(string name, double price, double weight, int expirationdDate, DateTime date)
        {
            Products.Add(new DairyProduct(name, weight, price, expirationdDate, date));
        }

        public void AddProduct(string name, double price, double weight, int expirationdDate, DateTime date)
        {
            Products.Add(new Product(name, weight, price, expirationdDate, date));
        }

        public void DeleteLastProduct()
        {
            if (Products == null)
            {
                Console.WriteLine("Empty array!");
                return;
            }
            Products.RemoveAt(Products.Count - 1);
            Console.WriteLine("Delete completed!");
        }

        public string PrintCheck()
        {
            return Check.DisplayProducts(Products);
        }

        public List<Meat> FindAllMeatProducts()
        {
            if (Products == null)
                return null;

            List<Meat> meats = new List<Meat>();

            foreach (var product in Products)
            {
                if (product is Meat)
                {
                    meats.Add((Meat)product);
                }

            }

            return meats;
        }

        public void ChangePrice(double percent)
        {
            foreach (var item in Products)
            {
                item.ChangePrice(percent);
            }
        }

        public List<Product> GetJointProducts(Storage other)
        {
            if (other == null)
                throw new ArgumentNullException();

            var jointProducts = new List<Product>();

            foreach (var product in this.Products)
            {
                if (other.Products.Contains(product))
                    jointProducts.Add(product);
            }

            return jointProducts;
        }

        public List<Product> GetProductsNotContainsOther(Storage other)
        {
            if (other == null)
                throw new ArgumentNullException();

            var products = new List<Product>();

            foreach (var product in this.Products)
            {
                if (!other.Products.Contains(product))
                    products.Add(product);
            }

            return products;
        }

        public List<Product> GetDifferentProducts(Storage other)
        {
            if (other == null)
                throw new ArgumentNullException();

            var differentProducts = new List<Product>();

            foreach (var product in this.Products)
            {
                if (!other.Products.Contains(product))
                    differentProducts.Add(product);
            }

            foreach (var product in other.Products)
            {
                if (!this.Products.Contains(product))
                    differentProducts.Add(product);
            }

            return differentProducts;
        }


    }
}
