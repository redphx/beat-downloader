namespace BeatDownloader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.rdoZing = new System.Windows.Forms.RadioButton();
            this.rdoSanNhac = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.Label();
            this.btnStopSearch = new System.Windows.Forms.Button();
            this.txtSearchQuery = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSinger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGenre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpOption = new System.Windows.Forms.GroupBox();
            this.btnExplorer = new System.Windows.Forms.Button();
            this.btnSaveFolder = new System.Windows.Forms.Button();
            this.txtSaveFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fbdSaveFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnStopDownload = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stlInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgDownload = new System.Windows.Forms.ProgressBar();
            this.txtDownloadStatus = new System.Windows.Forms.Label();
            this.lbDownloadSize = new System.Windows.Forms.Label();
            this.lbDownloadPercent = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.bgwSearch = new System.ComponentModel.BackgroundWorker();
            this.bgwDownload = new System.ComponentModel.BackgroundWorker();
            this.bgwCheckForUpdate = new System.ComponentModel.BackgroundWorker();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.grpOption.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.rdoZing);
            this.grpSearch.Controls.Add(this.rdoSanNhac);
            this.grpSearch.Controls.Add(this.label3);
            this.grpSearch.Controls.Add(this.txtResult);
            this.grpSearch.Controls.Add(this.btnStopSearch);
            this.grpSearch.Controls.Add(this.txtSearchQuery);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.label1);
            this.grpSearch.Controls.Add(this.dgvResult);
            this.grpSearch.Location = new System.Drawing.Point(12, 12);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(590, 305);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Tìm";
            // 
            // rdoZing
            // 
            this.rdoZing.AutoSize = true;
            this.rdoZing.Location = new System.Drawing.Point(164, 50);
            this.rdoZing.Name = "rdoZing";
            this.rdoZing.Size = new System.Drawing.Size(79, 17);
            this.rdoZing.TabIndex = 15;
            this.rdoZing.Text = "star.zing.vn";
            this.rdoZing.UseVisualStyleBackColor = true;
            // 
            // rdoSanNhac
            // 
            this.rdoSanNhac.AutoSize = true;
            this.rdoSanNhac.Checked = true;
            this.rdoSanNhac.Location = new System.Drawing.Point(69, 50);
            this.rdoSanNhac.Name = "rdoSanNhac";
            this.rdoSanNhac.Size = new System.Drawing.Size(89, 17);
            this.rdoSanNhac.TabIndex = 14;
            this.rdoSanNhac.TabStop = true;
            this.rdoSanNhac.Text = "sannhac.com";
            this.rdoSanNhac.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tìm trong :";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(484, 289);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(94, 13);
            this.txtResult.TabIndex = 12;
            this.txtResult.Text = "0 kết quả";
            this.txtResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnStopSearch
            // 
            this.btnStopSearch.Enabled = false;
            this.btnStopSearch.Location = new System.Drawing.Point(504, 20);
            this.btnStopSearch.Name = "btnStopSearch";
            this.btnStopSearch.Size = new System.Drawing.Size(75, 23);
            this.btnStopSearch.TabIndex = 11;
            this.btnStopSearch.Text = "Dừng";
            this.btnStopSearch.UseVisualStyleBackColor = true;
            this.btnStopSearch.Click += new System.EventHandler(this.btnStopSearching_Click);
            // 
            // txtSearchQuery
            // 
            this.txtSearchQuery.Location = new System.Drawing.Point(69, 22);
            this.txtSearchQuery.Name = "txtSearchQuery";
            this.txtSearchQuery.Size = new System.Drawing.Size(348, 20);
            this.txtSearchQuery.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(423, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ khóa :";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTitle,
            this.colSinger,
            this.colGenre,
            this.colSite,
            this.colLink,
            this.colBeat});
            this.dgvResult.Location = new System.Drawing.Point(8, 89);
            this.dgvResult.MultiSelect = false;
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowTemplate.Height = 20;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.ShowCellToolTips = false;
            this.dgvResult.ShowEditingIcon = false;
            this.dgvResult.Size = new System.Drawing.Size(570, 197);
            this.dgvResult.TabIndex = 4;
            this.dgvResult.SelectionChanged += new System.EventHandler(this.dgvResult_SelectionChanged);
            // 
            // colTitle
            // 
            this.colTitle.FillWeight = 73.85786F;
            this.colTitle.HeaderText = "Tên bài hát";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            this.colTitle.Width = 280;
            // 
            // colSinger
            // 
            this.colSinger.FillWeight = 73.85786F;
            this.colSinger.HeaderText = "Ca sỹ";
            this.colSinger.Name = "colSinger";
            this.colSinger.ReadOnly = true;
            this.colSinger.Width = 180;
            // 
            // colGenre
            // 
            this.colGenre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGenre.HeaderText = "Thể loại";
            this.colGenre.Name = "colGenre";
            this.colGenre.ReadOnly = true;
            // 
            // colSite
            // 
            this.colSite.HeaderText = "Site";
            this.colSite.Name = "colSite";
            this.colSite.ReadOnly = true;
            this.colSite.Visible = false;
            // 
            // colLink
            // 
            this.colLink.HeaderText = "Link";
            this.colLink.Name = "colLink";
            this.colLink.ReadOnly = true;
            this.colLink.Visible = false;
            // 
            // colBeat
            // 
            this.colBeat.HeaderText = "Beat";
            this.colBeat.Name = "colBeat";
            this.colBeat.ReadOnly = true;
            this.colBeat.Visible = false;
            // 
            // grpOption
            // 
            this.grpOption.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grpOption.Controls.Add(this.btnExplorer);
            this.grpOption.Controls.Add(this.btnSaveFolder);
            this.grpOption.Controls.Add(this.txtSaveFolder);
            this.grpOption.Controls.Add(this.label2);
            this.grpOption.Location = new System.Drawing.Point(12, 453);
            this.grpOption.Name = "grpOption";
            this.grpOption.Size = new System.Drawing.Size(590, 49);
            this.grpOption.TabIndex = 5;
            this.grpOption.TabStop = false;
            this.grpOption.Text = "Tùy chọn";
            // 
            // btnExplorer
            // 
            this.btnExplorer.Location = new System.Drawing.Point(504, 17);
            this.btnExplorer.Name = "btnExplorer";
            this.btnExplorer.Size = new System.Drawing.Size(75, 23);
            this.btnExplorer.TabIndex = 3;
            this.btnExplorer.Text = "Mở thư mục";
            this.btnExplorer.UseVisualStyleBackColor = true;
            this.btnExplorer.Click += new System.EventHandler(this.btnExplorer_Click);
            // 
            // btnSaveFolder
            // 
            this.btnSaveFolder.Location = new System.Drawing.Point(423, 17);
            this.btnSaveFolder.Name = "btnSaveFolder";
            this.btnSaveFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFolder.TabIndex = 2;
            this.btnSaveFolder.Text = "Đổi thư mục";
            this.btnSaveFolder.UseVisualStyleBackColor = true;
            this.btnSaveFolder.Click += new System.EventHandler(this.btnSaveFolder_Click);
            // 
            // txtSaveFolder
            // 
            this.txtSaveFolder.Location = new System.Drawing.Point(118, 19);
            this.txtSaveFolder.Name = "txtSaveFolder";
            this.txtSaveFolder.ReadOnly = true;
            this.txtSaveFolder.Size = new System.Drawing.Size(299, 20);
            this.txtSaveFolder.TabIndex = 1;
            this.txtSaveFolder.Text = "C:\\";
            this.txtSaveFolder.TextChanged += new System.EventHandler(this.txtSaveFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Thư mục lưu beat :";
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(221, 323);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 6;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnStopDownload
            // 
            this.btnStopDownload.Enabled = false;
            this.btnStopDownload.Location = new System.Drawing.Point(302, 323);
            this.btnStopDownload.Name = "btnStopDownload";
            this.btnStopDownload.Size = new System.Drawing.Size(75, 23);
            this.btnStopDownload.TabIndex = 7;
            this.btnStopDownload.Text = "Dừng";
            this.btnStopDownload.UseVisualStyleBackColor = true;
            this.btnStopDownload.Click += new System.EventHandler(this.btnStopDownload_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stlInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 534);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(614, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "sttStatus";
            // 
            // stlInfo
            // 
            this.stlInfo.Name = "stlInfo";
            this.stlInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // prgDownload
            // 
            this.prgDownload.Location = new System.Drawing.Point(8, 42);
            this.prgDownload.Name = "prgDownload";
            this.prgDownload.Size = new System.Drawing.Size(573, 20);
            this.prgDownload.TabIndex = 9;
            // 
            // txtDownloadStatus
            // 
            this.txtDownloadStatus.AutoSize = true;
            this.txtDownloadStatus.Location = new System.Drawing.Point(6, 26);
            this.txtDownloadStatus.Name = "txtDownloadStatus";
            this.txtDownloadStatus.Size = new System.Drawing.Size(66, 13);
            this.txtDownloadStatus.TabIndex = 10;
            this.txtDownloadStatus.Text = "Đang chờ ...";
            // 
            // lbDownloadSize
            // 
            this.lbDownloadSize.Location = new System.Drawing.Point(6, 65);
            this.lbDownloadSize.Name = "lbDownloadSize";
            this.lbDownloadSize.Size = new System.Drawing.Size(278, 13);
            this.lbDownloadSize.TabIndex = 11;
            this.lbDownloadSize.Text = "   ";
            this.lbDownloadSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDownloadPercent
            // 
            this.lbDownloadPercent.Location = new System.Drawing.Point(516, 65);
            this.lbDownloadPercent.Name = "lbDownloadPercent";
            this.lbDownloadPercent.Size = new System.Drawing.Size(63, 13);
            this.lbDownloadPercent.TabIndex = 12;
            this.lbDownloadPercent.Text = "   ";
            this.lbDownloadPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBox1.Controls.Add(this.prgDownload);
            this.groupBox1.Controls.Add(this.lbDownloadPercent);
            this.groupBox1.Controls.Add(this.txtDownloadStatus);
            this.groupBox1.Controls.Add(this.lbDownloadSize);
            this.groupBox1.Location = new System.Drawing.Point(12, 352);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 92);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download";
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(527, 508);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 14;
            this.btnAbout.Text = "Thông tin";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkUpdate.Location = new System.Drawing.Point(260, 510);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(261, 18);
            this.lnkUpdate.TabIndex = 15;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "Đã có Beat Downloader";
            this.lnkUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkUpdate.Visible = false;
            this.lnkUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdate_LinkClicked);
            // 
            // bgwSearch
            // 
            this.bgwSearch.WorkerReportsProgress = true;
            this.bgwSearch.WorkerSupportsCancellation = true;
            this.bgwSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSearch_DoWork);
            this.bgwSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSearch_RunWorkerCompleted);
            this.bgwSearch.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSearch_ProgressChanged);
            // 
            // bgwDownload
            // 
            this.bgwDownload.WorkerReportsProgress = true;
            this.bgwDownload.WorkerSupportsCancellation = true;
            this.bgwDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDownload_DoWork);
            this.bgwDownload.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwDownload_ProgressChanged);
            // 
            // bgwCheckForUpdate
            // 
            this.bgwCheckForUpdate.WorkerReportsProgress = true;
            this.bgwCheckForUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCheckForUpdate_DoWork);
            this.bgwCheckForUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCheckForUpdate_ProgressChanged);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 556);
            this.Controls.Add(this.lnkUpdate);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStopDownload);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.grpOption);
            this.Controls.Add(this.grpSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Beat Downloader";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.grpOption.ResumeLayout(false);
            this.grpOption.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.TextBox txtSearchQuery;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Button btnStopSearch;
        private System.Windows.Forms.GroupBox grpOption;
        private System.Windows.Forms.TextBox txtSaveFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog fbdSaveFolder;
        private System.Windows.Forms.Button btnSaveFolder;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnStopDownload;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stlInfo;
        private System.Windows.Forms.ProgressBar prgDownload;
        private System.Windows.Forms.Button btnExplorer;
        private System.Windows.Forms.Label txtResult;
        private System.Windows.Forms.Label txtDownloadStatus;
        private System.Windows.Forms.Label lbDownloadSize;
        private System.Windows.Forms.Label lbDownloadPercent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.LinkLabel lnkUpdate;
        private System.ComponentModel.BackgroundWorker bgwSearch;
        private System.ComponentModel.BackgroundWorker bgwDownload;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSinger;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGenre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeat;
        private System.Windows.Forms.RadioButton rdoZing;
        private System.Windows.Forms.RadioButton rdoSanNhac;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bgwCheckForUpdate;
    }
}

