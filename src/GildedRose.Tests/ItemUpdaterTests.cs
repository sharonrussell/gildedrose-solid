using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using NUnit.Framework;


namespace GildedRose.Tests
{
    [TestFixture]
    public class ItemUpdaterTests
    {
        private List<Item> Items { get; set; }
        private ItemUpdater _itemUpdater;

        [SetUp]
        public void SetUp()
        {
            Items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            _itemUpdater = new ItemUpdater();
        }

        [Test]
        public void WhenADayPasses_QualityDecreases()
        {
            var vest = Items.Single(x => x.Name.Equals("+5 Dexterity Vest"));
            var initialVestQuality = vest.Quality;

            _itemUpdater.UpdateItems(Items);

            Assert.That(initialVestQuality > vest.Quality);
        }

        [Test]
        public void WhenADayPasses_SellInDecreases()
        {
            var vest = Items.Single(x => x.Name.Equals("+5 Dexterity Vest"));
            var initialVestSellIn = vest.SellIn;

            _itemUpdater.UpdateItems(Items);

            Assert.That(initialVestSellIn > vest.SellIn);
        }

        [Test]
        public void OnceTheSellByDateHasPassedQualityDegradesTwiceAsFast()
        {
            var vest = Items.Single(x => x.Name.Equals("+5 Dexterity Vest"));
            var initialVestQuality = vest.Quality;
            vest.SellIn = 1;

            _itemUpdater.UpdateItems(Items);

            var intermediaryVestQuality = vest.Quality;

            _itemUpdater.UpdateItems(Items);

            var initialDifference = initialVestQuality - intermediaryVestQuality;
            var newDifference = intermediaryVestQuality - vest.Quality;

            Assert.That(newDifference == initialDifference * 2);
        }

        [Test]
        public void TheQualityOfAnItemCannotBeNegative()
        {
            var manaCake = Items.Single(x => x.Name.Equals("Conjured Mana Cake"));
            manaCake.Quality = 0;
           
            _itemUpdater.UpdateItems(Items);

            Assert.That(manaCake.Quality == 0);
        }

        [Test]
        public void AgedBrieShouldIncreaseInQualityAsItAges()
        {
            var agedBrie = Items.Single(x => x.Name.Equals("Aged Brie"));
            var initialQuality = agedBrie.Quality;

            _itemUpdater.UpdateItems(Items);
            var newQuality = agedBrie.Quality;

            Assert.That(initialQuality < newQuality);
        }

        [Test]
        public void TheQualityOfAnItemCannotBeGreaterThan50()
        {
            var agedBrie = Items.Single(x => x.Name.Equals("Aged Brie"));
            agedBrie.Quality = 50;

            _itemUpdater.UpdateItems(Items);

            Assert.That(agedBrie.Quality == 50);
        }

        [Test]
        public void SulfurasShouldNotBeSoldOrDecreaseInQuality()
        {
            var sulfuras = Items.Single(x => x.Name.Equals("Sulfuras, Hand of Ragnaros"));
            var initialQuality = sulfuras.Quality;
            var initialSellin = sulfuras.SellIn;

            _itemUpdater.UpdateItems(Items);

            Assert.That(initialQuality, Is.EqualTo(sulfuras.Quality));
            Assert.That(initialSellin, Is.EqualTo(sulfuras.SellIn));
        }

        [Test]
        public void BackStagePassQualityIncreasesBy2WhenSellInIs10()
        {
            var backStagePass = Items.Single(x => x.Name.Equals("Backstage passes to a TAFKAL80ETC concert"));
            var initialQuality = backStagePass.Quality;
            backStagePass.SellIn = 9;

            _itemUpdater.UpdateItems(Items);

            Assert.That(initialQuality, Is.EqualTo(backStagePass.Quality - 2));
        }

        [Test]
        public void BackStagePassQualityIncreasesBy3WhenSellInIs5()
        {
            var backStagePass = Items.Single(x => x.Name.Equals("Backstage passes to a TAFKAL80ETC concert"));
            var initialQuality = backStagePass.Quality;
            backStagePass.SellIn = 4;

            _itemUpdater.UpdateItems(Items);

            Assert.That(initialQuality, Is.EqualTo(backStagePass.Quality - 3));
        }

        [Test]
        public void BackStagePassQualityDropsTo0WhenSellInIs0()
        {
            var backStagePass = Items.Single(x => x.Name.Equals("Backstage passes to a TAFKAL80ETC concert"));
            backStagePass.SellIn = 0;
            _itemUpdater.UpdateItems(Items);

            Assert.That(backStagePass.Quality, Is.EqualTo(0));
        }

        [Test]
        public void ConjuredItemsDegradeTwiceAsFast()
        {
            var conjuredItem = Items.Single(x => x.Name.Equals("Conjured Mana Cake"));
            var initialQuality = conjuredItem.Quality;

            _itemUpdater.UpdateItems(Items);
            var newQuality = conjuredItem.Quality;

            Assert.That(initialQuality, Is.EqualTo(newQuality + 2));
        }
    }
}