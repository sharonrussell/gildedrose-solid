using System.Collections.Generic;

namespace GildedRose.Console
{
    public class ItemUpdater
    {
        public IList<Item> Items { get; }

        public ItemUpdater()
        {
            Items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new AgedBrie {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Sulfuras {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new BackstagePass
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Conjured {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };
        }

        public void UpdateItems()
        {
            foreach (var item in Items)
            {
                item.UpdateQuality();
                item.UpdateSellIn();
            }
        }
    }
}