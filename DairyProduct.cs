
using System;
using System.Globalization;

namespace HomeTask8_2
{
    class DairyProduct : Product
    {
        public override void ChangePrice(double percent)
        {
            if (ExpirationDate < 0)
            {
                Price = 0;
            }
            else
            {
                Price += (percent + ExpirationDate * 5) * Price / 100;
            }
        }

        public static DairyProduct Parse(string line)
        {
            if (string.IsNullOrEmpty(line))
                return null;

            string[] data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 5)
                return null;

            string name;
            double weight;
            double price;
            int expirationDate;
            DateTime dateOfManifecture;

            name = data[0];

            bool result = double.TryParse(data[1], out weight);
            if (!result)
                throw new ArgumentException("Second value must be double!!");

            result = double.TryParse(data[2], out price);
            if (!result)
                throw new ArgumentException("Third value must be double!!");

            result = int.TryParse(data[3], out expirationDate);
            if (!result)
                throw new ArgumentException("Fourth value must be integer!!");

            result = DateTime.TryParseExact(data[4].Substring(0, 10), "dd.MM.yyyy", new CultureInfo(4), DateTimeStyles.None, out dateOfManifecture);
            if (!result)
                throw new ArgumentException("Date is not correct!");
            return new DairyProduct(name, weight, price, expirationDate, dateOfManifecture);
        }

        public override string ToString()
        {
            return "\nDairy Product\n" + base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && Equals(obj as DairyProduct);
        }

        public bool Equals(DairyProduct other)
        {
            return other != null && ExpirationDate == other.ExpirationDate;
        }

        public DairyProduct(string name, double weight, double price, int expirationDate, DateTime dateOfManifecture)
            : base(name, weight, price, expirationDate, dateOfManifecture)
        {
        }

        public DairyProduct(Product product) : base(product)
        {
        }

    }
}
