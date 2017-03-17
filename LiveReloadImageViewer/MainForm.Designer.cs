namespace LiveReloadImageViewer
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
			this._pictureBox = new System.Windows.Forms.PictureBox();
			this._statusStrip = new System.Windows.Forms.StatusStrip();
			this._toolStripStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._fileSystemWatcher = new System.IO.FileSystemWatcher();
			((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
			this._statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._fileSystemWatcher)).BeginInit();
			this.SuspendLayout();
			// 
			// _pictureBox
			// 
			this._pictureBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this._pictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this._pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pictureBox.Location = new System.Drawing.Point(0, 0);
			this._pictureBox.Name = "_pictureBox";
			this._pictureBox.Size = new System.Drawing.Size(665, 493);
			this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this._pictureBox.TabIndex = 0;
			this._pictureBox.TabStop = false;
			this._pictureBox.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.OnPictureLoaded);
			this._pictureBox.Click += new System.EventHandler(this.OnClick);
			this._pictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDropFile);
			// 
			// _statusStrip
			// 
			this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusLabelInfo});
			this._statusStrip.Location = new System.Drawing.Point(0, 471);
			this._statusStrip.Name = "_statusStrip";
			this._statusStrip.Size = new System.Drawing.Size(665, 22);
			this._statusStrip.TabIndex = 1;
			this._statusStrip.Text = "statusStrip1";
			// 
			// _toolStripStatusLabelInfo
			// 
			this._toolStripStatusLabelInfo.Name = "_toolStripStatusLabelInfo";
			this._toolStripStatusLabelInfo.Size = new System.Drawing.Size(147, 17);
			this._toolStripStatusLabelInfo.Text = "Click in the viewer to open";
			// 
			// _openFileDialog
			// 
			this._openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
			this._openFileDialog.Title = "Select an image file";
			// 
			// _fileSystemWatcher
			// 
			this._fileSystemWatcher.EnableRaisingEvents = true;
			this._fileSystemWatcher.SynchronizingObject = this;
			this._fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.OnFileChanged);
			this._fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.OnFileChanged);
			this._fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.OnFileChanged);
			this._fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.OnFileRenamed);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(665, 493);
			this.Controls.Add(this._statusStrip);
			this.Controls.Add(this._pictureBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Live-Reload Image Viewer";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
			this._statusStrip.ResumeLayout(false);
			this._statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._fileSystemWatcher)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox _pictureBox;
		private System.Windows.Forms.StatusStrip _statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusLabelInfo;
		private System.Windows.Forms.OpenFileDialog _openFileDialog;
		private System.IO.FileSystemWatcher _fileSystemWatcher;
	}
}

