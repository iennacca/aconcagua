using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using infrastructure;
using Ninject;
using Ninject.Extensions.Conventions;

namespace aconcagua.tests
{
    /// <summary>
    /// Summary description for LoggerTests
    /// </summary>
    [TestClass]
    public class LoggerTests
    {
        private readonly ILogService _logger; 

        public LoggerTests()
        {
            // Console.WriteLine($"TestContext DeploymentDirectory: {TestContext.DeploymentDirectory}");
            // Console.WriteLine($"AppDomain BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");

            var kernel = new StandardKernel();

            // kernel.Load("infrastructure*.dll");
            kernel.Bind(x => x
                .FromAssembliesMatching("infra*.dll")
                .SelectAllClasses()
                .BindAllInterfaces()
                .Configure(b => b.InSingletonScope()));

            var args = new Ninject.Parameters.ConstructorArgument("classType", typeof(LoggerTests));
            _logger = kernel.Get<ILogService>(args);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        // public static TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext)
        // {
        //     TestContext = testContext;
        // }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CanRunNLog()
        {
            _logger.Info("This is informational text.");
            _logger.Error("This is an error.");
        }
    }
}
