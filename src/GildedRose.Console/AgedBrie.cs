namespace GildedRose.Console
{
    public class AgedBrie : Item
    {
        protected override bool CanUpdateQuality => Quality < 50;

        public override void UpdateQuality()
        {
            if (!CanUpdateQuality) return;
            Quality++;
        }
    }
}