using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using NUnit.Framework;


namespace GildedRose.Tests
{
    [TestFixture]
    public class ItemUpdaterTests
    {
        private ItemUpdater _itemUpdater;
        private IList<Item> _items;

        [SetUp]
        public void SetUp()
        {
            _itemUpdater = new ItemUpdater();
            _items = _itemUpdater.Items;
        }

        [Test]
        public void WhenADayPasses_QualityDecreases()
        {
            var vest = _items.Single(x => x.Name.Equals("+5 Dexterity Vest"));
            var initialVestQuality = vest.Quality;

            _itemUpdater.UpdateItems();

            Assert.That(initialVestQuality, Is.GreaterThan(vest.Quality));
        }

        [Test]
        public void WhenADayPasses_SellInDecreases()
        {
            var vest = _items.Single(x => x.Name.Equals("+5 Dexterity Vest"));
            var initialVestSellIn = vest.SellIn;

            _itemUpdater.UpdateItems();

            Assert.That(initialVestSellIn, Is.GreaterThan(vest.SellIn));
        }

        [Test]
        public void OnceTheSellByDateHasPassedQualityDegradesTwiceAsFast()
        {
            var vest = _items.Single(x => x.Name.Equals("+5 Dexterity Vest"));
            var initialVestQuality = vest.Quality;
            vest.SellIn = 1;

            _itemUpdater.UpdateItems();

            var intermediaryVestQuality = vest.Quality;

            _itemUpdater.UpdateItems();

            var initialDifference = initialVestQuality - intermediaryVestQuality;
            var newDifference = intermediaryVestQuality - vest.Quality;

            Assert.That(newDifference, Is.EqualTo(initialDifference * 2));
        }

        [Test]
        public void TheQualityOfAnItemCannotBeNegative()
        {
            var manaCake = _items.Single(x => x.Name.Equals("Conjured Mana Cake"));
            manaCake.Quality = 0;
           
            _itemUpdater.UpdateItems();

            Assert.That(manaCake.Quality, Is.EqualTo(0));
        }

        [Test]
        public void AgedBrieShouldIncreaseInQualityAsItAges()
        {
            var agedBrie = _items.Single(x => x.Name.Equals("Aged Brie"));
            var initialQuality = agedBrie.Quality;

            _itemUpdater.UpdateItems();
            var newQuality = agedBrie.Quality;

            Assert.That(initialQuality, Is.LessThan(newQuality));
        }

        [Test]
        public void TheQualityOfAnItemCannotBeGreaterThan50()
        {
            var agedBrie = _items.Single(x => x.Name.Equals("Aged Brie"));
            agedBrie.Quality = 50;

            _itemUpdater.UpdateItems();

            Assert.That(agedBrie.Quality, Is.EqualTo(50));
        }

        [Test]
        public void SulfurasShouldNotBeSoldOrDecreaseInQuality()
        {
            var sulfuras = _items.Single(x => x.Name.Equals("Sulfuras, Hand of Ragnaros"));
            var initialQuality = sulfuras.Quality;
            var initialSellin = sulfuras.SellIn;

            _itemUpdater.UpdateItems();

            Assert.That(initialQuality, Is.EqualTo(sulfuras.Quality));
            Assert.That(initialSellin, Is.EqualTo(sulfuras.SellIn));
        }

        [Test]
        public void BackStagePassQualityIncreasesBy2WhenSellInIs10()
        {
            var backStagePass = _items.Single(x => x.Name.Equals("Backstage passes to a TAFKAL80ETC concert"));
            var initialQuality = backStagePass.Quality;
            backStagePass.SellIn = 9;

            _itemUpdater.UpdateItems();

            Assert.That(initialQuality, Is.EqualTo(backStagePass.Quality - 2));
        }

        [Test]
        public void BackStagePassQualityIncreasesBy3WhenSellInIs5()
        {
            var backStagePass = _items.Single(x => x.Name.Equals("Backstage passes to a TAFKAL80ETC concert"));
            var initialQuality = backStagePass.Quality;
            backStagePass.SellIn = 4;

            _itemUpdater.UpdateItems();

            Assert.That(initialQuality, Is.EqualTo(backStagePass.Quality - 3));
        }

        [Test]
        public void BackStagePassQualityDropsTo0WhenSellInIs0()
        {
            var backStagePass = _items.Single(x => x.Name.Equals("Backstage passes to a TAFKAL80ETC concert"));
            backStagePass.SellIn = 0;
            _itemUpdater.UpdateItems();

            Assert.That(backStagePass.Quality, Is.EqualTo(0));
        }

        [Test]
        public void ConjuredItemsDegradeTwiceAsFast()
        {
            var conjuredItem = _items.Single(x => x.Name.Equals("Conjured Mana Cake"));
            var initialQuality = conjuredItem.Quality;

            _itemUpdater.UpdateItems();
            var newQuality = conjuredItem.Quality;

            Assert.That(initialQuality, Is.EqualTo(newQuality + 2));
        }
    }
}