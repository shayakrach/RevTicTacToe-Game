using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex5.WindowsGUI
{
    public class FormSettings : Form
    {
        private TextBox m_TextboxPlayer1Name;
        private TextBox m_TextboxPlayer2Name;
        private CheckBox m_CheckBoxIsComputer;
        private NumericUpDown m_NumericUpDownRows;
        private NumericUpDown m_NumericUpDownCols;
        private Label m_LabelPlayers;
        private Label m_LabelPlayer1Name;
        private Label m_LabelPlayer2Name;
        private Label m_LabelBoardSize;
        private Label m_LabelRows;
        private Label m_LabelColumns;
        private Button m_ButtonStart;

        public FormSettings()
        {
            InitializeComponent();
        }

        public string Player1Name
        {
            get
            {
                return m_TextboxPlayer1Name.Text.Equals(string.Empty) ? "Player1" : m_TextboxPlayer1Name.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_TextboxPlayer1Name.Text.Equals(string.Empty) ? "Player2" : m_TextboxPlayer2Name.Text;
            }
        }

        public int BoardSize
        {
            get { return (int)m_NumericUpDownRows.Value; }
        }

        public bool IsAgainstComputer
        {
            get { return !m_CheckBoxIsComputer.Checked; }
        }

        private void InitializeComponent()
        {
            this.m_LabelPlayers = new System.Windows.Forms.Label();
            this.m_LabelPlayer1Name = new System.Windows.Forms.Label();
            this.m_TextboxPlayer1Name = new System.Windows.Forms.TextBox();
            this.m_CheckBoxIsComputer = new System.Windows.Forms.CheckBox();
            this.m_LabelPlayer2Name = new System.Windows.Forms.Label();
            this.m_TextboxPlayer2Name = new System.Windows.Forms.TextBox();
            this.m_LabelBoardSize = new System.Windows.Forms.Label();
            this.m_LabelRows = new System.Windows.Forms.Label();
            this.m_NumericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.m_LabelColumns = new System.Windows.Forms.Label();
            this.m_NumericUpDownCols = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)this.m_NumericUpDownRows).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.m_NumericUpDownCols).BeginInit();
            this.SuspendLayout();
             
            //// m_LabelPlayers
             
            this.m_LabelPlayers.AutoSize = true;
            this.m_LabelPlayers.Location = new System.Drawing.Point(22, 22);
            this.m_LabelPlayers.Name = "m_LabelPlayers";
            this.m_LabelPlayers.Size = new System.Drawing.Size(44, 13);
            this.m_LabelPlayers.TabIndex = 0;
            this.m_LabelPlayers.Text = "Players:";
            
            //// m_LabelPlayer1Name
            
            this.m_LabelPlayer1Name.AutoSize = true;
            this.m_LabelPlayer1Name.Location = new System.Drawing.Point(32, 48);
            this.m_LabelPlayer1Name.Name = "m_LabelPlayer1Name";
            this.m_LabelPlayer1Name.Size = new System.Drawing.Size(48, 13);
            this.m_LabelPlayer1Name.TabIndex = 1;
            this.m_LabelPlayer1Name.Text = "Player 1:";

            //// m_TextboxPlayer1Name

            this.m_TextboxPlayer1Name.Location = new System.Drawing.Point(128, 48);
            this.m_TextboxPlayer1Name.Name = "m_TextboxPlayer1Name";
            this.m_TextboxPlayer1Name.Size = new System.Drawing.Size(100, 20);
            this.m_TextboxPlayer1Name.TabIndex = 2;

            //// m_CheckBoxIsComputer

            this.m_CheckBoxIsComputer.AutoSize = true;
            this.m_CheckBoxIsComputer.BackColor = System.Drawing.Color.Blue;
            this.m_CheckBoxIsComputer.Location = new System.Drawing.Point(36, 80);
            this.m_CheckBoxIsComputer.Name = "m_CheckBoxIsComputer";
            this.m_CheckBoxIsComputer.Size = new System.Drawing.Size(15, 14);
            this.m_CheckBoxIsComputer.TabIndex = 3;
            this.m_CheckBoxIsComputer.UseVisualStyleBackColor = false;
            this.m_CheckBoxIsComputer.CheckedChanged += new System.EventHandler(this.checkBoxIsComputer_CheckedChanged);

            //// m_LabelPlayer2Name

            this.m_LabelPlayer2Name.AutoSize = true;
            this.m_LabelPlayer2Name.Location = new System.Drawing.Point(56, 81);
            this.m_LabelPlayer2Name.Name = "m_LabelPlayer2Name";
            this.m_LabelPlayer2Name.Size = new System.Drawing.Size(48, 13);
            this.m_LabelPlayer2Name.TabIndex = 4;
            this.m_LabelPlayer2Name.Text = "Player 2:";

            //// m_TextboxPlayer2Name

            this.m_TextboxPlayer2Name.Enabled = false;
            this.m_TextboxPlayer2Name.Location = new System.Drawing.Point(128, 78);
            this.m_TextboxPlayer2Name.Name = "m_TextboxPlayer2Name";
            this.m_TextboxPlayer2Name.Size = new System.Drawing.Size(100, 20);
            this.m_TextboxPlayer2Name.TabIndex = 5;
            this.m_TextboxPlayer2Name.Text = "[Computer]";

            //// m_LabelBoardSize

            this.m_LabelBoardSize.AutoSize = true;
            this.m_LabelBoardSize.Location = new System.Drawing.Point(25, 122);
            this.m_LabelBoardSize.Name = "m_LabelBoardSize";
            this.m_LabelBoardSize.Size = new System.Drawing.Size(61, 13);
            this.m_LabelBoardSize.TabIndex = 6;
            this.m_LabelBoardSize.Text = "Board Size:";

            //// m_LabelRows

            this.m_LabelRows.AutoSize = true;
            this.m_LabelRows.Location = new System.Drawing.Point(33, 147);
            this.m_LabelRows.Name = "m_LabelRows";
            this.m_LabelRows.Size = new System.Drawing.Size(37, 13);
            this.m_LabelRows.TabIndex = 7;
            this.m_LabelRows.Text = "Rows:";

            //// m_NumericUpDownRows

            this.m_NumericUpDownRows.Location = new System.Drawing.Point(82, 143);
            this.m_NumericUpDownRows.Maximum = new decimal(new int[] 
            {
            9,
            0,
            0,
            0
            });
            this.m_NumericUpDownRows.Minimum = new decimal(new int[] 
            {
            3,
            0,
            0,
            0
            });
            this.m_NumericUpDownRows.Name = "m_NumericUpDownRows";
            this.m_NumericUpDownRows.ReadOnly = true;
            this.m_NumericUpDownRows.Size = new System.Drawing.Size(35, 20);
            this.m_NumericUpDownRows.TabIndex = 8;
            this.m_NumericUpDownRows.Value = new decimal(new int[] 
            {
            3,
            0,
            0,
            0
            });
            this.m_NumericUpDownRows.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);

            //// m_LabelColumns

            this.m_LabelColumns.AutoSize = true;
            this.m_LabelColumns.Location = new System.Drawing.Point(154, 147);
            this.m_LabelColumns.Name = "m_LabelColumns";
            this.m_LabelColumns.Size = new System.Drawing.Size(30, 13);
            this.m_LabelColumns.TabIndex = 9;
            this.m_LabelColumns.Text = "Cols:";

            //// m_NumericUpDownCols

            this.m_NumericUpDownCols.Location = new System.Drawing.Point(190, 143);
            this.m_NumericUpDownCols.Maximum = new decimal(new int[] 
            {
            9,
            0,
            0,
            0
            });
            this.m_NumericUpDownCols.Minimum = new decimal(new int[] 
            {
            3,
            0,
            0,
            0
            });
            this.m_NumericUpDownCols.Name = "m_NumericUpDownCols";
            this.m_NumericUpDownCols.ReadOnly = true;
            this.m_NumericUpDownCols.Size = new System.Drawing.Size(35, 20);
            this.m_NumericUpDownCols.TabIndex = 10;
            this.m_NumericUpDownCols.Value = new decimal(new int[] 
            {
            3,
            0,
            0,
            0
            });
            this.m_NumericUpDownCols.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);

            //// m_ButtonStart

            this.m_ButtonStart.Location = new System.Drawing.Point(52, 200);
            this.m_ButtonStart.Name = "m_ButtonStart";
            this.m_ButtonStart.Size = new System.Drawing.Size(163, 23);
            this.m_ButtonStart.TabIndex = 11;
            this.m_ButtonStart.Text = "Start!";
            this.m_ButtonStart.Click += new System.EventHandler(this.buttonStart_Click);

            //// FormSettings

            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ClientSize = new System.Drawing.Size(250, 250);
            this.Controls.Add(this.m_LabelPlayers);
            this.Controls.Add(this.m_LabelPlayer1Name);
            this.Controls.Add(this.m_TextboxPlayer1Name);
            this.Controls.Add(this.m_CheckBoxIsComputer);
            this.Controls.Add(this.m_LabelPlayer2Name);
            this.Controls.Add(this.m_TextboxPlayer2Name);
            this.Controls.Add(this.m_LabelBoardSize);
            this.Controls.Add(this.m_LabelRows);
            this.Controls.Add(this.m_NumericUpDownRows);
            this.Controls.Add(this.m_LabelColumns);
            this.Controls.Add(this.m_NumericUpDownCols);
            this.Controls.Add(this.m_ButtonStart);
            this.Name = "FormSettings";
            ((System.ComponentModel.ISupportInitialize)this.m_NumericUpDownRows).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.m_NumericUpDownCols).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if(sender == m_NumericUpDownRows)
            {
                m_NumericUpDownCols.Value = m_NumericUpDownRows.Value;
            }
            else if(sender == m_NumericUpDownCols)
            {
                m_NumericUpDownRows.Value = m_NumericUpDownCols.Value;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBoxIsComputer_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckBox).Checked)
            {
                m_TextboxPlayer2Name.Enabled = true;
            }
            else
            {
                m_TextboxPlayer2Name.Enabled = false;
                m_TextboxPlayer2Name.Text = "[Computer]";
            }
        }
    }
}
