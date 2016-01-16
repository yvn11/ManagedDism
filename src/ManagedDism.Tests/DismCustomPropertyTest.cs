using System;
using System.Collections.Generic;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismCustomPropertyCollectionTest : DismCollectionTest<DismCustomPropertyCollection, DismCustomProperty>
    {
        protected override DismCustomPropertyCollection CreateCollection(List<DismCustomProperty> expectedCollection)
        {
            return new DismCustomPropertyCollection(expectedCollection);
        }

        protected override DismCustomPropertyCollection CreateCollection()
        {
            return new DismCustomPropertyCollection();
        }

        protected override List<DismCustomProperty> GetCollection()
        {
            return new List<DismCustomProperty>
            {
                new DismCustomProperty(new DismApi.DismCustomProperty_
                {
                    Name = "Name1",
                    Path = "Path1",
                    Value = "Value1",
                }),
                new DismCustomProperty(new DismApi.DismCustomProperty_
                {
                    Name = "Name2",
                    Path = "Path2",
                    Value = "Value2",
                }),
            };
        }
    }

    [TestFixture]
    public class DismCustomPropertyTest : DismStructTest<DismCustomProperty>
    {
        private readonly DismApi.DismCustomProperty_ _customPropertyStruct = new DismApi.DismCustomProperty_()
        {
            Name = "Name",
            Path = "Path",
            Value = "Value",
        };

        protected override DismCustomProperty Item
        {
            get
            {
                return ItemPtr != IntPtr.Zero ? new DismCustomProperty(ItemPtr) : new DismCustomProperty(_customPropertyStruct);
            }
        }

        protected override object Struct
        {
            get
            {
                return _customPropertyStruct;
            }
        }

        protected override void VerifyProperties(DismCustomProperty customProperty)
        {
            Assert.AreEqual("Name", customProperty.Name, "Name is incorrect");
            Assert.AreEqual("Path", customProperty.Path, "Path is incorrect");
            Assert.AreEqual("Value", customProperty.Value, "Value is incorrect");
        }
    }
}