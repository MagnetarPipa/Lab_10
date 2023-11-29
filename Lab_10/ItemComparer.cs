using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public class ItemComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
