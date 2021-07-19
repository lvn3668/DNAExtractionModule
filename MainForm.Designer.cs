// Author Lalitha Viswanathan
// DNA Extraction Module 
// 
namespace DNAExtractionModule {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbl_Status = new System.Windows.Forms.Label();
            this.tb_1DScanner = new System.Windows.Forms.TextBox();
            this.lbl_Instructions = new System.Windows.Forms.Label();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Transfer = new System.Windows.Forms.Button();
            this.btn_Extract = new System.Windows.Forms.Button();
            this.lbl_ScannedBarcode = new System.Windows.Forms.Label();
            this.lbl_Barcode = new System.Windows.Forms.Label();
            this.panel_ScannedBarcode = new System.Windows.Forms.Panel();
            this.panel_RackPositionDisplay = new System.Windows.Forms.Panel();
            this.panel_RackRowLabels = new System.Windows.Forms.Panel();
            this.lbl_H = new System.Windows.Forms.Label();
            this.lbl_G = new System.Windows.Forms.Label();
            this.lbl_F = new System.Windows.Forms.Label();
            this.lbl_E = new System.Windows.Forms.Label();
            this.lbl_D = new System.Windows.Forms.Label();
            this.lbl_C = new System.Windows.Forms.Label();
            this.lbl_B = new System.Windows.Forms.Label();
            this.lbl_A = new System.Windows.Forms.Label();
            this.panel_RackColumnLabels = new System.Windows.Forms.Panel();
            this.lbl_12 = new System.Windows.Forms.Label();
            this.lbl_11 = new System.Windows.Forms.Label();
            this.lbl_10 = new System.Windows.Forms.Label();
            this.lbl_9 = new System.Windows.Forms.Label();
            this.lbl_8 = new System.Windows.Forms.Label();
            this.lbl_7 = new System.Windows.Forms.Label();
            this.lbl_6 = new System.Windows.Forms.Label();
            this.lbl_5 = new System.Windows.Forms.Label();
            this.lbl_4 = new System.Windows.Forms.Label();
            this.lbl_3 = new System.Windows.Forms.Label();
            this.lbl_2 = new System.Windows.Forms.Label();
            this.lbl_1 = new System.Windows.Forms.Label();
            this.panel_DeepWellDisplay = new System.Windows.Forms.Panel();
            this.panel_VolumeAdjust = new System.Windows.Forms.Panel();
            this.gb_VolumeAdjust = new System.Windows.Forms.GroupBox();
            this.lbl_Plasma1 = new System.Windows.Forms.Label();
            this.btn_xferVolUp = new System.Windows.Forms.Button();
            this.btn_xferVolDown = new System.Windows.Forms.Button();
            this.TwoDScannerBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lbl_modeBanner = new System.Windows.Forms.Label();
            this.tb_2dErrors = new System.Windows.Forms.TextBox();
            this.tb_Rack1dScan = new System.Windows.Forms.TextBox();
            this.panel_ScannedBarcode.SuspendLayout();
            this.panel_RackPositionDisplay.SuspendLayout();
            this.panel_RackRowLabels.SuspendLayout();
            this.panel_RackColumnLabels.SuspendLayout();
            this.panel_VolumeAdjust.SuspendLayout();
            this.gb_VolumeAdjust.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.Location = new System.Drawing.Point(21, 20);
            this.lbl_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(206, 39);
            this.lbl_Status.TabIndex = 0;
            this.lbl_Status.Text = "Status Label";
            // 
            // tb_1DScanner
            // 
            this.tb_1DScanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_1DScanner.Location = new System.Drawing.Point(32, 69);
            this.tb_1DScanner.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_1DScanner.Name = "tb_1DScanner";
            this.tb_1DScanner.Size = new System.Drawing.Size(585, 45);
            this.tb_1DScanner.TabIndex = 1;
            this.tb_1DScanner.Text = "Input";
            this.tb_1DScanner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_1DScanner_KeyDown);
            // 
            // lbl_Instructions
            // 
            this.lbl_Instructions.AutoSize = true;
            this.lbl_Instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Instructions.Location = new System.Drawing.Point(21, 128);
            this.lbl_Instructions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Instructions.Name = "lbl_Instructions";
            this.lbl_Instructions.Size = new System.Drawing.Size(284, 39);
            this.lbl_Instructions.TabIndex = 2;
            this.lbl_Instructions.Text = "Instructions Label";
            // 
            // btn_Exit
            // 
            this.btn_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.Location = new System.Drawing.Point(1269, 660);
            this.btn_Exit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(171, 92);
            this.btn_Exit.TabIndex = 33;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_Exit_MouseClick);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Reset.Location = new System.Drawing.Point(11, 660);
            this.btn_Reset.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(171, 92);
            this.btn_Reset.TabIndex = 34;
            this.btn_Reset.Text = "Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Visible = false;
            this.btn_Reset.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_Reset_MouseClick);
            // 
            // btn_Transfer
            // 
            this.btn_Transfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Transfer.Location = new System.Drawing.Point(245, 660);
            this.btn_Transfer.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Transfer.Name = "btn_Transfer";
            this.btn_Transfer.Size = new System.Drawing.Size(171, 92);
            this.btn_Transfer.TabIndex = 35;
            this.btn_Transfer.Text = "Transfer";
            this.btn_Transfer.UseVisualStyleBackColor = true;
            this.btn_Transfer.Visible = false;
            this.btn_Transfer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_Transfer_MouseClick);
            // 
            // btn_Extract
            // 
            this.btn_Extract.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Extract.Location = new System.Drawing.Point(1035, 660);
            this.btn_Extract.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Extract.Name = "btn_Extract";
            this.btn_Extract.Size = new System.Drawing.Size(171, 92);
            this.btn_Extract.TabIndex = 36;
            this.btn_Extract.Text = "Extract";
            this.btn_Extract.UseVisualStyleBackColor = true;
            this.btn_Extract.Visible = false;
            this.btn_Extract.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_Extract_MouseClick);
            // 
            // lbl_ScannedBarcode
            // 
            this.lbl_ScannedBarcode.AutoSize = true;
            this.lbl_ScannedBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ScannedBarcode.Location = new System.Drawing.Point(0, 0);
            this.lbl_ScannedBarcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_ScannedBarcode.Name = "lbl_ScannedBarcode";
            this.lbl_ScannedBarcode.Size = new System.Drawing.Size(297, 39);
            this.lbl_ScannedBarcode.TabIndex = 37;
            this.lbl_ScannedBarcode.Text = "Scanned Barcode:";
            // 
            // lbl_Barcode
            // 
            this.lbl_Barcode.AutoSize = true;
            this.lbl_Barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Barcode.Location = new System.Drawing.Point(320, 0);
            this.lbl_Barcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Barcode.Name = "lbl_Barcode";
            this.lbl_Barcode.Size = new System.Drawing.Size(264, 39);
            this.lbl_Barcode.TabIndex = 38;
            this.lbl_Barcode.Text = "9600045326782";
            // 
            // panel_ScannedBarcode
            // 
            this.panel_ScannedBarcode.Controls.Add(this.lbl_Barcode);
            this.panel_ScannedBarcode.Controls.Add(this.lbl_ScannedBarcode);
            this.panel_ScannedBarcode.Location = new System.Drawing.Point(832, 20);
            this.panel_ScannedBarcode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_ScannedBarcode.Name = "panel_ScannedBarcode";
            this.panel_ScannedBarcode.Size = new System.Drawing.Size(597, 49);
            this.panel_ScannedBarcode.TabIndex = 39;
            this.panel_ScannedBarcode.Visible = false;
            // 
            // panel_RackPositionDisplay
            // 
            this.panel_RackPositionDisplay.BackColor = System.Drawing.Color.Beige;
            this.panel_RackPositionDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RackPositionDisplay.Controls.Add(this.panel_RackRowLabels);
            this.panel_RackPositionDisplay.Controls.Add(this.panel_RackColumnLabels);
            this.panel_RackPositionDisplay.Controls.Add(this.panel_DeepWellDisplay);
            this.panel_RackPositionDisplay.Location = new System.Drawing.Point(32, 177);
            this.panel_RackPositionDisplay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_RackPositionDisplay.Name = "panel_RackPositionDisplay";
            this.panel_RackPositionDisplay.Size = new System.Drawing.Size(986, 467);
            this.panel_RackPositionDisplay.TabIndex = 40;
            // 
            // panel_RackRowLabels
            // 
            this.panel_RackRowLabels.Controls.Add(this.lbl_H);
            this.panel_RackRowLabels.Controls.Add(this.lbl_G);
            this.panel_RackRowLabels.Controls.Add(this.lbl_F);
            this.panel_RackRowLabels.Controls.Add(this.lbl_E);
            this.panel_RackRowLabels.Controls.Add(this.lbl_D);
            this.panel_RackRowLabels.Controls.Add(this.lbl_C);
            this.panel_RackRowLabels.Controls.Add(this.lbl_B);
            this.panel_RackRowLabels.Controls.Add(this.lbl_A);
            this.panel_RackRowLabels.Location = new System.Drawing.Point(0, 62);
            this.panel_RackRowLabels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_RackRowLabels.Name = "panel_RackRowLabels";
            this.panel_RackRowLabels.Size = new System.Drawing.Size(64, 394);
            this.panel_RackRowLabels.TabIndex = 1;
            // 
            // lbl_H
            // 
            this.lbl_H.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_H.Location = new System.Drawing.Point(21, 345);
            this.lbl_H.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_H.Name = "lbl_H";
            this.lbl_H.Size = new System.Drawing.Size(45, 38);
            this.lbl_H.TabIndex = 6;
            this.lbl_H.Text = "H";
            // 
            // lbl_G
            // 
            this.lbl_G.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_G.Location = new System.Drawing.Point(21, 295);
            this.lbl_G.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_G.Name = "lbl_G";
            this.lbl_G.Size = new System.Drawing.Size(47, 38);
            this.lbl_G.TabIndex = 2;
            this.lbl_G.Text = "G";
            // 
            // lbl_F
            // 
            this.lbl_F.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_F.Location = new System.Drawing.Point(21, 246);
            this.lbl_F.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_F.Name = "lbl_F";
            this.lbl_F.Size = new System.Drawing.Size(41, 38);
            this.lbl_F.TabIndex = 5;
            this.lbl_F.Text = "F";
            // 
            // lbl_E
            // 
            this.lbl_E.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_E.Location = new System.Drawing.Point(21, 197);
            this.lbl_E.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_E.Name = "lbl_E";
            this.lbl_E.Size = new System.Drawing.Size(43, 38);
            this.lbl_E.TabIndex = 4;
            this.lbl_E.Text = "E";
            // 
            // lbl_D
            // 
            this.lbl_D.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_D.Location = new System.Drawing.Point(21, 148);
            this.lbl_D.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_D.Name = "lbl_D";
            this.lbl_D.Size = new System.Drawing.Size(45, 38);
            this.lbl_D.TabIndex = 3;
            this.lbl_D.Text = "D";
            // 
            // lbl_C
            // 
            this.lbl_C.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_C.Location = new System.Drawing.Point(21, 98);
            this.lbl_C.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_C.Name = "lbl_C";
            this.lbl_C.Size = new System.Drawing.Size(45, 38);
            this.lbl_C.TabIndex = 2;
            this.lbl_C.Text = "C";
            // 
            // lbl_B
            // 
            this.lbl_B.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_B.Location = new System.Drawing.Point(21, 49);
            this.lbl_B.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_B.Name = "lbl_B";
            this.lbl_B.Size = new System.Drawing.Size(43, 38);
            this.lbl_B.TabIndex = 1;
            this.lbl_B.Text = "B";
            // 
            // lbl_A
            // 
            this.lbl_A.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_A.Location = new System.Drawing.Point(21, 0);
            this.lbl_A.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_A.Name = "lbl_A";
            this.lbl_A.Size = new System.Drawing.Size(43, 38);
            this.lbl_A.TabIndex = 0;
            this.lbl_A.Text = "A";
            // 
            // panel_RackColumnLabels
            // 
            this.panel_RackColumnLabels.Controls.Add(this.lbl_12);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_11);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_10);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_9);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_8);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_7);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_6);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_5);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_4);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_3);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_2);
            this.panel_RackColumnLabels.Controls.Add(this.lbl_1);
            this.panel_RackColumnLabels.Location = new System.Drawing.Point(20, 0);
            this.panel_RackColumnLabels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_RackColumnLabels.Name = "panel_RackColumnLabels";
            this.panel_RackColumnLabels.Size = new System.Drawing.Size(971, 49);
            this.panel_RackColumnLabels.TabIndex = 0;
            // 
            // lbl_12
            // 
            this.lbl_12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_12.Location = new System.Drawing.Point(885, 10);
            this.lbl_12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_12.Name = "lbl_12";
            this.lbl_12.Size = new System.Drawing.Size(59, 38);
            this.lbl_12.TabIndex = 17;
            this.lbl_12.Text = "12";
            // 
            // lbl_11
            // 
            this.lbl_11.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_11.Location = new System.Drawing.Point(811, 10);
            this.lbl_11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_11.Name = "lbl_11";
            this.lbl_11.Size = new System.Drawing.Size(59, 38);
            this.lbl_11.TabIndex = 16;
            this.lbl_11.Text = "11";
            // 
            // lbl_10
            // 
            this.lbl_10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_10.Location = new System.Drawing.Point(736, 10);
            this.lbl_10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_10.Name = "lbl_10";
            this.lbl_10.Size = new System.Drawing.Size(59, 38);
            this.lbl_10.TabIndex = 15;
            this.lbl_10.Text = "10";
            // 
            // lbl_9
            // 
            this.lbl_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_9.Location = new System.Drawing.Point(661, 10);
            this.lbl_9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_9.Name = "lbl_9";
            this.lbl_9.Size = new System.Drawing.Size(39, 38);
            this.lbl_9.TabIndex = 14;
            this.lbl_9.Text = "9";
            // 
            // lbl_8
            // 
            this.lbl_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_8.Location = new System.Drawing.Point(587, 10);
            this.lbl_8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_8.Name = "lbl_8";
            this.lbl_8.Size = new System.Drawing.Size(39, 38);
            this.lbl_8.TabIndex = 13;
            this.lbl_8.Text = "8";
            // 
            // lbl_7
            // 
            this.lbl_7.AutoSize = true;
            this.lbl_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_7.Location = new System.Drawing.Point(512, 10);
            this.lbl_7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_7.Name = "lbl_7";
            this.lbl_7.Size = new System.Drawing.Size(36, 39);
            this.lbl_7.TabIndex = 12;
            this.lbl_7.Text = "7";
            // 
            // lbl_6
            // 
            this.lbl_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_6.Location = new System.Drawing.Point(437, 10);
            this.lbl_6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_6.Name = "lbl_6";
            this.lbl_6.Size = new System.Drawing.Size(39, 38);
            this.lbl_6.TabIndex = 11;
            this.lbl_6.Text = "6";
            // 
            // lbl_5
            // 
            this.lbl_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_5.Location = new System.Drawing.Point(363, 10);
            this.lbl_5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_5.Name = "lbl_5";
            this.lbl_5.Size = new System.Drawing.Size(39, 38);
            this.lbl_5.TabIndex = 10;
            this.lbl_5.Text = "5";
            // 
            // lbl_4
            // 
            this.lbl_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_4.Location = new System.Drawing.Point(288, 10);
            this.lbl_4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_4.Name = "lbl_4";
            this.lbl_4.Size = new System.Drawing.Size(39, 38);
            this.lbl_4.TabIndex = 9;
            this.lbl_4.Text = "4";
            // 
            // lbl_3
            // 
            this.lbl_3.AutoSize = true;
            this.lbl_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_3.Location = new System.Drawing.Point(213, 10);
            this.lbl_3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_3.Name = "lbl_3";
            this.lbl_3.Size = new System.Drawing.Size(36, 39);
            this.lbl_3.TabIndex = 8;
            this.lbl_3.Text = "3";
            // 
            // lbl_2
            // 
            this.lbl_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_2.Location = new System.Drawing.Point(139, 10);
            this.lbl_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_2.Name = "lbl_2";
            this.lbl_2.Size = new System.Drawing.Size(39, 38);
            this.lbl_2.TabIndex = 2;
            this.lbl_2.Text = "2";
            // 
            // lbl_1
            // 
            this.lbl_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_1.Location = new System.Drawing.Point(64, 10);
            this.lbl_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(39, 38);
            this.lbl_1.TabIndex = 7;
            this.lbl_1.Text = "1";
            // 
            // panel_DeepWellDisplay
            // 
            this.panel_DeepWellDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.panel_DeepWellDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_DeepWellDisplay.Location = new System.Drawing.Point(87, 68);
            this.panel_DeepWellDisplay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_DeepWellDisplay.Name = "panel_DeepWellDisplay";
            this.panel_DeepWellDisplay.Size = new System.Drawing.Size(866, 375);
            this.panel_DeepWellDisplay.TabIndex = 0;
            // 
            // panel_VolumeAdjust
            // 
            this.panel_VolumeAdjust.Controls.Add(this.gb_VolumeAdjust);
            this.panel_VolumeAdjust.Location = new System.Drawing.Point(32, 226);
            this.panel_VolumeAdjust.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_VolumeAdjust.Name = "panel_VolumeAdjust";
            this.panel_VolumeAdjust.Size = new System.Drawing.Size(245, 197);
            this.panel_VolumeAdjust.TabIndex = 0;
            // 
            // gb_VolumeAdjust
            // 
            this.gb_VolumeAdjust.Controls.Add(this.lbl_Plasma1);
            this.gb_VolumeAdjust.Controls.Add(this.btn_xferVolUp);
            this.gb_VolumeAdjust.Controls.Add(this.btn_xferVolDown);
            this.gb_VolumeAdjust.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_VolumeAdjust.Location = new System.Drawing.Point(0, 0);
            this.gb_VolumeAdjust.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gb_VolumeAdjust.Name = "gb_VolumeAdjust";
            this.gb_VolumeAdjust.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gb_VolumeAdjust.Size = new System.Drawing.Size(235, 187);
            this.gb_VolumeAdjust.TabIndex = 0;
            this.gb_VolumeAdjust.TabStop = false;
            this.gb_VolumeAdjust.Text = "transfer volume";
            this.gb_VolumeAdjust.Visible = false;
            // 
            // lbl_Plasma1
            // 
            this.lbl_Plasma1.AutoSize = true;
            this.lbl_Plasma1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Plasma1.Location = new System.Drawing.Point(21, 69);
            this.lbl_Plasma1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Plasma1.Name = "lbl_Plasma1";
            this.lbl_Plasma1.Size = new System.Drawing.Size(64, 36);
            this.lbl_Plasma1.TabIndex = 15;
            this.lbl_Plasma1.Text = "0 µl";
            // 
            // btn_xferVolUp
            // 
            this.btn_xferVolUp.Image = ((System.Drawing.Image)(resources.GetObject("btn_xferVolUp.Image")));
            this.btn_xferVolUp.Location = new System.Drawing.Point(128, 27);
            this.btn_xferVolUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_xferVolUp.Name = "btn_xferVolUp";
            this.btn_xferVolUp.Size = new System.Drawing.Size(72, 65);
            this.btn_xferVolUp.TabIndex = 26;
            this.btn_xferVolUp.UseVisualStyleBackColor = true;
            this.btn_xferVolUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_xferVolUp_MouseClick);
            // 
            // btn_xferVolDown
            // 
            this.btn_xferVolDown.Image = ((System.Drawing.Image)(resources.GetObject("btn_xferVolDown.Image")));
            this.btn_xferVolDown.Location = new System.Drawing.Point(128, 96);
            this.btn_xferVolDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_xferVolDown.Name = "btn_xferVolDown";
            this.btn_xferVolDown.Size = new System.Drawing.Size(72, 65);
            this.btn_xferVolDown.TabIndex = 27;
            this.btn_xferVolDown.UseVisualStyleBackColor = true;
            this.btn_xferVolDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_xferVolDown_MouseClick);
            // 
            // TwoDScannerBackgroundWorker
            // 
            this.TwoDScannerBackgroundWorker.WorkerReportsProgress = true;
            this.TwoDScannerBackgroundWorker.WorkerSupportsCancellation = true;
            this.TwoDScannerBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TwoDScannerBackgroundWorker_DoWork);
            this.TwoDScannerBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.TwoDScannerBackgroundWorker_ProgressChanged);
            this.TwoDScannerBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TwoDScannerBackgroundWorker_RunWorkerCompleted);
            // 
            // lbl_modeBanner
            // 
            this.lbl_modeBanner.AutoSize = true;
            this.lbl_modeBanner.BackColor = System.Drawing.Color.Red;
            this.lbl_modeBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_modeBanner.Location = new System.Drawing.Point(459, 689);
            this.lbl_modeBanner.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_modeBanner.Name = "lbl_modeBanner";
            this.lbl_modeBanner.Size = new System.Drawing.Size(487, 39);
            this.lbl_modeBanner.TabIndex = 41;
            this.lbl_modeBanner.Text = "CONFIGURED FOR TESTING";
            // 
            // tb_2dErrors
            // 
            this.tb_2dErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_2dErrors.Location = new System.Drawing.Point(32, 177);
            this.tb_2dErrors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_2dErrors.Multiline = true;
            this.tb_2dErrors.Name = "tb_2dErrors";
            this.tb_2dErrors.Size = new System.Drawing.Size(1385, 452);
            this.tb_2dErrors.TabIndex = 42;
            this.tb_2dErrors.Text = "Errors from 2d scan...";
            this.tb_2dErrors.Visible = false;
            // 
            // tb_Rack1dScan
            // 
            this.tb_Rack1dScan.Enabled = false;
            this.tb_Rack1dScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Rack1dScan.Location = new System.Drawing.Point(32, 70);
            this.tb_Rack1dScan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_Rack1dScan.Name = "tb_Rack1dScan";
            this.tb_Rack1dScan.Size = new System.Drawing.Size(585, 45);
            this.tb_Rack1dScan.TabIndex = 43;
            this.tb_Rack1dScan.Text = "Input";
            this.tb_Rack1dScan.Visible = false;
            this.tb_Rack1dScan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Rack1dScan_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 767);
            this.Controls.Add(this.tb_Rack1dScan);
            this.Controls.Add(this.lbl_modeBanner);
            this.Controls.Add(this.panel_VolumeAdjust);
            this.Controls.Add(this.panel_RackPositionDisplay);
            this.Controls.Add(this.panel_ScannedBarcode);
            this.Controls.Add(this.btn_Extract);
            this.Controls.Add(this.btn_Transfer);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.lbl_Instructions);
            this.Controls.Add(this.tb_1DScanner);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.tb_2dErrors);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "DNA Extraction Module";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel_ScannedBarcode.ResumeLayout(false);
            this.panel_ScannedBarcode.PerformLayout();
            this.panel_RackPositionDisplay.ResumeLayout(false);
            this.panel_RackRowLabels.ResumeLayout(false);
            this.panel_RackColumnLabels.ResumeLayout(false);
            this.panel_RackColumnLabels.PerformLayout();
            this.panel_VolumeAdjust.ResumeLayout(false);
            this.gb_VolumeAdjust.ResumeLayout(false);
            this.gb_VolumeAdjust.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		/// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lbl_Status;
		/// <summary>
        /// 1D scanner text box (to scan barcode of DNA Sample)
        /// </summary>
        private System.Windows.Forms.TextBox tb_1DScanner;

		private System.Windows.Forms.Label lbl_Instructions;

		/// <summary>
        /// Exit Button
        /// </summary>
        private System.Windows.Forms.Button btn_Exit;

		/// <summary>
        /// Reset button
        /// </summary>
        private System.Windows.Forms.Button btn_Reset;
		/// <summary>
        /// Transfer to deep well plate (button)
        /// </summary>
        private System.Windows.Forms.Button btn_Transfer;
		/// <summary>
        /// Extract DNA button
        /// </summary>
        private System.Windows.Forms.Button btn_Extract;
		/// <summary>
        /// Barcode of scanned plate
        /// </summary>
        private System.Windows.Forms.Label lbl_ScannedBarcode;

		/// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lbl_Barcode;
		/// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Panel panel_ScannedBarcode;
		/// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Panel panel_RackPositionDisplay;
        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Panel panel_DeepWellDisplay;
		/// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Panel panel_RackRowLabels;
		private System.Windows.Forms.Label lbl_H;
		private System.Windows.Forms.Label lbl_G;
		private System.Windows.Forms.Label lbl_F;
		private System.Windows.Forms.Label lbl_E;
		private System.Windows.Forms.Label lbl_D;
		private System.Windows.Forms.Label lbl_C;
		private System.Windows.Forms.Label lbl_B;
		private System.Windows.Forms.Label lbl_A;
		private System.Windows.Forms.Panel panel_RackColumnLabels;
		private System.Windows.Forms.Label lbl_12;
		private System.Windows.Forms.Label lbl_11;
		private System.Windows.Forms.Label lbl_10;
		private System.Windows.Forms.Label lbl_9;
		private System.Windows.Forms.Label lbl_8;
		private System.Windows.Forms.Label lbl_7;
		private System.Windows.Forms.Label lbl_6;
		private System.Windows.Forms.Label lbl_5;
		private System.Windows.Forms.Label lbl_4;
		private System.Windows.Forms.Label lbl_3;
		private System.Windows.Forms.Label lbl_2;
		private System.Windows.Forms.Label lbl_1;
		/// <summary>
        /// Volume adjust up/down
        /// </summary>
        private System.Windows.Forms.Panel panel_VolumeAdjust;
		private System.Windows.Forms.GroupBox gb_VolumeAdjust;

		/// <summary>
        /// Plasma label
        /// </summary>
        private System.Windows.Forms.Label lbl_Plasma1;
		/// <summary>
        /// Xfer volume up/down
        /// </summary>
        private System.Windows.Forms.Button btn_xferVolUp;
		private System.Windows.Forms.Button btn_xferVolDown;

		/// <summary>
        /// 2D Scanner Background worker
        /// </summary>
        private System.ComponentModel.BackgroundWorker TwoDScannerBackgroundWorker;

		private System.Windows.Forms.Label lbl_modeBanner;
		/// <summary>
        /// Error message for 2D scans
        /// </summary>
        private System.Windows.Forms.TextBox tb_2dErrors;
		/// <summary>
        /// Rack Scan (1D Scan)
        /// </summary>
        private System.Windows.Forms.TextBox tb_Rack1dScan;
	}
}

