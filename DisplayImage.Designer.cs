namespace stichingFirstOne
{
    partial class DisplayImage
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
            this.pbResult = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
            this.SuspendLayout();
            // 
            // pbResult
            // 
            this.pbResult.Location = new System.Drawing.Point(12, 12);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(852, 792);
            this.pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbResult.TabIndex = 0;
            this.pbResult.TabStop = false;
            // 
            // DisplayImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 816);
            this.Controls.Add(this.pbResult);
            this.Name = "DisplayImage";
            this.Text = "Result";
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbResult;
    }
}