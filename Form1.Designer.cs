namespace stichingFirstOne
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
            this.components = new System.ComponentModel.Container();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.btnChooseImages = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtChosenImages = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnOpenResult = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMessages = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.Location = new System.Drawing.Point(0, 0);
            this.imageBox1.Margin = new System.Windows.Forms.Padding(4);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(861, 512);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // btnChooseImages
            // 
            this.btnChooseImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseImages.AutoSize = true;
            this.btnChooseImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnChooseImages.Location = new System.Drawing.Point(4, 4);
            this.btnChooseImages.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseImages.MinimumSize = new System.Drawing.Size(139, 39);
            this.btnChooseImages.Name = "btnChooseImages";
            this.btnChooseImages.Size = new System.Drawing.Size(164, 39);
            this.btnChooseImages.TabIndex = 3;
            this.btnChooseImages.Text = "Choose Images";
            this.btnChooseImages.UseVisualStyleBackColor = true;
            this.btnChooseImages.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnChooseImages_MouseClick);
            // 
            // txtChosenImages
            // 
            this.txtChosenImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtChosenImages.Location = new System.Drawing.Point(12, 10);
            this.txtChosenImages.Multiline = true;
            this.txtChosenImages.Name = "txtChosenImages";
            this.txtChosenImages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChosenImages.Size = new System.Drawing.Size(656, 332);
            this.txtChosenImages.TabIndex = 4;
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.AutoSize = true;
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnProcess.Location = new System.Drawing.Point(3, 50);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(165, 39);
            this.btnProcess.TabIndex = 5;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnOpenResult
            // 
            this.btnOpenResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenResult.AutoSize = true;
            this.btnOpenResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOpenResult.Location = new System.Drawing.Point(5, 449);
            this.btnOpenResult.Name = "btnOpenResult";
            this.btnOpenResult.Size = new System.Drawing.Size(163, 39);
            this.btnOpenResult.TabIndex = 8;
            this.btnOpenResult.Text = "Open result";
            this.btnOpenResult.UseVisualStyleBackColor = true;
            this.btnOpenResult.Click += new System.EventHandler(this.btnOpenResult_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnChooseImages);
            this.panel1.Controls.Add(this.btnOpenResult);
            this.panel1.Controls.Add(this.btnProcess);
            this.panel1.Location = new System.Drawing.Point(674, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 500);
            this.panel1.TabIndex = 9;
            // 
            // txtMessages
            // 
            this.txtMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMessages.Location = new System.Drawing.Point(12, 348);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.Size = new System.Drawing.Size(656, 152);
            this.txtMessages.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 512);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtChosenImages);
            this.Controls.Add(this.imageBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Image stiching";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button btnChooseImages;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtChosenImages;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnOpenResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMessages;
    }
}

