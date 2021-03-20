
namespace RamboFirstConfig
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
            this.bt_save = new System.Windows.Forms.Button();
            this.ch_fps = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nm_volume = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nm_fps = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nm_volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nm_fps)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(294, 3);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(128, 24);
            this.bt_save.TabIndex = 0;
            this.bt_save.Text = "Save";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // ch_fps
            // 
            this.ch_fps.AutoSize = true;
            this.ch_fps.Location = new System.Drawing.Point(138, 36);
            this.ch_fps.Name = "ch_fps";
            this.ch_fps.Size = new System.Drawing.Size(15, 14);
            this.ch_fps.TabIndex = 1;
            this.ch_fps.UseVisualStyleBackColor = true;
            this.ch_fps.CheckedChanged += new System.EventHandler(this.ch_fps_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Volume";
            // 
            // nm_volume
            // 
            this.nm_volume.Location = new System.Drawing.Point(76, 7);
            this.nm_volume.Name = "nm_volume";
            this.nm_volume.Size = new System.Drawing.Size(56, 20);
            this.nm_volume.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "FPS limit";
            // 
            // nm_fps
            // 
            this.nm_fps.Location = new System.Drawing.Point(76, 35);
            this.nm_fps.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nm_fps.Name = "nm_fps";
            this.nm_fps.Size = new System.Drawing.Size(56, 20);
            this.nm_fps.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 71);
            this.Controls.Add(this.nm_fps);
            this.Controls.Add(this.nm_volume);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ch_fps);
            this.Controls.Add(this.bt_save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rambo First Blood Part 2 Remake Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.nm_volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nm_fps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.CheckBox ch_fps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nm_volume;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nm_fps;
    }
}

