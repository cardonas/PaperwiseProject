using LogicLayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PaperwiseProject
{
    public partial class MainWindow : Form
    {
        private FileManager _fileManager = new FileManager();

        public MainWindow()
        {

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var btn = new System.Windows.Forms.Button();
            btn.Size = new Size(100, txtSelectDir.ClientSize.Height + 4);
            btn.Location = new Point(txtSelectDir.ClientSize.Width - btn.Width, -1);
            btn.Cursor = Cursors.Default;
            btn.Text = "Select Directory";
            btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btn.Click += new System.EventHandler(BtnSelectDir_Click);
            txtSelectDir.Size = new Size(txtSelectDir.Size.Width, txtSelectDir.Size.Height + 5);
            txtSelectDir.Controls.Add(btn);
            base.OnLoad(e);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            string selectedDir = txtSelectDir.Text;
            string outputDir = txtOutputDir.Text;

            if (string.IsNullOrEmpty(selectedDir))
            {
                MessageBox.Show("Please provide an Input Directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(outputDir))
            {
                MessageBox.Show("Please provide an Output Directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _fileManager.GetFiles(selectedDir);
            _fileManager.GetFileInfos();

            try
            {
                _fileManager.WriteCSV(txtOutputDir.Text);
            }
            catch (FileManagerException fme)
            {
                MessageBox.Show(fme.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Operation Completed Successfuly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSelectDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtSelectDir.Text = dialog.SelectedPath;
        }
    }
}
