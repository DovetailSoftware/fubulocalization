using FubuCore.Reflection;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuLocalization.Tests
{
    [TestFixture]
    public class DefaultLocalizationManagerTester
    {
        private MockRepository _mocks;
        private ILocalizationDataProvider _provider;
        private DefaultLocalizationManager _manager;

        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();
            _provider = _mocks.StrictMock<ILocalizationDataProvider>();
            _manager = new DefaultLocalizationManager(_provider);
        }

        [Test]
        public void GetText_for_property()
        {
            var property = ReflectionHelper.GetProperty<DummyEntity>(c => c.SimpleProperty);

            using (_mocks.Record())
            {
                Expect.Call(_provider.GetHeader(property)).Return("TheText");
            }

            using (_mocks.Playback())
            {
                _manager.GetText(property).ShouldEqual("TheText");
            }
        }

        [Test]
        public void GetText_for_property_by_expression()
        {
            var property = ReflectionHelper.GetProperty<DummyEntity>(c => c.SimpleProperty);

            using (_mocks.Record())
            {
                Expect.Call(_provider.GetHeader(property)).Return("TheText");
            }

            using (_mocks.Playback())
            {
                _manager.GetText<DummyEntity>(c => c.SimpleProperty).ShouldEqual("TheText");
            }
        }

        [Test]
        public void GetHeader_for_property()
        {
            var property = ReflectionHelper.GetProperty<DummyEntity>(c => c.SimpleProperty);
            var theHeader = "TheText";

            using (_mocks.Record())
            {
                Expect.Call(_provider.GetHeader(property)).Return(theHeader);
            }

            using (_mocks.Playback())
            {
                _manager.GetHeader(property).ShouldEqual(theHeader);
            }
        }

        [Test]
        public void GetHeader_for_property_by_expression()
        {
            var property = ReflectionHelper.GetProperty<DummyEntity>(c => c.SimpleProperty);
            var theHeader = "TheText";

            using (_mocks.Record())
            {
                Expect.Call(_provider.GetHeader(property)).Return(theHeader);
            }

            using (_mocks.Playback())
            {
                _manager.GetHeader<DummyEntity>(c => c.SimpleProperty).ShouldEqual(theHeader);
            }
        }

        public class DummyEntity
        {
            public string SimpleProperty { get; set; }
        }
    }
}