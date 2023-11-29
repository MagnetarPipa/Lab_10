using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item item1 = new Item("Axe", "12345", 23, 12);
            Item item3 = new Item("Shotgun", "12334567", 3, 4);
            Item item2 = new Item("Shovel", "945687893112", 1, 8);
            Item item4 = new Item("Rifle", "8567123", 15, 100);

            Inventory inventory1 = new Inventory();
            inventory1.AddItem(item1);
            inventory1.AddItem(item2);
            inventory1.AddItem(item3);
            inventory1.AddItem(item4);

            Console.WriteLine(inventory1);

            InventoryFilter filter = new InventoryFilter();
            InventorySorter sorter = new InventorySorter();

            // Стандартный делегат
            InventorySorter.ItemFilterDelegate filterDelegate = item => item.Durability >= 90;
            sorter.BubbleSort(inventory1.InventoryList, filterDelegate);

            Console.WriteLine("\nОтсортированный инвентарь:");
            Console.WriteLine(inventory1);

            // Стандартный делегат
            HashSet<Item> filteredInventory = filter.FilteredInventory(inventory1.InventoryList, InventoryFilter.FilterQuantityLower, 10);
            Console.WriteLine("Отфильтрованные предметы по количеству");
            foreach (Item item in filteredInventory)
            {
                Console.WriteLine(item);
            }

            // Использование анонимной функции
            HashSet<Item> filteredInventory1 = filter.FilteredInventory(inventory1.InventoryList, (item, filterValue) => item.Durability < filterValue, 10);
            Console.WriteLine("Отфильтрованные предметы по прочности");
            foreach (Item item in filteredInventory1)
            {
                Console.WriteLine(item);
            }

            // Использование лямбда-выражения
            HashSet<Item> filteredInventory2 = filter.FilteredInventory(inventory1.InventoryList, (item, filterValue) => item.Quantity > filterValue, 3);
            Console.WriteLine("Отфильтрованные предметы по количеству");
            foreach (Item item in filteredInventory2)
            {
                Console.WriteLine(item);
            }

            // Замер времени для сортировки без распараллеливания
            Stopwatch sequentialSortStopwatch = Stopwatch.StartNew();
            sorter.BubbleSort(inventory1.InventoryList, filterDelegate);
            sequentialSortStopwatch.Stop();
            Console.WriteLine($"Время сортировки без распараллеливания: {sequentialSortStopwatch.ElapsedMilliseconds} мс");

            // Замер времени для параллельной сортировки с разным количеством потоков
            int[] threadCounts = { 1, 2, 4, 8 };
            foreach (var threadCount in threadCounts)
            {
                Console.WriteLine($"\nПараллельная сортировка с {threadCount} потоками:");

                Stopwatch parallelSortStopwatch = Stopwatch.StartNew();
                ParallelSortInventory(inventory1, threadCount);
                parallelSortStopwatch.Stop();

                Console.WriteLine($"Время сортировки с {threadCount} потоками: {parallelSortStopwatch.ElapsedMilliseconds} мс");
            }

            Console.ReadLine();
        }

        // Параллельная сортировка инвентаря с заданным количеством потоков
        private static void ParallelSortInventory(Inventory inventory, int numThreads)
        {
            List<Item> itemList = new List<Item>(inventory.InventoryList);

            Task[] tasks = new Task[numThreads];

            for (int i = 0; i < numThreads; i++)
            {
                int threadIndex = i;
                tasks[i] = Task.Run(() =>
                {
                    int start = (itemList.Count / numThreads) * threadIndex;
                    int end = (threadIndex == numThreads - 1) ? itemList.Count : start + (itemList.Count / numThreads);

                    itemList.Sort(start, end - start, new ItemComparer());
                });
            }

            Task.WaitAll(tasks);

            inventory.InventoryList = new HashSet<Item>(itemList);
        }
    }
}
