using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public class Item
    {
        private string _name;
        private string _id;
        private uint _quantity;
        private uint _durability;
        private const byte MIN_ID_LENGHT = 5;
        private const byte MAX_ID_LENGHT = 20;
        private const byte MAX_DURABILITY = 100;

        public Item(string name, string id, uint quantity, uint durability)
        {
            Name = name;
            Id = id;
            Quantity = quantity;
            Durability = durability;
        }

        public Item()
        {

        }

        public string Name
        {
            get => _name;
            init
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value.Trim();
                }
                else
                {
                    throw new ArgumentNullException("Неправильное название");
                }
            }
        }

        public string Id
        {
            get => _id;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length >= MIN_ID_LENGHT && value.Length <= MAX_ID_LENGHT)
                {
                    _id = (value.ToUpper()).Trim();
                }
                else
                {
                    throw new FormatException("Неправильный id");
                }
            }
        }

        public uint Quantity { get => _quantity; set => _quantity = value; }

        public uint Durability
        {
            get => _durability;
            set
            {
                if (value <= MAX_DURABILITY)
                {
                    _durability = value;
                }
                else
                {
                    throw new FormatException("Неправильная прочность");
                }
            }
        }

        public override string ToString()
        {
            return $"\nНазвание предмета: {Name}\n" + $"Id предмета: {Id}\n" + $"Количество: {Quantity}\n" + $"Прочность: {Durability}\n";
        }
    }
}
