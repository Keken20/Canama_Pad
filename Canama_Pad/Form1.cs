using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Canama_Pad
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UpdateStatusLabel("Ready");
            UpdateLnClStatus();
            statusStrip1.Visible = false;
        }
        string filelName;
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveChanges == false)
            {
                this.Text = "Canama_Pad";
                richTextBox1.Text = "";
                SaveChanges = false;
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveDialog();
                    SaveChanges = false;
                }
                else if(result == DialogResult.No)
                {
                    this.Text = "Canama_Pad";
                    richTextBox1.Text = "";
                    SaveChanges = false;
                }
            }         
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveChanges == false)
            {
                OpenDialog();
                SaveChanges = false;
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveDialog();
                    SaveChanges = false;
                }
                else if (result == DialogResult.No)
                {
                    OpenDialog();
                    SaveChanges = false;

                }
            }
             
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
                SaveDialog();
                SaveChanges = false;                        
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you want to close this window?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                //Modify this code..
                MessageBox.Show("Ok", "Ok", MessageBoxButtons.OK);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
            UpdateStatusLabel("Undo");
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
            UpdateStatusLabel("Redo");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
            UpdateStatusLabel("Copy");
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
            UpdateStatusLabel("Paste");
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
            UpdateStatusLabel("Cut");
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            UpdateStatusLabel("SelectAll");
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = System.DateTime.Now.ToString();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog op = new FontDialog();
            if (op.ShowDialog() == DialogResult.OK)
                richTextBox1.Font = op.Font;

        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog op = new ColorDialog();
            if (op.ShowDialog() == DialogResult.OK)
                richTextBox1.ForeColor = op.Color;
        }

        private bool SaveChanges = false;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SaveChanges)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveDialog();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            SaveChanges = true;
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                SaveChanges = false;
            }
            else
            {
                SaveChanges = true;
            }
        }

        private void UpdateStatusLabel(string status)
        {
            toolStripStatusLabel1.Text = status;
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateLnClStatus();
        }

        private void UpdateLnClStatus()
        {
            int pos = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(pos) + 1;
            int col = pos - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;

            toolStripStatusLabel2.Text = "Ln " + line + ", " + "Col " + col;
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(status.Checked)
            {
                statusStrip1.Visible = true;
            }
            else
            {
                statusStrip1.Visible = false;
            }        
        }
        private void OpenDialog()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "open";
            op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                filelName = Path.GetFileName(op.FileName);
                richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = filelName;
                SaveChanges = false;
            }
        }
        private void SaveDialog()
        {
            if(this.Text == "Canama_Pad")
            {
                SaveFileDialog sv = new SaveFileDialog();
                sv.Title = "Save";
                sv.Filter = "Text Document(.txt)|.txt| All Files(.)|.";
                if (sv.ShowDialog() == DialogResult.OK)
                    richTextBox1.SaveFile(sv.FileName, RichTextBoxStreamType.PlainText);
                this.Text = sv.FileName;
            }
            else
            {  
                richTextBox1.SaveFile(Path.GetFileName(this.Text), RichTextBoxStreamType.RichText);
            }                        
        }
        


    }
}
