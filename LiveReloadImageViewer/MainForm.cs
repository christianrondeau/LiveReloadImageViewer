using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace LiveReloadImageViewer
{
	public partial class MainForm : Form
	{
		private string _watchedFile;
		private const int MaxTries = 5;
		private const int MillisecondsBetweenRetries = 200;

		public MainForm()
		{
			InitializeComponent();
		}

		private void ListenAndShow(string file)
		{
			ShowImage(file, true);

			_fileSystemWatcher.EnableRaisingEvents = false;
			_watchedFile = file;
			_fileSystemWatcher.Path = Path.GetDirectoryName(file);
			_fileSystemWatcher.Filter = $"*{Path.GetExtension(file)}";
			_fileSystemWatcher.EnableRaisingEvents = true;
		}

		private void OpenFile()
		{
			if (_openFileDialog.ShowDialog() == DialogResult.OK)
			{
				ListenAndShow(_openFileDialog.FileName);
			}
		}

		private void MainFormLoad(object sender, EventArgs e)
		{
			var file = Environment.GetCommandLineArgs().FirstOrDefault();
			if (Path.GetExtension(file) != ".exe")
				ListenAndShow(file);
		}

		private void OnDropFile(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				var file = ((string[])e.Data.GetData(DataFormats.FileDrop)).First();
				ListenAndShow(file);
				UpdateStatus($@"File dropped: {file}");
			}
			else
			{
				UpdateStatus(@"Invalid dropped file");
			}
		}

		private void OnClick(object sender, EventArgs e)
		{
			OpenFile();
		}

		private void OnFileChanged(object sender, FileSystemEventArgs e)
		{
			if (Path.GetFileName(e.FullPath) == Path.GetFileName(_watchedFile))
				ShowImage(e.FullPath, false);
		}

		private void OnFileRenamed(object sender, RenamedEventArgs e)
		{
			if (Path.GetFileName(e.OldFullPath) == Path.GetFileName(_watchedFile))
				ShowImage(null, false);
			else if (Path.GetFileName(e.FullPath) == Path.GetFileName(_watchedFile))
				ShowImage(e.FullPath, false);
		}

		private void ShowImage(string file, bool initial)
		{
			try
			{
				if (!string.IsNullOrEmpty(file) && File.Exists(file))
				{
					if (WaitForFileReady(file))
					{
						_pictureBox.ImageLocation = file;
						var text = initial ? "opened" : "updated";
						UpdateStatus($@"File {text}: {DateTime.Now.ToLongTimeString()}", Path.GetFileName(file));
					}
				}
				else
				{
					_pictureBox.Image = null;
					var text = initial ? "not found" : "deleted";
					UpdateStatus($@"File {text}: {DateTime.Now.ToLongTimeString()}", Path.GetFileName(file));
				}
			}
			catch (Exception exc)
			{
				UpdateStatus($@"Error {exc.Message}", Path.GetFileName(file));
			}
		}

		private bool WaitForFileReady(string file)
		{
			for (var tries = 0; tries < MaxTries; tries++)
			{
				try
				{
					Thread.Sleep(MillisecondsBetweenRetries);
					using (var inputStream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
						if (inputStream.Length > 0)
							return true;
				}
				catch (IOException exc)
				{
					UpdateStatus($@"File could not be loaded: {exc.Message}", Path.GetFileName(file));
					Update();
				}
			}
			return false;
		}

		private void OnPictureLoaded(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
				UpdateStatus($@"File error: {e.Error.Message}");
		}

		private void UpdateStatus(string message, string filename = null)
		{
			_toolStripStatusLabelFilename.Text = filename ?? "(No File Opened)";
			_toolStripStatusLabelInfo.Text = message;
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Application.Exit();

			if (e.KeyCode == Keys.O)
				OpenFile();

			if (e.KeyCode == Keys.F5)
				ListenAndShow(_watchedFile);
		}

		private void _toolStripMenuItem100_Click(object sender, EventArgs e)
		{
			_toolStripDropDownButtonZoomMode.Text = ((ToolStripMenuItem) sender).Text;
			_pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
		}

		private void _toolStripMenuItemFit_Click(object sender, EventArgs e)
		{
			_toolStripDropDownButtonZoomMode.Text = ((ToolStripMenuItem) sender).Text;
			_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
		}
	}
}
