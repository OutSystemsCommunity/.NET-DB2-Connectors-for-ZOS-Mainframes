/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System.Collections.Generic;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;
using OutSystems.RuntimeCommon;

namespace OutSystems.HubEdition.Extensibility.Data.IntrospectionService {

    /// <summary>
    /// Inspects a database server to retrieve information about its data model.
    /// </summary>
    public interface IIntrospectionService {

        /// <summary>
        /// Sets the command timeout value to use in introspection queries.
        /// </summary>
        /// <value>
        /// The query timeout.
        /// </value>
        int QueryTimeout { set; }

        /// <summary>
        /// Gets the database services.
        /// </summary>
        /// <value>
        /// The database services associated.
        /// </value>
        IDatabaseServices DatabaseServices { get; }
        
        /// <summary>
        /// Returns the list of databases that can be accessed from the current configuration. A database is a logical group of data objects (e.g. tables, views)
        /// that the plugin maps to a db-specific concept (e.g. SQL Server catalog or Oracle schema).
        /// </summary>
        /// <returns>List of available databases in the given server</returns>
        /// <exception cref="System.Data.Common.DbException">If an error occurs while accessing the database</exception>
        IEnumerable<IDatabaseInfo> ListDatabases();
        
        /// <summary>
        /// Returns a list of table sources (e.g. tables, views) that belong to a given database. 
        /// The returned table sources must have different display names.
        /// </summary>
        /// <param name="database">Database from which we want to fetch the list of tables</param>
        /// <param name="isTableSourceToIgnore">The delegate to call to see if the table source should be ignored and excluded from the returned list</param>
        /// <returns>List of available table sources in the given database</returns>
        /// <exception cref="System.Data.Common.DbException">if an error occurs while accessing the database</exception>
        IEnumerable<ITableSourceInfo> ListTableSources(IDatabaseInfo database, IsTableSourceToIgnore isTableSourceToIgnore);

        /// <summary>
        /// Returns the list of foreign keys of the table source (e.g. table, view)
        /// </summary>
        /// <param name="tableSource">Table source from which we want to fetch the list of foreign keys</param>        
        /// <returns>The list of foreign keys of the table</returns>
        /// <exception cref="System.Data.Common.DbException">if an error occurs while accessing the database</exception>
        IEnumerable<ITableSourceForeignKeyInfo> GetTableSourceForeignKeys(ITableSourceInfo tableSource);

        /// <summary>
        /// Returns the list of columns of the table source (e.g. table, view)
        /// </summary>
        /// <param name="tableSource">Table source from which we want to fetch the list of columns</param>        
        /// <returns>The columns of the table</returns>
        /// <exception cref="System.Data.Common.DbException">if an error occurs while accessing the database</exception>
        IEnumerable<ITableSourceColumnInfo> GetTableSourceColumns(ITableSourceInfo tableSource);
    }
}
