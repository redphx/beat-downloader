namespace BeatDownloader
{
    partial class DownloadTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.btnDownload = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOptions = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.dgvDownload = new System.Windows.Forms.DataGridView();
            this.colSite = new System.Windows.Forms.DataGridViewImageColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.prgDownload = new System.Windows.Forms.ProgressBar();
            this.lbDownloadPercent = new System.Windows.Forms.Label();
            this.lbDownloadStatus = new System.Windows.Forms.Label();
            this.lbDownloadSize = new System.Windows.Forms.Label();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownload)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDownload,
            this.btnStop,
            this.toolStripSeparator1,
            this.btnAbout,
            this.toolStripSeparator2,
            this.btnOptions,
            this.btnDelete});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(0);
            this.toolBar.Size = new System.Drawing.Size(650, 25);
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "toolStrip1";
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Image = global::BeatDownloader.Properties.Resources.control_play_blue;
            this.btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(81, 22);
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Image = global::BeatDownloader.Properties.Resources.control_stop_blue;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(51, 22);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbout
            // 
            this.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAbout.Image = global::BeatDownloader.Properties.Resources.information;
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(60, 22);
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOptions
            // 
            this.btnOptions.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnOptions.Image = global::BeatDownloader.Properties.Resources.prefs;
            this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(69, 22);
            this.btnOptions.Text = "Options";
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = global::BeatDownloader.Properties.Resources.delete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvDownload
            // 
            this.dgvDownload.AllowUserToAddRows = false;
            this.dgvDownload.AllowUserToDeleteRows = false;
            this.dgvDownload.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(233)))), ((int)(((byte)(134)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDownload.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDownload.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDownload.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDownload.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDownload.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDownload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDownload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSite,
            this.colTitle,
            this.colArtist,
            this.colLink,
            this.colBeat,
            this.colStatus});
            this.dgvDownload.Location = new System.Drawing.Point(0, 28);
            this.dgvDownload.Name = "dgvDownload";
            this.dgvDownload.ReadOnly = true;
            this.dgvDownload.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(233)))), ((int)(((byte)(134)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDownload.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDownload.RowTemplate.Height = 20;
            this.dgvDownload.RowTemplate.ReadOnly = true;
            this.dgvDownload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDownload.ShowCellToolTips = false;
            this.dgvDownload.ShowEditingIcon = false;
            this.dgvDownload.Size = new System.Drawing.Size(650, 347);
            this.dgvDownload.TabIndex = 7;
            this.dgvDownload.SelectionChanged += new System.EventHandler(this.dgvDownload_SelectionChanged);
            // 
            // colSite
            // 
            this.colSite.HeaderText = "Site";
            this.colSite.Name = "colSite";
            this.colSite.ReadOnly = true;
            this.colSite.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSite.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colSite.Width = 40;
            // 
            // colTitle
            // 
            this.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTitle.FillWeight = 230.9664F;
            this.colTitle.HeaderText = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            // 
            // colArtist
            // 
            this.colArtist.FillWeight = 4.196832F;
            this.colArtist.HeaderText = "Artist";
            this.colArtist.Name = "colArtist";
            this.colArtist.ReadOnly = true;
            this.colArtist.Width = 120;
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
            // colStatus
            // 
            this.colStatus.FillWeight = 12.5525F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 120;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 103);
            this.panel1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prgDownload);
            this.groupBox1.Controls.Add(this.lbDownloadPercent);
            this.groupBox1.Controls.Add(this.lbDownloadStatus);
            this.groupBox1.Controls.Add(this.lbDownloadSize);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download Status";
            // 
            // prgDownload
            // 
            this.prgDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgDownload.Location = new System.Drawing.Point(6, 42);
            this.prgDownload.Name = "prgDownload";
            this.prgDownload.Size = new System.Drawing.Size(638, 20);
            this.prgDownload.TabIndex = 13;
            // 
            // lbDownloadPercent
            // 
            this.lbDownloadPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDownloadPercent.Location = new System.Drawing.Point(535, 68);
            this.lbDownloadPercent.Name = "lbDownloadPercent";
            this.lbDownloadPercent.Size = new System.Drawing.Size(109, 13);
            this.lbDownloadPercent.TabIndex = 16;
            this.lbDownloadPercent.Text = "Download Percent";
            this.lbDownloadPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDownloadStatus
            // 
            this.lbDownloadStatus.AutoSize = true;
            this.lbDownloadStatus.Location = new System.Drawing.Point(6, 23);
            this.lbDownloadStatus.Name = "lbDownloadStatus";
            this.lbDownloadStatus.Size = new System.Drawing.Size(55, 13);
            this.lbDownloadStatus.TabIndex = 14;
            this.lbDownloadStatus.Text = "Waiting ...";
            // 
            // lbDownloadSize
            // 
            this.lbDownloadSize.Location = new System.Drawing.Point(6, 68);
            this.lbDownloadSize.Name = "lbDownloadSize";
            this.lbDownloadSize.Size = new System.Drawing.Size(441, 13);
            this.lbDownloadSize.TabIndex = 15;
            this.lbDownloadSize.Text = "Download Size";
            this.lbDownloadSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DownloadTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvDownload);
            this.Controls.Add(this.toolBar);
            this.Name = "DownloadTab";
            this.Size = new System.Drawing.Size(650, 478);
            this.Load += new System.EventHandler(this.DownloadTab_Load);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownload)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton btnDownload;
        private System.Windows.Forms.DataGridView dgvDownload;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.DataGridViewImageColumn colSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArtist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgDownload;
        private System.Windows.Forms.Label lbDownloadPercent;
        private System.Windows.Forms.Label lbDownloadStatus;
        private System.Windows.Forms.Label lbDownloadSize;
    }
}
