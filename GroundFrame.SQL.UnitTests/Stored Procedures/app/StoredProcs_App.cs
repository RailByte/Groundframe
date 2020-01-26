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
    public class StoredProcs_App : SqlDatabaseTestClass
    {

        public StoredProcs_App()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction app_Usp_GET_USER_SETTINGSTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_Usp_GET_USER_SETTINGS_Check_Row_Count;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_USER_SETTINGS_Check_Row1_Key;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_USER_SETTINGS_Check_Row1_Value;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction app_Usp_GET_USER_SETTINGSTest_PosttestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoredProcs_App));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition chk_Usp_GET_USER_SETTINGS_Check_Debug;
            this.app_Usp_GET_USER_SETTINGSTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            app_Usp_GET_USER_SETTINGSTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_USER_SETTINGS_Check_Row_Count = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Key = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Value = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            app_Usp_GET_USER_SETTINGSTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_USER_SETTINGS_Check_Debug = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            // 
            // app_Usp_GET_USER_SETTINGSTestData
            // 
            this.app_Usp_GET_USER_SETTINGSTestData.PosttestAction = app_Usp_GET_USER_SETTINGSTest_PosttestAction;
            this.app_Usp_GET_USER_SETTINGSTestData.PretestAction = null;
            this.app_Usp_GET_USER_SETTINGSTestData.TestAction = app_Usp_GET_USER_SETTINGSTest_TestAction;
            // 
            // app_Usp_GET_USER_SETTINGSTest_TestAction
            // 
            app_Usp_GET_USER_SETTINGSTest_TestAction.Conditions.Add(chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error);
            app_Usp_GET_USER_SETTINGSTest_TestAction.Conditions.Add(chk_Usp_GET_USER_SETTINGS_Check_Row_Count);
            app_Usp_GET_USER_SETTINGSTest_TestAction.Conditions.Add(chk_Usp_GET_USER_SETTINGS_Check_Row1_Key);
            app_Usp_GET_USER_SETTINGSTest_TestAction.Conditions.Add(chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc);
            app_Usp_GET_USER_SETTINGSTest_TestAction.Conditions.Add(chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType);
            app_Usp_GET_USER_SETTINGSTest_TestAction.Conditions.Add(chk_Usp_GET_USER_SETTINGS_Check_Row1_Value);
            app_Usp_GET_USER_SETTINGSTest_TestAction.Conditions.Add(chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value);
            app_Usp_GET_USER_SETTINGSTest_TestAction.Conditions.Add(chk_Usp_GET_USER_SETTINGS_Check_Debug);
            resources.ApplyResources(app_Usp_GET_USER_SETTINGSTest_TestAction, "app_Usp_GET_USER_SETTINGSTest_TestAction");
            // 
            // chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error
            // 
            chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error.ColumnNumber = 1;
            chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error.Enabled = true;
            chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error.ExpectedValue = "The user is not logged in.";
            chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error.Name = "chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error";
            chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error.NullExpected = false;
            chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error.ResultSet = 1;
            chk_Usp_GET_USER_SETTINGS_Check_User_Not_Logged_In_Error.RowNumber = 1;
            // 
            // chk_Usp_GET_USER_SETTINGS_Check_Row_Count
            // 
            chk_Usp_GET_USER_SETTINGS_Check_Row_Count.Enabled = true;
            chk_Usp_GET_USER_SETTINGS_Check_Row_Count.Name = "chk_Usp_GET_USER_SETTINGS_Check_Row_Count";
            chk_Usp_GET_USER_SETTINGS_Check_Row_Count.ResultSet = 2;
            chk_Usp_GET_USER_SETTINGS_Check_Row_Count.RowCount = 3;
            // 
            // chk_Usp_GET_USER_SETTINGS_Check_Row1_Key
            // 
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Key.ColumnNumber = 1;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Key.Enabled = true;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Key.ExpectedValue = "CULTURE";
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Key.Name = "chk_Usp_GET_USER_SETTINGS_Check_Row1_Key";
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Key.NullExpected = false;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Key.ResultSet = 2;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Key.RowNumber = 1;
            // 
            // chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc
            // 
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc.ColumnNumber = 2;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc.Enabled = true;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc.ExpectedValue = "The culture of the user";
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc.Name = "chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc";
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc.NullExpected = false;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc.ResultSet = 2;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Desc.RowNumber = 1;
            // 
            // chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType
            // 
            chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType.ColumnNumber = 3;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType.Enabled = true;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType.ExpectedValue = "system.string";
            chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType.Name = "chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType";
            chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType.NullExpected = false;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType.ResultSet = 2;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_DataType.RowNumber = 1;
            // 
            // chk_Usp_GET_USER_SETTINGS_Check_Row1_Value
            // 
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Value.ColumnNumber = 4;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Value.Enabled = true;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Value.ExpectedValue = "en-GB";
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Value.Name = "chk_Usp_GET_USER_SETTINGS_Check_Row1_Value";
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Value.NullExpected = false;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Value.ResultSet = 2;
            chk_Usp_GET_USER_SETTINGS_Check_Row1_Value.RowNumber = 1;
            // 
            // app_Usp_GET_USER_SETTINGSTest_PosttestAction
            // 
            resources.ApplyResources(app_Usp_GET_USER_SETTINGSTest_PosttestAction, "app_Usp_GET_USER_SETTINGSTest_PosttestAction");
            // 
            // chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value
            // 
            chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value.ColumnNumber = 4;
            chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value.Enabled = true;
            chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value.ExpectedValue = "fr-FR";
            chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value.Name = "chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value";
            chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value.NullExpected = false;
            chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value.ResultSet = 3;
            chk_Usp_GET_USER_SETTINGS_Check_UserSetting_Row1_Value.RowNumber = 1;
            // 
            // chk_Usp_GET_USER_SETTINGS_Check_Debug
            // 
            chk_Usp_GET_USER_SETTINGS_Check_Debug.Enabled = true;
            chk_Usp_GET_USER_SETTINGS_Check_Debug.Name = "chk_Usp_GET_USER_SETTINGS_Check_Debug";
            chk_Usp_GET_USER_SETTINGS_Check_Debug.ResultSet = 4;
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
        public void app_Usp_GET_USER_SETTINGSTest()
        {
            SqlDatabaseTestActions testActions = this.app_Usp_GET_USER_SETTINGSTestData;
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
        private SqlDatabaseTestActions app_Usp_GET_USER_SETTINGSTestData;
    }
}
