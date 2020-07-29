/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using OutSystems.HubEdition.Extensibility.Data;

namespace SampleDatabaseProvider {

    /// <summary>
    /// Base implementation of a class to represent a set of properties that are specific to a database provider.
    /// </summary>
    public class ProviderProperties : BaseProviderProperties {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderProperties"/> class.
        /// </summary>
        /// <param name="provider">The database provider.</param>
        public ProviderProperties(IDatabaseProvider provider) : base(provider) { }

        /// <summary>
        /// Gets the friendly name of the database provider
        /// </summary>
        public override string DisplayName {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the friendly name of the database container (e.g. database, catalog, schema, ...), used
        /// for UI generation and messages displayed to the end-user.
        /// This implementation return "Database"
        /// </summary>
        public override string DatabaseContainerName {
            get {
                throw new NotImplementedException();
            }
        }
    }
}