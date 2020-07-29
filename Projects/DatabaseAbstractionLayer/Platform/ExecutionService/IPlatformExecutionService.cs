/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System.Data;
using OutSystems.HubEdition.Extensibility.Data.ExecutionService;
using OutSystems.RuntimeCommon;

namespace OutSystems.HubEdition.Extensibility.Data.Platform.ExecutionService {
    /// <summary>
    /// Platform Database service that handles the execution of statements made while connected to a database.
    /// </summary>
    public interface IPlatformExecutionService : IExecutionService {
        new IPlatformDatabaseServices DatabaseServices { get; }

        /// <summary>
        /// Executes a stored procedure using a command.
        /// That store procedure should return a cursor.
        /// </summary>
        /// <param name="cmd">The stored procedure command.</param>
        /// <param name="readerParamName">Name of the output parameter, without the prefix, to associate the reader with, if the procedure returns one (e.g. a cursor)</param>
        /// <returns>A reader with the results of the stored procedure.</returns>
        IDataReader ExecuteStoredProcedureWithResultSet(IDbCommand cmd, string readerParamName);
    }
}
