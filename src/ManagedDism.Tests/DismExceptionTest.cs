using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using NUnit.Framework;

namespace ManagedDism.Tests
{
    [TestFixture]
    public class DismExceptionTest
    {
        [Test]
        public void DismRebootRequiredExceptionTest()
        {
            VerifyDismException<DismRebootRequiredException>(Win32Error.ERROR_SUCCESS_REBOOT_REQUIRED, "A restart is required to complete the operation.");
        }

        [Test]
        public void DismNotInitializedExceptionTest()
        {
            VerifyDismException<DismNotInitializedException>(DismApi.DISMAPI_E_DISMAPI_NOT_INITIALIZED, "There are one or more open sessions");
        }

        [Test]
        public void DismOpenSessionsExceptionTest()
        {
            VerifyDismException<DismOpenSessionsException>(DismApi.DISMAPI_E_OPEN_SESSION_HANDLES, "The DismApi has not been initialized");
        }


        [Test]
        public void GetLastErrorMessageTest()
        {
            const string message = "Hello World";

            using(ShimsContext.Create())
            {
                ShimDismApi.GetLastErrorMessage = () => message;

                VerifyDismException<DismException>(Win32Error.ERROR_OUTOFMEMORY, message);
            }
        }

        [Test]
        public void Win32ExceptionTest()
        {
            const int errorCode = unchecked((int)0x80020012);

            const string errorMessage = "Attempted to divide by zero.";

            using (ShimsContext.Create())
            {
                ShimDismApi.GetLastErrorMessage = () => null;

                var exception = DismException.GetDismExceptionForHR(errorCode);

                Assert.IsInstanceOfType(exception, typeof(DivideByZeroException));

                Assert.AreEqual(errorMessage, exception.Message);

                Assert.AreEqual(errorCode, exception.HResult);
            }
        }

        [Test]
        public void OperationCanceledExceptionTest()
        {
            const int errorCode = unchecked((int)0x800704D3);
            const int hresult = unchecked((int) 0x8013153B);

            const string errorMessage = "The operation was canceled.";

            using (ShimsContext.Create())
            {
                ShimDismApi.GetLastErrorMessage = () => null;

                var exception = DismException.GetDismExceptionForHR(errorCode);

                Assert.IsInstanceOfType(exception, typeof(OperationCanceledException));

                Assert.AreEqual(errorMessage, exception.Message);

                Assert.AreEqual(hresult, exception.HResult);
            }
        }

        private void VerifyDismException<T>(uint errorCode, string message)
            where T : DismException
        {
            var exception = DismException.GetDismExceptionForHR((int)errorCode);

            Assert.IsInstanceOfType(exception, typeof(T));

            var dismException = (DismException)exception;

            Assert.AreEqual(message, exception.Message);

            Assert.AreEqual(errorCode, (uint)dismException.ErrorCode);
            Assert.AreEqual(errorCode, (uint)dismException.HResult);
            Assert.AreEqual(errorCode, (uint)dismException.NativeErrorCode);
        }
    }
}
