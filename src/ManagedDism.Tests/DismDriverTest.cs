using System;
using System.Collections.Generic;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismDriverCollectionTest : DismCollectionTest<DismDriverCollection, DismDriver>
    {
        protected override DismDriverCollection CreateCollection(List<DismDriver> expectedCollection)
        {
            return new DismDriverCollection(expectedCollection);
        }

        protected override DismDriverCollection CreateCollection()
        {
            return new DismDriverCollection();
        }

        protected override List<DismDriver> GetCollection()
        {
            return new List<DismDriver>
            {
                new DismDriver(new DismApi.DismDriver_
                {
                   Architecture = (ushort)DismProcessorArchitecture.AMD64,
                   CompatibleIds = "CompatibleIds",
                   ExcludeIds = "ExcludeIds",
                   HardwareDescription = "HardwareDescription",
                   HardwareId = "HardwareId",
                   ManufacturerName = "ManufacturerName",
                   ServerName = "ServerName",
                }),
            };
        }
    }

    [TestFixture]
    public class DismDriverTest : DismStructTest<DismDriver>
    {
        private readonly DismApi.DismDriver_ _driver = new DismApi.DismDriver_
        {
            Architecture = 9,
            CompatibleIds = "CompatibleIds",
            ExcludeIds = "ExcludeIds",
            HardwareDescription = "HardwareDescription",
            HardwareId = "HardwareId",
            ManufacturerName = "ManufacturerName",
            ServerName = "ServerName",
        };

        protected override DismDriver Item
        {
            get
            {
                return ItemPtr != IntPtr.Zero ? new DismDriver(ItemPtr) : new DismDriver(_driver);
            }
        }

        protected override object Struct
        {
            get
            {
                return _driver;
            }
        }

        protected override void VerifyProperties(DismDriver item)
        {
            Assert.AreEqual(DismProcessorArchitecture.AMD64, item.Architecture, "Architecture is incorrect");
            Assert.AreEqual("CompatibleIds", item.CompatibleIds, "CompatibleIds is incorrect");
            Assert.AreEqual("ExcludeIds", item.ExcludeIds, "ExcludeIds is incorrect");
            Assert.AreEqual("HardwareDescription", item.HardwareDescription, "HardwareDescription is incorrect");
            Assert.AreEqual("HardwareId", item.HardwareId, "HardwareId is incorrect");
            Assert.AreEqual("ManufacturerName", item.ManufacturerName, "ManufacturerName is incorrect");
            Assert.AreEqual("ServerName", item.ServerName, "ServerName is incorrect");
        }
    }
}