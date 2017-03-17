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
				_toolStripStatusLabelInfo.Text = $@"File dropped: {file}";
			}
			else
			{
				_toolStripStatusLabelInfo.Text = @"Invalid dropped file";
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
						_toolStripStatusLabelInfo.Text = $@"File {text}: {DateTime.Now.ToLongTimeString()}";
					}
				}
				else
				{
					_pictureBox.Image = null;
					var text = initial ? "not found" : "deleted";
					_toolStripStatusLabelInfo.Text = $@"File {text}: {DateTime.Now.ToLongTimeString()}";
				}
			}
			catch (Exception exc)
			{
				_toolStripStatusLabelInfo.Text = $@"Error {exc.Message}";
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
					_toolStripStatusLabelInfo.Text = $@"File could not be loaded: {exc.Message}";
					Update();
				}
			}
			return false;
		}

		private void OnPictureLoaded(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				_toolStripStatusLabelInfo.Text = $@"File error: {e.Error.Message}";
			}
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
	}
}
