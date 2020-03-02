using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroundFrame.SQL.UnitTests
{
    [TestClass()]
    public class ScalarFunctions_SimSig : SqlDatabaseTestClass
    {

        public ScalarFunctions_SimSig()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_Fn_GET_LOCATIONNODE_NODEIDTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScalarFunctions_SimSig));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_FN_GET_LOCATIONNODE_NODEID_correct_value;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_Fn_GET_LOCATIONNODE_NODEIDTest_PosttestAction;
            this.simsig_Fn_GET_LOCATIONNODE_NODEIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            simsig_Fn_GET_LOCATIONNODE_NODEIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_FN_GET_LOCATIONNODE_NODEID_correct_value = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_Fn_GET_LOCATIONNODE_NODEIDTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            // 
            // simsig_Fn_GET_LOCATIONNODE_NODEIDTest_TestAction
            // 
            simsig_Fn_GET_LOCATIONNODE_NODEIDTest_TestAction.Conditions.Add(chk_FN_GET_LOCATIONNODE_NODEID_correct_value);
            resources.ApplyResources(simsig_Fn_GET_LOCATIONNODE_NODEIDTest_TestAction, "simsig_Fn_GET_LOCATIONNODE_NODEIDTest_TestAction");
            // 
            // simsig_Fn_GET_LOCATIONNODE_NODEIDTestData
            // 
            this.simsig_Fn_GET_LOCATIONNODE_NODEIDTestData.PosttestAction = simsig_Fn_GET_LOCATIONNODE_NODEIDTest_PosttestAction;
            this.simsig_Fn_GET_LOCATIONNODE_NODEIDTestData.PretestAction = null;
            this.simsig_Fn_GET_LOCATIONNODE_NODEIDTestData.TestAction = simsig_Fn_GET_LOCATIONNODE_NODEIDTest_TestAction;
            // 
            // chk_FN_GET_LOCATIONNODE_NODEID_correct_value
            // 
            chk_FN_GET_LOCATIONNODE_NODEID_correct_value.ColumnNumber = 1;
            chk_FN_GET_LOCATIONNODE_NODEID_correct_value.Enabled = true;
            chk_FN_GET_LOCATIONNODE_NODEID_correct_value.ExpectedValue = "true";
            chk_FN_GET_LOCATIONNODE_NODEID_correct_value.Name = "chk_FN_GET_LOCATIONNODE_NODEID_correct_value";
            chk_FN_GET_LOCATIONNODE_NODEID_correct_value.NullExpected = false;
            chk_FN_GET_LOCATIONNODE_NODEID_correct_value.ResultSet = 1;
            chk_FN_GET_LOCATIONNODE_NODEID_correct_value.RowNumber = 1;
            // 
            // simsig_Fn_GET_LOCATIONNODE_NODEIDTest_PosttestAction
            // 
            resources.ApplyResources(simsig_Fn_GET_LOCATIONNODE_NODEIDTest_PosttestAction, "simsig_Fn_GET_LOCATIONNODE_NODEIDTest_PosttestAction");
        }

        #endregion


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
        #endregion

        [TestMethod()]
        public void simsig_Fn_GET_LOCATIONNODE_NODEIDTest()
        {
            SqlDatabaseTestActions testActions = this.simsig_Fn_GET_LOCATIONNODE_NODEIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        private SqlDatabaseTestActions simsig_Fn_GET_LOCATIONNODE_NODEIDTestData;
    }
}
