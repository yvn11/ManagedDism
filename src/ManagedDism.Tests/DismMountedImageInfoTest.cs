using System.Collections.Generic;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismMountedImageInfoCollectionTest : DismCollectionTest<DismMountedImageInfoCollection, DismMountedImageInfo>
    {
        protected override DismMountedImageInfoCollection CreateCollection(List<DismMountedImageInfo> expectedCollection)
        {
            return new DismMountedImageInfoCollection(expectedCollection);
        }

        protected override DismMountedImageInfoCollection CreateCollection()
        {
            return new DismMountedImageInfoCollection();
        }

        protected override List<DismMountedImageInfo> GetCollection()
        {
            return new List<DismMountedImageInfo>
            {
                new DismMountedImageInfo(new DismApi.DismMountedImageInfo_
                {
                   ImageFilePath = "ImageFilePath1",
                   ImageIndex = 1,
                   MountMode = DismMountMode.ReadOnly,
                   MountPath = "MountPath1",
                   MountStatus = DismMountStatus.Invalid,
                }),
                new DismMountedImageInfo(new DismApi.DismMountedImageInfo_
                {
                   ImageFilePath = "ImageFilePath2",
                   ImageIndex = 2,
                   MountMode = DismMountMode.ReadWrite,
                   MountPath = "MountPath2",
                   MountStatus = DismMountStatus.NeedsRemount,
                }),
                new DismMountedImageInfo(new DismApi.DismMountedImageInfo_
                {
                   ImageFilePath = "ImageFilePath3",
                   ImageIndex = 3,
                   MountMode = DismMountMode.ReadOnly,
                   MountPath = "MountPath3",
                   MountStatus = DismMountStatus.Ok,
                }),
            };
        }
    }

    [TestFixture]
    public class DismMountedImageInfoTest : DismStructTest<DismMountedImageInfo>
    {
        private readonly DismApi.DismMountedImageInfo_ _mountedImageInfo = new DismApi.DismMountedImageInfo_
        {
            ImageFilePath = "ImageFilePath",
            ImageIndex = 2,
            MountMode = DismMountMode.ReadWrite,
            MountPath = "MountPath",
            MountStatus = DismMountStatus.NeedsRemount,
        };

        protected override DismMountedImageInfo Item
        {
            get
            {
                return new DismMountedImageInfo(_mountedImageInfo);
            }
        }

        protected override object Struct
        {
            get
            {
                return _mountedImageInfo;
            }
        }

        protected override void VerifyProperties(DismMountedImageInfo item)
        {
            Assert.AreEqual("ImageFilePath", item.ImageFilePath, "ImageFilePath is incorrect");
            Assert.AreEqual(2, item.ImageIndex, "ImageIndex is incorrect");
            Assert.AreEqual(DismMountMode.ReadWrite, item.MountMode, "MountMode is incorrect");
            Assert.AreEqual("MountPath", item.MountPath, "MountPath is incorrect");
            Assert.AreEqual(DismMountStatus.NeedsRemount, item.MountStatus, "MountStatus is incorrect");
        }
    }
}