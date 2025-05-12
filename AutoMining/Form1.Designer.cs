namespace AutoMining
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
            labelLiveMouseLocation = new Label();
            buttonStartStopButton = new Button();
            textBoxBox1TopLeftX = new TextBox();
            labelBox1CornerLocations = new Label();
            labelBox1TopLeftX = new Label();
            labelBox1TopLeftY = new Label();
            textBoxBox1TopLeftY = new TextBox();
            textBoxBox1BottomRightY = new TextBox();
            labelBox1BottomRightY = new Label();
            labelBox1BottomRightX = new Label();
            textBoxBox1BottomRightX = new TextBox();
            labelLoopInterval = new Label();
            textBoxLoopInterval = new TextBox();
            labelLiveMouseLocationLabel = new Label();
            labelStatusLabel = new Label();
            LabelStatus = new Label();
            SuspendLayout();
            // 
            // labelLiveMouseLocation
            // 
            labelLiveMouseLocation.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelLiveMouseLocation.AutoSize = true;
            labelLiveMouseLocation.Location = new Point(143, 137);
            labelLiveMouseLocation.Name = "labelLiveMouseLocation";
            labelLiveMouseLocation.Size = new Size(24, 15);
            labelLiveMouseLocation.TabIndex = 0;
            labelLiveMouseLocation.Text = "X Y";
            // 
            // buttonStartStopButton
            // 
            buttonStartStopButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStartStopButton.Location = new Point(406, 126);
            buttonStartStopButton.Name = "buttonStartStopButton";
            buttonStartStopButton.Size = new Size(75, 23);
            buttonStartStopButton.TabIndex = 1;
            buttonStartStopButton.Text = "Start";
            buttonStartStopButton.UseVisualStyleBackColor = true;
            buttonStartStopButton.Click += ButtonStartStopButton_Click;
            // 
            // textBoxBox1TopLeftX
            // 
            textBoxBox1TopLeftX.Location = new Point(115, 27);
            textBoxBox1TopLeftX.Name = "textBoxBox1TopLeftX";
            textBoxBox1TopLeftX.Size = new Size(100, 23);
            textBoxBox1TopLeftX.TabIndex = 2;
            textBoxBox1TopLeftX.Text = "220";
            // 
            // labelBox1CornerLocations
            // 
            labelBox1CornerLocations.AutoSize = true;
            labelBox1CornerLocations.Location = new Point(12, 9);
            labelBox1CornerLocations.Name = "labelBox1CornerLocations";
            labelBox1CornerLocations.Size = new Size(133, 15);
            labelBox1CornerLocations.TabIndex = 3;
            labelBox1CornerLocations.Text = "Box 1 corner locations : ";
            // 
            // labelBox1TopLeftX
            // 
            labelBox1TopLeftX.AutoSize = true;
            labelBox1TopLeftX.Location = new Point(12, 30);
            labelBox1TopLeftX.Name = "labelBox1TopLeftX";
            labelBox1TopLeftX.Size = new Size(97, 15);
            labelBox1TopLeftX.TabIndex = 4;
            labelBox1TopLeftX.Text = "Box 1 Top Left X :";
            // 
            // labelBox1TopLeftY
            // 
            labelBox1TopLeftY.AutoSize = true;
            labelBox1TopLeftY.Location = new Point(221, 30);
            labelBox1TopLeftY.Name = "labelBox1TopLeftY";
            labelBox1TopLeftY.Size = new Size(97, 15);
            labelBox1TopLeftY.TabIndex = 5;
            labelBox1TopLeftY.Text = "Box 1 Top Left Y :";
            // 
            // textBoxBox1TopLeftY
            // 
            textBoxBox1TopLeftY.Location = new Point(324, 27);
            textBoxBox1TopLeftY.Name = "textBoxBox1TopLeftY";
            textBoxBox1TopLeftY.Size = new Size(100, 23);
            textBoxBox1TopLeftY.TabIndex = 7;
            textBoxBox1TopLeftY.Text = "840";
            // 
            // textBoxBox1BottomRightY
            // 
            textBoxBox1BottomRightY.Location = new Point(388, 56);
            textBoxBox1BottomRightY.Name = "textBoxBox1BottomRightY";
            textBoxBox1BottomRightY.Size = new Size(100, 23);
            textBoxBox1BottomRightY.TabIndex = 16;
            textBoxBox1BottomRightY.Text = "861";
            // 
            // labelBox1BottomRightY
            // 
            labelBox1BottomRightY.AutoSize = true;
            labelBox1BottomRightY.Location = new Point(253, 59);
            labelBox1BottomRightY.Name = "labelBox1BottomRightY";
            labelBox1BottomRightY.Size = new Size(129, 15);
            labelBox1BottomRightY.TabIndex = 15;
            labelBox1BottomRightY.Text = "Box 1 Bottom Right Y : ";
            // 
            // labelBox1BottomRightX
            // 
            labelBox1BottomRightX.AutoSize = true;
            labelBox1BottomRightX.Location = new Point(12, 59);
            labelBox1BottomRightX.Name = "labelBox1BottomRightX";
            labelBox1BottomRightX.Size = new Size(129, 15);
            labelBox1BottomRightX.TabIndex = 14;
            labelBox1BottomRightX.Text = "Box 1 Bottom Right X : ";
            // 
            // textBoxBox1BottomRightX
            // 
            textBoxBox1BottomRightX.Location = new Point(147, 56);
            textBoxBox1BottomRightX.Name = "textBoxBox1BottomRightX";
            textBoxBox1BottomRightX.Size = new Size(100, 23);
            textBoxBox1BottomRightX.TabIndex = 13;
            textBoxBox1BottomRightX.Text = "665";
            // 
            // labelLoopInterval
            // 
            labelLoopInterval.AutoSize = true;
            labelLoopInterval.Location = new Point(12, 88);
            labelLoopInterval.Name = "labelLoopInterval";
            labelLoopInterval.Size = new Size(85, 15);
            labelLoopInterval.TabIndex = 21;
            labelLoopInterval.Text = "Loop Interval : ";
            // 
            // textBoxLoopInterval
            // 
            textBoxLoopInterval.Location = new Point(103, 85);
            textBoxLoopInterval.Name = "textBoxLoopInterval";
            textBoxLoopInterval.Size = new Size(100, 23);
            textBoxLoopInterval.TabIndex = 22;
            textBoxLoopInterval.Text = "96,3";
            // 
            // labelLiveMouseLocationLabel
            // 
            labelLiveMouseLocationLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelLiveMouseLocationLabel.AutoSize = true;
            labelLiveMouseLocationLabel.Location = new Point(12, 137);
            labelLiveMouseLocationLabel.Name = "labelLiveMouseLocationLabel";
            labelLiveMouseLocationLabel.Size = new Size(125, 15);
            labelLiveMouseLocationLabel.TabIndex = 23;
            labelLiveMouseLocationLabel.Text = "Live Mouse Location : ";
            // 
            // labelStatusLabel
            // 
            labelStatusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelStatusLabel.AutoSize = true;
            labelStatusLabel.Location = new Point(12, 122);
            labelStatusLabel.Name = "labelStatusLabel";
            labelStatusLabel.Size = new Size(48, 15);
            labelStatusLabel.TabIndex = 24;
            labelStatusLabel.Text = "Status : ";
            // 
            // LabelStatus
            // 
            LabelStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LabelStatus.AutoSize = true;
            LabelStatus.Location = new Point(66, 122);
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new Size(38, 15);
            LabelStatus.TabIndex = 25;
            LabelStatus.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(493, 161);
            Controls.Add(LabelStatus);
            Controls.Add(labelStatusLabel);
            Controls.Add(labelLiveMouseLocationLabel);
            Controls.Add(textBoxLoopInterval);
            Controls.Add(labelLoopInterval);
            Controls.Add(textBoxBox1BottomRightY);
            Controls.Add(labelBox1BottomRightY);
            Controls.Add(labelBox1BottomRightX);
            Controls.Add(textBoxBox1BottomRightX);
            Controls.Add(textBoxBox1TopLeftY);
            Controls.Add(labelBox1TopLeftY);
            Controls.Add(labelBox1TopLeftX);
            Controls.Add(labelBox1CornerLocations);
            Controls.Add(textBoxBox1TopLeftX);
            Controls.Add(buttonStartStopButton);
            Controls.Add(labelLiveMouseLocation);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelLiveMouseLocation;
        private Button buttonStartStopButton;
        private TextBox textBoxBox1TopLeftX;
        private Label labelBox1CornerLocations;
        private Label labelBox1TopLeftX;
        private Label labelBox1TopLeftY;
        private TextBox textBoxBox1TopLeftY;
        private TextBox textBoxBox1BottomRightY;
        private Label labelBox1BottomRightY;
        private Label labelBox1BottomRightX;
        private TextBox textBoxBox1BottomRightX;
        private Label labelLoopInterval;
        private TextBox textBoxLoopInterval;
        private Label labelLiveMouseLocationLabel;
        private Label labelStatusLabel;
        private Label LabelStatus;
    }
}
