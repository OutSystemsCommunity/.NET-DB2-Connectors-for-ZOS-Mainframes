/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System.Net;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService;

namespace OutSystems.HubEdition.Extensibility.Data.Platform.Configuration {
    public abstract class BaseSessionDatabaseConfiguration : ISessionDatabaseConfiguration, ISuggestionableConfiguration {

        /// <summary>
        /// Gets the database provider. It provides information about the database,
        /// and access to its services.
        /// </summary>
        public abstract IPlatformDatabaseProvider PlatformDatabaseProvider { get; }

        public bool ImplementsElevatedPrivilegesOperations {
            get { return this is IElevatedUserConfiguration; }
        }

        public virtual bool RequiresElevatedPrivileges {
            get { return this is IElevatedUserConfiguration; }
        }

        public abstract AuthenticationType AuthenticationMode { get; set; }

        public ISuggestor UserNameSuggestor {
            get;
            set;
        }

        public ISuggestor TableSpaceSuggestor {
            get;
            set;
        }

        private string sessionUser;

        public abstract IRuntimeDatabaseConfiguration RuntimeDatabaseConfiguration();
        
        [UserDefinedConfigurationParameter(Label = "User", IsMandatory = true, Order = 1, Region = ParameterRegion.UserSessionSpecific, Prompt = "Session username")]
        public virtual string SessionUser {
            get {
                if (sessionUser == null && UserNameSuggestor != null) {
                    return UserNameSuggestor.GetSuggestion("STATE", "OSSTATE");
                }
                return sessionUser ?? "OSSTATE";
            }
            set {
                sessionUser = value;
            }
        }

        [UserDefinedConfigurationParameter(Label = "Password", IsPassword = true, IsMandatory = true, Order = 2, Region = ParameterRegion.UserSessionSpecific, Prompt = "Session password", Encrypt=true)]
        public virtual string SessionPassword { get; set; }

        [UserDefinedConfigurationParameter(Label = "Extra parameters", Order = 1, Region = ParameterRegion.AdvancedSession, Example = "Max Pool Size= 100; Connection Timeout = 15;", Prompt = "Session TNS name")]
        public virtual string SessionAdvancedSettings { get; set; }

        public NetworkCredential SessionAuthenticationCredential {
            get {
                return new NetworkCredential(SessionUser, SessionPassword);
            }
            set {
                SessionUser = value.UserName;
                SessionPassword = value.Password;
            }
        }

        public abstract bool Equals(ISessionDatabaseConfiguration other);

        [ConfigurationParameter]
        public bool AdvancedConfigurationMode {
            get;
            set;
        }

        public virtual string ContextualHelpForAdvancedMode {
            get {
                return "";
            }
        }

        public virtual string ContextualHelpForBasicMode {
            get {
                return "";
            }
        }

        protected const int DEFAULT_DELETEEXPIREDSESSIONSROWCOUNT_VALUE = 100;

        [ConfigurationParameter]
        public int DeleteExpiredSessionsAvoidLockRowCount { get; set; }

        public BaseSessionDatabaseConfiguration() {
            DeleteExpiredSessionsAvoidLockRowCount = DEFAULT_DELETEEXPIREDSESSIONSROWCOUNT_VALUE;
        }
    }
}
