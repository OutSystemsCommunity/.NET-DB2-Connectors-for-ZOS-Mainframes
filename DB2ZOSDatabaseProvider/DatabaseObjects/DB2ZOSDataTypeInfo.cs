/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;

namespace OutSystems.HubEdition.DatabaseProvider.DB2ZOS.DatabaseObjects {
    public class DB2ZOSDataTypeInfo : IDataTypeInfo {

        public DB2ZOSDataTypeInfo(DBDataType type, string sqlDataType, int length, int decimals) {
            Type = type;
            SqlDataType = sqlDataType;
            Length = length;
            Decimals = decimals;
        }

        public DBDataType Type { get; set; }
        public string SqlDataType { get; private set; }
        public int Length { get; set; }
        public int Decimals { get; private set; }
    }
}
