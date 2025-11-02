namespace TetrisGame
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label lblScore;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            bottomPanel = new Panel();
            btnRestart = new Button();
            lblScore = new Label();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // bottomPanel
            // 
            bottomPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bottomPanel.BackColor = Color.LightGray;
            bottomPanel.Controls.Add(lblScore);
            bottomPanel.Controls.Add(btnRestart);
            bottomPanel.Location = new Point(0, 820);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(400, 40);
            bottomPanel.TabIndex = 0;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(308, 7);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(80, 28);
            btnRestart.TabIndex = 0;
            btnRestart.TabStop = false;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // lblScore
            // 
            lblScore.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblScore.ForeColor = Color.Black;
            lblScore.Location = new Point(10, 10);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(200, 20);
            lblScore.TabIndex = 1;
            lblScore.Text = "Score: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 860);
            Controls.Add(bottomPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Tetris Game";
            Load += Form1_Load;
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}