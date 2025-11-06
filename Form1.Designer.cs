namespace TetrisGame
{
    partial class Form1
    {
        #region Declarations

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Label lblScoreText;
        private System.Windows.Forms.Label lblScoreValue;
        private System.Windows.Forms.Label lblHighScoreText;
        private System.Windows.Forms.Label lblHighScoreValue;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.PictureBox iconRestart;
        private System.Windows.Forms.PictureBox iconPause;
        private System.Windows.Forms.PictureBox iconResume;
        private System.Windows.Forms.PictureBox iconSettings;
        private System.Windows.Forms.FlowLayoutPanel settingsPanel;
        private System.Windows.Forms.CheckBox chkGhostPiece;
        private System.Windows.Forms.CheckBox chkMirrorEffect;

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
            lblScoreText = new Label();
            lblScoreValue = new Label();
            lblHighScoreText = new Label();
            lblHighScoreValue = new Label();
            topPanel = new Panel();
            iconRestart = new PictureBox();
            iconPause = new PictureBox();
            iconResume = new PictureBox();
            iconSettings = new PictureBox();
            settingsPanel = new FlowLayoutPanel();
            chkGhostPiece = new CheckBox();
            chkMirrorEffect = new CheckBox();

            ToolTip tip = new ToolTip();

            bottomPanel.SuspendLayout();
            SuspendLayout();

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
            iconResume.Location = new Point(52, 14); 
            iconResume.SizeMode = PictureBoxSizeMode.Zoom;
            iconResume.Image = Image.FromFile("C://Users//offic//source//repos//Projects//Games//TetrisGame//ExternalResources//images/resume.png");
            iconResume.Click += IconResume_Click;
            iconResume.Visible = false;

            // iconRestart
            iconRestart.Name = "iconRestart";
            iconRestart.Size = new Size(32, 32);
            iconRestart.Location = new Point(10, 14);
            iconRestart.SizeMode = PictureBoxSizeMode.Zoom;
            iconRestart.Image = Image.FromFile("C://Users//offic//source//repos//Projects//Games//TetrisGame//ExternalResources//images/restart.png");
            iconRestart.Click += IconRestart_Click;

            // iconSettings
            iconSettings.Name = "iconSettings";
            iconSettings.Size = new Size(32, 32);
            iconSettings.Location = new Point(94, 14);
            iconSettings.SizeMode = PictureBoxSizeMode.Zoom;
            iconSettings.Image = Image.FromFile("C://Users//offic//source//repos//Projects//Games//TetrisGame//ExternalResources//images/settings.png");
            iconSettings.Click += IconSettings_Click;

            // chkGhostPiece
            chkGhostPiece.Text = "Ghost: OFF";
            chkGhostPiece.Appearance = Appearance.Button;
            chkGhostPiece.FlatStyle = FlatStyle.Flat;
            chkGhostPiece.Size = new Size(100, 30);
            chkGhostPiece.Checked = false;
            chkGhostPiece.Location = new Point(0, 0);
            chkGhostPiece.ForeColor = Color.Red;
            chkGhostPiece.CheckedChanged += ChkGhostPiece_Checked;

            // chkMirrorEffect
            chkMirrorEffect.Text = "Mirror: OFF";
            chkMirrorEffect.Appearance = Appearance.Button;
            chkMirrorEffect.FlatStyle = FlatStyle.Flat;
            chkMirrorEffect.Size = new Size(100, 30);
            chkMirrorEffect.Checked = false;
            chkMirrorEffect.Location = new Point(0, 0);
            chkMirrorEffect.ForeColor = Color.Red;
            chkMirrorEffect.CheckedChanged += ChkMirrorEffect_Checked;

            // settingsPanel
            settingsPanel = new FlowLayoutPanel();
            settingsPanel.Size = new Size(215, 40);
            settingsPanel.Location = new Point(145, 10);
            settingsPanel.BackColor = Color.WhiteSmoke;
            settingsPanel.Visible = false;
            settingsPanel.BorderStyle = BorderStyle.FixedSingle;
            settingsPanel.FlowDirection = FlowDirection.LeftToRight;
            settingsPanel.WrapContents = false;
            // Add to panel
            settingsPanel.Controls.Add(chkGhostPiece);
            settingsPanel.Controls.Add(chkMirrorEffect);

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

            // lblScoreText
            lblScoreText.Text = "Score:";
            lblScoreText.Font = new Font("Arial", 11, FontStyle.Bold);
            lblScoreText.ForeColor = Color.DeepSkyBlue;
            lblScoreText.Location = new Point(10, 18);
            lblScoreText.Size = new Size(75, 22);
            lblScoreText.TextAlign = ContentAlignment.MiddleLeft;

            // lblScoreValue
            lblScoreValue.Text = "0";
            lblScoreValue.Font = new Font("Arial", 10, FontStyle.Bold);
            lblScoreValue.ForeColor = Color.LightSkyBlue;
            lblScoreValue.Location = new Point(80, 18);
            lblScoreValue.Size = new Size(50, 22);
            lblScoreValue.TextAlign = ContentAlignment.MiddleLeft;

            // lblHighScoreText
            lblHighScoreText.Text = "🏆High Score:";
            lblHighScoreText.Font = new Font("Arial", 11, FontStyle.Bold);
            lblHighScoreText.ForeColor = Color.DeepSkyBlue;
            lblHighScoreText.Location = new Point(160, 18);
            lblHighScoreText.Size = new Size(155, 22);
            lblHighScoreText.TextAlign = ContentAlignment.MiddleRight;

            // lblHighScoreValue
            lblHighScoreValue.Text = "0";
            lblHighScoreValue.Font = new Font("Arial", 10, FontStyle.Bold);
            lblHighScoreValue.ForeColor = Color.LightSkyBlue;
            lblHighScoreValue.Location = new Point(300, 18);
            lblHighScoreValue.Size = new Size(60, 22);
            lblHighScoreValue.TextAlign = ContentAlignment.MiddleRight;

            // bottomPanel
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Size = new Size(400, 60);
            bottomPanel.TabIndex = 0;
            bottomPanel.BorderStyle = BorderStyle.FixedSingle;
            bottomPanel.BackColor = Color.LightGray;
            bottomPanel.Controls.Add(lblScoreText);
            bottomPanel.Controls.Add(lblScoreValue);
            bottomPanel.Controls.Add(lblHighScoreText);
            bottomPanel.Controls.Add(lblHighScoreValue);

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

            // ToolTips
            tip.SetToolTip(iconRestart, "Restart");
            tip.SetToolTip(iconPause, "Pause");
            tip.SetToolTip(iconResume, "Resume");
            tip.SetToolTip(iconSettings, "Settings");

            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}