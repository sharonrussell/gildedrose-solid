namespace GildedRose.Console
{
    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public virtual void UpdateQuality()
        {
            if (Expired())
                Quality -= 2;

            else if (Quality > 0)
                Quality--;
        }

        private bool Expired()
        {
            return SellIn == 0;
        }

        public void UpdateSellIn()
        {
            SellIn--;
        }
    }
}