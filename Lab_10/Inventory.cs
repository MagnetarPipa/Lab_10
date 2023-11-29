using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public class Inventory
    {
        HashSet<Item> inventoryList = new HashSet<Item>();

        public HashSet<Item> InventoryList
        {
            get => inventoryList;
            set => inventoryList = value;
        }

        public void AddItem(Item newItem)
        {
            if (newItem is not null)
            {
                inventoryList.Add(newItem);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Ошибка в добавлении предмета");
            }
        }

        public void DeleteItem(Item itemToDelete)
        {
            if (itemToDelete is not null && inventoryList.Contains(itemToDelete))
            {
                inventoryList.Remove(itemToDelete);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Таково предмета не существует");
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (Item part in inventoryList)
            {
                str.Append(part + "\n");
            }
            return str.ToString();
        }
    }
}
