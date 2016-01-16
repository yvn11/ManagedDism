using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismImageInfoCollectionTest : DismCollectionTest<DismImageInfoCollection, DismImageInfo>
    {
        protected override DismImageInfoCollection CreateCollection(List<DismImageInfo> expectedCollection)
        {
            return new DismImageInfoCollection(expectedCollection);
        }

        protected override DismImageInfoCollection CreateCollection()
        {
            return new DismImageInfoCollection();
        }

        protected override List<DismImageInfo> GetCollection()
        {
            return new List<DismImageInfo>
            {
                new DismImageInfo(new DismApi.DismImageInfo_
                {
                    Architecture = DismProcessorArchitecture.IA64,
                    Bootable = DismImageBootable.ImageBootableYes,
                    Build = 1000,
                    EditionId = "EditionId",
                    Hal = "Hal",
                    ImageDescription = "ImageDescription",
                    ImageIndex = 2,
                    ImageName = "ImageName",
                    ImageSize = 999,
                    ImageType = DismImageType.Wim,
                    InstallationType = "InstallationType",
                    MajorVersion = 2,
                    MinorVersion = 3,
                    ProductName = "ProductName",
                    ProductSuite = "ProductSuite",
                    ProductType = "ProductType",
                    SpBuild = 4,
                    SpLevel = 5,
                    SystemRoot = "SystemRoot",
                })
            };
        }
    }

    [TestFixture]
    public class DismImageInfoTest : DismStructTest<DismImageInfo>
    {
        private readonly DismApi.DismWimCustomizedInfo_ _wimCustomizedInfo = new DismApi.DismWimCustomizedInfo_
        {
            CreatedTime = DateTime.Today.AddDays(-7),
            DirectoryCount = 1234,
            FileCount = 5678,
            ModifiedTime = DateTime.Today,
            Size = 10,
        };

        private DismApi.DismImageInfo_ _imageInfo = new DismApi.DismImageInfo_
        {
            Architecture = DismProcessorArchitecture.IA64,
            Bootable = DismImageBootable.ImageBootableYes,
            Build = 1000,
            EditionId = "EditionId",
            Hal = "Hal",
            ImageDescription = "ImageDescription",
            ImageIndex = 2,
            ImageName = "ImageName",
            ImageSize = 999,
            ImageType = DismImageType.Wim,
            InstallationType = "InstallationType",
            MajorVersion = 2,
            MinorVersion = 3,
            ProductName = "ProductName",
            ProductSuite = "ProductSuite",
            ProductType = "ProductType",
            SpBuild = 4,
            SpLevel = 5,
            SystemRoot = "SystemRoot",
        };

        private List<DismApi.DismLanguage> _languages = new List<DismApi.DismLanguage>
        {
            new DismApi.DismLanguage
            {
                Value = "en-us",
            },
            new DismApi.DismLanguage
            {
                Value = "es-es",
            },
        };

        protected override DismImageInfo Item
        {
            get
            {
                return ItemPtr != IntPtr.Zero ? new DismImageInfo(ItemPtr) : new DismImageInfo(_imageInfo);
            }
        }

        protected override object Struct
        {
            get
            {
                return _imageInfo;
            }
        }

        [TearDown]
        public void TestCleanup()
        {
            Marshal.FreeHGlobal(_imageInfo.Language);

            Marshal.FreeHGlobal(_imageInfo.CustomizedInfo);
        }

        [SetUp]
        public void TestInitialize()
        {
            _imageInfo.Language = ListToPtrArray(_languages);
            _imageInfo.LanguageCount = (uint)_languages.Count;
            _imageInfo.DefaultLanguageIndex = 1;

            _imageInfo.CustomizedInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DismApi.DismWimCustomizedInfo_)));

            Marshal.StructureToPtr(_wimCustomizedInfo, _imageInfo.CustomizedInfo, false);
        }

        protected override void VerifyProperties(DismImageInfo item)
        {
            var languages = _languages.Select(i => new CultureInfo(i.Value)).ToList();

            Assert.AreEqual(DismProcessorArchitecture.IA64, item.Architecture, "Architecture is incorrect");
            Assert.AreEqual(DismImageBootable.ImageBootableYes, item.Bootable, "Bootable is incorrect");
            Assert.AreEqual(1000, item.ProductVersion.Build, "ProductVersion.Build is incorrect");
            Assert.AreEqual("EditionId", item.EditionId, "EditionId is incorrect");
            Assert.AreEqual("Hal", item.Hal, "Hal is incorrect");
            Assert.AreEqual("ImageDescription", item.ImageDescription, "ImageDescription is incorrect");
            Assert.AreEqual(2, item.ImageIndex, "ImageIndex is incorrect");
            Assert.AreEqual("ImageName", item.ImageName, "ImageName is incorrect");
            Assert.AreEqual((UInt64)999, item.ImageSize, "ImageSize is incorrect");
            Assert.AreEqual(DismImageType.Wim, item.ImageType, "ImageType is incorrect");
            Assert.AreEqual("InstallationType", item.InstallationType, "InstallationType is incorrect");
            Assert.AreEqual(2, item.ProductVersion.Major, "ProductVersion.Major is incorrect");
            Assert.AreEqual(3, item.ProductVersion.Minor, "ProductVersion.Minor is incorrect");
            Assert.AreEqual("ProductName", item.ProductName, "ProductName is incorrect");
            Assert.AreEqual("ProductSuite", item.ProductSuite, "ProductSuite is incorrect");
            Assert.AreEqual("ProductType", item.ProductType, "ProductType is incorrect");
            Assert.AreEqual(4, item.ProductVersion.Revision, "ProductVersion.Revision is incorrect");
            Assert.AreEqual(5, item.SpLevel, "SpLevel is incorrect");
            Assert.AreEqual("SystemRoot", item.SystemRoot, "InstallationType is incorrect");
            Assert.AreEqual(1, item.DefaultLanguageIndex, "DefaultLanguageIndex is incorrect");
            CollectionAssert.AreEqual(languages, item.Languages.ToList(), "Languages is incorrect");
            Assert.AreEqual(new CultureInfo("es-es"), item.DefaultLanguage, "DefaultLanguage is incorrect");

            Assert.AreEqual(DateTime.Today.AddDays(-7).ToUniversalTime(), item.CustomizedInfo.CreatedTime, "CustomizedInfo.CreatedTime is incorrect");
            Assert.AreEqual(1234, item.CustomizedInfo.DirectoryCount, "CustomizedInfo.DirectoryCount is incorrect");
            Assert.AreEqual(5678, item.CustomizedInfo.FileCount, "CustomizedInfo.FileCount is incorrect");
            Assert.AreEqual(DateTime.Today.ToUniversalTime(), item.CustomizedInfo.ModifiedTime, "CustomizedInfo.ModifiedTime is incorrect");
            Assert.AreEqual(10, item.CustomizedInfo.Size, "CustomizedInfo.Size is incorrect");
        }
    }
}