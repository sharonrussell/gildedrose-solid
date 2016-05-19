namespace GildedRose.Console
{
    public class Conjured : Item
    {
        public override void UpdateQuality()
        {
            if (CanUpdateQuality)
                Quality -= 2;
        }
    }
}