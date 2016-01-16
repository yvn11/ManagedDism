using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismPackageInfoTest : DismStructTest<DismPackageInfo>
    {
        private readonly List<DismApi.DismCustomProperty_> _customProperties = new List<DismApi.DismCustomProperty_>
        {
            new DismApi.DismCustomProperty_
            {
              Name = "Name1",
              Path = "Path1",
              Value = "Value1",
            },
            new DismApi.DismCustomProperty_
            {
              Name = "Name2",
              Path = "Path2",
              Value = "Value2",
            },
        };

        private readonly List<DismApi.DismFeature_> _features = new List<DismApi.DismFeature_>
        {
            new DismApi.DismFeature_
            {
                FeatureName = "FeatureName1",
                State = DismPackageFeatureState.Installed,
            },
            new DismApi.DismFeature_
            {
                FeatureName = "FeatureName2",
                State = DismPackageFeatureState.Superseded,
            },
        };

        private DismApi.DismPackageInfo_ _packageInfo = new DismApi.DismPackageInfo_
        {
            Applicable = true,
            Company = "Company",
            Copyright = "Copyright",
            CreationTime = DateTime.Today,
            Description = "Description",
            DisplayName = "DisplayName",
            FullyOffline = DismFullyOfflineInstallableType.FullyOfflineInstallable,
            InstallClient = "InstallClient",
            InstallPackageName = "InstallPackageName",
            InstallTime = DateTime.Today,
            LastUpdateTime = DateTime.Today,
            PackageName = "PackageName",
            PackageState = DismPackageFeatureState.Staged,
            ProductName = "ProductName",
            ProductVersion = "1.0.0.0",
            ReleaseType = DismReleaseType.ServicePack,
            RestartRequired = DismRestartType.Required,
            SupportInformation = "SupportInformation",
        };

        protected override DismPackageInfo Item
        {
            get
            {
                return new DismPackageInfo(_packageInfo);
            }
        }

        protected override object Struct
        {
            get
            {
                return _packageInfo;
            }
        }

        [TearDown]
        public void TestCleanup()
        {
            Marshal.FreeHGlobal(_packageInfo.CustomProperty);

            Marshal.FreeHGlobal(_packageInfo.Feature);
        }

        [SetUp]
        public void TestInitialize()
        {
            _packageInfo.CustomProperty = ListToPtrArray(_customProperties);
            _packageInfo.CustomPropertyCount = (uint)_customProperties.Count;

            _packageInfo.Feature = ListToPtrArray(_features);
            _packageInfo.FeatureCount = (uint)_features.Count;
        }

        protected override void VerifyProperties(DismPackageInfo item)
        {
            Assert.AreEqual(true, item.Applicable, "Applicable is incorrect");
            Assert.AreEqual("Company", item.Company, "Company is incorrect");
            Assert.AreEqual("Copyright", item.Copyright, "Copyright is incorrect");
            Assert.AreEqual(DateTime.Today.ToUniversalTime(), item.CreationTime, "CreationTime is incorrect");
            Assert.AreEqual("Description", item.Description, "Description is incorrect");
            Assert.AreEqual("DisplayName", item.DisplayName, "DisplayName is incorrect");
            Assert.AreEqual(DismFullyOfflineInstallableType.FullyOfflineInstallable, item.FullyOffline, "FullyOffline is incorrect");
            Assert.AreEqual("InstallClient", item.InstallClient, "InstallClient is incorrect");
            Assert.AreEqual("InstallPackageName", item.InstallPackageName, "InstallPackageName is incorrect");
            Assert.AreEqual(DateTime.Today.ToUniversalTime(), item.InstallTime, "InstallTime is incorrect");
            Assert.AreEqual(DateTime.Today.ToUniversalTime(), item.LastUpdateTime, "LastUpdateTime is incorrect");
            Assert.AreEqual("PackageName", item.PackageName, "PackageName is incorrect");
            Assert.AreEqual(DismPackageFeatureState.Staged, item.PackageState, "PackageState is incorrect");
            Assert.AreEqual("ProductName", item.ProductName, "ProductName is incorrect");
            Assert.AreEqual("1.0.0.0", item.ProductVersion, "ProductVersion is incorrect");
            Assert.AreEqual(DismReleaseType.ServicePack, item.ReleaseType, "ReleaseType is incorrect");
            Assert.AreEqual(DismRestartType.Required, item.RestartRequired, "RestartRequired is incorrect");
            Assert.AreEqual("SupportInformation", item.SupportInformation, "SupportInformation is incorrect");

            var customProperties = new DismCustomPropertyCollection(_customProperties.Select(i => new DismCustomProperty(i)).ToList());

            CollectionAssert.AreEqual(customProperties, item.CustomProperties, "CustomProperties is incorrect");

            var features = new DismFeatureCollection(_features.Select(i => new DismFeature(i)).ToList());

            CollectionAssert.AreEqual(features, item.Features, new DismFeatureComparer(), "Features is incorrect");
        }
    }

    public class DismFeatureComparer : IComparer<DismFeature>, IComparer
    {
        public int Compare(DismFeature x, DismFeature y)
        {
            return x.FeatureName == y.FeatureName && x.State == y.State ? 0 : 1;
        }

        public int Compare(object x, object y)
        {
            return Compare((DismFeature)x, (DismFeature)y);
        }
    }
}