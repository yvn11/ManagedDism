using System;
using System.Collections.Generic;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismDriverPackageCollectionTest : DismCollectionTest<DismDriverPackageCollection, DismDriverPackage>
    {
        protected override DismDriverPackageCollection CreateCollection(List<DismDriverPackage> expectedCollection)
        {
            return new DismDriverPackageCollection(expectedCollection);
        }

        protected override DismDriverPackageCollection CreateCollection()
        {
            return new DismDriverPackageCollection();
        }

        protected override List<DismDriverPackage> GetCollection()
        {
            return new List<DismDriverPackage>
            {
                new DismDriverPackage(new DismApi.DismDriverPackage_
                {
                   BootCritical = true,
                   Build = 1000,
                   CatalogFile = "CatalogFile",
                   ClassDescription = "ClassDescription",
                   ClassGuid = "ClassGuid",
                   ClassName = "ClassName",
                   Date = DateTime.Today,
                   DriverSignature = DismDriverSignature.Signed,
                   InBox = true,
                   MajorVersion = 1,
                   MinorVersion = 0,
                   OriginalFileName = "OriginalFileName",
                   ProviderName = "ProviderName",
                   PublishedName = "PublishedName",
                   Revision = 2,
                }),
            };
        }
    }

    [TestFixture]
    public class DismDriverPackageTest : DismStructTest<DismDriverPackage>
    {
        private readonly DismApi.DismDriverPackage_ _driverPackage = new DismApi.DismDriverPackage_
        {
            BootCritical = true,
            Build = 1000,
            CatalogFile = "CatalogFile",
            ClassDescription = "ClassDescription",
            ClassGuid = "ClassGuid",
            ClassName = "ClassName",
            Date = DateTime.Today,
            DriverSignature = DismDriverSignature.Signed,
            InBox = true,
            MajorVersion = 1,
            MinorVersion = 2,
            OriginalFileName = "OriginalFileName",
            ProviderName = "ProviderName",
            PublishedName = "PublishedName",
            Revision = 3,
        };

        protected override DismDriverPackage Item
        {
            get
            {
                return ItemPtr != IntPtr.Zero ? new DismDriverPackage(ItemPtr) : new DismDriverPackage(_driverPackage);
            }
        }

        protected override object Struct
        {
            get
            {
                return _driverPackage;
            }
        }

        protected override void VerifyProperties(DismDriverPackage item)
        {
            Assert.AreEqual(true, item.BootCritical, "BootCritical is incorrect");
            Assert.AreEqual(1000, item.Version.Build, "Version.Build is incorrect");
            Assert.AreEqual("CatalogFile", item.CatalogFile, "CatalogFile is incorrect");
            Assert.AreEqual("ClassDescription", item.ClassDescription, "ClassDescription is incorrect");
            Assert.AreEqual("ClassGuid", item.ClassGuid, "ClassGuid is incorrect");
            Assert.AreEqual("ClassName", item.ClassName, "ClassName is incorrect");
            Assert.AreEqual(DateTime.Today.ToUniversalTime(), item.Date, "Date is incorrect");
            Assert.AreEqual(DismDriverSignature.Signed, item.DriverSignature, "DriverSignature is incorrect");
            Assert.AreEqual(true, item.InBox, "InBox is incorrect");
            Assert.AreEqual(1, item.Version.Major, "Version.Major is incorrect");
            Assert.AreEqual(2, item.Version.Minor, "Version.Minor is incorrect");
            Assert.AreEqual("OriginalFileName", item.OriginalFileName, "OriginalFileName is incorrect");
            Assert.AreEqual("ProviderName", item.ProviderName, "ProviderName is incorrect");
            Assert.AreEqual("PublishedName", item.PublishedName, "PublishedName is incorrect");
            Assert.AreEqual(3, item.Version.Revision, "Version.Minor is incorrect");
        }
    }
}