using System;
using ManagedDism.Tests.Base;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismWimCustomizedInfoTest : DismStructTest<DismWimCustomizedInfo>
    {
        private readonly DismApi.DismWimCustomizedInfo_ _customizedInfo = new DismApi.DismWimCustomizedInfo_
        {
            CreatedTime = DateTime.Today,
            DirectoryCount = 123,
            FileCount = 456,
            ModifiedTime = DateTime.Today,
            Size = 789,
        };

        protected override DismWimCustomizedInfo Item
        {
            get
            {
                return new DismWimCustomizedInfo(_customizedInfo);
            }
        }

        protected override object Struct
        {
            get
            {
                return _customizedInfo;
            }
        }

        protected override void VerifyProperties(DismWimCustomizedInfo item)
        {
            Assert.AreEqual(DateTime.Today.ToUniversalTime(), item.CreatedTime, "CreatedTime is incorrect");
            Assert.AreEqual(123, item.DirectoryCount, "DirectoryCount is incorrect");
            Assert.AreEqual(456, item.FileCount, "FileCount is incorrect");
            Assert.AreEqual(DateTime.Today.ToUniversalTime(), item.ModifiedTime, "ModifiedTime is incorrect");
            Assert.AreEqual(789, item.Size, "Size is incorrect");
        }
    }
}