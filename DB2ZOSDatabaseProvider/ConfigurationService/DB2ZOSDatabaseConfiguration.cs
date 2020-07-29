/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService;
using OutSystems.RuntimeCommon.ObfuscationProperties;
using System.Text;
using OutSystems.HubEdition.DatabaseProvider.DB2ZOS.Extensions;
using System;

namespace OutSystems.HubEdition.DatabaseProvider.DB2ZOS.ConfigurationService
{
    [DoNotObfuscate]
    public class DB2ZOSDatabaseConfiguration : BaseDatabaseConfiguration
    {

        public override IDatabaseProvider DatabaseProvider { get { return DB2ZOSIntegrationDatabaseProvider.Instance; } }

        [UserDefinedConfigurationParameter(Label = "Server", IsMandatory = true, Order = 1, Region = ParameterRegion.DatabaseLocation)]
        public string DataSource { get; set; }

        [UserDefinedConfigurationParameter(Label = "Database", IsMandatory = true, Order = 2, Region = ParameterRegion.DatabaseLocation)]
        public string Database { get; set; }

        [UserDefinedConfigurationParameter(Label = "Schema", IsMandatory = true, Order = 3, Region = ParameterRegion.DatabaseLocation)]
        public string Schema { get; set; }

        [UserDefinedConfigurationParameter(Label = "Auto Commit", IsMandatory = true, Order = 4, Region = ParameterRegion.DatabaseLocation)]
        public bool AutoCommit { get; set; }


        [UserDefinedConfigurationParameter(Label = "Username", IsMandatory = true, Order = 1, Region = ParameterRegion.UserSpecific)]
        public string UserID { get; set; }

        [UserDefinedConfigurationParameter(Label = "Password", IsPassword = true, Order = 2, Region = ParameterRegion.UserSpecific)]
        public string Password { get; set; }




        private AdvancedConfiguration advancedConfiguration = new AdvancedConfiguration(
            "Insert configuration parameters separated by ';'. Username and Password will be inserted automatically if present. Example: Server=serverAddress1, serverAddress2, serverAddress3; Database=myDataBase;",
            "Connection String Parameters", AssembleAdvanceConfigTemplate());

        public override string DatabaseIdentifier
        {
            get { return Schema; }
        }

        public override AdvancedConfiguration AdvancedConfiguration
        {
            get { return advancedConfiguration; }
            set { advancedConfiguration = value; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ GetHashCodeBasedOnParts(DataSource, Database, Schema, AutoCommit, UserID, Password);
        }

        public override bool Equals(object obj)
        {
            DB2ZOSDatabaseConfiguration config = obj as DB2ZOSDatabaseConfiguration;

            return config != null && base.Equals(obj) &&
                DataSource == config.DataSource &&
                Database == config.Database &&
                Schema == config.Schema &&
                AutoCommit == config.AutoCommit &&
                UserID == config.UserID &&
                Password == config.Password &&
                ConnectionStringOverride == config.ConnectionStringOverride &&
                AdvancedConfiguration == config.AdvancedConfiguration;
        }

        /// <summary>
        /// Format
        ///        DataSource=<ip address/localhost>:<port number>; 
        ///        Database=<db name>;
        ///        UID=<userID>; 
        ///        PWD=<password>; 
        ///        Connect Timeout=<Timeout value>
        /// </summary>
        /// <returns></returns>
        protected override string AssembleBasicConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendConnectionStringParam(DB2ZOSIDs.DATA_SOURCE, DataSource);
            sb.AppendConnectionStringParam(DB2ZOSIDs.DATABASE, Database);
            sb.AppendConnectionStringParam(DB2ZOSIDs.USERNAME, UserID);
            sb.AppendConnectionStringParam(DB2ZOSIDs.PASSWORD, Password);
            sb.AppendConnectionStringParam(DB2ZOSIDs.SCHEMA, Schema);

            return sb.ToString();
        }

        protected override string AssembleAdvancedConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(AdvancedConfiguration.AdvancedConnectionStringField);
            if (!string.IsNullOrEmpty(UserID))
            {
                sb.AppendConnectionStringParam(DB2ZOSIDs.USERNAME, UserID);
                sb.AppendConnectionStringParam(DB2ZOSIDs.PASSWORD, Password);
            }
            return sb.ToString();
        }

        /// <summary>
        /// AdvanceConfiguration template ex: 
        ///             [UID=$Username;PWD=&lt;hidden&gt;;]$AdvancedConnectionStringField
        /// </summary>
        /// <returns>AdvancedConfiguration connection string template</returns>
        private static string AssembleAdvanceConfigTemplate()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.AppendConnectionStringParam(DB2ZOSIDs.USERNAME, "$UserID");
            sb.AppendConnectionStringParam(DB2ZOSIDs.PASSWORD, "<hidden>");
            sb.Append("]");
            sb.Append("$AdvancedConnectionStringField");

            return sb.ToString();

        }

        public override IRuntimeDatabaseConfiguration RuntimeDatabaseConfiguration
        {
            get
            {
                return new DB2ZOSRuntimeDatabaseConfiguration
                {
                    Username = UserID,
                    Schema = Schema,
                    AutoCommit = AutoCommit,
                    ConnectionString = ConnectionString
                };
            }
        }
    }
}
