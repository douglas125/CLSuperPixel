namespace CLSuperPixel
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picClustered = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.batchShrink = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.chkDrawUnique = new System.Windows.Forms.CheckBox();
            this.chkDrawMeanDist = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClustered)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.picClustered);
            this.panel1.Location = new System.Drawing.Point(12, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 460);
            this.panel1.TabIndex = 1;
            // 
            // picClustered
            // 
            this.picClustered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picClustered.Location = new System.Drawing.Point(15, 15);
            this.picClustered.Name = "picClustered";
            this.picClustered.Size = new System.Drawing.Size(661, 370);
            this.picClustered.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picClustered.TabIndex = 0;
            this.picClustered.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start Cam";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(622, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // batchShrink
            // 
            this.batchShrink.Location = new System.Drawing.Point(689, 4);
            this.batchShrink.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.batchShrink.Name = "batchShrink";
            this.batchShrink.Size = new System.Drawing.Size(169, 25);
            this.batchShrink.TabIndex = 4;
            this.batchShrink.Text = "Batch shrink images";
            this.batchShrink.UseVisualStyleBackColor = true;
            this.batchShrink.Visible = false;
            this.batchShrink.Click += new System.EventHandler(this.batchShrink_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(118, 0);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(143, 32);
            this.btnOpenFile.TabIndex = 1;
            this.btnOpenFile.Text = "&Open file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // chkDrawUnique
            // 
            this.chkDrawUnique.AutoSize = true;
            this.chkDrawUnique.Checked = true;
            this.chkDrawUnique.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawUnique.Location = new System.Drawing.Point(267, 4);
            this.chkDrawUnique.Name = "chkDrawUnique";
            this.chkDrawUnique.Size = new System.Drawing.Size(123, 17);
            this.chkDrawUnique.TabIndex = 5;
            this.chkDrawUnique.Text = "Draw unique colors?";
            this.chkDrawUnique.UseVisualStyleBackColor = true;
            // 
            // chkDrawMeanDist
            // 
            this.chkDrawMeanDist.AutoSize = true;
            this.chkDrawMeanDist.Location = new System.Drawing.Point(396, 4);
            this.chkDrawMeanDist.Name = "chkDrawMeanDist";
            this.chkDrawMeanDist.Size = new System.Drawing.Size(161, 17);
            this.chkDrawMeanDist.TabIndex = 5;
            this.chkDrawMeanDist.Text = "Draw mean region distance?";
            this.chkDrawMeanDist.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 495);
            this.Controls.Add(this.chkDrawMeanDist);
            this.Controls.Add(this.chkDrawUnique);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.batchShrink);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "CLSuperPixel - Robust Outdoor Optical Marker Recognition";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClustered)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picClustered;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button batchShrink;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.CheckBox chkDrawUnique;
        private System.Windows.Forms.CheckBox chkDrawMeanDist;
    }
}

