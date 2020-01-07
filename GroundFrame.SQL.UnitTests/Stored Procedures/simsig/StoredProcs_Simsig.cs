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
    public class StoredProcs_Simsig : SqlDatabaseTestClass
    {

        public StoredProcs_Simsig()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_UPSERT_TSIMTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoredProcs_Simsig));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NotLoggedIn_Errors;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NULLName_Errors;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_EmptyName_Errors;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NULLSimSigCode_Errors;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_EmptyStringSimSigCode_Errors;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_Description;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_SimSigWikiLink;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_SimSigCode;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_CreatedOn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_CreatedByID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_ModifiedOn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_ModifiedByID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_Default_SimEra_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Default_SimEra_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Default_SimEra_Description;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Default_SimEra_EraType;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_UpdateFromName_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromName_ID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromName_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromName_Description;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromName_SimSigWikiLink;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromName_SimSigCode;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromName_ModifiedOnChanged;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromID_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromID_Description;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromID_SimSigWikiLink;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UpdateFromID_SimSigCode;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_OneRecordCreated;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_NewRecord2_OneRecordCreated;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord2_NewIDIssued;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord2_CreatedOnCorrect;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord2_ModifiedOnCorrect;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord2_DebugEvents;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord3_DebugEvents;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_UPSERT_TSIMTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_GET_TSIMTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_UserLoggedIn_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_SpecificID_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_AllRows_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_DebugEvents_NoSession;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_DebugEvents_WithSession;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_GET_TSIMTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_Usp_DELETE_TSIMTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_DeleteSim_CheckSimCreation;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_DeleteSim_CheckSimEraCreation;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_DeleteSim_CheckSimDeletion;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_DeleteSim_CheckSimEraDeletion;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_DeleteSim_CheckUserLoggedInError;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_GetSimEraBySim_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_TestDataID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_testdataid_added;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_Usp_UPSERT_TSIMERATest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_User_LoggedIn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_No_Name_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Invalid_Sim_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Invalid_Type_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_Upsert_TSIMERA_Check_InsertRowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_UpdateRow_Era;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_InsertRow_CheckType;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_UpdateRow_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_UpdateRow_Description;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_UpdateRow_Type;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition chk_Upsert_TSIMERA_Check_Debug_Data;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition chk_Upsert_TSIMERA_Check_Test_Data;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_StandardUser_Errors;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_StandardUser_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_GetSimEraBySim_Check_NotLoggedIn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_DeleteSim_CheckStandardUserError;
            this.simsig_USp_UPSERT_TSIMTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_USp_GET_TSIMTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_Usp_DELETE_TSIMTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_Usp_GET_TSIMERA_BY_SIMTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_Usp_UPSERT_TSIMERATestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            simsig_USp_UPSERT_TSIMTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_NotLoggedIn_Errors = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NULLName_Errors = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_EmptyName_Errors = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NULLSimSigCode_Errors = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_EmptyStringSimSigCode_Errors = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord_Description = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord_SimSigWikiLink = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord_SimSigCode = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord_CreatedOn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord_CreatedByID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord_ModifiedOn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord_ModifiedByID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Default_SimEra_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_Default_SimEra_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Default_SimEra_Description = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Default_SimEra_EraType = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromName_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_UpdateFromName_ID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromName_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromName_Description = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromName_SimSigWikiLink = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromName_SimSigCode = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromName_ModifiedOnChanged = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromID_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromID_Description = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromID_SimSigWikiLink = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_UpdateFromID_SimSigCode = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_OneRecordCreated = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_NewRecord2_OneRecordCreated = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_NewRecord2_NewIDIssued = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord2_CreatedOnCorrect = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord2_ModifiedOnCorrect = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord2_DebugEvents = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_NewRecord3_DebugEvents = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_USp_UPSERT_TSIMTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            simsig_USp_GET_TSIMTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_UserLoggedIn_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_SpecificID_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_AllRows_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_DebugEvents_NoSession = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_DebugEvents_WithSession = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_USp_GET_TSIMTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            simsig_Usp_DELETE_TSIMTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_DeleteSim_CheckSimCreation = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_DeleteSim_CheckSimEraCreation = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_DeleteSim_CheckSimDeletion = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_DeleteSim_CheckSimEraDeletion = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_DeleteSim_CheckUserLoggedInError = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_GetSimEraBySim_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_NewRecord_TestDataID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_testdataid_added = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_Usp_UPSERT_TSIMERATest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Upsert_TSIMERA_User_LoggedIn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_No_Name_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Invalid_Sim_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Invalid_Type_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_InsertRowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_Upsert_TSIMERA_Check_UpdateRow_Era = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_InsertRow_CheckType = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_UpdateRow_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_UpdateRow_Description = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_UpdateRow_Type = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_Debug_Data = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            chk_Upsert_TSIMERA_Check_Test_Data = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            chk_StandardUser_Errors = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TSIMERA_Check_StandardUser_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_GetSimEraBySim_Check_NotLoggedIn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_DeleteSim_CheckStandardUserError = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // simsig_USp_UPSERT_TSIMTest_TestAction
            // 
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NotLoggedIn_Errors);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NULLName_Errors);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_EmptyName_Errors);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NULLSimSigCode_Errors);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_EmptyStringSimSigCode_Errors);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_Name);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_Description);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_SimSigWikiLink);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_SimSigCode);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_CreatedOn);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_CreatedByID);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_ModifiedOn);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_ModifiedByID);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord_TestDataID);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_Default_SimEra_RowCount);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_Default_SimEra_Name);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_Default_SimEra_Description);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_Default_SimEra_EraType);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromName_RowCount);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromName_ID);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromName_Name);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromName_Description);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromName_SimSigWikiLink);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromName_SimSigCode);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromName_ModifiedOnChanged);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromID_Name);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromID_Description);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromID_SimSigWikiLink);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_UpdateFromID_SimSigCode);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_OneRecordCreated);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord2_OneRecordCreated);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord2_NewIDIssued);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord2_CreatedOnCorrect);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord2_ModifiedOnCorrect);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord2_DebugEvents);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_NewRecord3_DebugEvents);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_testdataid_added);
            simsig_USp_UPSERT_TSIMTest_TestAction.Conditions.Add(chk_StandardUser_Errors);
            resources.ApplyResources(simsig_USp_UPSERT_TSIMTest_TestAction, "simsig_USp_UPSERT_TSIMTest_TestAction");
            // 
            // chk_NotLoggedIn_Errors
            // 
            chk_NotLoggedIn_Errors.ColumnNumber = 1;
            chk_NotLoggedIn_Errors.Enabled = true;
            chk_NotLoggedIn_Errors.ExpectedValue = "The user is not logged in.";
            chk_NotLoggedIn_Errors.Name = "chk_NotLoggedIn_Errors";
            chk_NotLoggedIn_Errors.NullExpected = false;
            chk_NotLoggedIn_Errors.ResultSet = 1;
            chk_NotLoggedIn_Errors.RowNumber = 1;
            // 
            // chk_NULLName_Errors
            // 
            chk_NULLName_Errors.ColumnNumber = 1;
            chk_NULLName_Errors.Enabled = true;
            chk_NULLName_Errors.ExpectedValue = "No valid name was supplied for the simulation.";
            chk_NULLName_Errors.Name = "chk_NULLName_Errors";
            chk_NULLName_Errors.NullExpected = false;
            chk_NULLName_Errors.ResultSet = 2;
            chk_NULLName_Errors.RowNumber = 1;
            // 
            // chk_EmptyName_Errors
            // 
            chk_EmptyName_Errors.ColumnNumber = 1;
            chk_EmptyName_Errors.Enabled = true;
            chk_EmptyName_Errors.ExpectedValue = "No valid name was supplied for the simulation.";
            chk_EmptyName_Errors.Name = "chk_EmptyName_Errors";
            chk_EmptyName_Errors.NullExpected = false;
            chk_EmptyName_Errors.ResultSet = 3;
            chk_EmptyName_Errors.RowNumber = 1;
            // 
            // chk_NULLSimSigCode_Errors
            // 
            chk_NULLSimSigCode_Errors.ColumnNumber = 1;
            chk_NULLSimSigCode_Errors.Enabled = true;
            chk_NULLSimSigCode_Errors.ExpectedValue = "No valid simsig_code was supplied for the simulation.";
            chk_NULLSimSigCode_Errors.Name = "chk_NULLSimSigCode_Errors";
            chk_NULLSimSigCode_Errors.NullExpected = false;
            chk_NULLSimSigCode_Errors.ResultSet = 4;
            chk_NULLSimSigCode_Errors.RowNumber = 1;
            // 
            // chk_EmptyStringSimSigCode_Errors
            // 
            chk_EmptyStringSimSigCode_Errors.ColumnNumber = 1;
            chk_EmptyStringSimSigCode_Errors.Enabled = true;
            chk_EmptyStringSimSigCode_Errors.ExpectedValue = "No valid simsig_code was supplied for the simulation.";
            chk_EmptyStringSimSigCode_Errors.Name = "chk_EmptyStringSimSigCode_Errors";
            chk_EmptyStringSimSigCode_Errors.NullExpected = false;
            chk_EmptyStringSimSigCode_Errors.ResultSet = 5;
            chk_EmptyStringSimSigCode_Errors.RowNumber = 1;
            // 
            // chk_NewRecord_Name
            // 
            chk_NewRecord_Name.ColumnNumber = 1;
            chk_NewRecord_Name.Enabled = true;
            chk_NewRecord_Name.ExpectedValue = "Test Name";
            chk_NewRecord_Name.Name = "chk_NewRecord_Name";
            chk_NewRecord_Name.NullExpected = false;
            chk_NewRecord_Name.ResultSet = 6;
            chk_NewRecord_Name.RowNumber = 1;
            // 
            // chk_NewRecord_Description
            // 
            chk_NewRecord_Description.ColumnNumber = 2;
            chk_NewRecord_Description.Enabled = true;
            chk_NewRecord_Description.ExpectedValue = "Test Description";
            chk_NewRecord_Description.Name = "chk_NewRecord_Description";
            chk_NewRecord_Description.NullExpected = false;
            chk_NewRecord_Description.ResultSet = 6;
            chk_NewRecord_Description.RowNumber = 1;
            // 
            // chk_NewRecord_SimSigWikiLink
            // 
            chk_NewRecord_SimSigWikiLink.ColumnNumber = 3;
            chk_NewRecord_SimSigWikiLink.Enabled = true;
            chk_NewRecord_SimSigWikiLink.ExpectedValue = "Test Wiki Link";
            chk_NewRecord_SimSigWikiLink.Name = "chk_NewRecord_SimSigWikiLink";
            chk_NewRecord_SimSigWikiLink.NullExpected = false;
            chk_NewRecord_SimSigWikiLink.ResultSet = 6;
            chk_NewRecord_SimSigWikiLink.RowNumber = 1;
            // 
            // chk_NewRecord_SimSigCode
            // 
            chk_NewRecord_SimSigCode.ColumnNumber = 4;
            chk_NewRecord_SimSigCode.Enabled = true;
            chk_NewRecord_SimSigCode.ExpectedValue = "test simsig code";
            chk_NewRecord_SimSigCode.Name = "chk_NewRecord_SimSigCode";
            chk_NewRecord_SimSigCode.NullExpected = false;
            chk_NewRecord_SimSigCode.ResultSet = 6;
            chk_NewRecord_SimSigCode.RowNumber = 1;
            // 
            // chk_NewRecord_CreatedOn
            // 
            chk_NewRecord_CreatedOn.ColumnNumber = 5;
            chk_NewRecord_CreatedOn.Enabled = true;
            chk_NewRecord_CreatedOn.ExpectedValue = "true";
            chk_NewRecord_CreatedOn.Name = "chk_NewRecord_CreatedOn";
            chk_NewRecord_CreatedOn.NullExpected = false;
            chk_NewRecord_CreatedOn.ResultSet = 6;
            chk_NewRecord_CreatedOn.RowNumber = 1;
            // 
            // chk_NewRecord_CreatedByID
            // 
            chk_NewRecord_CreatedByID.ColumnNumber = 6;
            chk_NewRecord_CreatedByID.Enabled = true;
            chk_NewRecord_CreatedByID.ExpectedValue = "2";
            chk_NewRecord_CreatedByID.Name = "chk_NewRecord_CreatedByID";
            chk_NewRecord_CreatedByID.NullExpected = false;
            chk_NewRecord_CreatedByID.ResultSet = 6;
            chk_NewRecord_CreatedByID.RowNumber = 1;
            // 
            // chk_NewRecord_ModifiedOn
            // 
            chk_NewRecord_ModifiedOn.ColumnNumber = 7;
            chk_NewRecord_ModifiedOn.Enabled = true;
            chk_NewRecord_ModifiedOn.ExpectedValue = "true";
            chk_NewRecord_ModifiedOn.Name = "chk_NewRecord_ModifiedOn";
            chk_NewRecord_ModifiedOn.NullExpected = false;
            chk_NewRecord_ModifiedOn.ResultSet = 6;
            chk_NewRecord_ModifiedOn.RowNumber = 1;
            // 
            // chk_NewRecord_ModifiedByID
            // 
            chk_NewRecord_ModifiedByID.ColumnNumber = 8;
            chk_NewRecord_ModifiedByID.Enabled = true;
            chk_NewRecord_ModifiedByID.ExpectedValue = "2";
            chk_NewRecord_ModifiedByID.Name = "chk_NewRecord_ModifiedByID";
            chk_NewRecord_ModifiedByID.NullExpected = false;
            chk_NewRecord_ModifiedByID.ResultSet = 6;
            chk_NewRecord_ModifiedByID.RowNumber = 1;
            // 
            // chk_Default_SimEra_RowCount
            // 
            chk_Default_SimEra_RowCount.Enabled = true;
            chk_Default_SimEra_RowCount.Name = "chk_Default_SimEra_RowCount";
            chk_Default_SimEra_RowCount.ResultSet = 7;
            chk_Default_SimEra_RowCount.RowCount = 1;
            // 
            // chk_Default_SimEra_Name
            // 
            chk_Default_SimEra_Name.ColumnNumber = 3;
            chk_Default_SimEra_Name.Enabled = true;
            chk_Default_SimEra_Name.ExpectedValue = "Test Name Template";
            chk_Default_SimEra_Name.Name = "chk_Default_SimEra_Name";
            chk_Default_SimEra_Name.NullExpected = false;
            chk_Default_SimEra_Name.ResultSet = 7;
            chk_Default_SimEra_Name.RowNumber = 1;
            // 
            // chk_Default_SimEra_Description
            // 
            chk_Default_SimEra_Description.ColumnNumber = 4;
            chk_Default_SimEra_Description.Enabled = true;
            chk_Default_SimEra_Description.ExpectedValue = "Default era template.";
            chk_Default_SimEra_Description.Name = "chk_Default_SimEra_Description";
            chk_Default_SimEra_Description.NullExpected = false;
            chk_Default_SimEra_Description.ResultSet = 7;
            chk_Default_SimEra_Description.RowNumber = 1;
            // 
            // chk_Default_SimEra_EraType
            // 
            chk_Default_SimEra_EraType.ColumnNumber = 5;
            chk_Default_SimEra_EraType.Enabled = true;
            chk_Default_SimEra_EraType.ExpectedValue = "2";
            chk_Default_SimEra_EraType.Name = "chk_Default_SimEra_EraType";
            chk_Default_SimEra_EraType.NullExpected = false;
            chk_Default_SimEra_EraType.ResultSet = 7;
            chk_Default_SimEra_EraType.RowNumber = 1;
            // 
            // chk_UpdateFromName_RowCount
            // 
            chk_UpdateFromName_RowCount.Enabled = true;
            chk_UpdateFromName_RowCount.Name = "chk_UpdateFromName_RowCount";
            chk_UpdateFromName_RowCount.ResultSet = 8;
            chk_UpdateFromName_RowCount.RowCount = 1;
            // 
            // chk_UpdateFromName_ID
            // 
            chk_UpdateFromName_ID.ColumnNumber = 1;
            chk_UpdateFromName_ID.Enabled = true;
            chk_UpdateFromName_ID.ExpectedValue = "true";
            chk_UpdateFromName_ID.Name = "chk_UpdateFromName_ID";
            chk_UpdateFromName_ID.NullExpected = false;
            chk_UpdateFromName_ID.ResultSet = 8;
            chk_UpdateFromName_ID.RowNumber = 1;
            // 
            // chk_UpdateFromName_Name
            // 
            chk_UpdateFromName_Name.ColumnNumber = 2;
            chk_UpdateFromName_Name.Enabled = true;
            chk_UpdateFromName_Name.ExpectedValue = "Test Name";
            chk_UpdateFromName_Name.Name = "chk_UpdateFromName_Name";
            chk_UpdateFromName_Name.NullExpected = false;
            chk_UpdateFromName_Name.ResultSet = 8;
            chk_UpdateFromName_Name.RowNumber = 1;
            // 
            // chk_UpdateFromName_Description
            // 
            chk_UpdateFromName_Description.ColumnNumber = 3;
            chk_UpdateFromName_Description.Enabled = true;
            chk_UpdateFromName_Description.ExpectedValue = "Test Description 1";
            chk_UpdateFromName_Description.Name = "chk_UpdateFromName_Description";
            chk_UpdateFromName_Description.NullExpected = false;
            chk_UpdateFromName_Description.ResultSet = 8;
            chk_UpdateFromName_Description.RowNumber = 1;
            // 
            // chk_UpdateFromName_SimSigWikiLink
            // 
            chk_UpdateFromName_SimSigWikiLink.ColumnNumber = 4;
            chk_UpdateFromName_SimSigWikiLink.Enabled = true;
            chk_UpdateFromName_SimSigWikiLink.ExpectedValue = "Test Wiki Link 1";
            chk_UpdateFromName_SimSigWikiLink.Name = "chk_UpdateFromName_SimSigWikiLink";
            chk_UpdateFromName_SimSigWikiLink.NullExpected = false;
            chk_UpdateFromName_SimSigWikiLink.ResultSet = 8;
            chk_UpdateFromName_SimSigWikiLink.RowNumber = 1;
            // 
            // chk_UpdateFromName_SimSigCode
            // 
            chk_UpdateFromName_SimSigCode.ColumnNumber = 5;
            chk_UpdateFromName_SimSigCode.Enabled = true;
            chk_UpdateFromName_SimSigCode.ExpectedValue = "test simsig code";
            chk_UpdateFromName_SimSigCode.Name = "chk_UpdateFromName_SimSigCode";
            chk_UpdateFromName_SimSigCode.NullExpected = false;
            chk_UpdateFromName_SimSigCode.ResultSet = 8;
            chk_UpdateFromName_SimSigCode.RowNumber = 1;
            // 
            // chk_UpdateFromName_ModifiedOnChanged
            // 
            chk_UpdateFromName_ModifiedOnChanged.ColumnNumber = 8;
            chk_UpdateFromName_ModifiedOnChanged.Enabled = true;
            chk_UpdateFromName_ModifiedOnChanged.ExpectedValue = "true";
            chk_UpdateFromName_ModifiedOnChanged.Name = "chk_UpdateFromName_ModifiedOnChanged";
            chk_UpdateFromName_ModifiedOnChanged.NullExpected = false;
            chk_UpdateFromName_ModifiedOnChanged.ResultSet = 8;
            chk_UpdateFromName_ModifiedOnChanged.RowNumber = 1;
            // 
            // chk_UpdateFromID_Name
            // 
            chk_UpdateFromID_Name.ColumnNumber = 1;
            chk_UpdateFromID_Name.Enabled = true;
            chk_UpdateFromID_Name.ExpectedValue = "Test Name";
            chk_UpdateFromID_Name.Name = "chk_UpdateFromID_Name";
            chk_UpdateFromID_Name.NullExpected = false;
            chk_UpdateFromID_Name.ResultSet = 9;
            chk_UpdateFromID_Name.RowNumber = 1;
            // 
            // chk_UpdateFromID_Description
            // 
            chk_UpdateFromID_Description.ColumnNumber = 2;
            chk_UpdateFromID_Description.Enabled = true;
            chk_UpdateFromID_Description.ExpectedValue = "Test Description 2";
            chk_UpdateFromID_Description.Name = "chk_UpdateFromID_Description";
            chk_UpdateFromID_Description.NullExpected = false;
            chk_UpdateFromID_Description.ResultSet = 9;
            chk_UpdateFromID_Description.RowNumber = 1;
            // 
            // chk_UpdateFromID_SimSigWikiLink
            // 
            chk_UpdateFromID_SimSigWikiLink.ColumnNumber = 3;
            chk_UpdateFromID_SimSigWikiLink.Enabled = true;
            chk_UpdateFromID_SimSigWikiLink.ExpectedValue = "Test Wiki Link 2";
            chk_UpdateFromID_SimSigWikiLink.Name = "chk_UpdateFromID_SimSigWikiLink";
            chk_UpdateFromID_SimSigWikiLink.NullExpected = false;
            chk_UpdateFromID_SimSigWikiLink.ResultSet = 9;
            chk_UpdateFromID_SimSigWikiLink.RowNumber = 1;
            // 
            // chk_UpdateFromID_SimSigCode
            // 
            chk_UpdateFromID_SimSigCode.ColumnNumber = 4;
            chk_UpdateFromID_SimSigCode.Enabled = true;
            chk_UpdateFromID_SimSigCode.ExpectedValue = "test simsig code";
            chk_UpdateFromID_SimSigCode.Name = "chk_UpdateFromID_SimSigCode";
            chk_UpdateFromID_SimSigCode.NullExpected = false;
            chk_UpdateFromID_SimSigCode.ResultSet = 9;
            chk_UpdateFromID_SimSigCode.RowNumber = 1;
            // 
            // chk_OneRecordCreated
            // 
            chk_OneRecordCreated.Enabled = true;
            chk_OneRecordCreated.Name = "chk_OneRecordCreated";
            chk_OneRecordCreated.ResultSet = 10;
            chk_OneRecordCreated.RowCount = 1;
            // 
            // chk_NewRecord2_OneRecordCreated
            // 
            chk_NewRecord2_OneRecordCreated.Enabled = true;
            chk_NewRecord2_OneRecordCreated.Name = "chk_NewRecord2_OneRecordCreated";
            chk_NewRecord2_OneRecordCreated.ResultSet = 11;
            chk_NewRecord2_OneRecordCreated.RowCount = 1;
            // 
            // chk_NewRecord2_NewIDIssued
            // 
            chk_NewRecord2_NewIDIssued.ColumnNumber = 1;
            chk_NewRecord2_NewIDIssued.Enabled = true;
            chk_NewRecord2_NewIDIssued.ExpectedValue = "true";
            chk_NewRecord2_NewIDIssued.Name = "chk_NewRecord2_NewIDIssued";
            chk_NewRecord2_NewIDIssued.NullExpected = false;
            chk_NewRecord2_NewIDIssued.ResultSet = 11;
            chk_NewRecord2_NewIDIssued.RowNumber = 1;
            // 
            // chk_NewRecord2_CreatedOnCorrect
            // 
            chk_NewRecord2_CreatedOnCorrect.ColumnNumber = 6;
            chk_NewRecord2_CreatedOnCorrect.Enabled = true;
            chk_NewRecord2_CreatedOnCorrect.ExpectedValue = "true";
            chk_NewRecord2_CreatedOnCorrect.Name = "chk_NewRecord2_CreatedOnCorrect";
            chk_NewRecord2_CreatedOnCorrect.NullExpected = false;
            chk_NewRecord2_CreatedOnCorrect.ResultSet = 11;
            chk_NewRecord2_CreatedOnCorrect.RowNumber = 1;
            // 
            // chk_NewRecord2_ModifiedOnCorrect
            // 
            chk_NewRecord2_ModifiedOnCorrect.ColumnNumber = 8;
            chk_NewRecord2_ModifiedOnCorrect.Enabled = true;
            chk_NewRecord2_ModifiedOnCorrect.ExpectedValue = "true";
            chk_NewRecord2_ModifiedOnCorrect.Name = "chk_NewRecord2_ModifiedOnCorrect";
            chk_NewRecord2_ModifiedOnCorrect.NullExpected = false;
            chk_NewRecord2_ModifiedOnCorrect.ResultSet = 11;
            chk_NewRecord2_ModifiedOnCorrect.RowNumber = 1;
            // 
            // chk_NewRecord2_DebugEvents
            // 
            chk_NewRecord2_DebugEvents.ColumnNumber = 1;
            chk_NewRecord2_DebugEvents.Enabled = true;
            chk_NewRecord2_DebugEvents.ExpectedValue = "true";
            chk_NewRecord2_DebugEvents.Name = "chk_NewRecord2_DebugEvents";
            chk_NewRecord2_DebugEvents.NullExpected = false;
            chk_NewRecord2_DebugEvents.ResultSet = 12;
            chk_NewRecord2_DebugEvents.RowNumber = 1;
            // 
            // chk_NewRecord3_DebugEvents
            // 
            chk_NewRecord3_DebugEvents.ColumnNumber = 1;
            chk_NewRecord3_DebugEvents.Enabled = true;
            chk_NewRecord3_DebugEvents.ExpectedValue = "true";
            chk_NewRecord3_DebugEvents.Name = "chk_NewRecord3_DebugEvents";
            chk_NewRecord3_DebugEvents.NullExpected = false;
            chk_NewRecord3_DebugEvents.ResultSet = 13;
            chk_NewRecord3_DebugEvents.RowNumber = 1;
            // 
            // simsig_USp_UPSERT_TSIMTest_PosttestAction
            // 
            resources.ApplyResources(simsig_USp_UPSERT_TSIMTest_PosttestAction, "simsig_USp_UPSERT_TSIMTest_PosttestAction");
            // 
            // simsig_USp_GET_TSIMTest_TestAction
            // 
            simsig_USp_GET_TSIMTest_TestAction.Conditions.Add(chk_UserLoggedIn_Error);
            simsig_USp_GET_TSIMTest_TestAction.Conditions.Add(chk_SpecificID_RowCount);
            simsig_USp_GET_TSIMTest_TestAction.Conditions.Add(chk_AllRows_RowCount);
            simsig_USp_GET_TSIMTest_TestAction.Conditions.Add(chk_DebugEvents_NoSession);
            simsig_USp_GET_TSIMTest_TestAction.Conditions.Add(chk_DebugEvents_WithSession);
            resources.ApplyResources(simsig_USp_GET_TSIMTest_TestAction, "simsig_USp_GET_TSIMTest_TestAction");
            // 
            // chk_UserLoggedIn_Error
            // 
            chk_UserLoggedIn_Error.ColumnNumber = 1;
            chk_UserLoggedIn_Error.Enabled = true;
            chk_UserLoggedIn_Error.ExpectedValue = "The user is not logged in.";
            chk_UserLoggedIn_Error.Name = "chk_UserLoggedIn_Error";
            chk_UserLoggedIn_Error.NullExpected = false;
            chk_UserLoggedIn_Error.ResultSet = 1;
            chk_UserLoggedIn_Error.RowNumber = 1;
            // 
            // chk_SpecificID_RowCount
            // 
            chk_SpecificID_RowCount.Enabled = true;
            chk_SpecificID_RowCount.Name = "chk_SpecificID_RowCount";
            chk_SpecificID_RowCount.ResultSet = 2;
            chk_SpecificID_RowCount.RowCount = 1;
            // 
            // chk_AllRows_RowCount
            // 
            chk_AllRows_RowCount.Enabled = true;
            chk_AllRows_RowCount.Name = "chk_AllRows_RowCount";
            chk_AllRows_RowCount.ResultSet = 3;
            chk_AllRows_RowCount.RowCount = 2;
            // 
            // chk_DebugEvents_NoSession
            // 
            chk_DebugEvents_NoSession.ColumnNumber = 1;
            chk_DebugEvents_NoSession.Enabled = true;
            chk_DebugEvents_NoSession.ExpectedValue = "true";
            chk_DebugEvents_NoSession.Name = "chk_DebugEvents_NoSession";
            chk_DebugEvents_NoSession.NullExpected = false;
            chk_DebugEvents_NoSession.ResultSet = 7;
            chk_DebugEvents_NoSession.RowNumber = 1;
            // 
            // chk_DebugEvents_WithSession
            // 
            chk_DebugEvents_WithSession.ColumnNumber = 1;
            chk_DebugEvents_WithSession.Enabled = true;
            chk_DebugEvents_WithSession.ExpectedValue = "true";
            chk_DebugEvents_WithSession.Name = "chk_DebugEvents_WithSession";
            chk_DebugEvents_WithSession.NullExpected = false;
            chk_DebugEvents_WithSession.ResultSet = 5;
            chk_DebugEvents_WithSession.RowNumber = 1;
            // 
            // simsig_USp_GET_TSIMTest_PosttestAction
            // 
            resources.ApplyResources(simsig_USp_GET_TSIMTest_PosttestAction, "simsig_USp_GET_TSIMTest_PosttestAction");
            // 
            // simsig_Usp_DELETE_TSIMTest_TestAction
            // 
            simsig_Usp_DELETE_TSIMTest_TestAction.Conditions.Add(chk_DeleteSim_CheckSimCreation);
            simsig_Usp_DELETE_TSIMTest_TestAction.Conditions.Add(chk_DeleteSim_CheckSimEraCreation);
            simsig_Usp_DELETE_TSIMTest_TestAction.Conditions.Add(chk_DeleteSim_CheckUserLoggedInError);
            simsig_Usp_DELETE_TSIMTest_TestAction.Conditions.Add(chk_DeleteSim_CheckSimDeletion);
            simsig_Usp_DELETE_TSIMTest_TestAction.Conditions.Add(chk_DeleteSim_CheckSimEraDeletion);
            simsig_Usp_DELETE_TSIMTest_TestAction.Conditions.Add(chk_DeleteSim_CheckStandardUserError);
            resources.ApplyResources(simsig_Usp_DELETE_TSIMTest_TestAction, "simsig_Usp_DELETE_TSIMTest_TestAction");
            // 
            // chk_DeleteSim_CheckSimCreation
            // 
            chk_DeleteSim_CheckSimCreation.Enabled = true;
            chk_DeleteSim_CheckSimCreation.Name = "chk_DeleteSim_CheckSimCreation";
            chk_DeleteSim_CheckSimCreation.ResultSet = 1;
            chk_DeleteSim_CheckSimCreation.RowCount = 1;
            // 
            // chk_DeleteSim_CheckSimEraCreation
            // 
            chk_DeleteSim_CheckSimEraCreation.Enabled = true;
            chk_DeleteSim_CheckSimEraCreation.Name = "chk_DeleteSim_CheckSimEraCreation";
            chk_DeleteSim_CheckSimEraCreation.ResultSet = 2;
            chk_DeleteSim_CheckSimEraCreation.RowCount = 1;
            // 
            // chk_DeleteSim_CheckSimDeletion
            // 
            chk_DeleteSim_CheckSimDeletion.Enabled = true;
            chk_DeleteSim_CheckSimDeletion.Name = "chk_DeleteSim_CheckSimDeletion";
            chk_DeleteSim_CheckSimDeletion.ResultSet = 5;
            chk_DeleteSim_CheckSimDeletion.RowCount = 0;
            // 
            // chk_DeleteSim_CheckSimEraDeletion
            // 
            chk_DeleteSim_CheckSimEraDeletion.Enabled = true;
            chk_DeleteSim_CheckSimEraDeletion.Name = "chk_DeleteSim_CheckSimEraDeletion";
            chk_DeleteSim_CheckSimEraDeletion.ResultSet = 6;
            chk_DeleteSim_CheckSimEraDeletion.RowCount = 0;
            // 
            // chk_DeleteSim_CheckUserLoggedInError
            // 
            chk_DeleteSim_CheckUserLoggedInError.ColumnNumber = 1;
            chk_DeleteSim_CheckUserLoggedInError.Enabled = true;
            chk_DeleteSim_CheckUserLoggedInError.ExpectedValue = "The user is not logged in.";
            chk_DeleteSim_CheckUserLoggedInError.Name = "chk_DeleteSim_CheckUserLoggedInError";
            chk_DeleteSim_CheckUserLoggedInError.NullExpected = false;
            chk_DeleteSim_CheckUserLoggedInError.ResultSet = 3;
            chk_DeleteSim_CheckUserLoggedInError.RowNumber = 1;
            // 
            // simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction
            // 
            simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction.Conditions.Add(chk_GetSimEraBySim_RowCount);
            simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction.Conditions.Add(chk_GetSimEraBySim_Check_NotLoggedIn);
            resources.ApplyResources(simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction, "simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction");
            // 
            // chk_GetSimEraBySim_RowCount
            // 
            chk_GetSimEraBySim_RowCount.Enabled = true;
            chk_GetSimEraBySim_RowCount.Name = "chk_GetSimEraBySim_RowCount";
            chk_GetSimEraBySim_RowCount.ResultSet = 1;
            chk_GetSimEraBySim_RowCount.RowCount = 1;
            // 
            // simsig_USp_UPSERT_TSIMTestData
            // 
            this.simsig_USp_UPSERT_TSIMTestData.PosttestAction = simsig_USp_UPSERT_TSIMTest_PosttestAction;
            this.simsig_USp_UPSERT_TSIMTestData.PretestAction = null;
            this.simsig_USp_UPSERT_TSIMTestData.TestAction = simsig_USp_UPSERT_TSIMTest_TestAction;
            // 
            // simsig_USp_GET_TSIMTestData
            // 
            this.simsig_USp_GET_TSIMTestData.PosttestAction = simsig_USp_GET_TSIMTest_PosttestAction;
            this.simsig_USp_GET_TSIMTestData.PretestAction = null;
            this.simsig_USp_GET_TSIMTestData.TestAction = simsig_USp_GET_TSIMTest_TestAction;
            // 
            // simsig_Usp_DELETE_TSIMTestData
            // 
            this.simsig_Usp_DELETE_TSIMTestData.PosttestAction = null;
            this.simsig_Usp_DELETE_TSIMTestData.PretestAction = null;
            this.simsig_Usp_DELETE_TSIMTestData.TestAction = simsig_Usp_DELETE_TSIMTest_TestAction;
            // 
            // simsig_Usp_GET_TSIMERA_BY_SIMTestData
            // 
            this.simsig_Usp_GET_TSIMERA_BY_SIMTestData.PosttestAction = null;
            this.simsig_Usp_GET_TSIMERA_BY_SIMTestData.PretestAction = null;
            this.simsig_Usp_GET_TSIMERA_BY_SIMTestData.TestAction = simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction;
            // 
            // chk_NewRecord_TestDataID
            // 
            chk_NewRecord_TestDataID.ColumnNumber = 9;
            chk_NewRecord_TestDataID.Enabled = true;
            chk_NewRecord_TestDataID.ExpectedValue = null;
            chk_NewRecord_TestDataID.Name = "chk_NewRecord_TestDataID";
            chk_NewRecord_TestDataID.NullExpected = true;
            chk_NewRecord_TestDataID.ResultSet = 6;
            chk_NewRecord_TestDataID.RowNumber = 1;
            // 
            // chk_testdataid_added
            // 
            chk_testdataid_added.ColumnNumber = 1;
            chk_testdataid_added.Enabled = true;
            chk_testdataid_added.ExpectedValue = "true";
            chk_testdataid_added.Name = "chk_testdataid_added";
            chk_testdataid_added.NullExpected = false;
            chk_testdataid_added.ResultSet = 14;
            chk_testdataid_added.RowNumber = 1;
            // 
            // simsig_Usp_UPSERT_TSIMERATestData
            // 
            this.simsig_Usp_UPSERT_TSIMERATestData.PosttestAction = null;
            this.simsig_Usp_UPSERT_TSIMERATestData.PretestAction = null;
            this.simsig_Usp_UPSERT_TSIMERATestData.TestAction = simsig_Usp_UPSERT_TSIMERATest_TestAction;
            // 
            // simsig_Usp_UPSERT_TSIMERATest_TestAction
            // 
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_User_LoggedIn);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_No_Name_Error);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Invalid_Sim_Error);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Invalid_Type_Error);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_InsertRowCount);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_UpdateRow_Era);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_InsertRow_CheckType);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_UpdateRow_Name);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_UpdateRow_Description);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_UpdateRow_Type);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_Debug_Data);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_Test_Data);
            simsig_Usp_UPSERT_TSIMERATest_TestAction.Conditions.Add(chk_Upsert_TSIMERA_Check_StandardUser_Error);
            resources.ApplyResources(simsig_Usp_UPSERT_TSIMERATest_TestAction, "simsig_Usp_UPSERT_TSIMERATest_TestAction");
            // 
            // chk_Upsert_TSIMERA_User_LoggedIn
            // 
            chk_Upsert_TSIMERA_User_LoggedIn.ColumnNumber = 1;
            chk_Upsert_TSIMERA_User_LoggedIn.Enabled = true;
            chk_Upsert_TSIMERA_User_LoggedIn.ExpectedValue = "The user is not logged in.";
            chk_Upsert_TSIMERA_User_LoggedIn.Name = "chk_Upsert_TSIMERA_User_LoggedIn";
            chk_Upsert_TSIMERA_User_LoggedIn.NullExpected = false;
            chk_Upsert_TSIMERA_User_LoggedIn.ResultSet = 1;
            chk_Upsert_TSIMERA_User_LoggedIn.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_No_Name_Error
            // 
            chk_Upsert_TSIMERA_No_Name_Error.ColumnNumber = 1;
            chk_Upsert_TSIMERA_No_Name_Error.Enabled = true;
            chk_Upsert_TSIMERA_No_Name_Error.ExpectedValue = "No valid name was supplied for the simulation era.";
            chk_Upsert_TSIMERA_No_Name_Error.Name = "chk_Upsert_TSIMERA_No_Name_Error";
            chk_Upsert_TSIMERA_No_Name_Error.NullExpected = false;
            chk_Upsert_TSIMERA_No_Name_Error.ResultSet = 2;
            chk_Upsert_TSIMERA_No_Name_Error.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Invalid_Sim_Error
            // 
            chk_Upsert_TSIMERA_Invalid_Sim_Error.ColumnNumber = 1;
            chk_Upsert_TSIMERA_Invalid_Sim_Error.Enabled = true;
            chk_Upsert_TSIMERA_Invalid_Sim_Error.ExpectedValue = "A valid simulation wasn\'t suppled for the simulation era.";
            chk_Upsert_TSIMERA_Invalid_Sim_Error.Name = "chk_Upsert_TSIMERA_Invalid_Sim_Error";
            chk_Upsert_TSIMERA_Invalid_Sim_Error.NullExpected = false;
            chk_Upsert_TSIMERA_Invalid_Sim_Error.ResultSet = 3;
            chk_Upsert_TSIMERA_Invalid_Sim_Error.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Invalid_Type_Error
            // 
            chk_Upsert_TSIMERA_Invalid_Type_Error.ColumnNumber = 1;
            chk_Upsert_TSIMERA_Invalid_Type_Error.Enabled = true;
            chk_Upsert_TSIMERA_Invalid_Type_Error.ExpectedValue = "A valid era type wasn\'t suppled for the simulation era.";
            chk_Upsert_TSIMERA_Invalid_Type_Error.Name = "chk_Upsert_TSIMERA_Invalid_Type_Error";
            chk_Upsert_TSIMERA_Invalid_Type_Error.NullExpected = false;
            chk_Upsert_TSIMERA_Invalid_Type_Error.ResultSet = 4;
            chk_Upsert_TSIMERA_Invalid_Type_Error.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_InsertRowCount
            // 
            chk_Upsert_TSIMERA_Check_InsertRowCount.Enabled = true;
            chk_Upsert_TSIMERA_Check_InsertRowCount.Name = "chk_Upsert_TSIMERA_Check_InsertRowCount";
            chk_Upsert_TSIMERA_Check_InsertRowCount.ResultSet = 5;
            chk_Upsert_TSIMERA_Check_InsertRowCount.RowCount = 1;
            // 
            // chk_Upsert_TSIMERA_Check_UpdateRow_Era
            // 
            chk_Upsert_TSIMERA_Check_UpdateRow_Era.ColumnNumber = 2;
            chk_Upsert_TSIMERA_Check_UpdateRow_Era.Enabled = true;
            chk_Upsert_TSIMERA_Check_UpdateRow_Era.ExpectedValue = "Test SimEra Name";
            chk_Upsert_TSIMERA_Check_UpdateRow_Era.Name = "chk_Upsert_TSIMERA_Check_UpdateRow_Era";
            chk_Upsert_TSIMERA_Check_UpdateRow_Era.NullExpected = false;
            chk_Upsert_TSIMERA_Check_UpdateRow_Era.ResultSet = 5;
            chk_Upsert_TSIMERA_Check_UpdateRow_Era.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc
            // 
            chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc.ColumnNumber = 3;
            chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc.Enabled = true;
            chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc.ExpectedValue = "Test SimEra Description";
            chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc.Name = "chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc";
            chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc.NullExpected = false;
            chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc.ResultSet = 5;
            chk_Upsert_TSIMERA_Check_InsertRow_CheckDesc.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_InsertRow_CheckType
            // 
            chk_Upsert_TSIMERA_Check_InsertRow_CheckType.ColumnNumber = 4;
            chk_Upsert_TSIMERA_Check_InsertRow_CheckType.Enabled = true;
            chk_Upsert_TSIMERA_Check_InsertRow_CheckType.ExpectedValue = "1";
            chk_Upsert_TSIMERA_Check_InsertRow_CheckType.Name = "chk_Upsert_TSIMERA_Check_InsertRow_CheckType";
            chk_Upsert_TSIMERA_Check_InsertRow_CheckType.NullExpected = false;
            chk_Upsert_TSIMERA_Check_InsertRow_CheckType.ResultSet = 5;
            chk_Upsert_TSIMERA_Check_InsertRow_CheckType.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert
            // 
            chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert.ColumnNumber = 1;
            chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert.Enabled = true;
            chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert.ExpectedValue = "true";
            chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert.Name = "chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert";
            chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert.NullExpected = false;
            chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert.ResultSet = 6;
            chk_Upsert_TSIMERA_Check_InsertRow_Check_SameName_Insert.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_UpdateRow_Name
            // 
            chk_Upsert_TSIMERA_Check_UpdateRow_Name.ColumnNumber = 2;
            chk_Upsert_TSIMERA_Check_UpdateRow_Name.Enabled = true;
            chk_Upsert_TSIMERA_Check_UpdateRow_Name.ExpectedValue = "Updated Name";
            chk_Upsert_TSIMERA_Check_UpdateRow_Name.Name = "chk_Upsert_TSIMERA_Check_UpdateRow_Name";
            chk_Upsert_TSIMERA_Check_UpdateRow_Name.NullExpected = false;
            chk_Upsert_TSIMERA_Check_UpdateRow_Name.ResultSet = 7;
            chk_Upsert_TSIMERA_Check_UpdateRow_Name.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_UpdateRow_Description
            // 
            chk_Upsert_TSIMERA_Check_UpdateRow_Description.ColumnNumber = 3;
            chk_Upsert_TSIMERA_Check_UpdateRow_Description.Enabled = true;
            chk_Upsert_TSIMERA_Check_UpdateRow_Description.ExpectedValue = "Updated Description";
            chk_Upsert_TSIMERA_Check_UpdateRow_Description.Name = "chk_Upsert_TSIMERA_Check_UpdateRow_Description";
            chk_Upsert_TSIMERA_Check_UpdateRow_Description.NullExpected = false;
            chk_Upsert_TSIMERA_Check_UpdateRow_Description.ResultSet = 7;
            chk_Upsert_TSIMERA_Check_UpdateRow_Description.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_UpdateRow_Type
            // 
            chk_Upsert_TSIMERA_Check_UpdateRow_Type.ColumnNumber = 4;
            chk_Upsert_TSIMERA_Check_UpdateRow_Type.Enabled = true;
            chk_Upsert_TSIMERA_Check_UpdateRow_Type.ExpectedValue = "2";
            chk_Upsert_TSIMERA_Check_UpdateRow_Type.Name = "chk_Upsert_TSIMERA_Check_UpdateRow_Type";
            chk_Upsert_TSIMERA_Check_UpdateRow_Type.NullExpected = false;
            chk_Upsert_TSIMERA_Check_UpdateRow_Type.ResultSet = 7;
            chk_Upsert_TSIMERA_Check_UpdateRow_Type.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_Debug_Data
            // 
            chk_Upsert_TSIMERA_Check_Debug_Data.Enabled = true;
            chk_Upsert_TSIMERA_Check_Debug_Data.Name = "chk_Upsert_TSIMERA_Check_Debug_Data";
            chk_Upsert_TSIMERA_Check_Debug_Data.ResultSet = 8;
            // 
            // chk_Upsert_TSIMERA_Check_Test_Data
            // 
            chk_Upsert_TSIMERA_Check_Test_Data.Enabled = true;
            chk_Upsert_TSIMERA_Check_Test_Data.Name = "chk_Upsert_TSIMERA_Check_Test_Data";
            chk_Upsert_TSIMERA_Check_Test_Data.ResultSet = 9;
            // 
            // chk_StandardUser_Errors
            // 
            chk_StandardUser_Errors.ColumnNumber = 1;
            chk_StandardUser_Errors.Enabled = true;
            chk_StandardUser_Errors.ExpectedValue = "The user does not have permission to perform this action.";
            chk_StandardUser_Errors.Name = "chk_StandardUser_Errors";
            chk_StandardUser_Errors.NullExpected = false;
            chk_StandardUser_Errors.ResultSet = 15;
            chk_StandardUser_Errors.RowNumber = 1;
            // 
            // chk_Upsert_TSIMERA_Check_StandardUser_Error
            // 
            chk_Upsert_TSIMERA_Check_StandardUser_Error.ColumnNumber = 1;
            chk_Upsert_TSIMERA_Check_StandardUser_Error.Enabled = true;
            chk_Upsert_TSIMERA_Check_StandardUser_Error.ExpectedValue = "The user does not have permission to perform this action.";
            chk_Upsert_TSIMERA_Check_StandardUser_Error.Name = "chk_Upsert_TSIMERA_Check_StandardUser_Error";
            chk_Upsert_TSIMERA_Check_StandardUser_Error.NullExpected = false;
            chk_Upsert_TSIMERA_Check_StandardUser_Error.ResultSet = 10;
            chk_Upsert_TSIMERA_Check_StandardUser_Error.RowNumber = 1;
            // 
            // chk_GetSimEraBySim_Check_NotLoggedIn
            // 
            chk_GetSimEraBySim_Check_NotLoggedIn.ColumnNumber = 1;
            chk_GetSimEraBySim_Check_NotLoggedIn.Enabled = true;
            chk_GetSimEraBySim_Check_NotLoggedIn.ExpectedValue = "The user is not logged in.";
            chk_GetSimEraBySim_Check_NotLoggedIn.Name = "chk_GetSimEraBySim_Check_NotLoggedIn";
            chk_GetSimEraBySim_Check_NotLoggedIn.NullExpected = false;
            chk_GetSimEraBySim_Check_NotLoggedIn.ResultSet = 2;
            chk_GetSimEraBySim_Check_NotLoggedIn.RowNumber = 1;
            // 
            // chk_DeleteSim_CheckStandardUserError
            // 
            chk_DeleteSim_CheckStandardUserError.ColumnNumber = 1;
            chk_DeleteSim_CheckStandardUserError.Enabled = true;
            chk_DeleteSim_CheckStandardUserError.ExpectedValue = "The user does not have permission to perform this action.";
            chk_DeleteSim_CheckStandardUserError.Name = "chk_DeleteSim_CheckStandardUserError";
            chk_DeleteSim_CheckStandardUserError.NullExpected = false;
            chk_DeleteSim_CheckStandardUserError.ResultSet = 4;
            chk_DeleteSim_CheckStandardUserError.RowNumber = 1;
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
        public void simsig_USp_UPSERT_TSIMTest()
        {
            SqlDatabaseTestActions testActions = this.simsig_USp_UPSERT_TSIMTestData;
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
        public void simsig_USp_GET_TSIMTest()
        {
            SqlDatabaseTestActions testActions = this.simsig_USp_GET_TSIMTestData;
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
        public void simsig_Usp_DELETE_TSIMTest()
        {
            SqlDatabaseTestActions testActions = this.simsig_Usp_DELETE_TSIMTestData;
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
        public void simsig_Usp_GET_TSIMERA_BY_SIMTest()
        {
            SqlDatabaseTestActions testActions = this.simsig_Usp_GET_TSIMERA_BY_SIMTestData;
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
        public void simsig_Usp_UPSERT_TSIMERATest()
        {
            SqlDatabaseTestActions testActions = this.simsig_Usp_UPSERT_TSIMERATestData;
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




        private SqlDatabaseTestActions simsig_USp_UPSERT_TSIMTestData;
        private SqlDatabaseTestActions simsig_USp_GET_TSIMTestData;
        private SqlDatabaseTestActions simsig_Usp_DELETE_TSIMTestData;
        private SqlDatabaseTestActions simsig_Usp_GET_TSIMERA_BY_SIMTestData;
        private SqlDatabaseTestActions simsig_Usp_UPSERT_TSIMERATestData;
    }
}
