using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoMining
{
    public partial class Form1 : Form
    {
        private readonly System.Windows.Forms.Timer mouseTrackerTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer actionTimer = new System.Windows.Forms.Timer();
        private bool isRunning = false;
        private Random random = new Random();
        private Point rightClickPosition;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        // Context menu position offsets (modify these according to your context menu)
        private const int MenuXOffset = 407 - 390;  // Difference between right-click X and menu X
        private const int MenuYOffset = 910-850; // Difference between right-click Y and menu Y
        private const int MenuItemWidth = 609-465; // Width of the menu item (537-367)
        private const int MenuItemHeight = 965-946; // Height of the menu item (834-816)

        public Form1()
        {
            InitializeComponent();

            mouseTrackerTimer.Interval = 50;
            mouseTrackerTimer.Tick += MouseTrackerTimer_Tick;
            mouseTrackerTimer.Start();

            actionTimer.Interval = 1000;
            actionTimer.Tick += ActionTimer_Tick;
        }

        private void MouseTrackerTimer_Tick(object sender, EventArgs e)
        {
            var cursorPos = Cursor.Position;
            labelLiveMouseLocation.Invoke((MethodInvoker)(() =>
                labelLiveMouseLocation.Text = $"Mouse Position: {cursorPos.X}, {cursorPos.Y}"));
        }

        private void ButtonStartStopButton_Click(object sender, EventArgs e)
        {
            isRunning = !isRunning;
            buttonStartStopButton.Text = isRunning ? "Stop" : "Start";
            UpdateStatus("Ready");

            if (isRunning)
            {
                // Change to double.TryParse to handle decimal values
                if (double.TryParse(textBoxLoopInterval.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double interval) && interval > 0)
                {
                    actionTimer.Interval = (int)(random.Next((int)interval +1 , (int)(interval * 2) -1 ) * 1000); // Convert seconds to milliseconds
                    actionTimer.Start();
                }
                else
                {
                    MessageBox.Show("Please enter a valid positive number for seconds (decimal values allowed)");
                    isRunning = false;
                    buttonStartStopButton.Text = "Start";
                }
            }
            else
            {
                actionTimer.Stop();
            }
        }

        private void ActionTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                PerformAutomationSteps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                actionTimer.Stop();
                isRunning = false;
                buttonStartStopButton.Text = "Start";
            }
        }

        private void PerformAutomationSteps()
        {
            var box1 = GetCoordinates("Box1");

            // Step 1: Move to Box1 and click
            UpdateStatus("Moving to Box 1");
            var target1 = GetRandomPointInRectangle(box1);
            HumanLikeMouseMove(target1);
            Thread.Sleep(random.Next(500, 1000));
            LeftClick();
            Thread.Sleep(random.Next(500, 1000));

            // Step 2: Ctrl+A
            UpdateStatus("Selecting All");
            SendKeys.SendWait("^a");
            Thread.Sleep(random.Next(500, 1000));

            // Step 3: Right click (store position first)
            UpdateStatus("Right Clicking");
            rightClickPosition = Cursor.Position;
            RightClick();
            Thread.Sleep(random.Next(500, 1000)); // Wait for context menu to appear

            // Step 4: Calculate menu item position based on right-click location
            var menuItemRect = new Rectangle(
                rightClickPosition.X + MenuXOffset,
                rightClickPosition.Y + MenuYOffset,
                MenuItemWidth,
                MenuItemHeight
            );

            UpdateStatus("Moving to Context Menu");
            var target2 = GetRandomPointInRectangle(menuItemRect);
            HumanLikeMouseMove(target2);
            Thread.Sleep(random.Next(500, 1000));
            LeftClick();
            Thread.Sleep(random.Next(500, 1000));
        }

        private Rectangle GetCoordinates(string boxPrefix)
        {
            // Only Box1 needs coordinates now
            return new Rectangle(
                int.Parse(Controls[$"textBox{boxPrefix}TopLeftX"].Text),
                int.Parse(Controls[$"textBox{boxPrefix}TopLeftY"].Text),
                int.Parse(Controls[$"textBox{boxPrefix}BottomRightX"].Text) - int.Parse(Controls[$"textBox{boxPrefix}TopLeftX"].Text),
                int.Parse(Controls[$"textBox{boxPrefix}BottomRightY"].Text) - int.Parse(Controls[$"textBox{boxPrefix}TopLeftY"].Text)
            );
        }

        private Point GetRandomPointInRectangle(Rectangle rect)
        {
            return new Point(
                random.Next(rect.Left, rect.Right),
                random.Next(rect.Top, rect.Bottom)
            );
        }

        private void HumanLikeMouseMove(Point target)
        {
            var start = Cursor.Position;
            var steps = random.Next(20, 40);

            for (int i = 0; i <= steps; i++)
            {
                var x = start.X + (target.X - start.X) * i / steps;
                var y = start.Y + (target.Y - start.Y) * i / steps;
                Cursor.Position = new Point(x, y);
                Thread.Sleep(random.Next(5, 15));
            }
        }

        private void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => UpdateStatus(message)));
                return;
            }
            LabelStatus.Text = $"Status: {message}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mouseTrackerTimer.Stop();
            actionTimer.Stop();
        }
    }
}
