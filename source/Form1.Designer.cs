namespace Gomoku
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Previous = new System.Windows.Forms.PictureBox();
            this.BlackFirst = new System.Windows.Forms.RadioButton();
            this.WhiteFirst = new System.Windows.Forms.RadioButton();
            this.Start = new System.Windows.Forms.Button();
            this.End = new System.Windows.Forms.Button();
            this.Position = new System.Windows.Forms.ListBox();
            this.PlayWithPeople = new System.Windows.Forms.RadioButton();
            this.PlayWithComputer = new System.Windows.Forms.RadioButton();
            this.PlayWithWhoPanel = new System.Windows.Forms.Panel();
            this.WhoFirstPanel = new System.Windows.Forms.Panel();
            this.ComputerPanel = new System.Windows.Forms.Panel();
            this.ComputerFirst = new System.Windows.Forms.RadioButton();
            this.ComputerAfter = new System.Windows.Forms.RadioButton();
            this.DisplayNumber = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Previous)).BeginInit();
            this.PlayWithWhoPanel.SuspendLayout();
            this.WhoFirstPanel.SuspendLayout();
            this.ComputerPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(586, -4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 0;
            // 
            // Previous
            // 
            this.Previous.BackColor = System.Drawing.Color.Transparent;
            this.Previous.BackgroundImage = global::Gomoku.Properties.Resources.previous;
            this.Previous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Previous.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Previous.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Previous.Location = new System.Drawing.Point(859, 342);
            this.Previous.Name = "Previous";
            this.Previous.Size = new System.Drawing.Size(40, 40);
            this.Previous.TabIndex = 1;
            this.Previous.TabStop = false;
            this.Previous.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Previous_MouseDown);
            this.Previous.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Previous_MouseMove);
            // 
            // BlackFirst
            // 
            this.BlackFirst.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BlackFirst.Location = new System.Drawing.Point(18, 3);
            this.BlackFirst.Name = "BlackFirst";
            this.BlackFirst.Size = new System.Drawing.Size(122, 29);
            this.BlackFirst.TabIndex = 2;
            this.BlackFirst.TabStop = true;
            this.BlackFirst.Text = "黑棋先";
            this.BlackFirst.UseVisualStyleBackColor = true;
            // 
            // WhiteFirst
            // 
            this.WhiteFirst.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.WhiteFirst.Location = new System.Drawing.Point(18, 38);
            this.WhiteFirst.Name = "WhiteFirst";
            this.WhiteFirst.Size = new System.Drawing.Size(122, 36);
            this.WhiteFirst.TabIndex = 3;
            this.WhiteFirst.TabStop = true;
            this.WhiteFirst.Text = "白棋先";
            this.WhiteFirst.UseVisualStyleBackColor = true;
            // 
            // Start
            // 
            this.Start.Enabled = false;
            this.Start.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Start.Location = new System.Drawing.Point(849, 240);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(145, 48);
            this.Start.TabIndex = 4;
            this.Start.Text = "開始遊戲";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // End
            // 
            this.End.Enabled = false;
            this.End.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.End.Location = new System.Drawing.Point(849, 288);
            this.End.Name = "End";
            this.End.Size = new System.Drawing.Size(145, 48);
            this.End.TabIndex = 5;
            this.End.Text = "結束遊戲";
            this.End.UseVisualStyleBackColor = true;
            this.End.Click += new System.EventHandler(this.End_Click);
            // 
            // Position
            // 
            this.Position.BackColor = System.Drawing.SystemColors.Window;
            this.Position.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Position.Font = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Position.FormattingEnabled = true;
            this.Position.ItemHeight = 20;
            this.Position.Location = new System.Drawing.Point(849, 388);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(145, 240);
            this.Position.TabIndex = 7;
            // 
            // PlayWithPeople
            // 
            this.PlayWithPeople.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PlayWithPeople.Location = new System.Drawing.Point(18, 3);
            this.PlayWithPeople.Name = "PlayWithPeople";
            this.PlayWithPeople.Size = new System.Drawing.Size(125, 29);
            this.PlayWithPeople.TabIndex = 8;
            this.PlayWithPeople.TabStop = true;
            this.PlayWithPeople.Text = "跟人玩";
            this.PlayWithPeople.UseVisualStyleBackColor = true;
            this.PlayWithPeople.CheckedChanged += new System.EventHandler(this.PlayWithPeople_CheckedChanged);
            // 
            // PlayWithComputer
            // 
            this.PlayWithComputer.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PlayWithComputer.Location = new System.Drawing.Point(18, 38);
            this.PlayWithComputer.Name = "PlayWithComputer";
            this.PlayWithComputer.Size = new System.Drawing.Size(144, 29);
            this.PlayWithComputer.TabIndex = 9;
            this.PlayWithComputer.TabStop = true;
            this.PlayWithComputer.Text = "跟電腦玩";
            this.PlayWithComputer.UseVisualStyleBackColor = true;
            this.PlayWithComputer.CheckedChanged += new System.EventHandler(this.PlayWithComputer_CheckedChanged);
            // 
            // PlayWithWhoPanel
            // 
            this.PlayWithWhoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayWithWhoPanel.Controls.Add(this.PlayWithPeople);
            this.PlayWithWhoPanel.Controls.Add(this.PlayWithComputer);
            this.PlayWithWhoPanel.Location = new System.Drawing.Point(849, 12);
            this.PlayWithWhoPanel.Name = "PlayWithWhoPanel";
            this.PlayWithWhoPanel.Size = new System.Drawing.Size(145, 68);
            this.PlayWithWhoPanel.TabIndex = 10;
            // 
            // WhoFirstPanel
            // 
            this.WhoFirstPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WhoFirstPanel.Controls.Add(this.BlackFirst);
            this.WhoFirstPanel.Controls.Add(this.WhiteFirst);
            this.WhoFirstPanel.Location = new System.Drawing.Point(849, 159);
            this.WhoFirstPanel.Name = "WhoFirstPanel";
            this.WhoFirstPanel.Size = new System.Drawing.Size(145, 69);
            this.WhoFirstPanel.TabIndex = 11;
            // 
            // ComputerPanel
            // 
            this.ComputerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ComputerPanel.Controls.Add(this.ComputerFirst);
            this.ComputerPanel.Controls.Add(this.ComputerAfter);
            this.ComputerPanel.Enabled = false;
            this.ComputerPanel.Location = new System.Drawing.Point(849, 86);
            this.ComputerPanel.Name = "ComputerPanel";
            this.ComputerPanel.Size = new System.Drawing.Size(145, 68);
            this.ComputerPanel.TabIndex = 14;
            // 
            // ComputerFirst
            // 
            this.ComputerFirst.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ComputerFirst.Location = new System.Drawing.Point(18, 3);
            this.ComputerFirst.Name = "ComputerFirst";
            this.ComputerFirst.Size = new System.Drawing.Size(125, 29);
            this.ComputerFirst.TabIndex = 8;
            this.ComputerFirst.TabStop = true;
            this.ComputerFirst.Text = "電腦先";
            this.ComputerFirst.UseVisualStyleBackColor = true;
            // 
            // ComputerAfter
            // 
            this.ComputerAfter.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ComputerAfter.Location = new System.Drawing.Point(18, 38);
            this.ComputerAfter.Name = "ComputerAfter";
            this.ComputerAfter.Size = new System.Drawing.Size(144, 29);
            this.ComputerAfter.TabIndex = 9;
            this.ComputerAfter.TabStop = true;
            this.ComputerAfter.Text = "電腦後";
            this.ComputerAfter.UseVisualStyleBackColor = true;
            // 
            // DisplayNumber
            // 
            this.DisplayNumber.AutoSize = true;
            this.DisplayNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DisplayNumber.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DisplayNumber.Font = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DisplayNumber.Location = new System.Drawing.Point(8, 3);
            this.DisplayNumber.Name = "DisplayNumber";
            this.DisplayNumber.Size = new System.Drawing.Size(105, 36);
            this.DisplayNumber.TabIndex = 16;
            this.DisplayNumber.Text = "順序";
            this.DisplayNumber.UseVisualStyleBackColor = true;
            this.DisplayNumber.CheckedChanged += new System.EventHandler(this.DisplayNumber_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DisplayNumber);
            this.panel1.Location = new System.Drawing.Point(913, 346);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(76, 32);
            this.panel1.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::Gomoku.Properties.Resources.board;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(999, 821);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ComputerPanel);
            this.Controls.Add(this.WhoFirstPanel);
            this.Controls.Add(this.PlayWithWhoPanel);
            this.Controls.Add(this.Position);
            this.Controls.Add(this.End);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Previous);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("新細明體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.Previous)).EndInit();
            this.PlayWithWhoPanel.ResumeLayout(false);
            this.WhoFirstPanel.ResumeLayout(false);
            this.ComputerPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Previous;
        private System.Windows.Forms.RadioButton BlackFirst;
        private System.Windows.Forms.RadioButton WhiteFirst;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button End;
        private System.Windows.Forms.RadioButton PlayWithPeople;
        private System.Windows.Forms.RadioButton PlayWithComputer;
        private System.Windows.Forms.Panel PlayWithWhoPanel;
        private System.Windows.Forms.Panel WhoFirstPanel;
        private System.Windows.Forms.ListBox Position;
        private System.Windows.Forms.Panel ComputerPanel;
        private System.Windows.Forms.RadioButton ComputerFirst;
        private System.Windows.Forms.RadioButton ComputerAfter;
        private System.Windows.Forms.CheckBox DisplayNumber;
        private System.Windows.Forms.Panel panel1;
    }
}

