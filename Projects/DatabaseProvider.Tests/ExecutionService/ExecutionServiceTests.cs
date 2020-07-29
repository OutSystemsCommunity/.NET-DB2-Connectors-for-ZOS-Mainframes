/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NUnit.Framework;
using NUnitExtension.OutSystems.Framework;
using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.ExecutionService;
using OutSystems.RuntimeCommon;
using OutSystems.ServerTests.DatabaseProvider.Framework;


namespace OutSystems.ServerTests.DatabaseProvider.ExecutionService {

    public class TestConfiguration : AgnosticDatabaseProviderTestConfiguration {

        protected override string ConfigurationPathSettingName {
            get {
                return "DatabaseProviderTests.ExecutionServiceFilesPath";
            }
        }
    }

    public class ServerOnlyTestConfiguration : TestConfiguration {

        protected override bool IsServerOnly {
            get {
                return true;
            }
        }

    }

    [DashboardTestFixture(DashboardTest.DashboardTestKind)]
    public class ExecutionServiceTests : DatabaseProviderTest<TestConfiguration> {

        [IterativeTestCase(typeof(ServerOnlyTestConfiguration), Description = "Validates that the ParameterPrefix returns a valid prefix by executing a query with a parameter. GetParameterForSQL is used for generating the parameter SQL and ParameterPrefix is used to add the parameter to the command.")]
        [TestDetails(TestIssue = "616967", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        public void TestParameterPrefix(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            var sqlExecutor = new SQLExecutor(databaseServices);
            string sql = string.Format("SELECT VAL FROM DUMMY" + MachineName + " WHERE ID = {0}", sqlExecutor.GetParameterName(0, typeof(int)));

            int value = sqlExecutor.ExecuteScalar(sql, 1).RuntimeValue<int>();
            Assert.AreEqual(1, value, "ParameterPrefix didn't work as expected. SQL: " + sql);
        }

        [TestDetails(TestIssue = "568662", Feature = "Database Abstraction Layer")]
        [IterativeTestCase(typeof(ServerOnlyTestConfiguration), Description = "Check if basic date types are recognized as date parameters.")]
        public void CheckDateTypes(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            var executionService = databaseServices.ExecutionService;
            DbType[] dateTypes = new DbType[] { DbType.Date, DbType.DateTime, DbType.Time };
            foreach (DbType dateType in dateTypes) {
                Assert.IsTrue(executionService.IsDateType(dateType));
            }
        }

        [TestDetails(TestIssue = "568662", Feature = "Database Abstraction Layer")]
        [IterativeTestCase(typeof(ServerOnlyTestConfiguration), Description = "Checks if parameter directions are set correctly")]
        public void CheckParameterDirection(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            var executionService = databaseServices.ExecutionService;
            var transactionService = databaseServices.TransactionService;

            using (var connection = transactionService.CreateConnection()) {
                using (var cmd = executionService.CreateCommand(connection, "")) {
                    var param = executionService.CreateParameter(cmd, "Name", DbType.String, "John");
                    
                    ParameterDirection[] paramDirection = new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.InputOutput, ParameterDirection.Output, ParameterDirection.ReturnValue };

                    foreach (ParameterDirection dir in paramDirection) {
                        executionService.SetParameterDirection(param, dir);
                        
                        Assert.AreEqual(param.Direction, dir);
                    }

                }
            }
        }

        [TestDetails(TestIssue = "568662", Feature = "Database Abstraction Layer")]
        [IterativeTestCase(typeof(ServerOnlyTestConfiguration), Description = "Checks if parameter values are set correctly")]
        public void ParameterSetTest(DatabaseProviderTestCase tc)
        {
            var databaseServices = tc.Services;
            var executionService = databaseServices.ExecutionService;
            var transactionService = databaseServices.TransactionService;

            using (var connection = transactionService.CreateConnection())
            {
                using (var cmd = executionService.CreateCommand(connection, "select ID from PARAMS" + MachineName + " where VAL_SMALL1 like '%' || @SEARCH_STR || '%' or VAL_SMALL2 like '%' || @SEARCH_STR || '%'"))
                {
                    var param = executionService.CreateParameter(cmd, "@SEARCH_STR", DbType.String, "Test");
                    Assert.IsNotNull(param);
                    Assert.AreEqual("@SEARCH_STR", param.ParameterName);
                    Assert.AreEqual("Test", Convert.ToString(param.Value));
                    executionService.SetParameterValue(param, DbType.String, "Hello");
                    Assert.AreEqual("Hello", Convert.ToString(param.Value));

                    var obtained = Convert.ToInt32(executionService.ExecuteScalar(cmd));
                    Assert.AreEqual(1, obtained);                                        
                }
            }
        }

        [TestDetails(TestIssue = "708477", Feature = "Database Abstraction Layer", CreatedBy = "rls")]
        [IterativeTestCase(typeof(ServerOnlyTestConfiguration), Description = "Validates the conversion of decimal values fetched from the database to the native types, using a query that divides 1.0 by 3.0. The query is executed using the ExecuteScalar and ExecuteReader methods and passing the results through the TransformDatabaseToRuntimeValue method.")]
        public void TestDecimalConversionPrecision(DatabaseProviderTestCase tc) {
            var executionService = tc.Services.ExecutionService;
            decimal expected = RoundDecimal(1m/3m);
            using (var conn = tc.Services.TransactionService.CreateConnection()) {
                string sql = string.Format("SELECT 1.0/3.0 as Val FROM DUMMY" + MachineName);
                // Test ExecuteScalar
                using (IDbCommand cmd = executionService.CreateCommand(conn, sql)) {
                    try {
                        decimal obtained = RoundDecimal(Convert.ToDecimal(executionService.TransformDatabaseToRuntimeValue(executionService.ExecuteScalar(cmd))));
                        Assert.AreEqual(expected, obtained, "'" + sql + "' didn't return the expected value");
                    }  catch (Exception e) {
                        throw new Exception("Error occurred when executing 'ExecuteScalar() from command '" + sql + "': " +
                                            e.Message);
                    }
                }
                // Test ExecuteReader
                using (IDbCommand cmd = executionService.CreateCommand(conn, sql)) {
                    try {
                        using (var reader = executionService.ExecuteReader(cmd)) {
                            if (!reader.Read()) {
                                throw new Exception("Reader didn't return a value from the database");
                            }
                            decimal obtained = RoundDecimal(Convert.ToDecimal(executionService.TransformDatabaseToRuntimeValue(reader[0])));
                            decimal obtained2 = RoundDecimal(Convert.ToDecimal(executionService.TransformDatabaseToRuntimeValue(reader["Val"])));
                            decimal obtained3 = RoundDecimal(Convert.ToDecimal(executionService.TransformDatabaseToRuntimeValue(reader.GetDecimal(0))));
                            Assert.AreEqual(expected, obtained, "'" + sql + "' didn't return the expected value");
                            Assert.AreEqual(obtained, obtained2, "IDataReader[i] didn't return the same as IDataReader[string]");
                            Assert.AreEqual(obtained, obtained3, "IDataReader[i] didn't return the same as IDataReader.GetDecimal(i)");
                        }
                    }  catch (Exception e) {
                        throw new Exception("Error occurred when executing 'ExecuteReader() from command '" + sql + "': " +
                                            e.Message);
                    }
                }
            }
        }

        [TestDetails(TestIssue = "1105735", Feature = "Database Abstraction Layer", CreatedBy = "ham")]
        [IterativeTestCase(typeof(ServerOnlyTestConfiguration), Description = "Validates that there is no conversion problem when reading a decimal from the DB that does not fit the primitive decimal type.")]
        public void TestDecimalOverPrimitivePrecisionLimit(DatabaseProviderTestCase tc) {
            var executionService = tc.Services.ExecutionService;

            //The 1st value should be rounded to 29 digits
            //The 2nd value should be rounded to 28 digits
            string[] vals = new string[] { "1000000000000000000000.123456789", "9000000000000000000000.123456789" };
            decimal[] expected =
                
                new decimal[] { 1000000000000000000000.123456789m, 9000000000000000000000.123456789m };

            using (var conn = tc.Services.TransactionService.CreateConnection()) {

                for (int i = 0; i < vals.Length; i++) {
                    string sql = string.Format("SELECT {0} as Val FROM DUMMY".F(vals[i]) + MachineName);
                    // Test ExecuteScalar
                    using (IDbCommand cmd = executionService.CreateCommand(conn, sql)) {
                        try {
                            decimal obtained = Convert.ToDecimal(executionService.TransformDatabaseToRuntimeValue(executionService.ExecuteScalar(cmd)));
                            Assert.AreEqual(expected[i], obtained, "'" + sql + "' didn't return the expected value");
                        }  catch (Exception e) {
                            throw new Exception("Error occured when executing 'ExecuteScalar() from command '" + sql + "': " +
                                                e.Message);
                        }
                    }
                    // Test ExecuteReader
                    using (IDbCommand cmd = executionService.CreateCommand(conn, sql)) {
                        try {
                            using (var reader = executionService.ExecuteReader(cmd)) {
                                if (!reader.Read()) {
                                    throw new Exception("Reader didn't return a value from the database");
                                }
                                decimal obtained = Convert.ToDecimal(executionService.TransformDatabaseToRuntimeValue(reader[0]));
                                decimal obtained2 = Convert.ToDecimal(executionService.TransformDatabaseToRuntimeValue(reader["Val"]));
                                decimal obtained3 = Convert.ToDecimal(executionService.TransformDatabaseToRuntimeValue(reader.GetDecimal(0)));
                                Assert.AreEqual(expected[i], obtained, "'" + sql + "' didn't return the expected value");
                                Assert.AreEqual(obtained, obtained2, "IDataReader[i] didn't return the same as IDataReader[string]");
                                Assert.AreEqual(obtained, obtained3, "IDataReader[i] didn't return the same as IDataReader.GetDecimal(i)");
                            }
                        }  catch (Exception e) {
                            throw new Exception("Error occured when executing 'ExecuteReader() from command '" + sql + "': " +
                                                e.Message);
                        }
                    }
                }
            }
        }


        [TestDetails(TestIssue = "729792", Feature = "Database Abstraction Layer", CreatedBy = "rfe")]
        [IterativeTestCase(typeof(ServerOnlyTestConfiguration),
            Description = "Validates if we can set more than one parameter with big (>4K chars) and small strings.")]
        public void TestSetBigParameterStringValues(DatabaseProviderTestCase tc) {
            IDatabaseServices databaseServices = tc.Services;
            var sqlExecutor = new SQLExecutor(databaseServices);

            string sql = String.Format(
                "INSERT INTO {0} (ID, VAL_SMALL1, VAL_SMALL2, VAL_BIG1, VAL_BIG2) VALUES (1, {1})",
                databaseServices.DMLService.Identifiers.EscapeIdentifier("BIG_DATA" + MachineName), 
                Enumerable.Range(0, 4).Select(i => sqlExecutor.GetParameterName(i, typeof(string))).StrCat(", "));

            const string verySmallStr = "a";
            string smallStr = NSizedString(50), mediumStr = NSizedString(4000), bigStr = NSizedString(20000);

            var paramValuesInTests = new string[][] {
                new string[] {verySmallStr, verySmallStr, verySmallStr, verySmallStr}, // First test will very small strings
                new string[] {smallStr, smallStr, mediumStr, mediumStr}, // Small and medium strings
                new string[] {smallStr, smallStr, bigStr, bigStr}, // Small and big strings
            };

            foreach (object[] paramValues in paramValuesInTests) {
                Assert.AreEqual(1, sqlExecutor.ExecuteNonQuery(sql, paramValues));
            }
        }

        private static decimal RoundDecimal(decimal value) {
            return Math.Round(value, 3);
        }

        private static string NSizedString(int n) {
            IEnumerable<char> chars = Enumerable.Repeat('a', n);

            char[] arrayChars =
                
                chars.ToArray();

            return new String(arrayChars);
        }
        [TestDetails(TestIssue = "JAVART-234", Feature = "Database Abstraction Layer", CreatedBy = "lfl")]
        [IterativeTestCase(typeof(ServerOnlyTestConfiguration),
            Description = "Validates that we can do 2 consecutive selects over a connection, without reading its resultset.")]
        public void TestConsecutiveExecutionsNonRead(DatabaseProviderTestCase tc) {
            IDatabaseServices databaseServices = tc.Services;

            try {
                using (var con = databaseServices.TransactionService.CreateConnection()) {
                    var e = databaseServices.ExecutionService;
                    using (var cmd = e.CreateCommand(con, "select ID from DUMMY" + MachineName)) {
                        cmd.CommandTimeout = 30;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "select ID, VAL from DUMMY" + MachineName;
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception e) {
                Assert.Fail("Should not crash : " + e.Message);
            }
        }

        [IterativeTestCase(typeof(TestConfiguration), Description = "Tests if we replaced empty strings by 0s when inspecting floats/doubles on the database side.")]
        [TestDetails(Feature = "Database Abstraction Layer")]
        public void TransformRuntimeToDatabaseValueReplacesEmptyStringsWithZeros(DatabaseProviderTestCase tc) {
            var databaseServices = tc.Services;
            var executionService = databaseServices.ExecutionService;

            var numericTypes = new DbType[] {
                DbType.Int16, DbType.Int32, DbType.Int64,
                DbType.UInt16, DbType.UInt32, DbType.UInt64,
                DbType.Byte, DbType.SByte };

            var decimalTypes = new DbType[] {
                DbType.Currency, DbType.Decimal,
                DbType.Single, DbType.Double,
                DbType.VarNumeric };

            object value = null;

            foreach (var numType in numericTypes) {
                value = executionService.TransformRuntimeToDatabaseValue(numType, "");
                Assert.AreEqual(0, value);
            }


            foreach (var decType in decimalTypes) {
                value = executionService.TransformRuntimeToDatabaseValue(decType, "");
                Assert.AreEqual(new Decimal(0), value);
            }
        }
    }
}