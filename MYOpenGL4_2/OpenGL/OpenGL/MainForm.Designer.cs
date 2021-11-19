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
            this.TopPanel = new System.Windows.Forms.Panel();
            this.BezierButton = new System.Windows.Forms.Button();
            this.paintScelet = new System.Windows.Forms.CheckBox();
            this.ButtonPyramid = new System.Windows.Forms.Button();
            this.ButtonCube = new System.Windows.Forms.Button();
            this.MouseInfo = new System.Windows.Forms.Label();
            this.ButtonLoadObj = new System.Windows.Forms.Button();
            this.ButtonTriangl = new System.Windows.Forms.Button();
            this.TimeInfo = new System.Windows.Forms.Label();
            this.ButtonLines = new System.Windows.Forms.Button();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.BotPanel = new System.Windows.Forms.Panel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.ElipsButton = new System.Windows.Forms.Button();
            this.DetailNum = new System.Windows.Forms.NumericUpDown();
            this.SectorNum = new System.Windows.Forms.NumericUpDown();
            this.TopPanel.SuspendLayout();
            this.BotPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SectorNum)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TopPanel.Controls.Add(this.SectorNum);
            this.TopPanel.Controls.Add(this.DetailNum);
            this.TopPanel.Controls.Add(this.ElipsButton);
            this.TopPanel.Controls.Add(this.BezierButton);
            this.TopPanel.Controls.Add(this.paintScelet);
            this.TopPanel.Controls.Add(this.ButtonPyramid);
            this.TopPanel.Controls.Add(this.ButtonCube);
            this.TopPanel.Controls.Add(this.MouseInfo);
            this.TopPanel.Controls.Add(this.ButtonLoadObj);
            this.TopPanel.Controls.Add(this.ButtonTriangl);
            this.TopPanel.Controls.Add(this.TimeInfo);
            this.TopPanel.Controls.Add(this.ButtonLines);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(800, 49);
            this.TopPanel.TabIndex = 0;
            // 
            // BezierButton
            // 
            this.BezierButton.Location = new System.Drawing.Point(254, 8);
            this.BezierButton.Name = "BezierButton";
            this.BezierButton.Size = new System.Drawing.Size(75, 23);
            this.BezierButton.TabIndex = 8;
            this.BezierButton.Text = "Безье";
            this.BezierButton.UseVisualStyleBackColor = true;
            this.BezierButton.Click += new System.EventHandler(this.BezierButton_Click);
            // 
            // paintScelet
            // 
            this.paintScelet.AutoSize = true;
            this.paintScelet.Location = new System.Drawing.Point(432, 12);
            this.paintScelet.Name = "paintScelet";
            this.paintScelet.Size = new System.Drawing.Size(62, 17);
            this.paintScelet.TabIndex = 7;
            this.paintScelet.Text = "Скелет";
            this.paintScelet.UseVisualStyleBackColor = true;
            this.paintScelet.CheckedChanged += new System.EventHandler(this.paintScelet_CheckedChanged);
            // 
            // ButtonPyramid
            // 
            this.ButtonPyramid.Location = new System.Drawing.Point(92, 8);
            this.ButtonPyramid.Name = "ButtonPyramid";
            this.ButtonPyramid.Size = new System.Drawing.Size(75, 23);
            this.ButtonPyramid.TabIndex = 6;
            this.ButtonPyramid.Text = "Пирамида";
            this.ButtonPyramid.UseVisualStyleBackColor = true;
            this.ButtonPyramid.Click += new System.EventHandler(this.ButtonPyramid_Click);
            // 
            // ButtonCube
            // 
            this.ButtonCube.Location = new System.Drawing.Point(724, -1);
            this.ButtonCube.Name = "ButtonCube";
            this.ButtonCube.Size = new System.Drawing.Size(75, 23);
            this.ButtonCube.TabIndex = 5;
            this.ButtonCube.Text = "Куб";
            this.ButtonCube.UseVisualStyleBackColor = true;
            this.ButtonCube.Visible = false;
            this.ButtonCube.Click += new System.EventHandler(this.ButtonCube_Click);
            // 
            // MouseInfo
            // 
            this.MouseInfo.AutoSize = true;
            this.MouseInfo.Location = new System.Drawing.Point(535, 12);
            this.MouseInfo.Name = "MouseInfo";
            this.MouseInfo.Size = new System.Drawing.Size(57, 13);
            this.MouseInfo.TabIndex = 4;
            this.MouseInfo.Text = "MouseInfo";
            this.MouseInfo.Visible = false;
            // 
            // ButtonLoadObj
            // 
            this.ButtonLoadObj.Location = new System.Drawing.Point(11, 8);
            this.ButtonLoadObj.Name = "ButtonLoadObj";
            this.ButtonLoadObj.Size = new System.Drawing.Size(75, 23);
            this.ButtonLoadObj.TabIndex = 3;
            this.ButtonLoadObj.Text = "Загрузить";
            this.ButtonLoadObj.UseVisualStyleBackColor = true;
            this.ButtonLoadObj.Click += new System.EventHandler(this.ButtonLoadObj_Click);
            // 
            // ButtonTriangl
            // 
            this.ButtonTriangl.Location = new System.Drawing.Point(709, -1);
            this.ButtonTriangl.Name = "ButtonTriangl";
            this.ButtonTriangl.Size = new System.Drawing.Size(90, 23);
            this.ButtonTriangl.TabIndex = 2;
            this.ButtonTriangl.Text = "Треугольники";
            this.ButtonTriangl.UseVisualStyleBackColor = true;
            this.ButtonTriangl.Visible = false;
            this.ButtonTriangl.Click += new System.EventHandler(this.ButtonTriangl_Click);
            // 
            // TimeInfo
            // 
            this.TimeInfo.AutoSize = true;
            this.TimeInfo.Location = new System.Drawing.Point(535, -1);
            this.TimeInfo.Name = "TimeInfo";
            this.TimeInfo.Size = new System.Drawing.Size(48, 13);
            this.TimeInfo.TabIndex = 1;
            this.TimeInfo.Text = "TimeInfo";
            this.TimeInfo.Visible = false;
            // 
            // ButtonLines
            // 
            this.ButtonLines.Location = new System.Drawing.Point(724, 0);
            this.ButtonLines.Name = "ButtonLines";
            this.ButtonLines.Size = new System.Drawing.Size(75, 23);
            this.ButtonLines.TabIndex = 0;
            this.ButtonLines.Text = "Линия";
            this.ButtonLines.UseVisualStyleBackColor = true;
            this.ButtonLines.Visible = false;
            this.ButtonLines.Click += new System.EventHandler(this.ButtonLines_Click);
            // 
            // RightPanel
            // 
            this.RightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(433, 49);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(367, 401);
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
            this.BotPanel.Controls.Add(this.PictureBox);
            this.BotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BotPanel.Location = new System.Drawing.Point(0, 49);
            this.BotPanel.Name = "BotPanel";
            this.BotPanel.Size = new System.Drawing.Size(433, 401);
            this.BotPanel.TabIndex = 1;
            // 
            // PictureBox
            // 
            this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(431, 399);
            this.PictureBox.TabIndex = 1;
            this.PictureBox.TabStop = false;
            this.PictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Paint);
            this.PictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.PictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.PictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            this.PictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseWheel);
            // 
            // ElipsButton
            // 
            this.ElipsButton.Location = new System.Drawing.Point(173, 8);
            this.ElipsButton.Name = "ElipsButton";
            this.ElipsButton.Size = new System.Drawing.Size(75, 23);
            this.ElipsButton.TabIndex = 9;
            this.ElipsButton.Text = "Элипсоид";
            this.ElipsButton.UseVisualStyleBackColor = true;
            this.ElipsButton.Click += new System.EventHandler(this.ElipsButton_Click);
            // 
            // DetailNum
            // 
            this.DetailNum.Location = new System.Drawing.Point(335, 10);
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
            this.DetailNum.Size = new System.Drawing.Size(39, 20);
            this.DetailNum.TabIndex = 10;
            this.DetailNum.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // SectorNum
            // 
            this.SectorNum.Location = new System.Drawing.Point(380, 11);
            this.SectorNum.Name = "SectorNum";
            this.SectorNum.Size = new System.Drawing.Size(46, 20);
            this.SectorNum.TabIndex = 11;
            this.SectorNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BotPanel);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.TopPanel);
            this.Name = "MainForm";
            this.Text = "OpenGL";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.BotPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SectorNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Button ButtonLines;
        private System.Windows.Forms.Button ButtonTriangl;
        private System.Windows.Forms.Button ButtonLoadObj;
        private System.Windows.Forms.Label TimeInfo;
        private System.Windows.Forms.Label MouseInfo;
        private System.Windows.Forms.Button ButtonCube;
        private System.Windows.Forms.Button ButtonPyramid;
        private System.Windows.Forms.CheckBox paintScelet;
        private System.Windows.Forms.Panel BotPanel;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button BezierButton;
        private System.Windows.Forms.Button ElipsButton;
        private System.Windows.Forms.NumericUpDown DetailNum;
        private System.Windows.Forms.NumericUpDown SectorNum;
    }
}

