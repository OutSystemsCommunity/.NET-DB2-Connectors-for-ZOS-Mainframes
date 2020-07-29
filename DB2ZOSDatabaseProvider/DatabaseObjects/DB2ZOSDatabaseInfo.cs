/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;

namespace OutSystems.HubEdition.DatabaseProvider.DB2ZOS.DatabaseObjects {
    public class DB2ZOSDatabaseInfo : BaseDatabaseInfo {

        public DB2ZOSDatabaseInfo(IDatabaseServices databaseServices, string database)
            : base(databaseServices) {
            Database = database;
        }

        public string Database { get; private set; }

        public override sealed string Identifier {
            get { return Database; }
        }

    }
}