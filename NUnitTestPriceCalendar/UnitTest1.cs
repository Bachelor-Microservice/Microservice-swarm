using NUnit.Framework;
using PriceCalendarService.Dtos;

namespace NUnitTestPriceCalendar
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var test = new GroupsDTO();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}