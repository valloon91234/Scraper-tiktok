namespace Scraper_tiktok
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
            this.numericUpDown_Proxy1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_IDStart = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_IDEnd = new System.Windows.Forms.NumericUpDown();
            this.button_Merge = new System.Windows.Forms.Button();
            this.button_BuildPage = new System.Windows.Forms.Button();
            this.button_BuildList = new System.Windows.Forms.Button();
            this.numericUpDown_Proxy2 = new System.Windows.Forms.NumericUpDown();
            this.button_BuildResult = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Proxy1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_IDStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_IDEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Proxy2)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_Proxy1
            // 
            this.numericUpDown_Proxy1.Location = new System.Drawing.Point(12, 46);
            this.numericUpDown_Proxy1.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDown_Proxy1.Name = "numericUpDown_Proxy1";
            this.numericUpDown_Proxy1.Size = new System.Drawing.Size(62, 27);
            this.numericUpDown_Proxy1.TabIndex = 3;
            // 
            // numericUpDown_IDStart
            // 
            this.numericUpDown_IDStart.Location = new System.Drawing.Point(13, 13);
            this.numericUpDown_IDStart.Name = "numericUpDown_IDStart";
            this.numericUpDown_IDStart.Size = new System.Drawing.Size(61, 27);
            this.numericUpDown_IDStart.TabIndex = 4;
            // 
            // numericUpDown_IDEnd
            // 
            this.numericUpDown_IDEnd.Location = new System.Drawing.Point(80, 12);
            this.numericUpDown_IDEnd.Name = "numericUpDown_IDEnd";
            this.numericUpDown_IDEnd.Size = new System.Drawing.Size(61, 27);
            this.numericUpDown_IDEnd.TabIndex = 5;
            this.numericUpDown_IDEnd.Value = new decimal(new int[] {
            58,
            0,
            0,
            0});
            // 
            // button_Merge
            // 
            this.button_Merge.Location = new System.Drawing.Point(391, 13);
            this.button_Merge.Name = "button_Merge";
            this.button_Merge.Size = new System.Drawing.Size(75, 60);
            this.button_Merge.TabIndex = 6;
            this.button_Merge.Text = "Merge";
            this.button_Merge.UseVisualStyleBackColor = true;
            this.button_Merge.Click += new System.EventHandler(this.button_Merge_Click);
            // 
            // button_BuildPage
            // 
            this.button_BuildPage.Location = new System.Drawing.Point(147, 13);
            this.button_BuildPage.Name = "button_BuildPage";
            this.button_BuildPage.Size = new System.Drawing.Size(75, 60);
            this.button_BuildPage.TabIndex = 7;
            this.button_BuildPage.Text = "Build Page";
            this.button_BuildPage.UseVisualStyleBackColor = true;
            this.button_BuildPage.Click += new System.EventHandler(this.button_BuildPage_Click);
            // 
            // button_BuildList
            // 
            this.button_BuildList.Location = new System.Drawing.Point(229, 13);
            this.button_BuildList.Name = "button_BuildList";
            this.button_BuildList.Size = new System.Drawing.Size(75, 60);
            this.button_BuildList.TabIndex = 8;
            this.button_BuildList.Text = "Build List";
            this.button_BuildList.UseVisualStyleBackColor = true;
            this.button_BuildList.Click += new System.EventHandler(this.button_BuildList_Click);
            // 
            // numericUpDown_Proxy2
            // 
            this.numericUpDown_Proxy2.Location = new System.Drawing.Point(79, 47);
            this.numericUpDown_Proxy2.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDown_Proxy2.Name = "numericUpDown_Proxy2";
            this.numericUpDown_Proxy2.Size = new System.Drawing.Size(62, 27);
            this.numericUpDown_Proxy2.TabIndex = 9;
            // 
            // button_BuildResult
            // 
            this.button_BuildResult.Location = new System.Drawing.Point(310, 13);
            this.button_BuildResult.Name = "button_BuildResult";
            this.button_BuildResult.Size = new System.Drawing.Size(75, 60);
            this.button_BuildResult.TabIndex = 10;
            this.button_BuildResult.Text = "Build Result";
            this.button_BuildResult.UseVisualStyleBackColor = true;
            this.button_BuildResult.Click += new System.EventHandler(this.button_BuildResult_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 81);
            this.Controls.Add(this.button_BuildResult);
            this.Controls.Add(this.numericUpDown_Proxy2);
            this.Controls.Add(this.button_BuildList);
            this.Controls.Add(this.button_BuildPage);
            this.Controls.Add(this.button_Merge);
            this.Controls.Add(this.numericUpDown_IDEnd);
            this.Controls.Add(this.numericUpDown_IDStart);
            this.Controls.Add(this.numericUpDown_Proxy1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "tiktok.com";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Proxy1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_IDStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_IDEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Proxy2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numericUpDown_Proxy1;
        private System.Windows.Forms.NumericUpDown numericUpDown_IDStart;
        private System.Windows.Forms.NumericUpDown numericUpDown_IDEnd;
        private System.Windows.Forms.Button button_Merge;
        private System.Windows.Forms.Button button_BuildPage;
        private System.Windows.Forms.Button button_BuildList;
        private System.Windows.Forms.NumericUpDown numericUpDown_Proxy2;
        private System.Windows.Forms.Button button_BuildResult;
    }
}

