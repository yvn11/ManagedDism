using System;
using System.Collections.Generic;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismPackageCollectionTest : DismCollectionTest<DismPackageCollection, DismPackage>
    {
        protected override DismPackageCollection CreateCollection(List<DismPackage> expectedCollection)
        {
            return new DismPackageCollection(expectedCollection);
        }

        protected override DismPackageCollection CreateCollection()
        {
            return new DismPackageCollection();
        }

        protected override List<DismPackage> GetCollection()
        {
            return new List<DismPackage>
            {
                new DismPackage(new DismApi.DismPackage_
                {
                   InstallTime = DateTime.Today,
                   PackageName = "PackageName1",
                   PackageState = DismPackageFeatureState.Installed,
                   ReleaseType = DismReleaseType.LanguagePack,
                }),
                new DismPackage(new DismApi.DismPackage_
                {
                   InstallTime = DateTime.Today,
                   PackageName = "PackageName2",
                   PackageState = DismPackageFeatureState.Removed,
                   ReleaseType = DismReleaseType.LanguagePack,
                }),
            };
        }
    }

    [TestFixture]
    public class DismPackageTest : DismStructTest<DismPackage>
    {
        private readonly DismApi.DismPackage_ _package = new DismApi.DismPackage_
        {
            InstallTime = DateTime.Today,
            PackageName = "PackageName",
            PackageState = DismPackageFeatureState.Removed,
            ReleaseType = DismReleaseType.SecurityUpdate,
        };

        protected override DismPackage Item
        {
            get
            {
                return new DismPackage(_package);
            }
        }

        protected override object Struct
        {
            get
            {
                return _package;
            }
        }

        protected override void VerifyProperties(DismPackage item)
        {
            Assert.AreEqual(DateTime.Today.ToUniversalTime(), item.InstallTime, "InstallTime is incorrect");
            Assert.AreEqual("PackageName", item.PackageName, "PackageName is incorrect");
            Assert.AreEqual(DismPackageFeatureState.Removed, item.PackageState, "PackageState is incorrect");
            Assert.AreEqual(DismReleaseType.SecurityUpdate, item.ReleaseType, "ReleaseType is incorrect");
        }
    }
}