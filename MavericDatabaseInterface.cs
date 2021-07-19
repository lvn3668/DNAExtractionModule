// Author Lalitha Viswanathan
// MAVERIC DB Interface Class
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Collections;

namespace DNAExtractionModule {

	public class MavericDBException : System.Exception {
		/// <summary>
        /// MavericDB Exception 
        /// </summary>
        /// <param name="message"></param>
        public MavericDBException(string message) : base(message) { }
		/// <summary>
        /// Exception class overloaded 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MavericDBException(string message, Exception innerException) : base(message, innerException) { }
	}

	/// <summary>
	/// This is an interface to a database.
	/// </summary>
	class MavericDatabaseInterface {

		private NameValueCollection appSettings = ConfigurationManager.AppSettings;

		//create the database connection
		private static SqlConnection conn;

        /// <summary>
        /// Getter for SqlConnection
        /// </summary>
        /// <returns></returns>
        public SqlConnection getConnection()
        {
            return conn;
        }

        /// <summary>
        /// Set db connection 
        /// </summary>
        /// <param name="sqlConnection"></param>
        public void setConnection(SqlConnection sqlConnection)
        {
            try
            {
                conn = sqlConnection;
            } catch (Exception exception)
            {
                throw new MavericDBException("Exception", exception);
            }
        }

		/// <summary>
		/// Constructor for the database interface
		/// </summary>
		public MavericDatabaseInterface() {
            try
            {
                string connectionString = String.Empty;
                string dataSource = ConfigurationManager.AppSettings["DataSource"];
                string initialCatalog = ConfigurationManager.AppSettings["InitialCatalog"];

                if (Convert.ToBoolean(appSettings["TrustedConnection"]))
                {
                    connectionString = String.Format("Data Source={0};Initial Catalog={1};Trusted_Connection=True;", dataSource, initialCatalog);
                }
                else
                {
                    string userName = ConfigurationManager.AppSettings["UserName"];
                    string passWord = B64Decode(ConfigurationManager.AppSettings["Password"]);
                    connectionString = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};",
                        dataSource, initialCatalog, userName, passWord);
                }
                conn = new SqlConnection(connectionString);
            } catch (Exception exception)
            {
                writeErrorToDebug(exception);
                throw new MavericDBException(exception.Message);
            }
		}

		#region utilities
	
		/// <summary>
		/// Tests if the database connection is good
		/// </summary>
		/// <returns>True if a database connection can be opened without error, false otherwise</returns>
		internal bool Connected() {
			try {
				conn.Open();
			} catch (Exception) {
				return false;
			} finally {
				conn.Close();
			}
			return true;
		}

		/// <summary>
		/// get file contents from DB 
		/// </summary>
		/// <param name="sqlfileName"></param>
		/// <returns></returns>
		private string getFileContents(string sqlfileName) {
            try
            {
                string fileContent = String.Empty;

                if (File.Exists(sqlfileName))
                {
                    fileContent = File.ReadAllText(sqlfileName);
                }
                else
                {
                    Debug.WriteLine("File does not exist: " + sqlfileName);
                    throw new MavericDBException(String.Format("{0} {1}", appSettings["FileDoesNotExist"], sqlfileName));
                }
                return fileContent;
            } catch (Exception exception)
            {
                throw new MavericDBException("Exception string ", exception);
            }

		}


		/// <summary>
        /// B64 Decoder 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string B64Decode(string str) {
            try
            {
                byte[] decbuff = Convert.FromBase64String(str);
                return System.Text.Encoding.UTF8.GetString(decbuff);
            } catch (Exception exception)
            {
                throw new MavericDBException("Exception ", exception);
            }
		}


		/// <summary>
        /// write error to Debug console 
        /// </summary>
        /// <param name="ex"></param>
        private static void writeErrorToDebug(Exception ex) {
            try
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            } catch (Exception exception)
            {
                throw new MavericDBException("Exception ", exception);
            }
		}

        /// <summary>
        /// Add Error messages to DB
        /// </summary>
        /// <param name="error_msg"></param>
        /// <param name="details"></param>
        /// <param name="bankID"></param>
        internal void AddError(string error_msg, string details, string wellID)
        {
            string sqlString;
            if (wellID != String.Empty)
            {
                sqlString = getFileContents(@"sql\ErrorAdd.sql");
            }
            else
            {
                sqlString = getFileContents(@"sql\ErrorAddNoWellID.sql");
            }
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@errorType", SqlDbType.VarChar));
                    cmd.Parameters["@errorType"].Value = error_msg;
                    cmd.Parameters.Add(new SqlParameter("@errorDesc", SqlDbType.VarChar));
                    cmd.Parameters["@errorDesc"].Value = details;
                    cmd.Parameters.Add(new SqlParameter("@timestamp", SqlDbType.DateTime));
                    cmd.Parameters["@timestamp"].Value = DateTime.Now;
                    if (wellID != String.Empty)
                    {
                        cmd.Parameters.Add(new SqlParameter("@wellID", SqlDbType.VarChar));
                        cmd.Parameters["@wellID"].Value = wellID;
                    }
                    cmd.Parameters.Add(new SqlParameter("@operatorID", SqlDbType.VarChar));
                    cmd.Parameters["@operatorID"].Value = System.Environment.UserName;
                    cmd.Parameters.Add(new SqlParameter("@hostname", SqlDbType.VarChar));
                    cmd.Parameters["@hostname"].Value = System.Environment.MachineName;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (InvalidOperationException ex)
            {
                writeErrorToDebug(ex);
                throw new MavericDBException(appSettings["InvalidOperationException"]);
            }
            catch (SqlException ex)
            {
                writeErrorToDebug(ex);
                if (ex.Number == 18487 || ex.Number == 18488)
                {
                    throw new MavericDBException(appSettings["BadPassword"]);
                }
                else
                {
                    throw new MavericDBException(appSettings["ExecuteNonQuery_SqlException"]);
                }
            }
            catch (Exception ex)
            {
                writeErrorToDebug(ex);
                throw new MavericDBException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

		#endregion

		/// <summary>
        /// Check if deep well plate id exists in DB 
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        internal bool DeepWellExists(string candidate) {
			string sqlString = getFileContents(@"sql\DeepWellExists.sql");

			try {
				conn.Open();

				using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {
					cmd.Parameters.Add(new SqlParameter("@plateID", SqlDbType.VarChar));
					cmd.Parameters["@plateID"].Value = candidate;
					int total = (Int32) cmd.ExecuteScalar();
					if (total > 0) {
						return true;
					}
				}

			} catch (InvalidOperationException ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(appSettings["InvalidOperationException"]);
			} catch (SqlException ex) {
				writeErrorToDebug(ex);
				if (ex.Number == 18487 || ex.Number == 18488) {
					throw new MavericDBException(appSettings["BadPassword"]);
				} else {
					throw new MavericDBException(appSettings["ExecuteScalar_SqlException"]);
				}
			} catch (Exception ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(ex.Message);
			} finally {
				conn.Close();
			}
			return false;
		}

		/// <summary>
        /// Get list of wells from DB
        /// </summary>
        /// <param name="plateID"></param>
        /// <returns></returns>
        internal ArrayList GetWells(string plateID) {
			string sqlString = getFileContents(@"sql\WellsForDeepWellPlate.sql");
			DataSet ds = new DataSet();
			try {
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {
					cmd.Parameters.Add(new SqlParameter("@plateID", SqlDbType.VarChar));
					cmd.Parameters["@plateID"].Value = plateID;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);

                    ArrayList list = new ArrayList();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        list.Add(row["plate_row"].ToString() + row["plate_column"].ToString());
                        //string[] item = new string[3] { row["aliquot_id"].ToString(), row["plate_column"].ToString(), row["plate_row"].ToString() };
                    }
                    return list;
				}
			} catch (InvalidOperationException ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(appSettings["InvalidOperationException"]);
			} catch (SqlException ex) {
				writeErrorToDebug(ex);
				if (ex.Number == 18487 || ex.Number == 18488) {
					throw new MavericDBException(appSettings["BadPassword"]);
				} else {
					throw new MavericDBException(appSettings["AdapterFill_SqlException"]);
				}
			} catch (Exception ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(ex.Message);
			} finally {
				conn.Close();
			}
		}

		/// <summary>
		/// Checks to see if the aliquots in a 2D scan are in our database
		/// </summary>
		/// <param name="twoDscan">A 2D Scan</param>
		/// <returns>True if all are accounted for</returns>
		internal bool AliquotsExist(TwoDScan twoDscan) {
			if (twoDscan.Barcodes.Count == 0) {
				return false;
			}
			string sqlString = getFileContents(@"sql\AliquotsExist.sql");
			sqlString += " WHERE ";
			Int32 total = 0;
			int i = 0;
			foreach (KeyValuePair<string, string> kv in twoDscan.Barcodes) {
				sqlString += String.Format(" aliquot_id = '{0}' ", kv.Value);
				i++;
				if (i < twoDscan.Barcodes.Count) {
					sqlString += " OR ";
				}
			}
			try {
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {
					total = (Int32) cmd.ExecuteScalar();
				}
			} catch (InvalidOperationException ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(appSettings["InvalidOperationException"]);
			} catch (SqlException ex) {
				writeErrorToDebug(ex);
				if (ex.Number == 18487 || ex.Number == 18488) {
					throw new MavericDBException(appSettings["BadPassword"]);
				} else {
					throw new MavericDBException(appSettings["ExecuteScalar_SqlException"]);
				}
			} catch (Exception ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(ex.Message);
			} finally {
				conn.Close();
			}
			if (total > 0) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
        /// Check if aliquots exist for transfer to deep well plate 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        internal ArrayList AliquotsExistForTransfer(Dictionary<String, String> dictionary) {
            int test =0;
            ArrayList results = new ArrayList();
            conn.Open();
            try
            {
                foreach (KeyValuePair<string, string> kv in dictionary)
                {
                    string sqlString = getFileContents(@"sql\AliquotsExist.sql");
                    sqlString += " WHERE ";
                    sqlString += String.Format(" aliquot_id = '{0}' ", kv.Value);
                    using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                    {
                        test = (Int32)cmd.ExecuteScalar();
                        if (test == 0)
                        {
                            results.Add(kv.Value);
                        }
                    }
                }
                
			} catch (InvalidOperationException ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(appSettings["InvalidOperationException"]);
			} catch (SqlException ex) {
				writeErrorToDebug(ex);
				if (ex.Number == 18487 || ex.Number == 18488) {
					throw new MavericDBException(appSettings["BadPassword"]);
				} else {
					throw new MavericDBException(appSettings["ExecuteScalar_SqlException"]);
				}
			} catch (Exception ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(ex.Message);
			} finally {
				conn.Close();
			}

            return results;
		
		}

		/// <summary>
        /// Add deep well to database 
        /// </summary>
        /// <param name="deepwellID"></param>
        /// <param name="timeStamp"></param>
        /// <param name="volume"></param>
        /// <param name="robotName"></param>
        /// <param name="scriptName"></param>
        internal void AddDeepWell(string deepwellID, string timeStamp, string volume, string robotName, string scriptName) {
            string sqlString = getFileContents(@"sql\AddDeepWell.sql");
            try{
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {

                    //(@plateID, @dateTransferred,@vol, @robotName, @scriptName, @dnaxOperatorID, @dnaxTimestamp, @dnaxHostname)
                    cmd.Parameters.Add(new SqlParameter("@plateID", SqlDbType.VarChar));
                    cmd.Parameters["@plateID"].Value = deepwellID;                    
                    cmd.Parameters.Add(new SqlParameter("@dateTransferred", SqlDbType.DateTime));
                    cmd.Parameters["@dateTransferred"].Value = timeStamp;
                    cmd.Parameters.Add(new SqlParameter("@vol", SqlDbType.Int));
                    cmd.Parameters["@vol"].Value = Convert.ToInt32(volume);
                    cmd.Parameters.Add(new SqlParameter("@robotName", SqlDbType.VarChar));
                    cmd.Parameters["@robotName"].Value = robotName;
                    cmd.Parameters.Add(new SqlParameter("@scriptName", SqlDbType.VarChar));
                    cmd.Parameters["@scriptName"].Value = scriptName;
                    cmd.Parameters.Add(new SqlParameter("@dnaxOperatorID", SqlDbType.VarChar));
                    cmd.Parameters["@dnaxOperatorID"].Value = System.Environment.UserName;
                    cmd.Parameters.Add(new SqlParameter("@dnaxTimestamp", SqlDbType.DateTime));
                    cmd.Parameters["@dnaxTimestamp"].Value = DateTime.Now;
                    cmd.Parameters.Add(new SqlParameter("@dnaxHostname", SqlDbType.VarChar));
                    cmd.Parameters["@dnaxHostname"].Value = System.Environment.MachineName;
                    cmd.ExecuteNonQuery();
                }
            } catch (InvalidOperationException ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(appSettings["InvalidOperationException"]);
            } catch (SqlException ex) {
                writeErrorToDebug(ex);
                if (ex.Number == 18487 || ex.Number == 18488) {
                    throw new MavericDBException(appSettings["BadPassword"]);
                } else {
                    throw new MavericDBException(appSettings["ExecuteNonQuery_SqlException"]);
                }
            } catch (Exception ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(ex.Message);
            } finally {
                conn.Close();
            }
            
		}

		/// <summary>
        /// Add deep well and aliquot to DB
        /// </summary>
        /// <param name="deepwellID"></param>
        /// <param name="aliquotID"></param>
        /// <param name="position"></param>
        internal void AddDeepWellResults(string deepwellID, string aliquotID, string position) {
            try {
                //deepwellID and aliquotID are only 50 chars long...but the DB should handle that anyway
                string sqlString = getFileContents(@"sql\AddDeepWellResults.sql");
                //row and column are in position in the following format: "A01" or "C10" or "B7"
                int column = Convert.ToInt32(position.Substring(1)); //column is the number, the 2nd-End
                char row = position.ToCharArray()[0]; //row is a single character, the first in the parameter
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {

                    //(@plateID, @aliquotID,@plateColumn, @plateRow)
                    cmd.Parameters.Add(new SqlParameter("@plateID", SqlDbType.VarChar));
                    cmd.Parameters["@plateID"].Value = deepwellID;
                    cmd.Parameters.Add(new SqlParameter("@aliquotID", SqlDbType.VarChar));
                    cmd.Parameters["@aliquotID"].Value = aliquotID;
                    cmd.Parameters.Add(new SqlParameter("@plateColumn", SqlDbType.Int));
                    cmd.Parameters["@plateColumn"].Value = column;
                    cmd.Parameters.Add(new SqlParameter("@plateRow", SqlDbType.Char));
                    cmd.Parameters["@plateRow"].Value = row;
                    cmd.ExecuteNonQuery();
                }
            } catch (InvalidOperationException ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(appSettings["InvalidOperationException"]);
            } catch (SqlException ex) {
                writeErrorToDebug(ex);
                if (ex.Number == 18487 || ex.Number == 18488) {
                    throw new MavericDBException(appSettings["BadPassword"]);
                } else {
                    throw new MavericDBException(appSettings["ExecuteNonQuery_SqlException"]);
                }
            } catch (Exception ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(ex.Message);
            } finally {
                conn.Close();
            }//try...catch
		}

		/// <summary>
        /// Check if shallow well plate exists
        /// </summary>
        /// <param name="shallowwellID"></param>
        /// <returns></returns>
        internal bool ShallowPlateExists(string shallowwellID) {
            //default to false, and make sure 
            bool retVal = false;
            string sqlString = getFileContents(@"sql\ShallowPlateExists.sql");
            try {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {
                    cmd.Parameters.Add(new SqlParameter("@shallowwellID", SqlDbType.VarChar));
                    cmd.Parameters["@shallowwellID"].Value = shallowwellID;
                    //cmd.ExecuteNonQuery();
                    int total = (Int32)cmd.ExecuteScalar();
                    if(total > 0){
                        retVal = true;
                    }
                }
            } catch (InvalidOperationException ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(appSettings["InvalidOperationException"]);
            } catch (SqlException ex) {
                writeErrorToDebug(ex);
                if (ex.Number == 18487 || ex.Number == 18488) {
                    throw new MavericDBException(appSettings["BadPassword"]);
                } else {
                    throw new MavericDBException(appSettings["ExecuteNonQuery_SqlException"]);
                }
            } catch (Exception ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(ex.Message);
            } finally {
                conn.Close();
            }

            return retVal;
		}

		/// <summary>
        /// Add Shallow Plate Id to DB
        /// </summary>
        /// <param name="shallowPlateID"></param>
        /// <param name="timeStamp"></param>
        /// <param name="volume"></param>
        /// <param name="robotName"></param>
        /// <param name="scriptName"></param>
        internal void AddShallowPlate(string shallowPlateID, string timeStamp, string volume, string robotName, string scriptName) {
            try {
                string sqlString = getFileContents(@"sql\AddShallowPlate.sql");
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {

                    //(@plateID, @dateTransferred,@vol, @robotName, @scriptName, @dnaxOperatorID, @dnaxTimestamp, @dnaxHostname)
                    cmd.Parameters.Add(new SqlParameter("@plateID", SqlDbType.VarChar));
                    cmd.Parameters["@plateID"].Value = shallowPlateID;
                    cmd.Parameters.Add(new SqlParameter("@dateExtracted", SqlDbType.DateTime));
                    cmd.Parameters["@dateExtracted"].Value = timeStamp;
                    cmd.Parameters.Add(new SqlParameter("@vol", SqlDbType.Int));
                    cmd.Parameters["@vol"].Value = Convert.ToInt32(volume);
                    cmd.Parameters.Add(new SqlParameter("@robotName", SqlDbType.VarChar));
                    cmd.Parameters["@robotName"].Value = robotName;
                    cmd.Parameters.Add(new SqlParameter("@scriptName", SqlDbType.VarChar));
                    cmd.Parameters["@scriptName"].Value = scriptName;
                    cmd.Parameters.Add(new SqlParameter("@dnaxOperatorID", SqlDbType.VarChar));
                    cmd.Parameters["@dnaxOperatorID"].Value = System.Environment.UserName;
                    cmd.Parameters.Add(new SqlParameter("@dnaxTimestamp", SqlDbType.DateTime));
                    cmd.Parameters["@dnaxTimestamp"].Value = DateTime.Now;
                    cmd.Parameters.Add(new SqlParameter("@dnaxHostname", SqlDbType.VarChar));
                    cmd.Parameters["@dnaxHostname"].Value = System.Environment.MachineName;
                    cmd.ExecuteNonQuery();
                }
            } catch (InvalidOperationException ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(appSettings["InvalidOperationException"]);
            } catch (SqlException ex) {
                writeErrorToDebug(ex);
                if (ex.Number == 18487 || ex.Number == 18488) {
                    throw new MavericDBException(appSettings["BadPassword"]);
                } else {
                    throw new MavericDBException(appSettings["ExecuteNonQuery_SqlException"]);
                }
            } catch (Exception ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(ex.Message);
            } finally {
                conn.Close();
            }
		}

		/// <summary>
        /// Add Shallow Plate results to DB 
        /// </summary>
        /// <param name="shallowPlateID"></param>
        /// <param name="aliquotID"></param>
        /// <param name="position"></param>
        internal void AddShallowPlateResults(string shallowPlateID, string aliquotID, string position) {
            try {
                //deepwellID and aliquotID are only 50 chars long...but the DB should handle that anyway
                string sqlString = getFileContents(@"sql\AddShallowPlateResults.sql");
                //row and column are in position in the following format: "A01" or "C10"
                int column = Convert.ToInt32(position.Substring(1, 2)); //column is the number (as from the DB)
                char row = position.ToCharArray()[0]; //row is a single character
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {

                    //(@plateID, @aliquotID,@plateColumn, @plateRow)
                    cmd.Parameters.Add(new SqlParameter("@plateID", SqlDbType.VarChar));
                    cmd.Parameters["@plateID"].Value = shallowPlateID;
                    cmd.Parameters.Add(new SqlParameter("@aliquotID", SqlDbType.VarChar));
                    cmd.Parameters["@aliquotID"].Value = aliquotID;
                    cmd.Parameters.Add(new SqlParameter("@plateColumn", SqlDbType.Int));
                    cmd.Parameters["@plateColumn"].Value = column;
                    cmd.Parameters.Add(new SqlParameter("@plateRow", SqlDbType.Char));
                    cmd.Parameters["@plateRow"].Value = row;
                    cmd.ExecuteNonQuery();
                }
            } catch (InvalidOperationException ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(appSettings["InvalidOperationException"]);
            } catch (SqlException ex) {
                writeErrorToDebug(ex);
                if (ex.Number == 18487 || ex.Number == 18488) {
                    throw new MavericDBException(appSettings["BadPassword"]);
                } else {
                    throw new MavericDBException(appSettings["ExecuteNonQuery_SqlException"]);
                }
            } catch (Exception ex) {
                writeErrorToDebug(ex);
                throw new MavericDBException(ex.Message);
            } finally {
                conn.Close();
            }//try...catch
		}

		/// <summary>
        /// Decrement Aliquot Volumes
        /// </summary>
        /// <param name="aliquotID"></param>
        /// <param name="volume"></param>
        internal void DecrementAliquotVolume(string aliquotID, string volume) {
			string sqlString = getFileContents(@"sql\DecrementAliquotVolume.sql");
			try {
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sqlString, conn)) {
					cmd.Parameters.Add(new SqlParameter("@aliquotID", SqlDbType.VarChar));
					cmd.Parameters["@aliquotID"].Value = aliquotID;
					cmd.Parameters.Add(new SqlParameter("@volume", SqlDbType.VarChar));
					cmd.Parameters["@volume"].Value = volume;
					cmd.ExecuteNonQuery();
				}
			} catch (InvalidOperationException ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(appSettings["InvalidOperationException"]);
			} catch (SqlException ex) {
				writeErrorToDebug(ex);
				if (ex.Number == 18487 || ex.Number == 18488) {
					throw new MavericDBException(appSettings["BadPassword"]);
				} else {
					throw new MavericDBException(appSettings["ExecuteNonQuery_SqlException"]);
				}
			} catch (Exception ex) {
				writeErrorToDebug(ex);
				throw new MavericDBException(ex.Message);
			} finally {
				conn.Close();
			}

		}

		}
	
}
