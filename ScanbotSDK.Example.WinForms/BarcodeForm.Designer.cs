namespace ScanbotSDK.Example.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBox1 = new ListBox();
            panel1 = new Panel();
            cameraPreview = new Common.UI.WinForms.CameraPreview();
            ((System.ComponentModel.ISupportInitialize)cameraPreview).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Left;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(396, 686);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1344, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0, 686);
            panel1.TabIndex = 1;
            // 
            // cameraPreview
            // 
            cameraPreview.CameraSymbolicLink = null;
            cameraPreview.Location = new Point(393, 0);
            cameraPreview.Name = "cameraPreview";
            cameraPreview.Size = new Size(954, 686);
            cameraPreview.TabIndex = 2;
            cameraPreview.TabStop = false;
            cameraPreview.ImageCaptured += cameraPreview1_ImageCaptured;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1344, 686);
            Controls.Add(cameraPreview);
            Controls.Add(panel1);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)cameraPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Panel panel1;
        private Common.UI.WinForms.CameraPreview cameraPreview;
    }
}
