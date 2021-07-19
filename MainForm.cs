using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections;
using Scanner;
using System.Threading;
using System.IO;
using GenericParsing;

namespace DNAExtractionModule {
	public partial class MainForm : Form {

		private NameValueCollection appSettings = ConfigurationManager.AppSettings;
		private Color okColor = Color.Green;
		private Color errorColor = Color.Red;
		private Color black = Color.Black;
		private MavericDatabaseInterface mdb;
		private TwoDScanner scanner;
		private TwoDScan twoDscan = null;
		private StringBuilder twoDScanFailureMessage = new StringBuilder();
		private string scannedDeepWellID = null;
		private string scannedRackID = null;
		
		public MainForm() {
			InitializeComponent();

			// clear messages left from designer
			lbl_Status.Text = String.Empty;
			lbl_Instructions.Text = String.Empty;
			tb_1DScanner.Text = String.Empty;
			lbl_Barcode.Text = String.Empty;

			// Configure banner for environment
			string mode = appSettings["mode"];
			if (mode == "TEST") {
				lbl_modeBanner.Text = "CONFIGURED FOR TESTING";
			} else if (mode == "INT") {
				lbl_modeBanner.Text = "CONFIGURED FOR INT";
			} else if (mode == "PROD") {
				lbl_modeBanner.Visible = false;
			} else {
				lbl_modeBanner.Text = "MODULE UNCONFIGURED";
			}

			// instantiate a new 2D scanner
			if (appSettings["Scanner"] == "THERMOFISHER") {
				scanner = new ThermoFisherTwoDScanner();
				Debug.WriteLine("Thermofisher scanner selected");
			} else if (appSettings["Scanner"] == "BIOSERO") {
				scanner = new BioserTwoDScanner();
				Debug.WriteLine("Biosero Scanner selected");
			}

			// connect to the scanner server
			bool connected = scanner.Connect(appSettings["2dScannerHostname"], Int32.Parse(appSettings["2dScannerPort"]));
			if (!connected) {
				MessageBox.Show(appSettings["ScannerMissing"], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				Debug.WriteLine("Scanner connectable");
			}

			// create new database interface
			mdb = new MavericDatabaseInterface();

			if (!mdb.Connected()) {
				MessageBox.Show(appSettings["DatabaseMissing"], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				Debug.WriteLine("Database connectable");
			}

			// Set up file watchers
			if (Directory.Exists(appSettings["BiomekFirstProtocolOutputPath"])) {
				FileSystemWatcher firstProtcolWatcher = new FileSystemWatcher();
				firstProtcolWatcher.Path = appSettings["BiomekFirstProtocolOutputPath"];
				firstProtcolWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
				firstProtcolWatcher.Filter = "*.*";
				firstProtcolWatcher.Created += new FileSystemEventHandler(firstProtcolWatcher_Created);

			} else {
				MessageBox.Show(appSettings["FirstProtocolPathNotExist"], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			if (Directory.Exists(appSettings["BiomekSecondProtocolOutputPath"])) {
				FileSystemWatcher secondProtocolWatcher = new FileSystemWatcher();
				secondProtocolWatcher.Path = appSettings["BiomekSecondProtocolOutputPath"];
				secondProtocolWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
				secondProtocolWatcher.Filter = "*.*";
				secondProtocolWatcher.Created += new FileSystemEventHandler(secondProtocolWatcher_Created);
			} else {
				MessageBox.Show(appSettings["SecondProtocolPathNotExist"], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
            checkForBiomekOutput();
			setGUIFor1DScan();
		
		}

        void checkForBiomekOutput() {
            //check if the first protocol directory exists
            //if it does, check to see if there are any files
            //for each file, loadFirstProtocol(filename)
            string[] files;
            if (Directory.Exists(appSettings["BiomekFirstProtocolOutputPath"])) {
                string firstPath = appSettings["BiomekFirstProtocolOutputPath"];
                files = Directory.GetFiles(appSettings["BiomekFirstProtocolOutputPath"]);
                foreach (string file in files) {
                    //note file is actually a full path!
                    loadFirstProtocol(file);
                }
            }
            //and the same with the second
            if (Directory.Exists(appSettings["BiomekSecondProtocolOutputPath"])) {
                string secondPath = appSettings["BiomekSecondProtocolOutputPath"];
                files = Directory.GetFiles(appSettings["BiomekSecondProtocolOutputPath"]);
                foreach (string file in files) {
                    //note file is actually a full path!
                    loadSecondProtocol(file);
                }
            }
        }

        void loadFirstProtocol(string fullPath) {
            if (!Directory.Exists(appSettings["BiomekFirstProtocolOutputBackupPath"])) {
                try {
                    Directory.CreateDirectory(appSettings["BiomekFirstProtocolOutputBackupPath"]);
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    MessageBox.Show(String.Format(appSettings["CannotCreateBackupDir"], appSettings["BiomekFirstProtocolOutputBackupPath"]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // parse output file
            try {
                using (GenericParserAdapter parser = new GenericParserAdapter(fullPath)) {
                    parser.FirstRowHasHeader = true;
                    DataTable protocolResults = parser.GetDataTable();
                    // TODO Figure out error checking here
                    // Do we do atomic error checking and only add stuff if eveything is kosher?
                    // Or do we check each one and then add it?

                    // if the deepwell doesn't exist in the database, add it now
                    if (protocolResults.Rows.Count > 0) {
                        /*DataRow sample = protocolResults.Rows[0];
                        string deepwellID = sample["DestBC"].ToString();
                        string volume = sample["Vol"].ToString();
                        if (!mdb.DeepWellExists(deepwellID)) {
                            mdb.AddDeepWell(deepwellID, sample["TimeStamp"].ToString(), volume, sample["RobotName"].ToString(), sample["ScriptName"].ToString());
                        }*/
                        foreach (DataRow row in protocolResults.Rows) {
                            string deepwellID = row["DestBC"].ToString();
                            string volume = row["Vol"].ToString();
                            if (!mdb.DeepWellExists(deepwellID)) {
                                mdb.AddDeepWell(deepwellID, row["TimeStamp"].ToString(), volume, row["RobotName"].ToString(), row["ScriptName"].ToString());
                            }
                            // decrement from the aliquot the volume that was transferred
                            string aliquotID = row["SrcDesc"].ToString();
                            mdb.DecrementAliquotVolume(aliquotID, volume);
                            // add the well
                            mdb.AddDeepWellResults(deepwellID, aliquotID, row["DestWellAlpha"].ToString());
                        }

                        removeAliquotsFromBiomekInput(protocolResults);
                    }

                }
            } catch (ParsingException ex) {
                // TODO a message box here is not ideal, as it will pop up randomly from the user perspective
                // it also doesn't do anything with the malformed file
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } catch (MavericDBException ex) {
                // TODO a message box here is not ideal, as it will pop up randomly from the user perspective
                // it also doesn't give the user an opportunity to resubmit the data
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // copy file to back up and delete from current directory
            try {
                string fileName = fullPath.Substring(fullPath.LastIndexOf("\\"));
                File.Copy(fullPath, appSettings["BiomekFirstProtocolOutputBackupPath"] + "\\" + fileName, false);
                File.Delete(fullPath);
            } catch (IOException ex) {
                // TODO a message box here is not ideal, as it will pop up randomly from the user perspective
                // it also doesn't give the user an opportunity retry the operation
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void loadSecondProtocol(string fullPath) {
            if (!Directory.Exists(appSettings["BiomekSecondProtocolOutputBackupPath"])) {
                try {
                    Directory.CreateDirectory(appSettings["BiomekSecondProtocolOutputBackupPath"]);
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    MessageBox.Show(String.Format(appSettings["CannotCreateBackupDir"], appSettings["BiomekSecondProtocolOutputBackupPath"]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // parse output file
            try {
                using (GenericParserAdapter parser = new GenericParserAdapter(fullPath)) {
                    parser.FirstRowHasHeader = true;
                    DataTable protocolResults = parser.GetDataTable();
                    // TODO Figure out error checking here
                    // Do we do atomic error checking and only add stuff if eveything is kosher?
                    // Or do we check each one and then add it?

                    // if the deepwell doesn't exist in the database, add it now
                    if (protocolResults.Rows.Count > 0) {
                        /*DataRow sample = protocolResults.Rows[0];
                        string shallowPlateID = sample["DestBC"].ToString();
                        if (!mdb.ShallowPlateExists(shallowPlateID)) {
                            mdb.AddShallowPlate(shallowPlateID, sample["TimeStamp"].ToString(), sample["Vol"].ToString(), sample["RobotName"].ToString(), sample["ScriptName"].ToString());
                        }*/
                        foreach (DataRow row in protocolResults.Rows) {
                            string shallowPlateID = row["DestBC"].ToString();
                            string aliquotID = row["SrcDesc"].ToString();
                            if(!mdb.ShallowPlateExists(shallowPlateID)){
                                mdb.AddShallowPlate(shallowPlateID,row["TimeStamp"].ToString(),row["Vol"].ToString(),row["RobotName"].ToString(),row["ScriptName"].ToString());
                            }
                            mdb.AddShallowPlateResults(shallowPlateID, aliquotID, row["DestWellAlpha"].ToString());
                        }
                        removeAliquotsFromBiomekInput(protocolResults);
                    }
                }
            } catch (ParsingException ex) {
                // TODO a message box here is not ideal, as it will pop up randomly from the user perspective
                // it also doesn't do anything with the malformed file
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } catch (MavericDBException ex) {
                // TODO a message box here is not ideal, as it will pop up randomly from the user perspective
                // it also doesn't give the user an opportunity to resubmit the data
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // copy file to back up and delete from current directory
            try {
                string fileName = fullPath.Substring(fullPath.LastIndexOf("\\"));
                File.Copy(fullPath, appSettings["BiomekSecondProtocolOutputBackupPath"] + "\\" + fileName, false);
                File.Delete(fullPath);
            } catch (IOException ex) {
                // TODO a message box here is not ideal, as it will pop up randomly from the user perspective
                // it also doesn't give the user an opportunity retry the operation
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool deepwellProcessed(string deepwellID) {
            bool retVal = false;
            try {
                using (GenericParserAdapter parser = new GenericParserAdapter(appSettings["BiomekInputFile"])) {
                    parser.FirstRowHasHeader = true;
                    DataTable inputTable = parser.GetDataTable();
                    string selectString = String.Format("SrcBC = '{0}'", deepwellID);
                    DataRow[] selected = inputTable.Select(selectString);
                    if (selected.Length == 0) {
                        retVal = true;
                    }
                }
            } catch (ParsingException ex) {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retVal;
        }

		#region events

		void firstProtcolWatcher_Created(object sender, FileSystemEventArgs e) {
            //we abstracted this function to:
            loadFirstProtocol(e.FullPath);			
		}

		void secondProtocolWatcher_Created(object sender, FileSystemEventArgs e) {
            //we abstracted this function to:
            loadSecondProtocol(e.FullPath);
		}

		private void btn_Reset_MouseClick(object sender, MouseEventArgs e) {
			string msg = appSettings["ResetMsg"];
			var result = MessageBox.Show(msg,
														"Reset",
														MessageBoxButtons.YesNo,
														MessageBoxIcon.Question);
			if (result == DialogResult.Yes) {
				
				try {
					if (TwoDScannerBackgroundWorker.IsBusy) {
						TwoDScannerBackgroundWorker.CancelAsync();
					}

				} catch (MavericDBException ex) {
					MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

		}

		private void btn_Transfer_MouseClick(object sender, MouseEventArgs e) {
			setGUIForRackScan();
		}

		private void btn_Extract_MouseClick(object sender, MouseEventArgs e) {
			addDeepwellsToBiomekInput(tb_1DScanner.Text);
			setGUIForExtractSuccess();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			var result = MessageBox.Show(appSettings["ExitMsg"],
											 "Close Application",
											 MessageBoxButtons.YesNo,
											 MessageBoxIcon.Question);
			if (result == DialogResult.No) {
				e.Cancel = true;
				tb_1DScanner.Focus();
			} else {
				if (TwoDScannerBackgroundWorker.IsBusy) {
					TwoDScannerBackgroundWorker.CancelAsync();
				}
			}
		}

		private void btn_Exit_MouseClick(object sender, MouseEventArgs e) {
			Application.Exit();
		}

		private void btn_xferVolUp_MouseClick(object sender, MouseEventArgs e) {

		}

		private void btn_xferVolDown_MouseClick(object sender, MouseEventArgs e) {

		}

		private void tb_1DScanner_KeyDown(object sender, KeyEventArgs e) {
			try {
				if (e.KeyCode == Keys.Enter) {
					Debug.WriteLine("Deep Well ID: " + tb_1DScanner.Text);
					if (validate1DScan(tb_1DScanner.Text)) {
						setGUIForDeepWell1DSuccess();

					} else {
						//add_error("invalid rack ID", candidate);
						lbl_Status.Visible = true;
						lbl_Status.ForeColor = errorColor;
						lbl_Status.Text = appSettings["InvalidDeepWell"];
						tb_1DScanner.Text = String.Empty;
						tb_1DScanner.Focus();
					}
				}
			} catch (MavericDBException ex) {
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void tb_Rack1dScan_KeyDown(object sender, KeyEventArgs e) {
			try {
				if (e.KeyCode == Keys.Enter) {
					Debug.WriteLine("Rack ID: " + tb_1DScanner.Text);
					if (validate1DScan(tb_1DScanner.Text)) {
						setGUIForTwoDScan();
						start2Dscan();
					} else {
						//add_error("invalid rack ID", candidate);
						lbl_Status.Visible = true;
						lbl_Status.ForeColor = errorColor;
						lbl_Status.Text = appSettings["InvalidRackID"];
						tb_1DScanner.Text = String.Empty;
						tb_1DScanner.Focus();
					}
				}
			} catch (MavericDBException ex) {
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		#endregion

		#region Background worker

		private void TwoDScannerBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
			BackgroundWorker worker = sender as BackgroundWorker;
			twoDscan = null;
			try {
				if (appSettings["Scanner"] == "THERMOFISHER") {
					thermoFisherScan(e, worker);
				} else if (appSettings["Scanner"] == "BIOSERO") {
					bioSeroScan(e, worker);
				}


			} catch (ScannerDisconnectedException error) {
				Debug.WriteLine(error.Message);
				Debug.WriteLine(error.StackTrace);
				MessageBox.Show(appSettings["ScannerDisconnected"], "LaminarFlowHoodModule", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} catch (Exception error) {
				Debug.WriteLine(error.Message);
				Debug.WriteLine(error.StackTrace);
				worker.ReportProgress(0, appSettings["ScannerError"]);
			}

		}

		private void bioSeroScan(DoWorkEventArgs e, BackgroundWorker worker) {
			//worker.ReportProgress(0, appSettings["PrepareScan"]);

			// Set a unique rack ID
			scanner.SetRackID(appSettings["BioseroTrayName"]);

			worker.ReportProgress(1, appSettings["Scanning"]);

			scanner.StartScan();

			bool status = scanner.GetStatus();

			while (!status) {
				if (worker.CancellationPending) {
					e.Cancel = true;
					break;
				}

				// Pause if no rack or if not oriented
				CheckRack(e, worker);

				status = scanner.GetStatus();
				worker.ReportProgress(2, appSettings["Scanning"]);
				Debug.WriteLine("Scanning.");
				Thread.Sleep(Int32.Parse(appSettings["ScannerScanSleep"]));
			}


			Debug.WriteLine("Scanning finished");

			if (!worker.CancellationPending) {
				worker.ReportProgress(3, appSettings["WaitingForResults"]);
				// scan has finished
				twoDscan = new TwoDScan(scanner);
			}
		}

		private void thermoFisherScan(DoWorkEventArgs e, BackgroundWorker worker) {
			worker.ReportProgress(0, appSettings["PrepareScan"]);

			// Set a unique rack ID
			scanner.SetRackID(System.DateTime.Now.ToString().Replace('/', '1').Replace(':', '1').Replace(' ', '1'));
			// Sets the expected number of tubes, more efficient scanning
			scanner.SetNumberOfTubes(appSettings["ExpectedTubes"]);

			// Pause if no rack or if not oriented
			CheckRack(e, worker);

			scanner.StartScan();
			// Poor scanner needs some time to reset itself
			// this seems to be the key to getting accurate scans.
			Thread.Sleep(Int32.Parse(appSettings["ScannerSleep"]));

			worker.ReportProgress(1, appSettings["Scanning"]);

			bool status = scanner.GetStatus();

			while (!status) {
				if (worker.CancellationPending) {
					e.Cancel = true;
					break;
				}

				// Pause if no rack or if not oriented
				CheckRack(e, worker);

				status = scanner.GetStatus();
				worker.ReportProgress(2, appSettings["Scanning"]);
				Debug.WriteLine("Scanning.");
				Thread.Sleep(Int32.Parse(appSettings["ScannerScanSleep"]));
			}

			if (!worker.CancellationPending) {
				worker.ReportProgress(3, appSettings["WaitingForResults"]);
				// scan has finished
				twoDscan = new TwoDScan(scanner);
			}
		}

		/// <summary>
		/// Checks a rack for presence correct orientation
		/// </summary>
		/// <param name="e"></param>
		/// <param name="worker"></param>
		private void CheckRack(DoWorkEventArgs e, BackgroundWorker worker) {
			while (!scanner.RackPresent()) {
				if (worker.CancellationPending) {
					e.Cancel = true;
					break;
				}
				Debug.WriteLine("Rack is not present");
				worker.ReportProgress(-1, appSettings["RackNotPresent"]);
				Thread.Sleep(Int32.Parse(appSettings["RackNotPresentSleep"]));
			}

			bool checkOrientation = Convert.ToBoolean(appSettings["CheckRackOrientation"]);

			if (checkOrientation) {
				while (!scanner.RackOriented()) {
					if (worker.CancellationPending) {
						e.Cancel = true;
						break;
					}
					Debug.WriteLine("Rack is not oriented");
					worker.ReportProgress(-1, appSettings["RackNotOriented"]);
					Thread.Sleep(Int32.Parse(appSettings["RackNotPresentSleep"]));
				}
			}
		}

		private void TwoDScannerBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			if (e.ProgressPercentage < 0) {
				lbl_Instructions.ForeColor = errorColor;
			} else {
				lbl_Instructions.ForeColor = black;
			}
			lbl_Instructions.Text = e.UserState.ToString();

		}

		private void TwoDScannerBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			BackgroundWorker worker = sender as BackgroundWorker;

			if (e.Cancelled) {
				setGUIFor1DScan();
				lbl_Status.Text = appSettings["TwoDScanCanelledText"];
				return;
			}

			if (twoDscan == null || twoDscan.Barcodes == null) {
				setGUIFor1DScan();
				lbl_Status.ForeColor = errorColor;
				lbl_Status.Text = appSettings["ScannerError"];
				lbl_Status.Visible = true;

				return;
			}

			bool success = true;
			twoDScanFailureMessage = new StringBuilder();

			// We have a valid scan, with barcodes 
			// What sort of errors could exist?
			ArrayList wells = mdb.GetWells(tb_1DScanner.Text);
			if (wellAliquotConflict(wells, twoDscan.Barcodes)) {
				success = false;
			}

			// check to make sure all of the aliquots in the 2d scan are aliquots we have a record of already
            ArrayList aliquots = mdb.AliquotsExistForTransfer(twoDscan.Barcodes);
			if (!allAliquotsExist(aliquots)) {
				success = false;
			}

			if (!success) {
					if (twoDScanFailureMessage.Length > 500) {
						twoDScanFailureMessage.AppendLine("And more...");
					}
					if (twoDScanFailureMessage.Length > 0) {
						lbl_Status.Visible = false;
						lbl_Status.ForeColor = errorColor;
						lbl_Status.Text = appSettings["TwoDScanError"];
						lbl_Status.Visible = true;
						tb_2dErrors.Text = twoDScanFailureMessage.ToString();
						tb_2dErrors.Visible = true;
					}
					// User will need to fix the rack
					Thread.Sleep(Int32.Parse(appSettings["FixRackSleep"]));
					start2Dscan();
			} else {
				setGUIFor2DSuccess();
				addAliquotsToBiomekInput(tb_Rack1dScan.Text, twoDscan.Barcodes);

                
				
			}

		}

		#endregion

		/// <summary>
		/// Validates whether or not a rack ID is in the correct format
		/// (req. # 3.5.2.b4)
		/// </summary>
		/// <param name="candidate"></param>
		/// <returns>True if the rack ID is in the correct format</returns>
		private bool validate1DScan(string candidate) {
			// 1D barcode is checked against a regular expression
			Regex r = new Regex(appSettings["1DRegex"]);
			if (!r.IsMatch(candidate)) {
				return false;
			}
			return true;
		}
        
		/// <summary>
		/// Sets up the GUI and kicks off a 2d scan thread
		/// </summary>
		private void start2Dscan() {
			if (!TwoDScannerBackgroundWorker.IsBusy) {
				TwoDScannerBackgroundWorker.RunWorkerAsync();
			}
		}

		private void setGUIFor1DScan() {
			lbl_Instructions.Text = appSettings["Ready1DScan"];
			lbl_Instructions.Visible = true;
			panel_ScannedBarcode.Visible = false;
			lbl_Barcode.Text = String.Empty;
			btn_Extract.Visible = false;
			btn_Transfer.Visible = false;
            panel_RackPositionDisplay.Visible = false;

            tb_1DScanner.Text = String.Empty;
            tb_1DScanner.Enabled = true;
            tb_1DScanner.Visible = true;
            tb_Rack1dScan.Enabled = false;
            tb_Rack1dScan.Visible = false;
		}

		private void setGUIForRackScan() {
            panel_ScannedBarcode.Visible = false;
            panel_RackPositionDisplay.Visible = false;
            lbl_Instructions.Text = appSettings["Ready2DScan"];
            btn_Extract.Visible = false;
            btn_Transfer.Visible = false;

            tb_Rack1dScan.Enabled = true;
            tb_Rack1dScan.Visible = true;
            tb_Rack1dScan.Text = String.Empty;
            tb_Rack1dScan.Focus();
            
            tb_1DScanner.Enabled = false;
            tb_1DScanner.Visible = false;

            lbl_Status.Text = String.Empty;
		}

		/// <summary>
		/// Called when a rack is about to be 2d scanned
		/// </summary>
		private void setGUIForTwoDScan() {
			//panel_ScannedBarcode.Visible = false;
			btn_Transfer.Visible = false;
			btn_Extract.Visible = false;
			lbl_Instructions.ForeColor = black;
			lbl_Instructions.Text = appSettings["Scanning"];
			lbl_Status.Visible = false;
			tb_1DScanner.Enabled = false;
            tb_Rack1dScan.Enabled = false;
			btn_Exit.Visible = true;
			btn_Reset.Visible = true;
		}

		/// <summary>
		/// Called after a successful Deepwell plate scan
		/// </summary>
		private void setGUIForDeepWell1DSuccess() {
			tb_1DScanner.Enabled = false;
			lbl_Barcode.Text = tb_1DScanner.Text;
			panel_ScannedBarcode.Visible = true;
			lbl_Instructions.Text = appSettings["DeepWellPositionsMsg"];
			lbl_Instructions.Visible = true;
			btn_Reset.Visible = true;
			lbl_Status.ForeColor = okColor;
			lbl_Status.Text = appSettings["DeepWellScanSuccess"];
			ArrayList wells = mdb.GetWells(tb_1DScanner.Text);
			displayDeepWellMap(wells);
            panel_RackPositionDisplay.Visible = true;
            panel_RackPositionDisplay.BringToFront();
            if (!deepwellFull(wells))
            {
                btn_Transfer.Visible = true;
            }
            else
            {
                btn_Transfer.Visible = false;
            }
            if (deepwellFilled(wells))
            {
                btn_Extract.Visible = true;
            }
            else
            {
                btn_Extract.Visible = false;
            }
		}

		private void setGUIFor2DSuccess() {
            lbl_Status.Text = appSettings["Success2DScan"];
            lbl_Status.Visible = true;
            setGUIFor1DScan();
		}

		private void setGUIForExtractSuccess() {
			throw new NotImplementedException();
		}

		private bool deepwellFull(ArrayList wells) {
			if (wells.Count == Int32.Parse(appSettings["PlateMaxSize"]))
				return true;
			return false;
		}

		private bool deepwellFilled(ArrayList wells) {
            int threshold = 96;
			try {
				threshold = Int32.Parse(appSettings["PlateFilledThreshold"]);
			} catch (ArgumentNullException ex) {
				Debug.WriteLine(ex.Message);
				// todo add message box
			} catch (FormatException ex) {
				Debug.WriteLine(ex.Message);
				// too add message box
			}
			if (wells.Count > threshold)
				return true;
			return false;
		}

		private void displayDeepWellMap(ArrayList wells) {
            Dictionary<String, System.Windows.Forms.Label> deepWellMap = new Dictionary<String, System.Windows.Forms.Label>();
            ArrayList lets = new ArrayList(new string[] {"A","B","C","D","E","F", "G", "H"});

            int xbase = 75;
            int ybase = 49;

            for(int i = 0; i < 12; i++)
            {
                int xcord = xbase * i;
                for (int j = 0; j < 8; j++)
                {
                    int ycord = ybase * j;
                    String key = lets[j] + (i + 1).ToString();
                    deepWellMap.Add(key, new System.Windows.Forms.Label());
                    System.Windows.Forms.Label lbl_x = deepWellMap[key];
			        lbl_x.Location = new System.Drawing.Point(xcord, ycord);
			        lbl_x.Size = new System.Drawing.Size(32, 31);

                    if (wells.Contains(key))
                    {
                        lbl_x.Image = Image.FromFile(@"images\full_oval.jpg");
                    }
                    else
                    {
                        lbl_x.Image = Image.FromFile(@"images\empty_oval.jpg");
                    }
                    panel_DeepWellDisplay.Controls.Add(lbl_x);

                }
            }

		}

		private bool wellAliquotConflict(ArrayList wells, Dictionary<string, string> barcodes) {
			bool conflict = false;
            foreach (KeyValuePair<string, string> kv in barcodes) {
                if(wells.Contains(kv.Key)) 
                {
                    conflict = true;
                    add2dFailureMessage(String.Format(appSettings["AliquotsIncorrectPosition"], kv.Key));
                    mdb.AddError("Well Aliquot Conflict", String.Format(appSettings["AliquotsIncorrectPosition"], kv.Key), String.Empty);
                }
            }
            return conflict;

		}

		private bool allAliquotsExist(ArrayList aliquots) {
            if (aliquots.Count == 0)
                return true;
            else
                return false;
		}

		private void addAliquotsToBiomekInput(string rackID, Dictionary<string, string> barcodes) {
			// check for file existence
			if (!File.Exists(appSettings["BiomekInputFile"])) {
				createBiomekInputFile();
			}

			// add barcodes to in memory representation
			try {
				using (GenericParserAdapter parser = new GenericParserAdapter(appSettings["BiomekInputFile"])) {
                    parser.FirstRowHasHeader = true;
					DataTable inputTable = parser.GetDataTable();
					foreach (KeyValuePair<string, string> kv in barcodes) {
						DataRow newRow = inputTable.NewRow();
						newRow["SrcBC"] = rackID;
						newRow["AliquotID"] = kv.Value;
						newRow["Position"] = kv.Key;
						inputTable.Rows.Add(newRow);
					}
					writeInputFleToDisk(inputTable);
				}
			} catch (ParsingException ex) {
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


		}

		private void addDeepwellsToBiomekInput(string id) {
			throw new NotImplementedException();
		}

		private void removeAliquotsFromBiomekInput(DataTable protocolResults) {
			try {
				using (GenericParserAdapter parser = new GenericParserAdapter(appSettings["BiomekInputFile"])) {
                    parser.FirstRowHasHeader = true;
                    DataTable inputTable = parser.GetDataTable();
					foreach (DataRow row in protocolResults.Rows) {
						string selectString = String.Format("SrcBC = '{0}' And AliquotID = '{1}'", row["SrcBC"].ToString(), row["SrcDesc"].ToString());
						DataRow[] deleteRows = inputTable.Select(selectString);
						foreach (DataRow deleteRow in deleteRows) {
							inputTable.Rows.Remove(deleteRow);
						}

					}
					writeInputFleToDisk(inputTable);
				}
			} catch (ParsingException ex) {
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void createBiomekInputFile() {
			try {
				// Open file and write header
				TextWriter writer = new StreamWriter(appSettings["BiomekInputFile"]);
				writer.WriteLine("SrcBC,AliquotID,Position");
				writer.Flush();
				writer.Close();
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void writeInputFleToDisk(DataTable inputTable) {
			try {
				// Open file and write header
				TextWriter writer = new StreamWriter(appSettings["BiomekInputFile"]);
				writer.WriteLine("SrcBC,AliquotID,Position");

				foreach (DataRow row in inputTable.Rows) {
					writer.WriteLine(String.Format("{0},{1},{2}", row["SrcBC"].ToString(), row["AliquotID"].ToString(), row["Position"].ToString()));
				}
                writer.Flush();
                writer.Close();
			} catch (Exception ex){
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void add2dFailureMessage(string msg) {
			if (twoDScanFailureMessage.Length < 500) {
				twoDScanFailureMessage.AppendLine(msg);
			}
		}

	}
}
