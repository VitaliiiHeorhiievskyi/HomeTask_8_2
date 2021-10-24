using System;
using System.Globalization;

namespace HomeTask8_2
{
    enum Category
    {
        SecondSort = 5,
        FirstSort = 10,
        HighSort = 15
    }

    enum Kind
    {
        Mutton = 1,
        Veal,
        Pork,
        Chicken
    }
    class Meat : Product
    {
        public Category Category { get; set; }

        public Kind Kind { get; set; }

        public Meat(string name, double weight, double price, int expirationDate, DateTime dateOfManufacture, Category category, Kind kind)
            : base(name, weight, price, expirationDate, dateOfManufacture)
        {
            Category = category;

            Kind = kind;
        }

        public Meat(Product product, Category category, Kind kind) : base(product)
        {
            Category = category;

            Kind = kind;
        }

        public static Meat Parse(string line)
        {
            if (string.IsNullOrEmpty(line))
                return null;

            string[] data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 7)
                return null;

            string name;
            double weight;
            double price;
            Category category;
            Kind kind;

            name = data[0];

            bool result = double.TryParse(data[1], out weight);
            if (!result)
                throw new ArgumentException("Second value must be double!!");

            result = double.TryParse(data[2], out price);
            if (!result)
                throw new ArgumentException("Third value must be double!!");

            result = int.TryParse(data[3], out int expirationDate);
            if (!result)
                throw new ArgumentException("Fourth value must be integer!!");

            result = DateTime.TryParseExact(data[4], "dd.MM.yyyy", new CultureInfo(4), DateTimeStyles.None, out DateTime dateOfManifecture);
            if (!result)
                throw new ArgumentException("Date is not correct!");

            switch (data[5])
            {
                case "First":
                    category = Category.FirstSort;
                    break;
                case "Second":
                    category = Category.SecondSort;
                    break;
                case "High":
                    category = Category.HighSort;
                    break;
                default: throw new ArgumentException("Category is not correct");
            }

            switch (data[6].Trim('\r'))
            {
                case "Mutton":
                    kind = Kind.Mutton;
                    break;
                case "Veal":
                    kind = Kind.Veal;
                    break;
                case "Pork":
                    kind = Kind.Pork;
                    break;
                case "Chicken":
                    kind = Kind.Chicken;
                    break;
                default: throw new ArgumentException("Kind is not correct");
            }



            return new Meat(name, weight, price, expirationDate, dateOfManifecture, category, kind);

        }
        public override void ChangePrice(double percent)
        {
            base.ChangePrice(percent);
            Price += (double)Category * Price / 100;
        }

        public override string ToString()
        {
            return "\n\nMeat\n" + base.ToString() + $"Category: {Category}\nKind: {Kind}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && Equals(obj as Meat);
        }

        public bool Equals(Meat other)
        {
            return other != null && Category == other.Category
                && Kind == other.Kind;
        }
    }
}
