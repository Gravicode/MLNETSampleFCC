namespace FCCApp
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
            this.button2 = new System.Windows.Forms.Button();
            this.lblFare = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTripID = new System.Windows.Forms.Label();
            this.lblTrip = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.plot1 = new OxyPlot.WindowsForms.PlotView();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(543, 531);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 13;
            this.button2.Text = "Previous";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblFare
            // 
            this.lblFare.AutoSize = true;
            this.lblFare.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFare.Location = new System.Drawing.Point(305, 9);
            this.lblFare.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFare.Name = "lblFare";
            this.lblFare.Size = new System.Drawing.Size(58, 29);
            this.lblFare.TabIndex = 12;
            this.lblFare.Text = "1.20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 29);
            this.label1.TabIndex = 11;
            this.label1.Text = "Predicted Score";
            // 
            // lblTripID
            // 
            this.lblTripID.AutoSize = true;
            this.lblTripID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTripID.Location = new System.Drawing.Point(84, 9);
            this.lblTripID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTripID.Name = "lblTripID";
            this.lblTripID.Size = new System.Drawing.Size(26, 29);
            this.lblTripID.TabIndex = 10;
            this.lblTripID.Text = "1";
            // 
            // lblTrip
            // 
            this.lblTrip.AutoSize = true;
            this.lblTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrip.Location = new System.Drawing.Point(13, 9);
            this.lblTrip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrip.Name = "lblTrip";
            this.lblTrip.Size = new System.Drawing.Size(81, 29);
            this.lblTrip.TabIndex = 9;
            this.lblTrip.Text = "Data #";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(651, 531);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // plot1
            // 
            this.plot1.Location = new System.Drawing.Point(17, 58);
            this.plot1.Margin = new System.Windows.Forms.Padding(4);
            this.plot1.Name = "plot1";
            this.plot1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot1.Size = new System.Drawing.Size(733, 439);
            this.plot1.TabIndex = 7;
            this.plot1.Text = "plot1";
            this.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 580);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblFare);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTripID);
            this.Controls.Add(this.lblTrip);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.plot1);
            this.Name = "Form1";
            this.Text = "Feature Contribution Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblFare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTripID;
        private System.Windows.Forms.Label lblTrip;
        private System.Windows.Forms.Button button1;
        public OxyPlot.WindowsForms.PlotView plot1;
    }
}

