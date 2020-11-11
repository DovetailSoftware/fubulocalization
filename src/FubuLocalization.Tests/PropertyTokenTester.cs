using FubuCore.Reflection;
using NUnit.Framework;
using System.Globalization;

namespace FubuLocalization.Tests
{
    [TestFixture]
    public class PropertyTokenTester
    {
        [Test]
        public void find_default_header_for_the_attribute()
        {
            var token = new PropertyToken
            {
                ParentType = typeof(PropertyTokenTarget),
                PropertyName = "Name"
            };

            token.FindDefaultHeader(new CultureInfo("en-US")).ShouldEqual("The Name");
            token.FindDefaultHeader(new CultureInfo("en-CA")).ShouldEqual("Different");
            token.FindDefaultHeader(new CultureInfo("ja-JP")).ShouldBeNull();
        }

        [Test]
        public void find_default_header_for_property_that_is_not_decorated_should_just_return_null()
        {
            var token = new PropertyToken
            {
                ParentType = typeof(PropertyTokenTarget),
                PropertyName = "NotDecorated"
            };

            token.FindDefaultHeader(new CultureInfo("en-US")).ShouldBeNull();
        }

        [Test]
        public void generic_parent_type_is_pretty()
        {
            // Ensures we don't have assembly info when using generics.
            var prop = ReflectionHelper.GetProperty<GenericPropertyTokenTarget<PropertyTokenTarget>>(_ => _.Name);
            var token = new PropertyToken(prop);
            token.ParentTypeName.ShouldEqual(prop.DeclaringType.ToString());
        }
    }

    public class PropertyTokenTarget
    {
        [HeaderText("The Name"), HeaderText("Different", Culture = "en-CA")]
        public string Name { get; set; }

        public string NotDecorated { get; set; }
    }

    public class GenericPropertyTokenTarget<T>
    {
        [HeaderText("The Name"), HeaderText("Different", Culture = "en-CA")]
        public string Name { get; set; }

        public string NotDecorated { get; set; }
    }
}