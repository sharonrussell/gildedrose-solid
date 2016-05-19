namespace GildedRose.Console
{
    public static class UpdateCalculator
    {
        public static int BackStagePassIncrease(Item item)
        {
            if (item.SellIn <= 5)
                return 3;

            return item.SellIn <= 10 ? 2 : 1;
        }

        public static int Decrease(Item item, bool isConjured)
        {
            var amountToDecreaseBy = item.SellIn < 1 ? 2 : 1;

            if (isConjured)
                amountToDecreaseBy *= 2;

            return amountToDecreaseBy;
        }


        public static int Increase(Item item)
        {
            return item.Quality >= 50 ? 0 : 1;
        }
    }
}