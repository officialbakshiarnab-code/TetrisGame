namespace TetrisGame
{
    partial class Form1
    {
        #region Declarations

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblHighScore;

        #endregion

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
            components = new System.ComponentModel.Container();
            bottomPanel = new Panel();
            btnRestart = new Button();
            lblScore = new Label();
            lblHighScore = new Label();

            bottomPanel.SuspendLayout();
            SuspendLayout();

            //
            // bottomPanel
            //
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Size = new Size(400, 60); // Reduced height for tighter layout
            bottomPanel.TabIndex = 0;
            bottomPanel.BackColor = Color.LightGray;
            bottomPanel.Controls.Add(lblScore);
            bottomPanel.Controls.Add(lblHighScore);
            bottomPanel.Controls.Add(btnRestart);

            //
            // lblScore
            //
            lblScore.Name = "lblScore";
            lblScore.Location = new Point(10, 8);
            lblScore.Size = new Size(200, 22);
            lblScore.TabIndex = 1;
            lblScore.Text = "Score: 0";
            lblScore.Font = new Font("Arial", 12, FontStyle.Bold);
            lblScore.ForeColor = Color.Black;

            //
            // lblHighScore
            //
            lblHighScore.Name = "lblHighScore";
            lblHighScore.Location = new Point(10, 32); // Slightly lower to avoid overlap
            lblHighScore.Size = new Size(200, 22);
            lblHighScore.TabIndex = 2;
            lblHighScore.Text = "High Score: 0";
            lblHighScore.Font = new Font("Arial", 12, FontStyle.Bold);
            lblHighScore.ForeColor = Color.Black;

            //
            // btnRestart
            //
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(80, 30);
            btnRestart.TabIndex = 3;
            btnRestart.TabStop = false;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRestart.Location = new Point(bottomPanel.Width - btnRestart.Width - 10, bottomPanel.Height - btnRestart.Height - 10);
            btnRestart.Click += btnRestart_Click;

            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 920); // Reduced height to match tighter layout
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