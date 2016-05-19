namespace GildedRose.Console
{
    public static class ItemChecker
    {
        public static bool IsBackStagePass(string itemName)
        {
            return itemName.Equals("Backstage passes to a TAFKAL80ETC concert");
        }

        private static bool IsAgedBrie(string itemName)
        {
            return itemName.Equals("Aged Brie");
        }

        public static bool IsConjured(string itemName)
        {
            return itemName.Contains("Conjured");
        }

        public static bool IncreasesOverTime(string itemName)
        {
            return IsAgedBrie(itemName) || IsBackStagePass(itemName);
        }

        public static bool IsLegendary(string itemName)
        {
            return itemName.Equals("Sulfuras, Hand of Ragnaros");
        }

        public static bool IsExpired(Item item)
        {
            return item.SellIn == 0;
        }
    }
}