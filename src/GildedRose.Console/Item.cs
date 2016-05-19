namespace GildedRose.Console
{
    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public bool Expired => SellIn == 0;
        protected virtual bool CanUpdateQuality => Quality > 0;
        protected virtual bool CanUpdateSellin => true;

        public virtual void UpdateQuality()
        {
            if (!CanUpdateQuality) return;

            DecreaseQuality();
        }

        private void DecreaseQuality()
        {
            if (Expired)
                Quality -= 2;
            else
                Quality--;
        }

        public void UpdateSellIn()
        {
            if (CanUpdateSellin)
                SellIn--;
        }
    }
}