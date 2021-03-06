/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;

namespace SampleDatabaseProvider.Introspection {

    /// <summary>
    /// Contains information about a table source (data source in tabular format), like a database table or view.
    /// </summary>
    public class TableSourceInfo : BaseTableSourceInfo {

        /// <summary>
        /// Initializes a new instance of the <see cref="TableSourceInfo"/> class.
        /// </summary>
        /// <param name="databaseServices">The database services.</param>
        /// <param name="database">The database.</param>
        /// <param name="name">The name.</param>
        /// <param name="qualifiedName">Name of the qualified.</param>
        public TableSourceInfo(IDatabaseServices databaseServices, DatabaseInfo database, string name, string qualifiedName) : base(databaseServices, database, name, qualifiedName) { }
    }
}
