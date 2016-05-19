namespace GildedRose.Console
{
    public class BackstagePass : Item
    {
        public override void UpdateQuality()
        {
            if (SellIn <= 5)
                Quality += 3;
            else if (SellIn <= 10)
                Quality += 2;
            else
                Quality += 1;

            if (Expired)
                Quality = 0;
        }
    }
}