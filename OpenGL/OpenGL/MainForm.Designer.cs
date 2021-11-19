namespace OpenGL
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SectorNum = new System.Windows.Forms.NumericUpDown();
            this.DetailNum = new System.Windows.Forms.NumericUpDown();
            this.ElipsButton = new System.Windows.Forms.Button();
            this.paintScelet = new System.Windows.Forms.CheckBox();
            this.MouseInfo = new System.Windows.Forms.Label();
            this.TimeInfo = new System.Windows.Forms.Label();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.BotPanel = new System.Windows.Forms.Panel();
            this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timerOfAnima = new System.Windows.Forms.Timer(this.components);
            this.timerOfMove = new System.Windows.Forms.Timer(this.components);
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectorNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailNum)).BeginInit();
            this.BotPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TopPanel.Controls.Add(this.button2);
            this.TopPanel.Controls.Add(this.button1);
            this.TopPanel.Controls.Add(this.SectorNum);
            this.TopPanel.Controls.Add(this.DetailNum);
            this.TopPanel.Controls.Add(this.ElipsButton);
            this.TopPanel.Controls.Add(this.paintScelet);
            this.TopPanel.Controls.Add(this.MouseInfo);
            this.TopPanel.Controls.Add(this.TimeInfo);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1200, 74);
            this.TopPanel.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(565, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 36);
            this.button2.TabIndex = 13;
            this.button2.Text = "Анимация движения";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(376, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 35);
            this.button1.TabIndex = 12;
            this.button1.Text = "Анимация вращения";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SectorNum
            // 
            this.SectorNum.Location = new System.Drawing.Point(200, 17);
            this.SectorNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SectorNum.Name = "SectorNum";
            this.SectorNum.Size = new System.Drawing.Size(69, 26);
            this.SectorNum.TabIndex = 11;
            this.SectorNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // DetailNum
            // 
            this.DetailNum.Location = new System.Drawing.Point(132, 17);
            this.DetailNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DetailNum.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.DetailNum.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.DetailNum.Name = "DetailNum";
            this.DetailNum.Size = new System.Drawing.Size(58, 26);
            this.DetailNum.TabIndex = 10;
            this.DetailNum.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // ElipsButton
            // 
            this.ElipsButton.Location = new System.Drawing.Point(12, 13);
            this.ElipsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ElipsButton.Name = "ElipsButton";
            this.ElipsButton.Size = new System.Drawing.Size(112, 35);
            this.ElipsButton.TabIndex = 9;
            this.ElipsButton.Text = "Эллипсоид";
            this.ElipsButton.UseVisualStyleBackColor = true;
            this.ElipsButton.Click += new System.EventHandler(this.ElipsButton_Click);
            // 
            // paintScelet
            // 
            this.paintScelet.AutoSize = true;
            this.paintScelet.Location = new System.Drawing.Point(278, 19);
            this.paintScelet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.paintScelet.Name = "paintScelet";
            this.paintScelet.Size = new System.Drawing.Size(91, 24);
            this.paintScelet.TabIndex = 7;
            this.paintScelet.Text = "Скелет";
            this.paintScelet.UseVisualStyleBackColor = true;
            this.paintScelet.CheckedChanged += new System.EventHandler(this.paintScelet_CheckedChanged);
            // 
            // MouseInfo
            // 
            this.MouseInfo.AutoSize = true;
            this.MouseInfo.Location = new System.Drawing.Point(802, 18);
            this.MouseInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MouseInfo.Name = "MouseInfo";
            this.MouseInfo.Size = new System.Drawing.Size(85, 20);
            this.MouseInfo.TabIndex = 4;
            this.MouseInfo.Text = "MouseInfo";
            this.MouseInfo.Visible = false;
            // 
            // TimeInfo
            // 
            this.TimeInfo.AutoSize = true;
            this.TimeInfo.Location = new System.Drawing.Point(802, -2);
            this.TimeInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimeInfo.Name = "TimeInfo";
            this.TimeInfo.Size = new System.Drawing.Size(71, 20);
            this.TimeInfo.TabIndex = 1;
            this.TimeInfo.Text = "TimeInfo";
            this.TimeInfo.Visible = false;
            // 
            // RightPanel
            // 
            this.RightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(652, 74);
            this.RightPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(548, 618);
            this.RightPanel.TabIndex = 2;
            this.RightPanel.SizeChanged += new System.EventHandler(this.RightPanel_SizeChanged);
            this.RightPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.RightPanel_Paint);
            this.RightPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightPanel_MouseDown);
            this.RightPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RightPanel_MouseMove);
            this.RightPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightPanel_MouseUp);
            // 
            // BotPanel
            // 
            this.BotPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BotPanel.Controls.Add(this.AnT);
            this.BotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BotPanel.Location = new System.Drawing.Point(0, 74);
            this.BotPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BotPanel.Name = "BotPanel";
            this.BotPanel.Size = new System.Drawing.Size(652, 618);
            this.BotPanel.TabIndex = 1;
            // 
            // AnT
            // 
            this.AnT.AccumBits = ((byte)(0));
            this.AnT.AutoCheckErrors = false;
            this.AnT.AutoFinish = false;
            this.AnT.AutoMakeCurrent = true;
            this.AnT.AutoSwapBuffers = true;
            this.AnT.BackColor = System.Drawing.Color.Black;
            this.AnT.ColorBits = ((byte)(32));
            this.AnT.DepthBits = ((byte)(16));
            this.AnT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnT.Location = new System.Drawing.Point(0, 0);
            this.AnT.Name = "AnT";
            this.AnT.Size = new System.Drawing.Size(650, 616);
            this.AnT.StencilBits = ((byte)(0));
            this.AnT.TabIndex = 0;
            this.AnT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.AnT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.AnT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            this.AnT.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseWheel);
            // 
            // timerOfAnima
            // 
            this.timerOfAnima.Interval = 30;
            this.timerOfAnima.Tick += new System.EventHandler(this.timerOfAnima_Tick);
            // 
            // timerOfMove
            // 
            this.timerOfMove.Interval = 30;
            this.timerOfMove.Tick += new System.EventHandler(this.timerOfMove_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.BotPanel);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.TopPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "OpenGL";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectorNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailNum)).EndInit();
            this.BotPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Label TimeInfo;
        private System.Windows.Forms.Label MouseInfo;
        private System.Windows.Forms.CheckBox paintScelet;
        private System.Windows.Forms.Panel BotPanel;
        private System.Windows.Forms.Button ElipsButton;
        private System.Windows.Forms.NumericUpDown DetailNum;
        private System.Windows.Forms.NumericUpDown SectorNum;
        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timerOfAnima;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timerOfMove;
    }
}

