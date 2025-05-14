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
            labelInventoryBoxCornerLocations = new Label();
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
            labelMiningStatusLabel = new Label();
            labelMiningStatus = new Label();
            label1 = new Label();
            textBoxMiningModuleX = new TextBox();
            textBoxMiningModuleY = new TextBox();
            label2 = new Label();
            textBoxDockButtonTopLeftX = new TextBox();
            textBoxDockButtonTopLeftY = new TextBox();
            textBoxDockButtonBottomRightX = new TextBox();
            textBoxDockButtonBottomRightY = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // labelLiveMouseLocation
            // 
            labelLiveMouseLocation.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelLiveMouseLocation.AutoSize = true;
            labelLiveMouseLocation.Location = new Point(143, 225);
            labelLiveMouseLocation.Name = "labelLiveMouseLocation";
            labelLiveMouseLocation.Size = new Size(24, 15);
            labelLiveMouseLocation.TabIndex = 0;
            labelLiveMouseLocation.Text = "X Y";
            // 
            // buttonStartStopButton
            // 
            buttonStartStopButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStartStopButton.Location = new Point(487, 214);
            buttonStartStopButton.Name = "buttonStartStopButton";
            buttonStartStopButton.Size = new Size(75, 23);
            buttonStartStopButton.TabIndex = 1;
            buttonStartStopButton.Text = "Start";
            buttonStartStopButton.UseVisualStyleBackColor = true;
            buttonStartStopButton.Click += ButtonStartStopButton_Click;
            // 
            // textBoxBox1TopLeftX
            // 
            textBoxBox1TopLeftX.Location = new Point(147, 27);
            textBoxBox1TopLeftX.Name = "textBoxBox1TopLeftX";
            textBoxBox1TopLeftX.Size = new Size(100, 23);
            textBoxBox1TopLeftX.TabIndex = 2;
            textBoxBox1TopLeftX.Text = "220";
            // 
            // labelInventoryBoxCornerLocations
            // 
            labelInventoryBoxCornerLocations.AutoSize = true;
            labelInventoryBoxCornerLocations.Location = new Point(12, 9);
            labelInventoryBoxCornerLocations.Name = "labelInventoryBoxCornerLocations";
            labelInventoryBoxCornerLocations.Size = new Size(177, 15);
            labelInventoryBoxCornerLocations.TabIndex = 3;
            labelInventoryBoxCornerLocations.Text = "Inventory Box corner locations : ";
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
            labelBox1TopLeftY.Location = new Point(253, 30);
            labelBox1TopLeftY.Name = "labelBox1TopLeftY";
            labelBox1TopLeftY.Size = new Size(97, 15);
            labelBox1TopLeftY.TabIndex = 5;
            labelBox1TopLeftY.Text = "Box 1 Top Left Y :";
            // 
            // textBoxBox1TopLeftY
            // 
            textBoxBox1TopLeftY.Location = new Point(388, 27);
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
            textBoxLoopInterval.Text = "96.3";
            // 
            // labelLiveMouseLocationLabel
            // 
            labelLiveMouseLocationLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelLiveMouseLocationLabel.AutoSize = true;
            labelLiveMouseLocationLabel.Location = new Point(12, 225);
            labelLiveMouseLocationLabel.Name = "labelLiveMouseLocationLabel";
            labelLiveMouseLocationLabel.Size = new Size(125, 15);
            labelLiveMouseLocationLabel.TabIndex = 23;
            labelLiveMouseLocationLabel.Text = "Live Mouse Location : ";
            // 
            // labelStatusLabel
            // 
            labelStatusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelStatusLabel.AutoSize = true;
            labelStatusLabel.Location = new Point(12, 210);
            labelStatusLabel.Name = "labelStatusLabel";
            labelStatusLabel.Size = new Size(48, 15);
            labelStatusLabel.TabIndex = 24;
            labelStatusLabel.Text = "Status : ";
            // 
            // LabelStatus
            // 
            LabelStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LabelStatus.AutoSize = true;
            LabelStatus.Location = new Point(66, 210);
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new Size(38, 15);
            LabelStatus.TabIndex = 25;
            LabelStatus.Text = "label1";
            // 
            // labelMiningStatusLabel
            // 
            labelMiningStatusLabel.AutoSize = true;
            labelMiningStatusLabel.Location = new Point(12, 285);
            labelMiningStatusLabel.Name = "labelMiningStatusLabel";
            labelMiningStatusLabel.Size = new Size(89, 15);
            labelMiningStatusLabel.TabIndex = 26;
            labelMiningStatusLabel.Text = "Mining Status : ";
            // 
            // labelMiningStatus
            // 
            labelMiningStatus.AutoSize = true;
            labelMiningStatus.Location = new Point(107, 285);
            labelMiningStatus.Name = "labelMiningStatus";
            labelMiningStatus.Size = new Size(38, 15);
            labelMiningStatus.TabIndex = 27;
            labelMiningStatus.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 117);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 28;
            label1.Text = "Mining Module X : ";
            // 
            // textBoxMiningModuleX
            // 
            textBoxMiningModuleX.Location = new Point(126, 114);
            textBoxMiningModuleX.Name = "textBoxMiningModuleX";
            textBoxMiningModuleX.Size = new Size(100, 23);
            textBoxMiningModuleX.TabIndex = 29;
            textBoxMiningModuleX.Text = "1366";
            // 
            // textBoxMiningModuleY
            // 
            textBoxMiningModuleY.Location = new Point(346, 114);
            textBoxMiningModuleY.Name = "textBoxMiningModuleY";
            textBoxMiningModuleY.Size = new Size(100, 23);
            textBoxMiningModuleY.TabIndex = 30;
            textBoxMiningModuleY.Text = "1265";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(232, 117);
            label2.Name = "label2";
            label2.Size = new Size(108, 15);
            label2.TabIndex = 31;
            label2.Text = "Mining Module Y : ";
            // 
            // textBoxDockButtonTopLeftX
            // 
            textBoxDockButtonTopLeftX.Location = new Point(184, 143);
            textBoxDockButtonTopLeftX.Name = "textBoxDockButtonTopLeftX";
            textBoxDockButtonTopLeftX.Size = new Size(100, 23);
            textBoxDockButtonTopLeftX.TabIndex = 32;
            textBoxDockButtonTopLeftX.Text = "2140";
            // 
            // textBoxDockButtonTopLeftY
            // 
            textBoxDockButtonTopLeftY.Location = new Point(462, 143);
            textBoxDockButtonTopLeftY.Name = "textBoxDockButtonTopLeftY";
            textBoxDockButtonTopLeftY.Size = new Size(100, 23);
            textBoxDockButtonTopLeftY.TabIndex = 33;
            textBoxDockButtonTopLeftY.Text = "140";
            // 
            // textBoxDockButtonBottomRightX
            // 
            textBoxDockButtonBottomRightX.Location = new Point(184, 172);
            textBoxDockButtonBottomRightX.Name = "textBoxDockButtonBottomRightX";
            textBoxDockButtonBottomRightX.Size = new Size(100, 23);
            textBoxDockButtonBottomRightX.TabIndex = 34;
            textBoxDockButtonBottomRightX.Text = "2155";
            // 
            // textBoxDockButtonBottomRightY
            // 
            textBoxDockButtonBottomRightY.Location = new Point(462, 172);
            textBoxDockButtonBottomRightY.Name = "textBoxDockButtonBottomRightY";
            textBoxDockButtonBottomRightY.Size = new Size(100, 23);
            textBoxDockButtonBottomRightY.TabIndex = 35;
            textBoxDockButtonBottomRightY.Text = "155";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 175);
            label3.Name = "label3";
            label3.Size = new Size(166, 15);
            label3.TabIndex = 36;
            label3.Text = "Dock Button Bottom Right X : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(290, 175);
            label4.Name = "label4";
            label4.Size = new Size(166, 15);
            label4.TabIndex = 37;
            label4.Text = "Dock Button Bottom Right Y : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 146);
            label5.Name = "label5";
            label5.Size = new Size(136, 15);
            label5.TabIndex = 38;
            label5.Text = "Dock Button Top Lelt X : ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(290, 146);
            label6.Name = "label6";
            label6.Size = new Size(136, 15);
            label6.TabIndex = 39;
            label6.Text = "Dock Button Top Lelt Y : ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(574, 249);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBoxDockButtonBottomRightY);
            Controls.Add(textBoxDockButtonBottomRightX);
            Controls.Add(textBoxDockButtonTopLeftY);
            Controls.Add(textBoxDockButtonTopLeftX);
            Controls.Add(label2);
            Controls.Add(textBoxMiningModuleY);
            Controls.Add(textBoxMiningModuleX);
            Controls.Add(label1);
            Controls.Add(labelMiningStatus);
            Controls.Add(labelMiningStatusLabel);
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
            Controls.Add(labelInventoryBoxCornerLocations);
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
        private Label labelInventoryBoxCornerLocations;
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
        private Label labelMiningStatusLabel;
        private Label labelMiningStatus;
        private Label label1;
        private TextBox textBoxMiningModuleX;
        private TextBox textBoxMiningModuleY;
        private Label label2;
        private TextBox textBoxDockButtonTopLeftX;
        private TextBox textBoxDockButtonTopLeftY;
        private TextBox textBoxDockButtonBottomRightX;
        private TextBox textBoxDockButtonBottomRightY;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
