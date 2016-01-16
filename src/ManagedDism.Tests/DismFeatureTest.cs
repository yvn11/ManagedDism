using System;
using System.Collections.Generic;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismFeatureCollectionTest : DismCollectionTest<DismFeatureCollection, DismFeature>
    {
        protected override DismFeatureCollection CreateCollection(List<DismFeature> expectedCollection)
        {
            return new DismFeatureCollection(expectedCollection);
        }

        protected override DismFeatureCollection CreateCollection()
        {
            return new DismFeatureCollection();
        }

        protected override List<DismFeature> GetCollection()
        {
            return new List<DismFeature>
            {
                new DismFeature(new DismApi.DismFeature_
                {
                    FeatureName = "FeatureName1",
                    State = DismPackageFeatureState.PartiallyInstalled,
                }),
                new DismFeature(new DismApi.DismFeature_
                {
                    FeatureName = "FeatureName2",
                    State = DismPackageFeatureState.Superseded,
                }),
            };
        }
    }

    [TestFixture]
    public class DismFeatureTest : DismStructTest<DismFeature>
    {
        private readonly DismApi.DismFeature_ _feature = new DismApi.DismFeature_
        {
            FeatureName = "FeatureName",
            State = DismPackageFeatureState.PartiallyInstalled,
        };

        protected override DismFeature Item
        {
            get
            {
                return ItemPtr != IntPtr.Zero ? new DismFeature(ItemPtr) : new DismFeature(_feature);
            }
        }

        protected override object Struct
        {
            get
            {
                return _feature;
            }
        }

        protected override void VerifyProperties(DismFeature item)
        {
            Assert.AreEqual("FeatureName", item.FeatureName, "FeatureName is incorrect");
            Assert.AreEqual(DismPackageFeatureState.PartiallyInstalled, item.State, "State is incorrect");
        }
    }
}