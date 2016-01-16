using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace ManagedDism.Tests.Base
{
    [TestFixture]
    public abstract class DismCollectionTest<TCollection, TItem> where TCollection : DismCollection<TItem>
        where TItem : class
    {
        [Test]
        public void CollectionTest()
        {
            var expectedCollection = GetCollection();

            VerifyCollection(expectedCollection, CreateCollection(expectedCollection));
        }

        [Test]
        public void CollectionTest_Empty()
        {
            VerifyCollection(new List<TItem>(), CreateCollection());
        }

        protected abstract TCollection CreateCollection(List<TItem> expectedCollection);

        protected abstract TCollection CreateCollection();

        protected abstract List<TItem> GetCollection();

        private void VerifyCollection(IList expectedCollection, TCollection actualCollection)
        {
            CollectionAssert.AreEqual(expectedCollection, actualCollection, "Collections are not the same");
        }
    }
}