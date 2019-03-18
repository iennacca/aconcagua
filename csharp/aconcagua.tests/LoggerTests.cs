using Microsoft.VisualStudio.TestTools.UnitTesting;
using infrastructure;

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

            _logger = IOCContainer.Logger;
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
