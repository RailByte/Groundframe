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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_NewRecord_TestDataID;
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_testdataid_added;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_StandardUser_Errors;
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_DeleteSim_CheckUserLoggedInError;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_DeleteSim_CheckSimDeletion;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_DeleteSim_CheckSimEraDeletion;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_DeleteSim_CheckStandardUserError;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_GetSimEraBySim_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_GetSimEraBySim_Check_NotLoggedIn;
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TSIMERA_Check_StandardUser_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_UPSERT_TVERSIONTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Not_Logged_In_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Editor_No_Permission;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_StandardUser_No_Permission;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_InitialAdd_ID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_InitialAdd_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_InitialAdd_Description;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Version_From;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_InitialAdd_Version_To;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_InitialAdd_VersionStatus;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_InitialAdd_TestData_ID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Update_Check_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Update_Check_Description;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Update_Check_Status;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Update_Version_From;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Update_Version_To;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_UpdateVersionCheck_Version_From;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_UpdateVersionCheck_Version_To;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition chk_Upsert_TVERSION_Add2ndVersion_Check_Debug;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TVERSION_Check_VersionSplitError;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_UPSERT_TVERSIONTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_Usp_GET_TSIMERA_BY_SIMTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_Usp_UPSERT_TSIMERATest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_GET_TVERSIONTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_VERSION_Check_SingleIDPassed_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition chk_Usp_GET_VERSION_Check_Debugging;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_GET_TVERSIONTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_UPSERT_TLOCATIONTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Not_LoggedIn_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_StandardUser_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_InValidName_EmptyString_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_InValidName_NULL_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_InValid_SimID_Error;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction simsig_USp_UPSERT_TLOCATIONTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_SimID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_Tiploc;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_SimSigCode;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_TestDataID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_CreatedByID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_CreatedOn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_ModifiedByID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Insert_ModifiedOn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_SimID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_TIPLOC;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_Name;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_Code;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_EntryFlag;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_CreatedByID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_CreatedAppID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_ModifiedByID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_ModifiedAppID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_Update_ModifiedOn;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition chk_Upsert_TLOCATION_Check_UpdateByName_CheckID;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition chk_Upsert_TLOCATION_Check_CheckDebug_CreatesLocation;
            this.simsig_USp_UPSERT_TSIMTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_USp_GET_TSIMTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_Usp_DELETE_TSIMTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_Usp_GET_TSIMERA_BY_SIMTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_Usp_UPSERT_TSIMERATestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_USp_UPSERT_TVERSIONTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_USp_GET_TVERSIONTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.simsig_USp_UPSERT_TLOCATIONTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
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
            chk_NewRecord_TestDataID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
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
            chk_testdataid_added = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_StandardUser_Errors = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
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
            chk_DeleteSim_CheckUserLoggedInError = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_DeleteSim_CheckSimDeletion = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_DeleteSim_CheckSimEraDeletion = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_DeleteSim_CheckStandardUserError = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_GetSimEraBySim_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_GetSimEraBySim_Check_NotLoggedIn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
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
            chk_Upsert_TSIMERA_Check_StandardUser_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_USp_UPSERT_TVERSIONTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Upsert_TVERSION_Not_Logged_In_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Editor_No_Permission = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_StandardUser_No_Permission = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_InitialAdd_ID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_InitialAdd_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_InitialAdd_Description = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Version_From = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_InitialAdd_Version_To = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_InitialAdd_VersionStatus = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_InitialAdd_TestData_ID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_Upsert_TVERSION_Update_Check_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Update_Check_Description = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Update_Check_Status = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Update_Version_From = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Update_Version_To = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_From = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_To = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TVERSION_Add2ndVersion_Check_Debug = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            chk_Upsert_TVERSION_Check_VersionSplitError = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_USp_UPSERT_TVERSIONTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            simsig_Usp_GET_TSIMERA_BY_SIMTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            simsig_Usp_UPSERT_TSIMERATest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            simsig_USp_GET_TVERSIONTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            chk_Usp_GET_VERSION_Check_Debugging = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            simsig_USp_GET_TVERSIONTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            simsig_USp_UPSERT_TLOCATIONTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Upsert_TLOCATION_Not_LoggedIn_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_StandardUser_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_InValidName_EmptyString_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_InValidName_NULL_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_InValid_SimID_Error = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            simsig_USp_UPSERT_TLOCATIONTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            chk_Upsert_TLOCATION_Check_Insert_SimID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_Tiploc = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_SimSigCode = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_TestDataID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_CreatedByID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_CreatedOn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Insert_ModifiedOn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_SimID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_TIPLOC = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_Name = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_Code = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_EntryFlag = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_CreatedByID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_CreatedAppID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_ModifiedByID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_ModifiedAppID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_Update_ModifiedOn = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_UpdateByName_CheckID = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            chk_Upsert_TLOCATION_Check_CheckDebug_CreatesLocation = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
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
            // chk_DeleteSim_CheckStandardUserError
            // 
            chk_DeleteSim_CheckStandardUserError.ColumnNumber = 1;
            chk_DeleteSim_CheckStandardUserError.Enabled = true;
            chk_DeleteSim_CheckStandardUserError.ExpectedValue = "The user does not have permission to perform this action.";
            chk_DeleteSim_CheckStandardUserError.Name = "chk_DeleteSim_CheckStandardUserError";
            chk_DeleteSim_CheckStandardUserError.NullExpected = false;
            chk_DeleteSim_CheckStandardUserError.ResultSet = 4;
            chk_DeleteSim_CheckStandardUserError.RowNumber = 1;
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
            // simsig_USp_UPSERT_TVERSIONTest_TestAction
            // 
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Not_Logged_In_Error);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Editor_No_Permission);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_StandardUser_No_Permission);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_InitialAdd_ID);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_InitialAdd_Name);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_InitialAdd_Description);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Version_From);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_InitialAdd_Version_To);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_InitialAdd_VersionStatus);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_InitialAdd_TestData_ID);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Update_Check_Name);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Update_Check_Description);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Update_Check_Status);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Update_Version_From);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Update_Version_To);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_UpdateVersionCheck_Version_From);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_UpdateVersionCheck_Version_To);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Add2ndVersion_Check_Debug);
            simsig_USp_UPSERT_TVERSIONTest_TestAction.Conditions.Add(chk_Upsert_TVERSION_Check_VersionSplitError);
            resources.ApplyResources(simsig_USp_UPSERT_TVERSIONTest_TestAction, "simsig_USp_UPSERT_TVERSIONTest_TestAction");
            // 
            // chk_Upsert_TVERSION_Not_Logged_In_Error
            // 
            chk_Upsert_TVERSION_Not_Logged_In_Error.ColumnNumber = 1;
            chk_Upsert_TVERSION_Not_Logged_In_Error.Enabled = true;
            chk_Upsert_TVERSION_Not_Logged_In_Error.ExpectedValue = "The user is not logged in.";
            chk_Upsert_TVERSION_Not_Logged_In_Error.Name = "chk_Upsert_TVERSION_Not_Logged_In_Error";
            chk_Upsert_TVERSION_Not_Logged_In_Error.NullExpected = false;
            chk_Upsert_TVERSION_Not_Logged_In_Error.ResultSet = 1;
            chk_Upsert_TVERSION_Not_Logged_In_Error.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Editor_No_Permission
            // 
            chk_Upsert_TVERSION_Editor_No_Permission.ColumnNumber = 1;
            chk_Upsert_TVERSION_Editor_No_Permission.Enabled = true;
            chk_Upsert_TVERSION_Editor_No_Permission.ExpectedValue = "The user does not have permission to perform this action.";
            chk_Upsert_TVERSION_Editor_No_Permission.Name = "chk_Upsert_TVERSION_Editor_No_Permission";
            chk_Upsert_TVERSION_Editor_No_Permission.NullExpected = false;
            chk_Upsert_TVERSION_Editor_No_Permission.ResultSet = 2;
            chk_Upsert_TVERSION_Editor_No_Permission.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_StandardUser_No_Permission
            // 
            chk_Upsert_TVERSION_StandardUser_No_Permission.ColumnNumber = 1;
            chk_Upsert_TVERSION_StandardUser_No_Permission.Enabled = true;
            chk_Upsert_TVERSION_StandardUser_No_Permission.ExpectedValue = "The user does not have permission to perform this action.";
            chk_Upsert_TVERSION_StandardUser_No_Permission.Name = "chk_Upsert_TVERSION_StandardUser_No_Permission";
            chk_Upsert_TVERSION_StandardUser_No_Permission.NullExpected = false;
            chk_Upsert_TVERSION_StandardUser_No_Permission.ResultSet = 3;
            chk_Upsert_TVERSION_StandardUser_No_Permission.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_InitialAdd_ID
            // 
            chk_Upsert_TVERSION_InitialAdd_ID.ColumnNumber = 1;
            chk_Upsert_TVERSION_InitialAdd_ID.Enabled = true;
            chk_Upsert_TVERSION_InitialAdd_ID.ExpectedValue = "true";
            chk_Upsert_TVERSION_InitialAdd_ID.Name = "chk_Upsert_TVERSION_InitialAdd_ID";
            chk_Upsert_TVERSION_InitialAdd_ID.NullExpected = false;
            chk_Upsert_TVERSION_InitialAdd_ID.ResultSet = 4;
            chk_Upsert_TVERSION_InitialAdd_ID.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_InitialAdd_Name
            // 
            chk_Upsert_TVERSION_InitialAdd_Name.ColumnNumber = 2;
            chk_Upsert_TVERSION_InitialAdd_Name.Enabled = true;
            chk_Upsert_TVERSION_InitialAdd_Name.ExpectedValue = "Test Version 1 Name";
            chk_Upsert_TVERSION_InitialAdd_Name.Name = "chk_Upsert_TVERSION_InitialAdd_Name";
            chk_Upsert_TVERSION_InitialAdd_Name.NullExpected = false;
            chk_Upsert_TVERSION_InitialAdd_Name.ResultSet = 4;
            chk_Upsert_TVERSION_InitialAdd_Name.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_InitialAdd_Description
            // 
            chk_Upsert_TVERSION_InitialAdd_Description.ColumnNumber = 3;
            chk_Upsert_TVERSION_InitialAdd_Description.Enabled = true;
            chk_Upsert_TVERSION_InitialAdd_Description.ExpectedValue = "Test Version 1 Description";
            chk_Upsert_TVERSION_InitialAdd_Description.Name = "chk_Upsert_TVERSION_InitialAdd_Description";
            chk_Upsert_TVERSION_InitialAdd_Description.NullExpected = false;
            chk_Upsert_TVERSION_InitialAdd_Description.ResultSet = 4;
            chk_Upsert_TVERSION_InitialAdd_Description.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Version_From
            // 
            chk_Upsert_TVERSION_Version_From.ColumnNumber = 4;
            chk_Upsert_TVERSION_Version_From.Enabled = true;
            chk_Upsert_TVERSION_Version_From.ExpectedValue = "1.0";
            chk_Upsert_TVERSION_Version_From.Name = "chk_Upsert_TVERSION_Version_From";
            chk_Upsert_TVERSION_Version_From.NullExpected = false;
            chk_Upsert_TVERSION_Version_From.ResultSet = 4;
            chk_Upsert_TVERSION_Version_From.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_InitialAdd_Version_To
            // 
            chk_Upsert_TVERSION_InitialAdd_Version_To.ColumnNumber = 5;
            chk_Upsert_TVERSION_InitialAdd_Version_To.Enabled = true;
            chk_Upsert_TVERSION_InitialAdd_Version_To.ExpectedValue = null;
            chk_Upsert_TVERSION_InitialAdd_Version_To.Name = "chk_Upsert_TVERSION_InitialAdd_Version_To";
            chk_Upsert_TVERSION_InitialAdd_Version_To.NullExpected = true;
            chk_Upsert_TVERSION_InitialAdd_Version_To.ResultSet = 4;
            chk_Upsert_TVERSION_InitialAdd_Version_To.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_InitialAdd_VersionStatus
            // 
            chk_Upsert_TVERSION_InitialAdd_VersionStatus.ColumnNumber = 6;
            chk_Upsert_TVERSION_InitialAdd_VersionStatus.Enabled = true;
            chk_Upsert_TVERSION_InitialAdd_VersionStatus.ExpectedValue = "2";
            chk_Upsert_TVERSION_InitialAdd_VersionStatus.Name = "chk_Upsert_TVERSION_InitialAdd_VersionStatus";
            chk_Upsert_TVERSION_InitialAdd_VersionStatus.NullExpected = false;
            chk_Upsert_TVERSION_InitialAdd_VersionStatus.ResultSet = 4;
            chk_Upsert_TVERSION_InitialAdd_VersionStatus.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_InitialAdd_TestData_ID
            // 
            chk_Upsert_TVERSION_InitialAdd_TestData_ID.ColumnNumber = 7;
            chk_Upsert_TVERSION_InitialAdd_TestData_ID.Enabled = true;
            chk_Upsert_TVERSION_InitialAdd_TestData_ID.ExpectedValue = null;
            chk_Upsert_TVERSION_InitialAdd_TestData_ID.Name = "chk_Upsert_TVERSION_InitialAdd_TestData_ID";
            chk_Upsert_TVERSION_InitialAdd_TestData_ID.NullExpected = true;
            chk_Upsert_TVERSION_InitialAdd_TestData_ID.ResultSet = 4;
            chk_Upsert_TVERSION_InitialAdd_TestData_ID.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID
            // 
            chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID.Enabled = true;
            chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID.Name = "chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID";
            chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID.ResultSet = 5;
            chk_Upsert_TVERSION_TestData_ID_CHeck_TestData_ID.RowCount = 1;
            // 
            // chk_Upsert_TVERSION_Update_Check_Name
            // 
            chk_Upsert_TVERSION_Update_Check_Name.ColumnNumber = 1;
            chk_Upsert_TVERSION_Update_Check_Name.Enabled = true;
            chk_Upsert_TVERSION_Update_Check_Name.ExpectedValue = "Test Version 2 Name";
            chk_Upsert_TVERSION_Update_Check_Name.Name = "chk_Upsert_TVERSION_Update_Check_Name";
            chk_Upsert_TVERSION_Update_Check_Name.NullExpected = false;
            chk_Upsert_TVERSION_Update_Check_Name.ResultSet = 6;
            chk_Upsert_TVERSION_Update_Check_Name.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Update_Check_Description
            // 
            chk_Upsert_TVERSION_Update_Check_Description.ColumnNumber = 2;
            chk_Upsert_TVERSION_Update_Check_Description.Enabled = true;
            chk_Upsert_TVERSION_Update_Check_Description.ExpectedValue = "Test Version 2 Description";
            chk_Upsert_TVERSION_Update_Check_Description.Name = "chk_Upsert_TVERSION_Update_Check_Description";
            chk_Upsert_TVERSION_Update_Check_Description.NullExpected = false;
            chk_Upsert_TVERSION_Update_Check_Description.ResultSet = 6;
            chk_Upsert_TVERSION_Update_Check_Description.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Update_Check_Status
            // 
            chk_Upsert_TVERSION_Update_Check_Status.ColumnNumber = 5;
            chk_Upsert_TVERSION_Update_Check_Status.Enabled = true;
            chk_Upsert_TVERSION_Update_Check_Status.ExpectedValue = "1";
            chk_Upsert_TVERSION_Update_Check_Status.Name = "chk_Upsert_TVERSION_Update_Check_Status";
            chk_Upsert_TVERSION_Update_Check_Status.NullExpected = false;
            chk_Upsert_TVERSION_Update_Check_Status.ResultSet = 6;
            chk_Upsert_TVERSION_Update_Check_Status.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Update_Version_From
            // 
            chk_Upsert_TVERSION_Update_Version_From.ColumnNumber = 3;
            chk_Upsert_TVERSION_Update_Version_From.Enabled = true;
            chk_Upsert_TVERSION_Update_Version_From.ExpectedValue = "1.0";
            chk_Upsert_TVERSION_Update_Version_From.Name = "chk_Upsert_TVERSION_Update_Version_From";
            chk_Upsert_TVERSION_Update_Version_From.NullExpected = false;
            chk_Upsert_TVERSION_Update_Version_From.ResultSet = 6;
            chk_Upsert_TVERSION_Update_Version_From.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Update_Version_To
            // 
            chk_Upsert_TVERSION_Update_Version_To.ColumnNumber = 4;
            chk_Upsert_TVERSION_Update_Version_To.Enabled = true;
            chk_Upsert_TVERSION_Update_Version_To.ExpectedValue = null;
            chk_Upsert_TVERSION_Update_Version_To.Name = "chk_Upsert_TVERSION_Update_Version_To";
            chk_Upsert_TVERSION_Update_Version_To.NullExpected = true;
            chk_Upsert_TVERSION_Update_Version_To.ResultSet = 6;
            chk_Upsert_TVERSION_Update_Version_To.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_UpdateVersionCheck_Version_From
            // 
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_From.ColumnNumber = 1;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_From.Enabled = true;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_From.ExpectedValue = "1.0";
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_From.Name = "chk_Upsert_TVERSION_UpdateVersionCheck_Version_From";
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_From.NullExpected = false;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_From.ResultSet = 7;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_From.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_UpdateVersionCheck_Version_To
            // 
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_To.ColumnNumber = 2;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_To.Enabled = true;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_To.ExpectedValue = null;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_To.Name = "chk_Upsert_TVERSION_UpdateVersionCheck_Version_To";
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_To.NullExpected = true;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_To.ResultSet = 7;
            chk_Upsert_TVERSION_UpdateVersionCheck_Version_To.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From
            // 
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From.ColumnNumber = 1;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From.Enabled = true;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From.ExpectedValue = "1.0";
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From.Name = "chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From";
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From.NullExpected = false;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From.ResultSet = 8;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_From.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To
            // 
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To.ColumnNumber = 2;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To.Enabled = true;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To.ExpectedValue = "1.9";
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To.Name = "chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To";
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To.NullExpected = false;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To.ResultSet = 8;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Version_To.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From
            // 
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From.ColumnNumber = 1;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From.Enabled = true;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From.ExpectedValue = "2.0";
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From.Name = "chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From";
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From.NullExpected = false;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From.ResultSet = 9;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_From.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To
            // 
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To.ColumnNumber = 2;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To.Enabled = true;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To.ExpectedValue = null;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To.Name = "chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To";
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To.NullExpected = true;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To.ResultSet = 9;
            chk_Upsert_TVERSION_Add2ndVersion_Check_NewVersion_To.RowNumber = 1;
            // 
            // chk_Upsert_TVERSION_Add2ndVersion_Check_Debug
            // 
            chk_Upsert_TVERSION_Add2ndVersion_Check_Debug.Enabled = true;
            chk_Upsert_TVERSION_Add2ndVersion_Check_Debug.Name = "chk_Upsert_TVERSION_Add2ndVersion_Check_Debug";
            chk_Upsert_TVERSION_Add2ndVersion_Check_Debug.ResultSet = 10;
            // 
            // chk_Upsert_TVERSION_Check_VersionSplitError
            // 
            chk_Upsert_TVERSION_Check_VersionSplitError.ColumnNumber = 1;
            chk_Upsert_TVERSION_Check_VersionSplitError.Enabled = true;
            chk_Upsert_TVERSION_Check_VersionSplitError.ExpectedValue = "An error has occurred creating a new version. You cannot specify a new version eq" +
                "ual or less to the exsting version";
            chk_Upsert_TVERSION_Check_VersionSplitError.Name = "chk_Upsert_TVERSION_Check_VersionSplitError";
            chk_Upsert_TVERSION_Check_VersionSplitError.NullExpected = false;
            chk_Upsert_TVERSION_Check_VersionSplitError.ResultSet = 11;
            chk_Upsert_TVERSION_Check_VersionSplitError.RowNumber = 1;
            // 
            // simsig_USp_UPSERT_TVERSIONTest_PosttestAction
            // 
            resources.ApplyResources(simsig_USp_UPSERT_TVERSIONTest_PosttestAction, "simsig_USp_UPSERT_TVERSIONTest_PosttestAction");
            // 
            // simsig_Usp_GET_TSIMERA_BY_SIMTest_PosttestAction
            // 
            resources.ApplyResources(simsig_Usp_GET_TSIMERA_BY_SIMTest_PosttestAction, "simsig_Usp_GET_TSIMERA_BY_SIMTest_PosttestAction");
            // 
            // simsig_Usp_UPSERT_TSIMERATest_PosttestAction
            // 
            resources.ApplyResources(simsig_Usp_UPSERT_TSIMERATest_PosttestAction, "simsig_Usp_UPSERT_TSIMERATest_PosttestAction");
            // 
            // simsig_USp_GET_TVERSIONTest_TestAction
            // 
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_SingleIDPassed_Name);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount);
            simsig_USp_GET_TVERSIONTest_TestAction.Conditions.Add(chk_Usp_GET_VERSION_Check_Debugging);
            resources.ApplyResources(simsig_USp_GET_TVERSIONTest_TestAction, "simsig_USp_GET_TVERSIONTest_TestAction");
            // 
            // chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error
            // 
            chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error.ColumnNumber = 1;
            chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error.Enabled = true;
            chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error.ExpectedValue = "The user is not logged in.";
            chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error.Name = "chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error";
            chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error.NullExpected = false;
            chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error.ResultSet = 1;
            chk_Usp_GET_VERSION_Check_User_Not_Logged_In_Error.RowNumber = 1;
            // 
            // chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount
            // 
            chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount.Enabled = true;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount.Name = "chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount.ResultSet = 2;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_RowCount.RowCount = 1;
            // 
            // chk_Usp_GET_VERSION_Check_SingleIDPassed_Name
            // 
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Name.ColumnNumber = 2;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Name.Enabled = true;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Name.ExpectedValue = "Test Get Version Name 1";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Name.Name = "chk_Usp_GET_VERSION_Check_SingleIDPassed_Name";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Name.NullExpected = false;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Name.ResultSet = 2;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Name.RowNumber = 1;
            // 
            // chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc
            // 
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc.ColumnNumber = 3;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc.Enabled = true;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc.ExpectedValue = "Test Get Version Desc 1";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc.Name = "chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc.NullExpected = false;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc.ResultSet = 2;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_Desc.RowNumber = 1;
            // 
            // chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom
            // 
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom.ColumnNumber = 4;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom.Enabled = true;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom.ExpectedValue = "1.0";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom.Name = "chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom.NullExpected = false;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom.ResultSet = 2;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerFrom.RowNumber = 1;
            // 
            // chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo
            // 
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo.ColumnNumber = 5;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo.Enabled = true;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo.ExpectedValue = "1.9";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo.Name = "chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo.NullExpected = false;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo.ResultSet = 2;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_VerTo.RowNumber = 1;
            // 
            // chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID
            // 
            chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID.ColumnNumber = 6;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID.Enabled = true;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID.ExpectedValue = "1";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID.Name = "chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID";
            chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID.NullExpected = false;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID.ResultSet = 2;
            chk_Usp_GET_VERSION_Check_SingleIDPassed_StatusID.RowNumber = 1;
            // 
            // chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount
            // 
            chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount.Enabled = true;
            chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount.Name = "chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount";
            chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount.ResultSet = 3;
            chk_Usp_GET_VERSION_Check_Get_All_Editor_User_RowCount.RowCount = 2;
            // 
            // chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount
            // 
            chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount.Enabled = true;
            chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount.Name = "chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount";
            chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount.ResultSet = 5;
            chk_Usp_GET_VERSION_Check_Get_All_StandardUser_User_RowCount.RowCount = 1;
            // 
            // chk_Usp_GET_VERSION_Check_Debugging
            // 
            chk_Usp_GET_VERSION_Check_Debugging.Enabled = true;
            chk_Usp_GET_VERSION_Check_Debugging.Name = "chk_Usp_GET_VERSION_Check_Debugging";
            chk_Usp_GET_VERSION_Check_Debugging.ResultSet = 6;
            // 
            // simsig_USp_GET_TVERSIONTest_PosttestAction
            // 
            resources.ApplyResources(simsig_USp_GET_TVERSIONTest_PosttestAction, "simsig_USp_GET_TVERSIONTest_PosttestAction");
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
            this.simsig_Usp_GET_TSIMERA_BY_SIMTestData.PosttestAction = simsig_Usp_GET_TSIMERA_BY_SIMTest_PosttestAction;
            this.simsig_Usp_GET_TSIMERA_BY_SIMTestData.PretestAction = null;
            this.simsig_Usp_GET_TSIMERA_BY_SIMTestData.TestAction = simsig_Usp_GET_TSIMERA_BY_SIMTest_TestAction;
            // 
            // simsig_Usp_UPSERT_TSIMERATestData
            // 
            this.simsig_Usp_UPSERT_TSIMERATestData.PosttestAction = simsig_Usp_UPSERT_TSIMERATest_PosttestAction;
            this.simsig_Usp_UPSERT_TSIMERATestData.PretestAction = null;
            this.simsig_Usp_UPSERT_TSIMERATestData.TestAction = simsig_Usp_UPSERT_TSIMERATest_TestAction;
            // 
            // simsig_USp_UPSERT_TVERSIONTestData
            // 
            this.simsig_USp_UPSERT_TVERSIONTestData.PosttestAction = simsig_USp_UPSERT_TVERSIONTest_PosttestAction;
            this.simsig_USp_UPSERT_TVERSIONTestData.PretestAction = null;
            this.simsig_USp_UPSERT_TVERSIONTestData.TestAction = simsig_USp_UPSERT_TVERSIONTest_TestAction;
            // 
            // simsig_USp_GET_TVERSIONTestData
            // 
            this.simsig_USp_GET_TVERSIONTestData.PosttestAction = simsig_USp_GET_TVERSIONTest_PosttestAction;
            this.simsig_USp_GET_TVERSIONTestData.PretestAction = null;
            this.simsig_USp_GET_TVERSIONTestData.TestAction = simsig_USp_GET_TVERSIONTest_TestAction;
            // 
            // simsig_USp_UPSERT_TLOCATIONTestData
            // 
            this.simsig_USp_UPSERT_TLOCATIONTestData.PosttestAction = simsig_USp_UPSERT_TLOCATIONTest_PosttestAction;
            this.simsig_USp_UPSERT_TLOCATIONTestData.PretestAction = null;
            this.simsig_USp_UPSERT_TLOCATIONTestData.TestAction = simsig_USp_UPSERT_TLOCATIONTest_TestAction;
            // 
            // simsig_USp_UPSERT_TLOCATIONTest_TestAction
            // 
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Not_LoggedIn_Error);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_StandardUser_Error);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_InValidName_EmptyString_Error);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_InValidName_NULL_Error);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_InValid_SimID_Error);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_SimID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_Tiploc);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_Name);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_SimSigCode);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_TestDataID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_CreatedByID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_CreatedOn);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_ModifiedByID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Insert_ModifiedOn);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_SimID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_TIPLOC);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_Name);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_Code);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_EntryFlag);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_CreatedByID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_CreatedAppID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_ModifiedByID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_ModifiedAppID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_Update_ModifiedOn);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_UpdateByName_CheckID);
            simsig_USp_UPSERT_TLOCATIONTest_TestAction.Conditions.Add(chk_Upsert_TLOCATION_Check_CheckDebug_CreatesLocation);
            resources.ApplyResources(simsig_USp_UPSERT_TLOCATIONTest_TestAction, "simsig_USp_UPSERT_TLOCATIONTest_TestAction");
            // 
            // chk_Upsert_TLOCATION_Not_LoggedIn_Error
            // 
            chk_Upsert_TLOCATION_Not_LoggedIn_Error.ColumnNumber = 1;
            chk_Upsert_TLOCATION_Not_LoggedIn_Error.Enabled = true;
            chk_Upsert_TLOCATION_Not_LoggedIn_Error.ExpectedValue = "The user is not logged in.";
            chk_Upsert_TLOCATION_Not_LoggedIn_Error.Name = "chk_Upsert_TLOCATION_Not_LoggedIn_Error";
            chk_Upsert_TLOCATION_Not_LoggedIn_Error.NullExpected = false;
            chk_Upsert_TLOCATION_Not_LoggedIn_Error.ResultSet = 1;
            chk_Upsert_TLOCATION_Not_LoggedIn_Error.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_StandardUser_Error
            // 
            chk_Upsert_TLOCATION_StandardUser_Error.ColumnNumber = 1;
            chk_Upsert_TLOCATION_StandardUser_Error.Enabled = true;
            chk_Upsert_TLOCATION_StandardUser_Error.ExpectedValue = "The user does not have permission to perform this action.";
            chk_Upsert_TLOCATION_StandardUser_Error.Name = "chk_Upsert_TLOCATION_StandardUser_Error";
            chk_Upsert_TLOCATION_StandardUser_Error.NullExpected = false;
            chk_Upsert_TLOCATION_StandardUser_Error.ResultSet = 2;
            chk_Upsert_TLOCATION_StandardUser_Error.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_InValidName_EmptyString_Error
            // 
            chk_Upsert_TLOCATION_InValidName_EmptyString_Error.ColumnNumber = 1;
            chk_Upsert_TLOCATION_InValidName_EmptyString_Error.Enabled = true;
            chk_Upsert_TLOCATION_InValidName_EmptyString_Error.ExpectedValue = "No valid name was supplied for the location.";
            chk_Upsert_TLOCATION_InValidName_EmptyString_Error.Name = "chk_Upsert_TLOCATION_InValidName_EmptyString_Error";
            chk_Upsert_TLOCATION_InValidName_EmptyString_Error.NullExpected = false;
            chk_Upsert_TLOCATION_InValidName_EmptyString_Error.ResultSet = 3;
            chk_Upsert_TLOCATION_InValidName_EmptyString_Error.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_InValidName_NULL_Error
            // 
            chk_Upsert_TLOCATION_InValidName_NULL_Error.ColumnNumber = 1;
            chk_Upsert_TLOCATION_InValidName_NULL_Error.Enabled = true;
            chk_Upsert_TLOCATION_InValidName_NULL_Error.ExpectedValue = "No valid name was supplied for the location.";
            chk_Upsert_TLOCATION_InValidName_NULL_Error.Name = "chk_Upsert_TLOCATION_InValidName_NULL_Error";
            chk_Upsert_TLOCATION_InValidName_NULL_Error.NullExpected = false;
            chk_Upsert_TLOCATION_InValidName_NULL_Error.ResultSet = 4;
            chk_Upsert_TLOCATION_InValidName_NULL_Error.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error
            // 
            chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error.ColumnNumber = 1;
            chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error.Enabled = true;
            chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error.ExpectedValue = "No valid simsig code was supplied for the location.";
            chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error.Name = "chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error";
            chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error.NullExpected = false;
            chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error.ResultSet = 5;
            chk_Upsert_TLOCATION_InValidSimSigCode_EmptyString_Error.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error
            // 
            chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error.ColumnNumber = 1;
            chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error.Enabled = true;
            chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error.ExpectedValue = "No valid simsig code was supplied for the location.";
            chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error.Name = "chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error";
            chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error.NullExpected = false;
            chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error.ResultSet = 6;
            chk_Upsert_TLOCATION_InValidSimSigCode_NULL_Error.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error
            // 
            chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error.ColumnNumber = 1;
            chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error.Enabled = true;
            chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error.ExpectedValue = "No valid simsig entry point flag was supplied for the location.";
            chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error.Name = "chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error";
            chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error.NullExpected = false;
            chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error.ResultSet = 7;
            chk_Upsert_TLOCATION_InValidEntryPointFlag_NULL_Error.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_InValid_SimID_Error
            // 
            chk_Upsert_TLOCATION_InValid_SimID_Error.ColumnNumber = 1;
            chk_Upsert_TLOCATION_InValid_SimID_Error.Enabled = true;
            chk_Upsert_TLOCATION_InValid_SimID_Error.ExpectedValue = "No valid simulation was supplied for the location.";
            chk_Upsert_TLOCATION_InValid_SimID_Error.Name = "chk_Upsert_TLOCATION_InValid_SimID_Error";
            chk_Upsert_TLOCATION_InValid_SimID_Error.NullExpected = false;
            chk_Upsert_TLOCATION_InValid_SimID_Error.ResultSet = 8;
            chk_Upsert_TLOCATION_InValid_SimID_Error.RowNumber = 1;
            // 
            // simsig_USp_UPSERT_TLOCATIONTest_PosttestAction
            // 
            resources.ApplyResources(simsig_USp_UPSERT_TLOCATIONTest_PosttestAction, "simsig_USp_UPSERT_TLOCATIONTest_PosttestAction");
            // 
            // chk_Upsert_TLOCATION_Check_Insert_SimID
            // 
            chk_Upsert_TLOCATION_Check_Insert_SimID.ColumnNumber = 1;
            chk_Upsert_TLOCATION_Check_Insert_SimID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_SimID.ExpectedValue = "true";
            chk_Upsert_TLOCATION_Check_Insert_SimID.Name = "chk_Upsert_TLOCATION_Check_Insert_SimID";
            chk_Upsert_TLOCATION_Check_Insert_SimID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_SimID.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_SimID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_Tiploc
            // 
            chk_Upsert_TLOCATION_Check_Insert_Tiploc.ColumnNumber = 2;
            chk_Upsert_TLOCATION_Check_Insert_Tiploc.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_Tiploc.ExpectedValue = null;
            chk_Upsert_TLOCATION_Check_Insert_Tiploc.Name = "chk_Upsert_TLOCATION_Check_Insert_Tiploc";
            chk_Upsert_TLOCATION_Check_Insert_Tiploc.NullExpected = true;
            chk_Upsert_TLOCATION_Check_Insert_Tiploc.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_Tiploc.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_Name
            // 
            chk_Upsert_TLOCATION_Check_Insert_Name.ColumnNumber = 3;
            chk_Upsert_TLOCATION_Check_Insert_Name.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_Name.ExpectedValue = "Test Upsert Location Name 1";
            chk_Upsert_TLOCATION_Check_Insert_Name.Name = "chk_Upsert_TLOCATION_Check_Insert_Name";
            chk_Upsert_TLOCATION_Check_Insert_Name.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_Name.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_Name.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_SimSigCode
            // 
            chk_Upsert_TLOCATION_Check_Insert_SimSigCode.ColumnNumber = 4;
            chk_Upsert_TLOCATION_Check_Insert_SimSigCode.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_SimSigCode.ExpectedValue = "TUPSERTLOC1";
            chk_Upsert_TLOCATION_Check_Insert_SimSigCode.Name = "chk_Upsert_TLOCATION_Check_Insert_SimSigCode";
            chk_Upsert_TLOCATION_Check_Insert_SimSigCode.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_SimSigCode.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_SimSigCode.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag
            // 
            chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag.ColumnNumber = 5;
            chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag.ExpectedValue = "false";
            chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag.Name = "chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag";
            chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_EntryPointFlag.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_TestDataID
            // 
            chk_Upsert_TLOCATION_Check_Insert_TestDataID.ColumnNumber = 6;
            chk_Upsert_TLOCATION_Check_Insert_TestDataID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_TestDataID.ExpectedValue = "true";
            chk_Upsert_TLOCATION_Check_Insert_TestDataID.Name = "chk_Upsert_TLOCATION_Check_Insert_TestDataID";
            chk_Upsert_TLOCATION_Check_Insert_TestDataID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_TestDataID.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_TestDataID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_CreatedByID
            // 
            chk_Upsert_TLOCATION_Check_Insert_CreatedByID.ColumnNumber = 7;
            chk_Upsert_TLOCATION_Check_Insert_CreatedByID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_CreatedByID.ExpectedValue = "4";
            chk_Upsert_TLOCATION_Check_Insert_CreatedByID.Name = "chk_Upsert_TLOCATION_Check_Insert_CreatedByID";
            chk_Upsert_TLOCATION_Check_Insert_CreatedByID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_CreatedByID.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_CreatedByID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID
            // 
            chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID.ColumnNumber = 8;
            chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID.ExpectedValue = "2";
            chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID.Name = "chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID";
            chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_CreatedByAppID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_CreatedOn
            // 
            chk_Upsert_TLOCATION_Check_Insert_CreatedOn.ColumnNumber = 9;
            chk_Upsert_TLOCATION_Check_Insert_CreatedOn.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_CreatedOn.ExpectedValue = "true";
            chk_Upsert_TLOCATION_Check_Insert_CreatedOn.Name = "chk_Upsert_TLOCATION_Check_Insert_CreatedOn";
            chk_Upsert_TLOCATION_Check_Insert_CreatedOn.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_CreatedOn.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_CreatedOn.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_ModifiedByID
            // 
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByID.ColumnNumber = 10;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByID.ExpectedValue = "4";
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByID.Name = "chk_Upsert_TLOCATION_Check_Insert_ModifiedByID";
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByID.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID
            // 
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID.ColumnNumber = 11;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID.ExpectedValue = "2";
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID.Name = "chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID";
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedByAppID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Insert_ModifiedOn
            // 
            chk_Upsert_TLOCATION_Check_Insert_ModifiedOn.ColumnNumber = 12;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedOn.Enabled = true;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedOn.ExpectedValue = "true";
            chk_Upsert_TLOCATION_Check_Insert_ModifiedOn.Name = "chk_Upsert_TLOCATION_Check_Insert_ModifiedOn";
            chk_Upsert_TLOCATION_Check_Insert_ModifiedOn.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedOn.ResultSet = 9;
            chk_Upsert_TLOCATION_Check_Insert_ModifiedOn.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_SimID
            // 
            chk_Upsert_TLOCATION_Check_Update_SimID.ColumnNumber = 1;
            chk_Upsert_TLOCATION_Check_Update_SimID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_SimID.ExpectedValue = "true";
            chk_Upsert_TLOCATION_Check_Update_SimID.Name = "chk_Upsert_TLOCATION_Check_Update_SimID";
            chk_Upsert_TLOCATION_Check_Update_SimID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_SimID.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_SimID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_TIPLOC
            // 
            chk_Upsert_TLOCATION_Check_Update_TIPLOC.ColumnNumber = 2;
            chk_Upsert_TLOCATION_Check_Update_TIPLOC.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_TIPLOC.ExpectedValue = "TIPLOC2";
            chk_Upsert_TLOCATION_Check_Update_TIPLOC.Name = "chk_Upsert_TLOCATION_Check_Update_TIPLOC";
            chk_Upsert_TLOCATION_Check_Update_TIPLOC.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_TIPLOC.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_TIPLOC.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_Name
            // 
            chk_Upsert_TLOCATION_Check_Update_Name.ColumnNumber = 3;
            chk_Upsert_TLOCATION_Check_Update_Name.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_Name.ExpectedValue = "Test Upsert Location Name 2";
            chk_Upsert_TLOCATION_Check_Update_Name.Name = "chk_Upsert_TLOCATION_Check_Update_Name";
            chk_Upsert_TLOCATION_Check_Update_Name.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_Name.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_Name.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_Code
            // 
            chk_Upsert_TLOCATION_Check_Update_Code.ColumnNumber = 4;
            chk_Upsert_TLOCATION_Check_Update_Code.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_Code.ExpectedValue = "TUPSERTLOC2";
            chk_Upsert_TLOCATION_Check_Update_Code.Name = "chk_Upsert_TLOCATION_Check_Update_Code";
            chk_Upsert_TLOCATION_Check_Update_Code.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_Code.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_Code.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_EntryFlag
            // 
            chk_Upsert_TLOCATION_Check_Update_EntryFlag.ColumnNumber = 5;
            chk_Upsert_TLOCATION_Check_Update_EntryFlag.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_EntryFlag.ExpectedValue = "true";
            chk_Upsert_TLOCATION_Check_Update_EntryFlag.Name = "chk_Upsert_TLOCATION_Check_Update_EntryFlag";
            chk_Upsert_TLOCATION_Check_Update_EntryFlag.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_EntryFlag.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_EntryFlag.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_CreatedByID
            // 
            chk_Upsert_TLOCATION_Check_Update_CreatedByID.ColumnNumber = 7;
            chk_Upsert_TLOCATION_Check_Update_CreatedByID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_CreatedByID.ExpectedValue = "4";
            chk_Upsert_TLOCATION_Check_Update_CreatedByID.Name = "chk_Upsert_TLOCATION_Check_Update_CreatedByID";
            chk_Upsert_TLOCATION_Check_Update_CreatedByID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_CreatedByID.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_CreatedByID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_CreatedAppID
            // 
            chk_Upsert_TLOCATION_Check_Update_CreatedAppID.ColumnNumber = 8;
            chk_Upsert_TLOCATION_Check_Update_CreatedAppID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_CreatedAppID.ExpectedValue = "2";
            chk_Upsert_TLOCATION_Check_Update_CreatedAppID.Name = "chk_Upsert_TLOCATION_Check_Update_CreatedAppID";
            chk_Upsert_TLOCATION_Check_Update_CreatedAppID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_CreatedAppID.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_CreatedAppID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_ModifiedByID
            // 
            chk_Upsert_TLOCATION_Check_Update_ModifiedByID.ColumnNumber = 10;
            chk_Upsert_TLOCATION_Check_Update_ModifiedByID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_ModifiedByID.ExpectedValue = "2";
            chk_Upsert_TLOCATION_Check_Update_ModifiedByID.Name = "chk_Upsert_TLOCATION_Check_Update_ModifiedByID";
            chk_Upsert_TLOCATION_Check_Update_ModifiedByID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_ModifiedByID.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_ModifiedByID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_ModifiedAppID
            // 
            chk_Upsert_TLOCATION_Check_Update_ModifiedAppID.ColumnNumber = 11;
            chk_Upsert_TLOCATION_Check_Update_ModifiedAppID.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_ModifiedAppID.ExpectedValue = "2";
            chk_Upsert_TLOCATION_Check_Update_ModifiedAppID.Name = "chk_Upsert_TLOCATION_Check_Update_ModifiedAppID";
            chk_Upsert_TLOCATION_Check_Update_ModifiedAppID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_ModifiedAppID.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_ModifiedAppID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_Update_ModifiedOn
            // 
            chk_Upsert_TLOCATION_Check_Update_ModifiedOn.ColumnNumber = 12;
            chk_Upsert_TLOCATION_Check_Update_ModifiedOn.Enabled = true;
            chk_Upsert_TLOCATION_Check_Update_ModifiedOn.ExpectedValue = "true";
            chk_Upsert_TLOCATION_Check_Update_ModifiedOn.Name = "chk_Upsert_TLOCATION_Check_Update_ModifiedOn";
            chk_Upsert_TLOCATION_Check_Update_ModifiedOn.NullExpected = false;
            chk_Upsert_TLOCATION_Check_Update_ModifiedOn.ResultSet = 10;
            chk_Upsert_TLOCATION_Check_Update_ModifiedOn.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_UpdateByName_CheckID
            // 
            chk_Upsert_TLOCATION_Check_UpdateByName_CheckID.ColumnNumber = 1;
            chk_Upsert_TLOCATION_Check_UpdateByName_CheckID.Enabled = true;
            chk_Upsert_TLOCATION_Check_UpdateByName_CheckID.ExpectedValue = "true";
            chk_Upsert_TLOCATION_Check_UpdateByName_CheckID.Name = "chk_Upsert_TLOCATION_Check_UpdateByName_CheckID";
            chk_Upsert_TLOCATION_Check_UpdateByName_CheckID.NullExpected = false;
            chk_Upsert_TLOCATION_Check_UpdateByName_CheckID.ResultSet = 11;
            chk_Upsert_TLOCATION_Check_UpdateByName_CheckID.RowNumber = 1;
            // 
            // chk_Upsert_TLOCATION_Check_CheckDebug_CreatesLocation
            // 
            chk_Upsert_TLOCATION_Check_CheckDebug_CreatesLocation.Enabled = true;
            chk_Upsert_TLOCATION_Check_CheckDebug_CreatesLocation.Name = "chk_Upsert_TLOCATION_Check_CheckDebug_CreatesLocation";
            chk_Upsert_TLOCATION_Check_CheckDebug_CreatesLocation.ResultSet = 12;
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
        [TestMethod()]
        public void simsig_USp_UPSERT_TVERSIONTest()
        {
            SqlDatabaseTestActions testActions = this.simsig_USp_UPSERT_TVERSIONTestData;
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
        public void simsig_USp_GET_TVERSIONTest()
        {
            SqlDatabaseTestActions testActions = this.simsig_USp_GET_TVERSIONTestData;
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
        public void simsig_USp_UPSERT_TLOCATIONTest()
        {
            SqlDatabaseTestActions testActions = this.simsig_USp_UPSERT_TLOCATIONTestData;
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
        private SqlDatabaseTestActions simsig_USp_UPSERT_TVERSIONTestData;
        private SqlDatabaseTestActions simsig_USp_GET_TVERSIONTestData;
        private SqlDatabaseTestActions simsig_USp_UPSERT_TLOCATIONTestData;
    }
}
