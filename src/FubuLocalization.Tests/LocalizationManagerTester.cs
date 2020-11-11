using System.Reflection;
using FubuCore.Reflection;
using Moq;
using NUnit.Framework;

namespace FubuLocalization.Tests
{
    [TestFixture]
    public class LocalizationManagerTester
    {
        private Mock<ILocalizationDataProvider> _provider;

        [SetUp]
        public void SetUp()
        {
            _provider = new Mock<ILocalizationDataProvider>(MockBehavior.Strict);

            LocalizationManager.Stub(_provider.Object);
        }

        [TearDown]
        public void TearDown()
        {
            LocalizationManager.Stub();
        }

        [Test]
        public void GetText_for_property()
        {
            var property = ReflectionHelper.GetProperty<DummyEntity>(c => c.SimpleProperty);
            _provider.Setup(_ => _.GetHeader(property)).Returns("TheText");

            LocalizationManager.GetText(property).ShouldEqual("TheText");
        }

        [Test]
        public void GetText_for_property_by_expression()
        {
            var property = ReflectionHelper.GetProperty<DummyEntity>(c => c.SimpleProperty);
            _provider.Setup(_ => _.GetHeader(property)).Returns("TheText");

            LocalizationManager.GetText<DummyEntity>(c => c.SimpleProperty).ShouldEqual("TheText");
        }

        [Test]
        public void GetHeader_for_property()
        {
            var property = ReflectionHelper.GetProperty<DummyEntity>(c => c.SimpleProperty);
            var theHeader = "TheText";

            _provider.Setup(_ => _.GetHeader(property)).Returns(theHeader);

            LocalizationManager.GetHeader(property).ShouldEqual(theHeader);
        }

        [Test]
        public void GetHeader_for_property_by_expression()
        {
            PropertyInfo property = ReflectionHelper.GetProperty<DummyEntity>(c => c.SimpleProperty);
            var theHeader = "TheText";

            _provider.Setup(_ => _.GetHeader(property)).Returns(theHeader);

            LocalizationManager.GetHeader<DummyEntity>(c => c.SimpleProperty).ShouldEqual(theHeader);
        }

        public class DummyEntity
        {
            public string SimpleProperty { get; set; }
        }
    }
}