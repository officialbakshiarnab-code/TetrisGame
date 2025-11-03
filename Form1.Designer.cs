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

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.PictureBox iconRestart;
        private System.Windows.Forms.PictureBox iconPause;
        private System.Windows.Forms.PictureBox iconResume;
        private System.Windows.Forms.PictureBox iconSettings;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.CheckBox chkGhostPiece;

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
            topPanel = new Panel();
            iconRestart = new PictureBox();
            iconPause = new PictureBox();
            iconResume = new PictureBox();
            iconSettings = new PictureBox();
            settingsPanel = new Panel();
            chkGhostPiece = new CheckBox();

            bottomPanel.SuspendLayout();
            SuspendLayout();

            // topPanel
            topPanel.Name = "topPanel";
            topPanel.Dock = DockStyle.Top;
            topPanel.Size = new Size(400, 60);
            topPanel.BackColor = Color.LightGray;
            topPanel.BorderStyle = BorderStyle.FixedSingle;
            topPanel.Controls.Add(iconRestart);
            topPanel.Controls.Add(iconPause);
            topPanel.Controls.Add(iconResume);
            topPanel.Controls.Add(iconSettings);
            topPanel.Controls.Add(settingsPanel);

            // iconRestart
            iconRestart.Size = new Size(32, 32);
            iconRestart.Location = new Point(10, 14);
            iconRestart.SizeMode = PictureBoxSizeMode.Zoom;

            // iconPause
            iconPause.Name = "iconPause";
            iconPause.Size = new Size(32, 32);
            iconPause.Location = new Point(52, 14);
            iconPause.SizeMode = PictureBoxSizeMode.Zoom;
            iconPause.Image = Image.FromFile("C://Users//offic//source//repos//Projects//Games//TetrisGame//ExternalResources//images/pause.png");
            iconPause.Click += IconPause_Click;

            // iconResume
            iconResume.Name = "iconResume";
            iconResume.Size = new Size(32, 32);
            iconResume.Location = new Point(52, 14); // same spot as pause
            iconResume.SizeMode = PictureBoxSizeMode.Zoom;
            iconResume.Image = Image.FromFile("C://Users//offic//source//repos//Projects//Games//TetrisGame//ExternalResources//images/resume.png");
            iconResume.Click += IconResume_Click;
            iconResume.Visible = false; // hidden at start

            // iconSettings
            iconSettings.Size = new Size(32, 32);
            iconSettings.Location = new Point(136, 14);
            iconSettings.SizeMode = PictureBoxSizeMode.Zoom;
            iconSettings.Click += IconSettings_Click;

            // settingsPanel
            settingsPanel.Size = new Size(120, 40);
            settingsPanel.Location = new Point(180, 10);
            settingsPanel.BackColor = Color.WhiteSmoke;
            settingsPanel.Visible = false;
            settingsPanel.Controls.Add(chkGhostPiece);

            // chkGhostPiece
            chkGhostPiece.Text = "Show Ghost";
            chkGhostPiece.Location = new Point(10, 10);
            chkGhostPiece.Checked = true;
            chkGhostPiece.CheckedChanged += ChkGhostPiece_Checked;

            // bottomPanel
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Size = new Size(400, 60);
            bottomPanel.TabIndex = 0;
            bottomPanel.BorderStyle = BorderStyle.FixedSingle;
            bottomPanel.BackColor = Color.LightGray;
            bottomPanel.Controls.Add(lblScore);
            bottomPanel.Controls.Add(lblHighScore);
            bottomPanel.Controls.Add(btnRestart);

            // lblScore
            lblScore.Name = "lblScore";
            lblScore.Location = new Point(10, 8);
            lblScore.Size = new Size(200, 22);
            lblScore.TabIndex = 1;
            lblScore.Text = "Score: 0";
            lblScore.Font = new Font("Arial", 12, FontStyle.Bold);
            lblScore.ForeColor = Color.Black;

            // lblHighScore
            lblHighScore.Name = "lblHighScore";
            lblHighScore.Location = new Point(10, 32);
            lblHighScore.Size = new Size(200, 22);
            lblHighScore.TabIndex = 2;
            lblHighScore.Text = "High Score: 0";
            lblHighScore.Font = new Font("Arial", 12, FontStyle.Bold);
            lblHighScore.ForeColor = Color.Black;

            // btnRestart
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(80, 30);
            btnRestart.TabIndex = 3;
            btnRestart.TabStop = false;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRestart.Location = new Point(bottomPanel.Width - btnRestart.Width - 10, bottomPanel.Height - btnRestart.Height - 10);
            btnRestart.Click += btnRestart_Click;

            // Form1
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 920);
            Controls.Add(topPanel);
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