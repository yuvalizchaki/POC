using POC.Infrastructure.Common.utils;

namespace UnitTests;

[TestFixture]
public class SortingUtilityTesting
{
    private class SampleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }

    [Test]
    public void SortItemsByFieldNames_SortsById()
    {
        // Arrange
        var items = new List<SampleItem>
        {
            new SampleItem { Id = 3, Name = "Item C", Date = DateTime.Today.AddDays(2), Price = 20.5m },
            new SampleItem { Id = 1, Name = "Item A", Date = DateTime.Today, Price = 15.75m },
            new SampleItem { Id = 2, Name = "Item B", Date = DateTime.Today.AddDays(1), Price = 18.0m }
        };
        var sortingFields = new List<string> { "Id" };

        // Act
        var sortedItems = SortingUtility.SortItemsByFieldNames(items, sortingFields).ToList();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(sortedItems[0].Id, Is.EqualTo(1));
            Assert.That(sortedItems[1].Id, Is.EqualTo(2));
            Assert.That(sortedItems[2].Id, Is.EqualTo(3));
        });
    }

    [Test]
    public void SortItemsByFieldNames_SortsByNameThenByDate()
    {
        // Arrange
        var items = new List<SampleItem>
        {
            new SampleItem { Id = 2, Name = "Item B", Date = DateTime.Today.AddDays(2), Price = 18.0m },
            new SampleItem { Id = 1, Name = "Item A", Date = DateTime.Today, Price = 15.75m },
            new SampleItem { Id = 3, Name = "Item C", Date = DateTime.Today.AddDays(1), Price = 20.5m }
        };
        var sortingFields = new List<string> { "Name", "Date" };

        // Act
        var sortedItems = SortingUtility.SortItemsByFieldNames(items, sortingFields).ToList();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(sortedItems[0].Name, Is.EqualTo("Item A"));
            Assert.That(sortedItems[1].Name, Is.EqualTo("Item B"));
            Assert.That(sortedItems[2].Name, Is.EqualTo("Item C"));
            Assert.That(sortedItems[0].Date, Is.EqualTo(DateTime.Today));
            Assert.That(sortedItems[1].Date, Is.EqualTo(DateTime.Today.AddDays(2)));
            Assert.That(sortedItems[2].Date, Is.EqualTo(DateTime.Today.AddDays(1)));
        });
    }

    [Test]
    public void SortItemsByFieldNames_DoesNotChangeOriginalList()
    {
        // Arrange
        var items = new List<SampleItem>
        {
            new SampleItem { Id = 3, Name = "Item B", Date = DateTime.Today.AddDays(2), Price = 15.5m },
            new SampleItem { Id = 1, Name = "Item A", Date = DateTime.Today, Price = 15.75m },
            new SampleItem { Id = 2, Name = "Item B", Date = DateTime.Today.AddDays(1), Price = 18.0m }
        };
        var originalItems = items.ToList(); // Create a copy of the original items
        var sortingFields = new List<string> { "Name", "Price" };

        // Act
        var sortedItems = SortingUtility.SortItemsByFieldNames(items, sortingFields).ToList();

        // Assert
        CollectionAssert.AreNotEqual(originalItems, sortedItems); // Ensure the original list is not changed
        CollectionAssert.AreEquivalent(originalItems, items); // Ensure the items list still contains the same items
        
        // Assert that the items are sorted
        Assert.Multiple(() =>
        {
            Assert.That(sortedItems[0].Id, Is.EqualTo(1));
            Assert.That(sortedItems[1].Id, Is.EqualTo(3));
            Assert.That(sortedItems[2].Id, Is.EqualTo(2));
        });
        
    }

}