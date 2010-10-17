using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhitePaperBible.Services;
using WhitePaperBible.Data;

namespace WhitePaperBible.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
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

        //[TestMethod]
        //public void CanRetrieveAListOfPapers()
        //{
        //    var svc = new PaperService();
        //    svc.GetPapers(onPapersReceived, onErrorReceived);
        //}

        //private void onErrorReceived(string error)
        //{
        //    Assert.Fail("received an error, you fail");
        //}

        //private void onPapersReceived(PaperCollection papers)
        //{
        //    Assert.IsNotNull(papers, "papsers should not be null");
        //    Assert.IsTrue(papers.Papers.Count > 1, "should have more than 1 paper");
        //}
    }
}
