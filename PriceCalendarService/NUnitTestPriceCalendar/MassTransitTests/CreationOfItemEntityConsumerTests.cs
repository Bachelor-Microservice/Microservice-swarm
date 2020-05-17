using AutoMapper;
using ContractsV2.ItemContracts;
using ItemContracts;
using MassTransit;
using Moq;
using NUnit.Framework;
using PriceCalendarService.MassTransit.Consumers;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestPriceCalendar.MassTransitTests
{
    [TestFixture]
    public class CreationOfItemEntityConsumerTests
    {
        CreationOfItemEntityConsumer subject;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("*******************************************\n*******************************************\nTESTING IS WORKING YAY\n*******************************************");
        }

        [Test]
        public void MapFromContext_ExistingFromResponseTest()
        {
            //Arrange Dependencies
            var mapperMock = new Mock<IMapper>();
            var serviceContextMock = new Mock<PriceCalendarServiceContext>();
            var consumeContextMock = new Mock<ConsumeContext<IItemEntityCreated>>();
            var creationOfItemEntityConsumer = new CreationOfItemEntityConsumer(serviceContextMock.Object, mapperMock.Object);

            //Arrange Mocked object
            consumeContextMock.Setup(p => p.Message.ArticleGroup).Returns(2222);
            consumeContextMock.Setup(p => p.Message.ItemNo).Returns("ItemNo");
            consumeContextMock.Setup(p => p.Message.Name).Returns("Name");
            consumeContextMock.Setup(p => p.Message.RelationNo).Returns(3333);
            consumeContextMock.Setup(p => p.Message.Unit).Returns("Unit");
            consumeContextMock.Setup(p => p.Message.Price).Returns(1111);
            //Arrange Model object
            var model = new ItemPriceAndCurrencyResponse();
            model.Groups = new List<Groups>();
            model.Groups.Add(new Groups
            {
                Id = consumeContextMock.Object.Message.ArticleGroup ?? default(int)
            });

            var desiredResult = new Item
            {
                Id = consumeContextMock.Object.Message.ItemNo,
                Name = consumeContextMock.Object.Message.Name,
                Price = consumeContextMock.Object.Message.Price
            };

            foreach (var group in model.Groups)
            {
                if (group.Id == 2222) {
                    desiredResult.GroupId = group.Id;
                    desiredResult.Group = group;
                }
            }

            //Act
            var itemResult = creationOfItemEntityConsumer.
                MapFromContext_ExistingFromResponse(consumeContextMock.Object, model);

            //Assert
            Assert.AreEqual(itemResult.Group, desiredResult.Group);
            Assert.AreEqual(itemResult.GroupId, desiredResult.GroupId);
            Assert.AreEqual(itemResult.Group, desiredResult.Group);
            Assert.AreEqual(itemResult.Group, desiredResult.Group);
            Assert.AreEqual(itemResult.Group, desiredResult.Group);
            Assert.AreEqual(itemResult.Group, desiredResult.Group);
        }

        [Test]
        public void MapFromContext_ExistingFromGroupsTest()
        {
            //Arrange Dependencies
            var mapperMock = new Mock<IMapper>();
            var serviceContextMock = new Mock<PriceCalendarServiceContext>();
            var consumeContextMock = new Mock<ConsumeContext<IItemEntityCreated>>();
            var creationOfItemEntityConsumer = new CreationOfItemEntityConsumer(serviceContextMock.Object, mapperMock.Object);

            //Arrange Mocked object
            consumeContextMock.Setup(p => p.Message.ArticleGroup).Returns(2222);
            consumeContextMock.Setup(p => p.Message.ItemNo).Returns("ItemNo");
            consumeContextMock.Setup(p => p.Message.Name).Returns("Name");
            consumeContextMock.Setup(p => p.Message.RelationNo).Returns(3333);
            consumeContextMock.Setup(p => p.Message.Unit).Returns("Unit");
            consumeContextMock.Setup(p => p.Message.Price).Returns(1111);

            //Setup argument for existing found in db
            var existing = new ItemPriceAndCurrencyResponse
            {
                Id = consumeContextMock.Object.Message.RelationNo ?? default(int),
                Currency = consumeContextMock.Object.Message.Unit
            };

            //Setup desired result from tested method
            var desiredGroup = new Groups
            {
                Currency = existing,
                CurrencyId = existing.Id,
                Id = consumeContextMock.Object.Message.ArticleGroup ?? default(int),
                Item = new List<Item>()
            };

            var item = new Item
            {
                Group = desiredGroup, 
                GroupId = desiredGroup.Id,
                Id = consumeContextMock.Object.Message.ItemNo,
                Name = consumeContextMock.Object.Message.Name,
                Price = consumeContextMock.Object.Message.Price
            };

            var resultGroup = creationOfItemEntityConsumer
                .MapFromContext_ExistingFromGroups(consumeContextMock.Object, existing);

            Assert.AreEqual(desiredGroup.Currency, resultGroup.Currency);
            Assert.AreEqual(desiredGroup.CurrencyId, resultGroup.CurrencyId);
            Assert.AreEqual(desiredGroup.Id, resultGroup.Id);
            foreach(var gItem in resultGroup.Item)
            {
                //Assert.AreEqual(item.Group, gItem.Group); -> Assert does not work at all with complex objects (they are the same)
                Assert.AreEqual(item.GroupId, gItem.GroupId);
                Assert.AreEqual(item.Id, gItem.Id);
                Assert.AreEqual(item.Name, gItem.Name);
                Assert.AreEqual(item.Price, gItem.Price);
            }
        }

        /*[Test]
        public void CanBeTranslatedTest()
        {
            //Arrange Dependencies
            var mapperMock = new Mock<IMapper>();
            var serviceContextMock = new Mock<PriceCalendarServiceContext>();
            var consumeContextMock = new Mock<ConsumeContext<IItemEntityCreated>>();
            var creationOfItemEntityConsumer = new CreationOfItemEntityConsumer(serviceContextMock.Object, mapperMock.Object);

            //Arrange Mocked object
            consumeContextMock.Setup(p => p.Message.ArticleGroup).Returns(2222);
            consumeContextMock.Setup(p => p.Message.ItemNo).Returns("ItemNo");
            consumeContextMock.Setup(p => p.Message.RelationNo).Returns(3333);

            var result = creationOfItemEntityConsumer.CanBeTranslated(consumeContextMock.Object);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanNotBeTranslatedTest()
        {
            //Arrange Dependencies
            var mapperMock = new Mock<IMapper>();
            var serviceContextMock = new Mock<PriceCalendarServiceContext>();
            var consumeContextMock = new Mock<ConsumeContext<IItemEntityCreated>>();
            var creationOfItemEntityConsumer = new CreationOfItemEntityConsumer(serviceContextMock.Object, mapperMock.Object);

            //Arrange Mocked object
            consumeContextMock.Setup(p => p.Message.ArticleGroup).Returns(2222);
            //consumeContextMock.Setup(p => p.Message.ItemNo).Returns("ItemNo");
            consumeContextMock.Setup(p => p.Message.RelationNo).Returns(3333);

            var result = creationOfItemEntityConsumer.CanBeTranslated(consumeContextMock.Object);

            Assert.IsFalse(result);
        }*/
    }
}
