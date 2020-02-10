namespace A_Star_Algorithm
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrASTARAnimate = new System.Windows.Forms.Timer(this.components);
            this.btnAStarSolutionOnly = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trackSize = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSize)).BeginInit();
            this.SuspendLayout();
            // 
            // picCanvas
            // 
            this.picCanvas.Location = new System.Drawing.Point(12, 12);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(703, 579);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvas_Paint);
            this.picCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseDown);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(759, 374);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(274, 41);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "A Star 알고리즘 애니메이션 시작";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrASTARAnimate
            // 
            this.tmrASTARAnimate.Interval = 10;
            this.tmrASTARAnimate.Tick += new System.EventHandler(this.tmrASTARAnimate_Tick);
            // 
            // btnAStarSolutionOnly
            // 
            this.btnAStarSolutionOnly.Location = new System.Drawing.Point(759, 431);
            this.btnAStarSolutionOnly.Name = "btnAStarSolutionOnly";
            this.btnAStarSolutionOnly.Size = new System.Drawing.Size(274, 41);
            this.btnAStarSolutionOnly.TabIndex = 4;
            this.btnAStarSolutionOnly.Text = "A Star 알고리즘 (솔루션만)";
            this.btnAStarSolutionOnly.UseVisualStyleBackColor = true;
            this.btnAStarSolutionOnly.Click += new System.EventHandler(this.btnAStarSolutionOnly_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(792, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "타일 한칸당 크기 :";
            // 
            // trackSize
            // 
            this.trackSize.LargeChange = 10;
            this.trackSize.Location = new System.Drawing.Point(808, 60);
            this.trackSize.Maximum = 50;
            this.trackSize.Minimum = 5;
            this.trackSize.Name = "trackSize";
            this.trackSize.Size = new System.Drawing.Size(274, 42);
            this.trackSize.SmallChange = 5;
            this.trackSize.TabIndex = 6;
            this.trackSize.Value = 10;
            this.trackSize.Scroll += new System.EventHandler(this.trackSize_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(796, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 84);
            this.label2.TabIndex = 7;
            this.label2.Text = "시작 : 파란색\r\n\r\n출구 : 녹색\r\n\r\n탐색 : 하늘색\r\n\r\n최단거리 : 빨간색\r\n";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(759, 230);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(274, 41);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "맵 다시 생성";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(757, 568);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(305, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "지도위에서 마우스 오른쪽 버튼 : 시작 , 왼쪽버튼 : 출구";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 603);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAStarSolutionOnly);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.picCanvas);
            this.Name = "frmMain";
            this.Text = "A Star Algorithm";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrASTARAnimate;
        private System.Windows.Forms.Button btnAStarSolutionOnly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label3;
    }
}