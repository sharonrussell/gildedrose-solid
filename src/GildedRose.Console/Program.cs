using System.Collections.Generic;

namespace GildedRose.Console
{
    public static class Program
    {
        private static void Main()
        {
            var itemUpdater = new ItemUpdater();
            itemUpdater.UpdateItems();

            System.Console.ReadKey();
        }
    }
}
