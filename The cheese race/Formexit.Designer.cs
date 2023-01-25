
namespace The_cheese_race
{
    partial class Formexit
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
            this.btnyes = new System.Windows.Forms.Button();
            this.btnno = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Imgno = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Imgyes = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Imgno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Imgyes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnyes
            // 
            this.btnyes.BackColor = System.Drawing.SystemColors.Control;
            this.btnyes.Location = new System.Drawing.Point(12, 318);
            this.btnyes.Name = "btnyes";
            this.btnyes.Size = new System.Drawing.Size(79, 23);
            this.btnyes.TabIndex = 1;
            this.btnyes.Text = "Yes";
            this.btnyes.UseVisualStyleBackColor = false;
            this.btnyes.Click += new System.EventHandler(this.btnyes_Click);
            this.btnyes.MouseLeave += new System.EventHandler(this.btnyes_MouseLeave);
            this.btnyes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnyes_MouseMove);
            // 
            // btnno
            // 
            this.btnno.Location = new System.Drawing.Point(173, 318);
            this.btnno.Name = "btnno";
            this.btnno.Size = new System.Drawing.Size(75, 23);
            this.btnno.TabIndex = 2;
            this.btnno.Text = "No";
            this.btnno.UseVisualStyleBackColor = true;
            this.btnno.Click += new System.EventHandler(this.btnno_Click);
            this.btnno.MouseLeave += new System.EventHandler(this.btnno_MouseLeave);
            this.btnno.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnno_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Are you sure you want to exit ?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Goudy Stout", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 66);
            this.label2.TabIndex = 4;
            this.label2.Text = "?";
            // 
            // Imgno
            // 
            this.Imgno.BackColor = System.Drawing.Color.Transparent;
            this.Imgno.Location = new System.Drawing.Point(193, 281);
            this.Imgno.Name = "Imgno";
            this.Imgno.Size = new System.Drawing.Size(35, 32);
            this.Imgno.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Imgno.TabIndex = 5;
            this.Imgno.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::The_cheese_race.Properties.Resources.souris_exit;
            this.pictureBox1.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(263, 253);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Imgyes
            // 
            this.Imgyes.BackColor = System.Drawing.Color.Transparent;
            this.Imgyes.Location = new System.Drawing.Point(33, 281);
            this.Imgyes.Name = "Imgyes";
            this.Imgyes.Size = new System.Drawing.Size(35, 32);
            this.Imgyes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Imgyes.TabIndex = 6;
            this.Imgyes.TabStop = false;
            // 
            // Formexit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(260, 353);
            this.Controls.Add(this.Imgyes);
            this.Controls.Add(this.Imgno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnno);
            this.Controls.Add(this.btnyes);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Formexit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formexit";
            ((System.ComponentModel.ISupportInitialize)(this.Imgno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Imgyes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnyes;
        private System.Windows.Forms.Button btnno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox Imgno;
        private System.Windows.Forms.PictureBox Imgyes;
    }
}