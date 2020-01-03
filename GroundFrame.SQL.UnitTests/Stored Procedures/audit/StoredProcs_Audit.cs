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
    public class StoredProcs_Audit : SqlDatabaseTestClass
    {

        public StoredProcs_Audit()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction debug_Usp_INSERT_TEVENTTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoredProcs_Audit));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NoSessionContext_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_InvalidSourceObjectID_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition check_Insert_SessionID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition check_Insert_EventDateTime;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition check_Insert_SourceObjectID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition check_Insert_Event;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition check_Insert_DBUser;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition check_Insert_AppID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition check_Insert_AppUserID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction debug_Usp_INSERT_TEVENTTest_PosttestAction;
            this.debug_Usp_INSERT_TEVENTTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            debug_Usp_INSERT_TEVENTTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_NoSessionContext_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_InvalidSourceObjectID_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            check_Insert_SessionID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            check_Insert_EventDateTime = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            check_Insert_SourceObjectID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            check_Insert_Event = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            check_Insert_DBUser = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            check_Insert_AppID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            check_Insert_AppUserID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            debug_Usp_INSERT_TEVENTTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            // 
            // debug_Usp_INSERT_TEVENTTest_TestAction
            // 
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(chk_NoSessionContext_Error);
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(chk_InvalidSourceObjectID_Error);
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(check_Insert_SessionID);
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(check_Insert_EventDateTime);
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(check_Insert_SourceObjectID);
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(check_Insert_Event);
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(check_Insert_DBUser);
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(check_Insert_AppID);
            debug_Usp_INSERT_TEVENTTest_TestAction.Conditions.Add(check_Insert_AppUserID);
            resources.ApplyResources(debug_Usp_INSERT_TEVENTTest_TestAction, "debug_Usp_INSERT_TEVENTTest_TestAction");
            // 
            // chk_NoSessionContext_Error
            // 
            chk_NoSessionContext_Error.ColumnNumber = 1;
            chk_NoSessionContext_Error.Enabled = true;
            chk_NoSessionContext_Error.ExpectedValue = "Error executing [audit].[USp_INSERT_TEVENT]: - The user is not logged in.";
            chk_NoSessionContext_Error.Name = "chk_NoSessionContext_Error";
            chk_NoSessionContext_Error.NullExpected = false;
            chk_NoSessionContext_Error.ResultSet = 1;
            chk_NoSessionContext_Error.RowNumber = 1;
            // 
            // chk_InvalidSourceObjectID_Error
            // 
            chk_InvalidSourceObjectID_Error.ColumnNumber = 1;
            chk_InvalidSourceObjectID_Error.Enabled = true;
            chk_InvalidSourceObjectID_Error.ExpectedValue = "Error executing [audit].[USp_INSERT_TEVENT]: - @object_id 10000001 is not a valid" +
                " object.";
            chk_InvalidSourceObjectID_Error.Name = "chk_InvalidSourceObjectID_Error";
            chk_InvalidSourceObjectID_Error.NullExpected = false;
            chk_InvalidSourceObjectID_Error.ResultSet = 2;
            chk_InvalidSourceObjectID_Error.RowNumber = 1;
            // 
            // check_Insert_SessionID
            // 
            check_Insert_SessionID.ColumnNumber = 1;
            check_Insert_SessionID.Enabled = true;
            check_Insert_SessionID.ExpectedValue = "true";
            check_Insert_SessionID.Name = "check_Insert_SessionID";
            check_Insert_SessionID.NullExpected = false;
            check_Insert_SessionID.ResultSet = 3;
            check_Insert_SessionID.RowNumber = 1;
            // 
            // check_Insert_EventDateTime
            // 
            check_Insert_EventDateTime.ColumnNumber = 2;
            check_Insert_EventDateTime.Enabled = true;
            check_Insert_EventDateTime.ExpectedValue = "true";
            check_Insert_EventDateTime.Name = "check_Insert_EventDateTime";
            check_Insert_EventDateTime.NullExpected = false;
            check_Insert_EventDateTime.ResultSet = 3;
            check_Insert_EventDateTime.RowNumber = 1;
            // 
            // check_Insert_SourceObjectID
            // 
            check_Insert_SourceObjectID.ColumnNumber = 3;
            check_Insert_SourceObjectID.Enabled = true;
            check_Insert_SourceObjectID.ExpectedValue = "true";
            check_Insert_SourceObjectID.Name = "check_Insert_SourceObjectID";
            check_Insert_SourceObjectID.NullExpected = false;
            check_Insert_SourceObjectID.ResultSet = 3;
            check_Insert_SourceObjectID.RowNumber = 1;
            // 
            // check_Insert_Event
            // 
            check_Insert_Event.ColumnNumber = 4;
            check_Insert_Event.Enabled = true;
            check_Insert_Event.ExpectedValue = "Check 3";
            check_Insert_Event.Name = "check_Insert_Event";
            check_Insert_Event.NullExpected = false;
            check_Insert_Event.ResultSet = 3;
            check_Insert_Event.RowNumber = 1;
            // 
            // check_Insert_DBUser
            // 
            check_Insert_DBUser.ColumnNumber = 5;
            check_Insert_DBUser.Enabled = true;
            check_Insert_DBUser.ExpectedValue = "true";
            check_Insert_DBUser.Name = "check_Insert_DBUser";
            check_Insert_DBUser.NullExpected = false;
            check_Insert_DBUser.ResultSet = 3;
            check_Insert_DBUser.RowNumber = 1;
            // 
            // check_Insert_AppID
            // 
            check_Insert_AppID.ColumnNumber = 6;
            check_Insert_AppID.Enabled = true;
            check_Insert_AppID.ExpectedValue = "2";
            check_Insert_AppID.Name = "check_Insert_AppID";
            check_Insert_AppID.NullExpected = false;
            check_Insert_AppID.ResultSet = 3;
            check_Insert_AppID.RowNumber = 1;
            // 
            // check_Insert_AppUserID
            // 
            check_Insert_AppUserID.ColumnNumber = 7;
            check_Insert_AppUserID.Enabled = true;
            check_Insert_AppUserID.ExpectedValue = "2";
            check_Insert_AppUserID.Name = "check_Insert_AppUserID";
            check_Insert_AppUserID.NullExpected = false;
            check_Insert_AppUserID.ResultSet = 3;
            check_Insert_AppUserID.RowNumber = 1;
            // 
            // debug_Usp_INSERT_TEVENTTest_PosttestAction
            // 
            resources.ApplyResources(debug_Usp_INSERT_TEVENTTest_PosttestAction, "debug_Usp_INSERT_TEVENTTest_PosttestAction");
            // 
            // debug_Usp_INSERT_TEVENTTestData
            // 
            this.debug_Usp_INSERT_TEVENTTestData.PosttestAction = debug_Usp_INSERT_TEVENTTest_PosttestAction;
            this.debug_Usp_INSERT_TEVENTTestData.PretestAction = null;
            this.debug_Usp_INSERT_TEVENTTestData.TestAction = debug_Usp_INSERT_TEVENTTest_TestAction;
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
        public void debug_Usp_INSERT_TEVENTTest()
        {
            SqlDatabaseTestActions testActions = this.debug_Usp_INSERT_TEVENTTestData;
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
        private SqlDatabaseTestActions debug_Usp_INSERT_TEVENTTestData;
    }
}
