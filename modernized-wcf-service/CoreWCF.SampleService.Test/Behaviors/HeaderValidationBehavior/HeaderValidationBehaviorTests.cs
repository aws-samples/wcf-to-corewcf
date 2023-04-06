// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

#if NETFRAMEWORK
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WCF.SampleService.Behaviors.ErrorBehavior;
using WCF.SampleService.Loggers;
using WCF.SampleService.Test;
using ErrorBehaviorNS = WCF.SampleService.Behaviors.ErrorBehavior;

namespace CoreWCF.SampleService.Test.Behaviors.HeaderValidationBehavior
{
    [TestFixture]
    public class HeaderValidationBehaviorTests
    {
        static TestServiceHostingEnvironment<TestService> hostingEnv;
        static Mock<IFileLogger> fileLogger;
        static string serviceUrl;

        [SetUp]
        public void Initialize()
        {
            serviceUrl = "http://localhost:9000/TestService.svc";
            fileLogger = new Mock<IFileLogger>();
            hostingEnv = new TestServiceHostingEnvironment<TestService>(serviceUrl, new ErrorBehaviorNS.ErrorBehavior(typeof(GlobalErrorHandler), fileLogger.Object), true);
        }

        [TearDown]
        public void CleanUp()
        {
            hostingEnv.Dispose();
        }

        static ITestService Channel
        {
            get
            {
                return hostingEnv.GetChannel<ITestService>(serviceUrl);

            }
        }

        [Test]
        public void HeadersAreValidatedByHeaderValidationBehavior()
        {
            // Arrange
            fileLogger.Setup(t => t.Log(It.IsAny<string>()));
            Exception expectedExcetpion = null;

            // Act
            try
            {
                int result = Channel.Test(2, 1);
            }
            catch (Exception ex)
            {
                expectedExcetpion = ex;
            }

            // Assert
            Assert.IsNotNull(expectedExcetpion);
            Assert.AreEqual("ClientId was not found the request.", expectedExcetpion.Message);
            fileLogger.Verify(t => t.Log(It.IsAny<string>()), Times.Once);
        }

        #region service

        [ServiceContract]
        public interface ITestService
        {
            [OperationContract]
            int Test(int num1, int num2);
        }

        class TestService : ITestService
        {
            public int Test(int num1, int num2)
            {
                return num1 / num2;
            }
        }
        #endregion
    }
}
#endif