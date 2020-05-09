using AutoMapper;
using ItemContracts;
using MassTransit;
using Moq;
using NUnit.Framework;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestPriceCalendar.MassTransitTests
{
    [TestFixture]
    public class UpdateOfItemEntityConsumerTest
    {
        IMapper mapperMock;
        PriceCalendarServiceContext serviceContextMock;
        //MAke these 3 the desired obs
        ItemPriceAndCurrencyResponse model;
        Groups _group;
        Item _item;

        [SetUp]
        public void SetUp()
        {
            var context = new Mock<ConsumeContext<ItemEntityUpdated>>();
            context.Setup(p => p.Message.Unit).Returns("Unit");
            context.Setup(p => p.Message.ArticleGroup).Returns(222);
            context.Setup(p => p.Message.Name).Returns("Name");
            context.Setup(p => p.Message.Price).Returns(111);
            context.Setup(p => p.Message.ItemNo).Returns("ItemNo");

            this.model = new ItemPriceAndCurrencyResponse{ Currency = "Currency" };
            var group = new Groups
            {
                Id = context.Object.Message.ArticleGroup ?? default(int)
            };

            model.Groups = new List<Groups>();
            model.Groups.Add(group);

            group.Item = new List<Item>();
            var item = new Item
            {
                Id = context.Object.Message.ItemNo,
                Name = context.Object.Message.Name,
                Price = context.Object.Message.Price
            };

            this.mapperMock = new Mock<IMapper>().Object;
            this.serviceContextMock = new Mock<PriceCalendarServiceContext>().Object;

            _group = group;
            _item = item;
        }

        [Test]
        public void TestMapFromContextWithItemPriceAndCurrencyResponse()
        {

        }

        [Test]
        public void TestMapFromContextWithGroup()
        { }

        [Test]
        public void TestMapFromContextWithItem()
        {

        }
    }
}
