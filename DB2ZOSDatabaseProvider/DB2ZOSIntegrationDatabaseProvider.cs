/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.DatabaseProvider.DB2ZOS.ConfigurationService;
using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService;
using OutSystems.RuntimeCommon;

namespace OutSystems.HubEdition.DatabaseProvider.DB2ZOS {

    public class DB2ZOSIntegrationDatabaseProvider : BaseDatabaseProvider {

        private readonly IProviderProperties properties;

        public static readonly IDatabaseProvider Instance = new DB2ZOSIntegrationDatabaseProvider();

        public DB2ZOSIntegrationDatabaseProvider() {
            properties = new DB2ZOSProviderProperties(this);
        }

        public override DatabaseProviderKey Key {
            get { return DatabaseProviderKey.Deserialize("DB2ZOS"); }
        }

        public override IProviderProperties Properties {
            get { return properties; }
        }

        public override IDatabaseServices GetIntegrationDatabaseServices(IRuntimeDatabaseConfiguration databaseConfiguration) {
            return new DB2ZOSDatabaseServices(databaseConfiguration);
        }

        public override IIntegrationDatabaseConfiguration CreateEmptyIntegrationDatabaseConfiguration() {
            return new DB2ZOSDatabaseConfiguration();
        }
    }
}