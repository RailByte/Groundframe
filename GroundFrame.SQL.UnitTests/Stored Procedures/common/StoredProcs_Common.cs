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
    public class StoredProcs_Common : SqlDatabaseTestClass
    {

        public StoredProcs_Common()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction common_Usp_SET_SESSIONCONTEXTTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoredProcs_Common));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Invalid_AppAPI;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Invalid_UserAPI;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Disabled_AppAPI;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Disabled_UserAPI;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Check_LoggedIn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Check_AppID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Check_UserID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction common_Usp_GENERATE_APIKEYTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NoLength_Supplied;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction common_Usp_SET_SESSIONCONTEXTTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Check_Cleared_LoggedIn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Check_Cleared_ApplicationID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Check_Cleared_ApplicationUserID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction common_Usp_TEARDOWN_TESTDATATest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_TearDown_NotLoggedIn_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_TearDown_TearDownFlagNotSet_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_TearDown_Zero_TSIM;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_TearDown_Zero_TSIMERA;
            this.common_Usp_SET_SESSIONCONTEXTTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.common_Usp_GENERATE_APIKEYTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.common_Usp_CLEAR_SESSIONCONTEXTTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.common_Usp_TEARDOWN_TESTDATATestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            common_Usp_SET_SESSIONCONTEXTTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Invalid_AppAPI = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Invalid_UserAPI = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Disabled_AppAPI = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Disabled_UserAPI = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Check_LoggedIn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Check_AppID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Check_UserID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            common_Usp_GENERATE_APIKEYTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_NoLength_Supplied = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            common_Usp_SET_SESSIONCONTEXTTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Check_Cleared_LoggedIn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Check_Cleared_ApplicationID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Check_Cleared_ApplicationUserID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            common_Usp_TEARDOWN_TESTDATATest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_TearDown_NotLoggedIn_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_TearDown_TearDownFlagNotSet_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_TearDown_Zero_TSIM = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_TearDown_Zero_TSIMERA = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            // 
            // common_Usp_SET_SESSIONCONTEXTTest_TestAction
            // 
            common_Usp_SET_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Invalid_AppAPI);
            common_Usp_SET_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Invalid_UserAPI);
            common_Usp_SET_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Disabled_AppAPI);
            common_Usp_SET_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Disabled_UserAPI);
            common_Usp_SET_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Check_LoggedIn);
            common_Usp_SET_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Check_AppID);
            common_Usp_SET_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Check_UserID);
            resources.ApplyResources(common_Usp_SET_SESSIONCONTEXTTest_TestAction, "common_Usp_SET_SESSIONCONTEXTTest_TestAction");
            // 
            // chk_Invalid_AppAPI
            // 
            chk_Invalid_AppAPI.ColumnNumber = 1;
            chk_Invalid_AppAPI.Enabled = true;
            chk_Invalid_AppAPI.ExpectedValue = "Error executing [common].[Usp_SET_SESSIONCONTEXT]: - The application isn\'t a vali" +
                "d application or has been disabled";
            chk_Invalid_AppAPI.Name = "chk_Invalid_AppAPI";
            chk_Invalid_AppAPI.NullExpected = false;
            chk_Invalid_AppAPI.ResultSet = 1;
            chk_Invalid_AppAPI.RowNumber = 1;
            // 
            // chk_Invalid_UserAPI
            // 
            chk_Invalid_UserAPI.ColumnNumber = 1;
            chk_Invalid_UserAPI.Enabled = true;
            chk_Invalid_UserAPI.ExpectedValue = "Error executing [common].[Usp_SET_SESSIONCONTEXT]: - The user isn\'t a valid user " +
                "or their account has been disabled";
            chk_Invalid_UserAPI.Name = "chk_Invalid_UserAPI";
            chk_Invalid_UserAPI.NullExpected = false;
            chk_Invalid_UserAPI.ResultSet = 2;
            chk_Invalid_UserAPI.RowNumber = 1;
            // 
            // chk_Disabled_AppAPI
            // 
            chk_Disabled_AppAPI.ColumnNumber = 1;
            chk_Disabled_AppAPI.Enabled = true;
            chk_Disabled_AppAPI.ExpectedValue = "Error executing [common].[Usp_SET_SESSIONCONTEXT]: - The application isn\'t a vali" +
                "d application or has been disabled";
            chk_Disabled_AppAPI.Name = "chk_Disabled_AppAPI";
            chk_Disabled_AppAPI.NullExpected = false;
            chk_Disabled_AppAPI.ResultSet = 3;
            chk_Disabled_AppAPI.RowNumber = 1;
            // 
            // chk_Disabled_UserAPI
            // 
            chk_Disabled_UserAPI.ColumnNumber = 1;
            chk_Disabled_UserAPI.Enabled = true;
            chk_Disabled_UserAPI.ExpectedValue = "Error executing [common].[Usp_SET_SESSIONCONTEXT]: - The user isn\'t a valid user " +
                "or their account has been disabled";
            chk_Disabled_UserAPI.Name = "chk_Disabled_UserAPI";
            chk_Disabled_UserAPI.NullExpected = false;
            chk_Disabled_UserAPI.ResultSet = 4;
            chk_Disabled_UserAPI.RowNumber = 1;
            // 
            // chk_Check_LoggedIn
            // 
            chk_Check_LoggedIn.ColumnNumber = 1;
            chk_Check_LoggedIn.Enabled = true;
            chk_Check_LoggedIn.ExpectedValue = "true";
            chk_Check_LoggedIn.Name = "chk_Check_LoggedIn";
            chk_Check_LoggedIn.NullExpected = false;
            chk_Check_LoggedIn.ResultSet = 5;
            chk_Check_LoggedIn.RowNumber = 1;
            // 
            // chk_Check_AppID
            // 
            chk_Check_AppID.ColumnNumber = 1;
            chk_Check_AppID.Enabled = true;
            chk_Check_AppID.ExpectedValue = "2";
            chk_Check_AppID.Name = "chk_Check_AppID";
            chk_Check_AppID.NullExpected = false;
            chk_Check_AppID.ResultSet = 6;
            chk_Check_AppID.RowNumber = 1;
            // 
            // chk_Check_UserID
            // 
            chk_Check_UserID.ColumnNumber = 1;
            chk_Check_UserID.Enabled = true;
            chk_Check_UserID.ExpectedValue = "2";
            chk_Check_UserID.Name = "chk_Check_UserID";
            chk_Check_UserID.NullExpected = false;
            chk_Check_UserID.ResultSet = 7;
            chk_Check_UserID.RowNumber = 1;
            // 
            // common_Usp_GENERATE_APIKEYTest_TestAction
            // 
            common_Usp_GENERATE_APIKEYTest_TestAction.Conditions.Add(chk_NoLength_Supplied);
            resources.ApplyResources(common_Usp_GENERATE_APIKEYTest_TestAction, "common_Usp_GENERATE_APIKEYTest_TestAction");
            // 
            // chk_NoLength_Supplied
            // 
            chk_NoLength_Supplied.ColumnNumber = 1;
            chk_NoLength_Supplied.Enabled = true;
            chk_NoLength_Supplied.ExpectedValue = "true";
            chk_NoLength_Supplied.Name = "chk_NoLength_Supplied";
            chk_NoLength_Supplied.NullExpected = false;
            chk_NoLength_Supplied.ResultSet = 1;
            chk_NoLength_Supplied.RowNumber = 1;
            // 
            // common_Usp_SET_SESSIONCONTEXTTest_PosttestAction
            // 
            resources.ApplyResources(common_Usp_SET_SESSIONCONTEXTTest_PosttestAction, "common_Usp_SET_SESSIONCONTEXTTest_PosttestAction");
            // 
            // common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction
            // 
            common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Check_Cleared_LoggedIn);
            common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Check_Cleared_ApplicationID);
            common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction.Conditions.Add(chk_Check_Cleared_ApplicationUserID);
            resources.ApplyResources(common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction, "common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction");
            // 
            // chk_Check_Cleared_LoggedIn
            // 
            chk_Check_Cleared_LoggedIn.ColumnNumber = 1;
            chk_Check_Cleared_LoggedIn.Enabled = true;
            chk_Check_Cleared_LoggedIn.ExpectedValue = "false";
            chk_Check_Cleared_LoggedIn.Name = "chk_Check_Cleared_LoggedIn";
            chk_Check_Cleared_LoggedIn.NullExpected = false;
            chk_Check_Cleared_LoggedIn.ResultSet = 1;
            chk_Check_Cleared_LoggedIn.RowNumber = 1;
            // 
            // chk_Check_Cleared_ApplicationID
            // 
            chk_Check_Cleared_ApplicationID.ColumnNumber = 1;
            chk_Check_Cleared_ApplicationID.Enabled = true;
            chk_Check_Cleared_ApplicationID.ExpectedValue = null;
            chk_Check_Cleared_ApplicationID.Name = "chk_Check_Cleared_ApplicationID";
            chk_Check_Cleared_ApplicationID.NullExpected = true;
            chk_Check_Cleared_ApplicationID.ResultSet = 2;
            chk_Check_Cleared_ApplicationID.RowNumber = 1;
            // 
            // chk_Check_Cleared_ApplicationUserID
            // 
            chk_Check_Cleared_ApplicationUserID.ColumnNumber = 1;
            chk_Check_Cleared_ApplicationUserID.Enabled = true;
            chk_Check_Cleared_ApplicationUserID.ExpectedValue = null;
            chk_Check_Cleared_ApplicationUserID.Name = "chk_Check_Cleared_ApplicationUserID";
            chk_Check_Cleared_ApplicationUserID.NullExpected = true;
            chk_Check_Cleared_ApplicationUserID.ResultSet = 3;
            chk_Check_Cleared_ApplicationUserID.RowNumber = 1;
            // 
            // common_Usp_TEARDOWN_TESTDATATest_TestAction
            // 
            common_Usp_TEARDOWN_TESTDATATest_TestAction.Conditions.Add(chk_TearDown_NotLoggedIn_Error);
            common_Usp_TEARDOWN_TESTDATATest_TestAction.Conditions.Add(chk_TearDown_TearDownFlagNotSet_Error);
            common_Usp_TEARDOWN_TESTDATATest_TestAction.Conditions.Add(chk_TearDown_Zero_TSIM);
            common_Usp_TEARDOWN_TESTDATATest_TestAction.Conditions.Add(chk_TearDown_Zero_TSIMERA);
            resources.ApplyResources(common_Usp_TEARDOWN_TESTDATATest_TestAction, "common_Usp_TEARDOWN_TESTDATATest_TestAction");
            // 
            // chk_TearDown_NotLoggedIn_Error
            // 
            chk_TearDown_NotLoggedIn_Error.ColumnNumber = 1;
            chk_TearDown_NotLoggedIn_Error.Enabled = true;
            chk_TearDown_NotLoggedIn_Error.ExpectedValue = "The user is not logged in.";
            chk_TearDown_NotLoggedIn_Error.Name = "chk_TearDown_NotLoggedIn_Error";
            chk_TearDown_NotLoggedIn_Error.NullExpected = false;
            chk_TearDown_NotLoggedIn_Error.ResultSet = 1;
            chk_TearDown_NotLoggedIn_Error.RowNumber = 1;
            // 
            // chk_TearDown_TearDownFlagNotSet_Error
            // 
            chk_TearDown_TearDownFlagNotSet_Error.ColumnNumber = 1;
            chk_TearDown_TearDownFlagNotSet_Error.Enabled = true;
            chk_TearDown_TearDownFlagNotSet_Error.ExpectedValue = "The Tear Down flag is not set therefore this proc cannot run";
            chk_TearDown_TearDownFlagNotSet_Error.Name = "chk_TearDown_TearDownFlagNotSet_Error";
            chk_TearDown_TearDownFlagNotSet_Error.NullExpected = false;
            chk_TearDown_TearDownFlagNotSet_Error.ResultSet = 2;
            chk_TearDown_TearDownFlagNotSet_Error.RowNumber = 1;
            // 
            // chk_TearDown_Zero_TSIM
            // 
            chk_TearDown_Zero_TSIM.Enabled = true;
            chk_TearDown_Zero_TSIM.Name = "chk_TearDown_Zero_TSIM";
            chk_TearDown_Zero_TSIM.ResultSet = 3;
            chk_TearDown_Zero_TSIM.RowCount = 0;
            // 
            // chk_TearDown_Zero_TSIMERA
            // 
            chk_TearDown_Zero_TSIMERA.Enabled = true;
            chk_TearDown_Zero_TSIMERA.Name = "chk_TearDown_Zero_TSIMERA";
            chk_TearDown_Zero_TSIMERA.ResultSet = 4;
            chk_TearDown_Zero_TSIMERA.RowCount = 0;
            // 
            // common_Usp_SET_SESSIONCONTEXTTestData
            // 
            this.common_Usp_SET_SESSIONCONTEXTTestData.PosttestAction = common_Usp_SET_SESSIONCONTEXTTest_PosttestAction;
            this.common_Usp_SET_SESSIONCONTEXTTestData.PretestAction = null;
            this.common_Usp_SET_SESSIONCONTEXTTestData.TestAction = common_Usp_SET_SESSIONCONTEXTTest_TestAction;
            // 
            // common_Usp_GENERATE_APIKEYTestData
            // 
            this.common_Usp_GENERATE_APIKEYTestData.PosttestAction = null;
            this.common_Usp_GENERATE_APIKEYTestData.PretestAction = null;
            this.common_Usp_GENERATE_APIKEYTestData.TestAction = common_Usp_GENERATE_APIKEYTest_TestAction;
            // 
            // common_Usp_CLEAR_SESSIONCONTEXTTestData
            // 
            this.common_Usp_CLEAR_SESSIONCONTEXTTestData.PosttestAction = null;
            this.common_Usp_CLEAR_SESSIONCONTEXTTestData.PretestAction = null;
            this.common_Usp_CLEAR_SESSIONCONTEXTTestData.TestAction = common_Usp_CLEAR_SESSIONCONTEXTTest_TestAction;
            // 
            // common_Usp_TEARDOWN_TESTDATATestData
            // 
            this.common_Usp_TEARDOWN_TESTDATATestData.PosttestAction = null;
            this.common_Usp_TEARDOWN_TESTDATATestData.PretestAction = null;
            this.common_Usp_TEARDOWN_TESTDATATestData.TestAction = common_Usp_TEARDOWN_TESTDATATest_TestAction;
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
        public void common_Usp_SET_SESSIONCONTEXTTest()
        {
            SqlDatabaseTestActions testActions = this.common_Usp_SET_SESSIONCONTEXTTestData;
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
        [TestMethod()]
        public void common_Usp_GENERATE_APIKEYTest()
        {
            SqlDatabaseTestActions testActions = this.common_Usp_GENERATE_APIKEYTestData;
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
        [TestMethod()]
        public void common_Usp_CLEAR_SESSIONCONTEXTTest()
        {
            SqlDatabaseTestActions testActions = this.common_Usp_CLEAR_SESSIONCONTEXTTestData;
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
        [TestMethod()]
        public void common_Usp_TEARDOWN_TESTDATATest()
        {
            SqlDatabaseTestActions testActions = this.common_Usp_TEARDOWN_TESTDATATestData;
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



        private SqlDatabaseTestActions common_Usp_SET_SESSIONCONTEXTTestData;
        private SqlDatabaseTestActions common_Usp_GENERATE_APIKEYTestData;
        private SqlDatabaseTestActions common_Usp_CLEAR_SESSIONCONTEXTTestData;
        private SqlDatabaseTestActions common_Usp_TEARDOWN_TESTDATATestData;
    }
}
