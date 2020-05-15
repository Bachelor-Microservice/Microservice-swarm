using AutoMapper;
using ContractsV2.ItemContracts;
using ItemContracts;
using MassTransit;
using MassTransit.Courier;
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
    public class UpdateOfItemEntityConsumerTest
    {
        IMapper mapperMock;
        PriceCalendarServiceContext serviceContextMock;
        //MAke these 3 the desired obs
        ItemPriceAndCurrencyResponse DesiredModel;
        ItemPriceAndCurrencyResponse ArgumentModel;
        Groups ArgumentGroup;
        Groups DesiredGroup;
        Item DesiredItem;
        Item ArgumentItem;
        Mock<ConsumeContext<IItemEntityUpdated>> context;

        /*
         Note that setup is called before every test - meaning they wont interfere with each other.
         We can setup both the arguments and the desired model, as the class being tested should return
         the same models, only different levels of nesting
         */
        [SetUp]
        public void SetUp()
        {
            context = new Mock<ConsumeContext<IItemEntityUpdated>>();
            context.Setup(p => p.Message.Unit).Returns("Unit");
            context.Setup(p => p.Message.ArticleGroup).Returns(222);
            context.Setup(p => p.Message.Name).Returns("Name");
            context.Setup(p => p.Message.Price).Returns(111);
            context.Setup(p => p.Message.ItemNo).Returns("ItemNo");

            this.DesiredModel = new ItemPriceAndCurrencyResponse{ Currency = "Unit" };
            DesiredGroup = new Groups
            {
                Id = context.Object.Message.ArticleGroup ?? default
            };

            DesiredModel.Groups = new List<Groups>();
            DesiredModel.Groups.Add(DesiredGroup);

            DesiredGroup.Item = new List<Item>();
            DesiredItem = new Item
            {
                Id = context.Object.Message.ItemNo,
                Name = context.Object.Message.Name,
                Price = context.Object.Message.Price
            };

            this.mapperMock = new Mock<IMapper>().Object;
            this.serviceContextMock = new Mock<PriceCalendarServiceContext>().Object;

            this.ArgumentModel = new ItemPriceAndCurrencyResponse();
            this.ArgumentGroup = new Groups();
            ArgumentGroup.Id = context.Object.Message.ArticleGroup ?? default;
            this.ArgumentItem = new Item();
            ArgumentItem.Id = context.Object.Message.ItemNo;

            ArgumentModel.Groups = new List<Groups>();
            ArgumentModel.Groups.Add(ArgumentGroup);
            ArgumentGroup.Item = new List<Item>();
            ArgumentGroup.Item.Add(ArgumentItem);
        }

        [Test]
        public void TestMapFromContextWithItemPriceAndCurrencyResponse()
        {
            //Most of the setup is done in [SetUp]
            //Actual class is set up here, as to not interfere with changes due to non-conformity
            //Arrange
            var testingClass = new UpdateOfItemEntityConsumer(serviceContextMock, mapperMock);
            //Act
            testingClass.MapFromContext(this.context.Object, this.ArgumentModel);
            //Assert
            //Since the overloaded function with argument ItemPriceAndCurrencyResponse,
            //, we can assert for the full equality of the test and desired model
            //We assert only for equality in non-key references, since the update function does
            // change relations
            Assert.AreEqual(ArgumentModel.Currency, DesiredModel.Currency);
            Assert.AreEqual(ArgumentItem.Name, DesiredItem.Name);
            Assert.AreEqual(ArgumentItem.Price, DesiredItem.Price);
        }

        [Test]
        public void TestMapFromContextWithGroup()
        { }

        [Test]
        public void TestMapFromContextWithItem()
        {
            var testingClass = new UpdateOfItemEntityConsumer(serviceContextMock, mapperMock);
            testingClass.MapFromContext(this.context.Object, this.ArgumentItem);
            Assert.AreEqual(ArgumentItem.Name, DesiredItem.Name);
            Assert.AreEqual(ArgumentItem.Price, DesiredItem.Price);
        }

        [Test]
        public void TestCheckForSuitabilityWithCorrectValues()
        {
            var testingClass = new UpdateOfItemEntityConsumer(serviceContextMock, mapperMock);
            context.Setup(p => p.Message.RelationNo).Returns(999); //Only one not set in setup (not needed for most tests here)
            var result = testingClass.CheckForSuitability(context.Object);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestCheckForSuitabilityWithIncorrectValueArticleGroup()
        {
            var testingClass = new UpdateOfItemEntityConsumer(serviceContextMock, mapperMock);
            context.Setup(p => p.Message.ArticleGroup).Returns(0); //Only one not set in setup (not needed for most tests here)
            var result = testingClass.CheckForSuitability(context.Object);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestCheckForSuitabilityWithIncorrectValueRelationNo()
        {
            var testingClass = new UpdateOfItemEntityConsumer(serviceContextMock, mapperMock);
            context.Setup(p => p.Message.RelationNo).Returns(0); //Only one not set in setup (not needed for most tests here)
            var result = testingClass.CheckForSuitability(context.Object);
            Assert.IsFalse(result);
        }
    }
}
