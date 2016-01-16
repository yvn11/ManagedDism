using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismFeatureInfoTest : DismStructTest<DismFeatureInfo>
    {
        private readonly List<DismApi.DismCustomProperty_> _customProperties = new List<DismApi.DismCustomProperty_>
        {
            new DismApi.DismCustomProperty_
            {
                Name = "Property1",
                Path = "Path1",
                Value = "Value1",
            },
            new DismApi.DismCustomProperty_
            {
                Name = "Property2",
                Path = "Path2",
                Value = "Value2",
            },
        };

        private DismApi.DismFeatureInfo_ _featureInfo = new DismApi.DismFeatureInfo_
        {
            Description = "Description",
            DisplayName = "DisplayName",
            FeatureName = "FeatureName",
            FeatureState = DismPackageFeatureState.Installed,
            RestartRequired = DismRestartType.Possible,
        };

        protected override DismFeatureInfo Item
        {
            get
            {
                return ItemPtr != IntPtr.Zero ? new DismFeatureInfo(ItemPtr) : new DismFeatureInfo(_featureInfo);
            }
        }

        protected override object Struct
        {
            get
            {
                return _featureInfo;
            }
        }

        [TearDown]
        public void TestCleanup()
        {
            Marshal.FreeHGlobal(_featureInfo.CustomProperty);
        }

        [SetUp]
        public void TestInitialize()
        {
            _featureInfo.CustomProperty = ListToPtrArray(_customProperties);

            _featureInfo.CustomPropertyCount = (uint)_customProperties.Count;
        }

        protected override void VerifyProperties(DismFeatureInfo item)
        {
            var customProperties = new DismCustomPropertyCollection(_customProperties.Select(i => new DismCustomProperty(i)).ToList());

            CollectionAssert.AreEqual(customProperties, item.CustomProperties, "CustomProperties is incorrect");

            Assert.AreEqual("Description", item.Description, "Description is incorrect");
            Assert.AreEqual("DisplayName", item.DisplayName, "DisplayName is incorrect");
            Assert.AreEqual("FeatureName", item.FeatureName, "FeatureName is incorrect");
            Assert.AreEqual(DismPackageFeatureState.Installed, item.FeatureState, "FeatureState is incorrect");
            Assert.AreEqual(DismRestartType.Possible, item.RestartRequired, "RestartRequired is incorrect");
        }
    }
}