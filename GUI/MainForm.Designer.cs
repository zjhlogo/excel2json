namespace excel2json.GUI {
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolReimport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCopyJson = new System.Windows.Forms.ToolStripButton();
            this.toolSaveJson = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCopyXml = new System.Windows.Forms.ToolStripButton();
            this.toolSaveXml = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCopyCSharp = new System.Windows.Forms.ToolStripButton();
            this.toolSaveCSharp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolHelp = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelExcelDropBox = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBoxExcel = new System.Windows.Forms.PictureBox();
            this.labelExcelFile = new System.Windows.Forms.Label();
            this.tabControlCode = new System.Windows.Forms.TabControl();
            this.tabPageJson = new System.Windows.Forms.TabPage();
            this.tabPageXml = new System.Windows.Forms.TabPage();
            this.tabCSharp = new System.Windows.Forms.TabPage();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolImportExcel = new System.Windows.Forms.ToolStripButton();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelExcelDropBox.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExcel)).BeginInit();
            this.tabControlCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "Ready";
            // 
            // statusLabel
            // 
            this.statusLabel.IsLink = true;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusLabel.Size = new System.Drawing.Size(139, 17);
            this.statusLabel.Text = "https://neil3d.github.io";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel.Click += new System.EventHandler(this.statusLabel_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolImportExcel,
            this.toolReimport,
            this.toolStripSeparator1,
            this.toolCopyJson,
            this.toolSaveJson,
            this.toolStripSeparator3,
            this.toolCopyXml,
            this.toolSaveXml,
            this.toolStripSeparator4,
            this.toolCopyCSharp,
            this.toolSaveCSharp,
            this.toolStripSeparator2,
            this.toolHelp});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(784, 48);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "Import excel file and export as JSON";
            // 
            // toolReimport
            // 
            this.toolReimport.Image = global::excel2json.Properties.Resources.excel;
            this.toolReimport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReimport.Name = "toolReimport";
            this.toolReimport.Size = new System.Drawing.Size(66, 45);
            this.toolReimport.Text = "Reimport";
            this.toolReimport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolReimport.ToolTipText = "Import Excel .xlsx file";
            this.toolReimport.Click += new System.EventHandler(this.btnReimport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // toolCopyJson
            // 
            this.toolCopyJson.Image = ((System.Drawing.Image)(resources.GetObject("toolCopyJson.Image")));
            this.toolCopyJson.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCopyJson.Name = "toolCopyJson";
            this.toolCopyJson.Size = new System.Drawing.Size(72, 45);
            this.toolCopyJson.Text = "Copy Json";
            this.toolCopyJson.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCopyJson.ToolTipText = "Copy JSON string to clipboard";
            this.toolCopyJson.Click += new System.EventHandler(this.btnCopyJson_Click);
            // 
            // toolSaveJson
            // 
            this.toolSaveJson.Image = ((System.Drawing.Image)(resources.GetObject("toolSaveJson.Image")));
            this.toolSaveJson.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveJson.Name = "toolSaveJson";
            this.toolSaveJson.Size = new System.Drawing.Size(69, 45);
            this.toolSaveJson.Text = "Save Json";
            this.toolSaveJson.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSaveJson.ToolTipText = "Save JSON file";
            this.toolSaveJson.Click += new System.EventHandler(this.btnSaveJson_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 48);
            // 
            // toolCopyXml
            // 
            this.toolCopyXml.Image = ((System.Drawing.Image)(resources.GetObject("toolCopyXml.Image")));
            this.toolCopyXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCopyXml.Name = "toolCopyXml";
            this.toolCopyXml.Size = new System.Drawing.Size(68, 45);
            this.toolCopyXml.Text = "Copy Xml";
            this.toolCopyXml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCopyXml.Click += new System.EventHandler(this.btnCopyXml_Click);
            // 
            // toolSaveXml
            // 
            this.toolSaveXml.Image = ((System.Drawing.Image)(resources.GetObject("toolSaveXml.Image")));
            this.toolSaveXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveXml.Name = "toolSaveXml";
            this.toolSaveXml.Size = new System.Drawing.Size(65, 45);
            this.toolSaveXml.Text = "Save Xml";
            this.toolSaveXml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSaveXml.Click += new System.EventHandler(this.btnSaveXml_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 48);
            // 
            // toolCopyCSharp
            // 
            this.toolCopyCSharp.Image = ((System.Drawing.Image)(resources.GetObject("toolCopyCSharp.Image")));
            this.toolCopyCSharp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCopyCSharp.Name = "toolCopyCSharp";
            this.toolCopyCSharp.Size = new System.Drawing.Size(62, 45);
            this.toolCopyCSharp.Text = "Copy C#";
            this.toolCopyCSharp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCopyCSharp.ToolTipText = "Save JSON file";
            this.toolCopyCSharp.Click += new System.EventHandler(this.btnCopyCSharp_Click);
            // 
            // toolSaveCSharp
            // 
            this.toolSaveCSharp.Image = ((System.Drawing.Image)(resources.GetObject("toolSaveCSharp.Image")));
            this.toolSaveCSharp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveCSharp.Name = "toolSaveCSharp";
            this.toolSaveCSharp.Size = new System.Drawing.Size(59, 45);
            this.toolSaveCSharp.Text = "Save C#";
            this.toolSaveCSharp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSaveCSharp.ToolTipText = "Save JSON file";
            this.toolSaveCSharp.Click += new System.EventHandler(this.btnSaveCSharp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
            // 
            // toolHelp
            // 
            this.toolHelp.Image = global::excel2json.Properties.Resources.about;
            this.toolHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHelp.Name = "toolHelp";
            this.toolHelp.Size = new System.Drawing.Size(39, 45);
            this.toolHelp.Text = "Help";
            this.toolHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolHelp.ToolTipText = "Help Document on web";
            this.toolHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlCode);
            this.splitContainer1.Size = new System.Drawing.Size(784, 492);
            this.splitContainer1.SplitterDistance = 288;
            this.splitContainer1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panelExcelDropBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(286, 490);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panelExcelDropBox
            // 
            this.panelExcelDropBox.AllowDrop = true;
            this.panelExcelDropBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelExcelDropBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExcelDropBox.Controls.Add(this.flowLayoutPanel2);
            this.panelExcelDropBox.Location = new System.Drawing.Point(8, 8);
            this.panelExcelDropBox.Margin = new System.Windows.Forms.Padding(8);
            this.panelExcelDropBox.Name = "panelExcelDropBox";
            this.panelExcelDropBox.Size = new System.Drawing.Size(270, 176);
            this.panelExcelDropBox.TabIndex = 1;
            this.panelExcelDropBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelExcelDropBox_DragDrop);
            this.panelExcelDropBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelExcelDropBox_DragEnter);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.pictureBoxExcel);
            this.flowLayoutPanel2.Controls.Add(this.labelExcelFile);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(268, 174);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // pictureBoxExcel
            // 
            this.pictureBoxExcel.Image = global::excel2json.Properties.Resources.excel_64;
            this.pictureBoxExcel.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxExcel.Name = "pictureBoxExcel";
            this.pictureBoxExcel.Size = new System.Drawing.Size(262, 130);
            this.pictureBoxExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxExcel.TabIndex = 0;
            this.pictureBoxExcel.TabStop = false;
            // 
            // labelExcelFile
            // 
            this.labelExcelFile.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelExcelFile.Location = new System.Drawing.Point(3, 136);
            this.labelExcelFile.Name = "labelExcelFile";
            this.labelExcelFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelExcelFile.Size = new System.Drawing.Size(260, 35);
            this.labelExcelFile.TabIndex = 1;
            this.labelExcelFile.Text = "Drop you .xlsx file here!";
            this.labelExcelFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControlCode
            // 
            this.tabControlCode.Controls.Add(this.tabPageJson);
            this.tabControlCode.Controls.Add(this.tabPageXml);
            this.tabControlCode.Controls.Add(this.tabCSharp);
            this.tabControlCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCode.Location = new System.Drawing.Point(0, 0);
            this.tabControlCode.Name = "tabControlCode";
            this.tabControlCode.SelectedIndex = 0;
            this.tabControlCode.Size = new System.Drawing.Size(490, 490);
            this.tabControlCode.TabIndex = 0;
            // 
            // tabPageJson
            // 
            this.tabPageJson.Location = new System.Drawing.Point(4, 22);
            this.tabPageJson.Name = "tabPageJson";
            this.tabPageJson.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJson.Size = new System.Drawing.Size(482, 464);
            this.tabPageJson.TabIndex = 0;
            this.tabPageJson.Text = "Json";
            this.tabPageJson.UseVisualStyleBackColor = true;
            // 
            // tabPageXml
            // 
            this.tabPageXml.Location = new System.Drawing.Point(4, 22);
            this.tabPageXml.Name = "tabPageXml";
            this.tabPageXml.Size = new System.Drawing.Size(482, 464);
            this.tabPageXml.TabIndex = 2;
            this.tabPageXml.Text = "Xml";
            this.tabPageXml.UseVisualStyleBackColor = true;
            // 
            // tabCSharp
            // 
            this.tabCSharp.Location = new System.Drawing.Point(4, 22);
            this.tabCSharp.Name = "tabCSharp";
            this.tabCSharp.Padding = new System.Windows.Forms.Padding(3);
            this.tabCSharp.Size = new System.Drawing.Size(482, 464);
            this.tabCSharp.TabIndex = 1;
            this.tabCSharp.Text = "C#";
            this.tabCSharp.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // toolImportExcel
            // 
            this.toolImportExcel.Image = global::excel2json.Properties.Resources.excel;
            this.toolImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolImportExcel.Name = "toolImportExcel";
            this.toolImportExcel.Size = new System.Drawing.Size(85, 45);
            this.toolImportExcel.Text = "Import Excel";
            this.toolImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolImportExcel.ToolTipText = "Import Excel .xlsx file";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "excel2json";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelExcelDropBox.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExcel)).EndInit();
            this.tabControlCode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolHelp;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton toolCopyJson;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelExcelDropBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBoxExcel;
        private System.Windows.Forms.Label labelExcelFile;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ToolStripButton toolSaveJson;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControlCode;
        private System.Windows.Forms.TabPage tabPageJson;
        private System.Windows.Forms.TabPage tabCSharp;
        private System.Windows.Forms.ToolStripButton toolCopyCSharp;
        private System.Windows.Forms.ToolStripButton toolSaveCSharp;
        private System.Windows.Forms.TabPage tabPageXml;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolCopyXml;
        private System.Windows.Forms.ToolStripButton toolSaveXml;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolImportExcel;
        private System.Windows.Forms.ToolStripButton toolReimport;
    }
}