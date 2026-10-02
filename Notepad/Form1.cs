using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form1 : Form
    {
        int textBoxCounter = 0;
        TextBox currentTextBox;

        /// <summary>
        /// Updates the current active text box based on the selected tab.
        /// </summary>
        private void TrackCurrentTextBox()
        {
             currentTextBox = GetCurrentTextBox(tabNotpadPages.SelectedTab);
        }
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new tab containing a multi-line text box.
        /// </summary>
        private void CreatTabPage()
        {
            TabPage newPage = new TabPage("new page");
            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.Dock = DockStyle.Fill;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.ContextMenuStrip = cmTextMenu;
            textBoxCounter++;
            newPage.Name = "tb" + textBoxCounter;
            textBox.Name = newPage.Name;
            newPage.Controls.Add(textBox);
            tabNotpadPages.Controls.Add(newPage);
        }

        /// <summary>
        /// Handles form loading, creating an initial tab if empty and tracking the active text box.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            if(tabNotpadPages.TabPages.Count == 0)
            {
                CreatTabPage();
            }

            TrackCurrentTextBox();
        }

        /// <summary>
        /// Handles the "New Tab" menu click to create a new editing page.
        /// </summary>
        private void newTapToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CreatTabPage();
        }

        /// <summary>
        /// Writes text content to a specified file path.
        /// </summary>
        private void WriteToFile(string filePath, string content)
        {
          File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// Checks if the text box has an existing saved file path stored in its Tag.
        /// </summary>
        private bool HasExistingPath(TextBox textBox)
        {
            return textBox.Tag != null;
        }

        /// <summary>
        /// Retrieves the text box control from a specific tab page.
        /// </summary>
        private TextBox GetCurrentTextBox(TabPage currentPage)
        {
            return (TextBox)currentPage.Controls[0];
        }

        /// <summary>
        /// Opens a Save File Dialog to save the content of the current text box to a new file.
        /// </summary>
        private void SaveFileAs()
        {
            sfdSaveFile.DefaultExt = "txt";
            sfdSaveFile.Filter = "txt files (*.txt)|*.txt";

            if (sfdSaveFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfdSaveFile.FileName;
                WriteToFile(filePath, currentTextBox.Text);
                currentTextBox.Tag = filePath;
            }
        }

        /// <summary>
        /// Handles the "Save As" menu click.
        /// </summary>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        /// <summary>
        /// Handles the "Save" menu click, updating an existing file or prompting "Save As" if new.
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HasExistingPath(currentTextBox)) {
                string filePath = currentTextBox.Tag.ToString();
                WriteToFile(filePath, currentTextBox.Text);
            }
            else
            {
                SaveFileAs();
            }

        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Selects all text in the current text box.
        /// </summary>
        private void selcetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTextBox.SelectAll();
        }

        /// <summary>
        /// Clears/deletes the currently selected text.
        /// </summary>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTextBox.SelectedText = "";
        
        }

        /// <summary>
        /// Opens the Font Dialog to change the text style and color.
        /// </summary>
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fdFont.ShowDialog() == DialogResult.OK)
            {
                currentTextBox.Font = fdFont.Font;
                currentTextBox.ForeColor = fdFont.Color;
            }
        }

        /// <summary>
        /// Applies font changes dynamically from the font dialog.
        /// </summary>
        private void fdFont_Apply(object sender, EventArgs e)
        {
            currentTextBox.Font = fdFont.Font;
            currentTextBox.ForeColor = fdFont.Color;
        }

        /// <summary>
        /// Resets the text box font and color back to default settings.
        /// </summary>
        private void clearFormaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTextBox.Font = new Font("Segoe UI", 8);
            currentTextBox.ForeColor = Color.Black;          
        }

        /// <summary>
        /// Inserts the current date and time at the cursor position.
        /// </summary>
        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dateTimeString = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            string beforeCursor = currentTextBox.Text.Substring(0, currentTextBox.SelectionStart);
            string afterCursor = currentTextBox.Text.Substring(currentTextBox.SelectionStart);
            currentTextBox.Text = beforeCursor + dateTimeString + afterCursor;
            currentTextBox.SelectionStart = dateTimeString.Length + beforeCursor.Length;
        }

        /// <summary>
        /// Copies selected text to the clipboard.
        /// </summary>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTextBox.Copy();                  
        }

        /// <summary>
        /// Copies selected text to the clipboard.
        /// </summary>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTextBox.Paste();
        }

        /// <summary>
        /// Cuts selected text to the clipboard.
        /// </summary>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTextBox.Cut();
        }

        /// <summary>
        /// Converts the selected text to uppercase.
        /// </summary>
        private void upperCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTextBox.SelectedText=  currentTextBox.SelectedText.ToUpper();

        }

        /// <summary>
        /// Handles tab switching to update the active text box reference.
        /// </summary>
        private void tabNotpadPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrackCurrentTextBox();
        }


        /// <summary>
        /// Converts the selected text to lowercase.
        /// </summary>
        private void lowerCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTextBox.SelectedText = currentTextBox.SelectedText.ToLower();

        }


    }
}
