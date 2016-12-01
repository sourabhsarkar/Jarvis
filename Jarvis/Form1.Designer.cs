namespace Jarvis
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
            this.cmnddisp = new System.Windows.Forms.ListBox();
            this.cmndlist = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // cmnddisp
            // 
            this.cmnddisp.BackColor = System.Drawing.Color.DimGray;
            this.cmnddisp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmnddisp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnddisp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.cmnddisp.FormattingEnabled = true;
            this.cmnddisp.ItemHeight = 15;
            this.cmnddisp.Location = new System.Drawing.Point(12, 12);
            this.cmnddisp.Name = "cmnddisp";
            this.cmnddisp.Size = new System.Drawing.Size(269, 240);
            this.cmnddisp.TabIndex = 0;
            // 
            // cmndlist
            // 
            this.cmndlist.BackColor = System.Drawing.Color.DimGray;
            this.cmndlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmndlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmndlist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.cmndlist.FormattingEnabled = true;
            this.cmndlist.ItemHeight = 15;
            this.cmndlist.Location = new System.Drawing.Point(285, 12);
            this.cmndlist.Name = "cmndlist";
            this.cmndlist.Size = new System.Drawing.Size(269, 240);
            this.cmndlist.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 279);
            this.Controls.Add(this.cmndlist);
            this.Controls.Add(this.cmnddisp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox cmndlist;
        private System.Windows.Forms.ListBox cmnddisp;








    }
}

