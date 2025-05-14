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
        // Existing members
        private readonly System.Windows.Forms.Timer mouseTrackerTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer actionTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer miningCheckTimer = new System.Windows.Forms.Timer();
        private bool isRunning = false;
        private bool isMiningStopped = false;
        private bool isDocking = false;
        private Random random = new Random();
        private Point rightClickPosition;
        private List<Color> colorSamples = new List<Color>();

        // Add these new constants
        private const int COLOR_CHECK_INTERVAL_MIN = 1000;
        private const int COLOR_CHECK_INTERVAL_MAX = 5000;
        private const int COLOR_SAMPLES_COUNT = 3;
        private const int DOCK_DELAY_MIN = 15000;
        private const int DOCK_DELAY_MAX = 30000;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        // Context menu position offsets (modify these according to your context menu)
        private const int MenuXOffset = 407 - 390;  // Difference between right-click X and menu X
        private const int MenuYOffset = 910 - 850; // Difference between right-click Y and menu Y
        private const int MenuItemWidth = 609 - 465; // Width of the menu item (537-367)
        private const int MenuItemHeight = 965 - 946; // Height of the menu item (834-816)

        private const int COLOR_TOLERANCE = 10;

        public Form1()
        {
            InitializeComponent();

            // Existing initialization
            mouseTrackerTimer.Interval = 50;
            mouseTrackerTimer.Tick += MouseTrackerTimer_Tick;
            mouseTrackerTimer.Start();

            actionTimer.Interval = 1000;
            actionTimer.Tick += ActionTimer_Tick;

            // New mining check timer setup
            miningCheckTimer.Tick += MiningCheckTimer_Tick;
            ResetMiningCheckTimer();
        }

        private void ResetMiningCheckTimer()
        {
            miningCheckTimer.Interval = random.Next(COLOR_CHECK_INTERVAL_MIN, COLOR_CHECK_INTERVAL_MAX);
            miningCheckTimer.Start();
        }

        private void MiningCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!isRunning || isDocking) return;

            try
            {
                // Get mining module coordinates
                var modulePoint = new Point(
                    int.Parse(textBoxMiningModuleX.Text),
                    int.Parse(textBoxMiningModuleY.Text)
                );

                // Capture screen color
                using (var bmp = new Bitmap(1, 1))
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(modulePoint, Point.Empty, new Size(1, 1));
                    var color = bmp.GetPixel(0, 0);
                    colorSamples.Add(color);

                    // Keep only last 3 samples
                    if (colorSamples.Count > COLOR_SAMPLES_COUNT)
                        colorSamples.RemoveAt(0);

                    UpdateMiningStatus(color);
                }

                // Check if mining stopped
                if (colorSamples.Count == COLOR_SAMPLES_COUNT &&
                    colorSamples.TrueForAll(c => c == colorSamples[0]))

                {
                    isMiningStopped = true;
                    UpdateStatus("Mining stopped - initiating docking");
                    actionTimer.Stop();
                    InitiateDockingSequence();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Color check error: {ex.Message}");
            }
            finally
            {
                ResetMiningCheckTimer();
            }
        }

        private void UpdateMiningStatus(Color currentColor)
        {
            labelMiningStatus.Invoke((MethodInvoker)(() =>
                labelMiningStatus.Text = $"Mining Status: {(isMiningStopped ? "STOPPED" : "ACTIVE")}\n" +
                                       $"Last Color: {currentColor.ToString()}"));
        }

        private async void InitiateDockingSequence()
        {
            if (isDocking) return;
            isDocking = true;

            try
            {
                // Stop all timers
                miningCheckTimer.Stop();

                // Press Shift+R
                SendKeys.SendWait("+R");

                // Wait random delay
                int delay = random.Next(DOCK_DELAY_MIN, DOCK_DELAY_MAX);
                UpdateStatus($"Waiting {delay / 1000} seconds before docking...");
                await Task.Delay(delay);

                // Click dock button
                var dockBox = new Rectangle(
                    int.Parse(textBoxDockButtonTopLeftX.Text),
                    int.Parse(textBoxDockButtonTopLeftY.Text),
                    int.Parse(textBoxDockButtonBottomRightX.Text) - int.Parse(textBoxDockButtonTopLeftX.Text),
                    int.Parse(textBoxDockButtonBottomRightY.Text) - int.Parse(textBoxDockButtonTopLeftY.Text)
                );

                var target = GetRandomPointInRectangle(dockBox);
                HumanLikeMouseMove(target);
                LeftClick();
                UpdateStatus("Docking initiated");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Docking error: {ex.Message}");
            }
            finally
            {
                isDocking = false;
                isMiningStopped = false;
                colorSamples.Clear();

                if (isRunning)
                {
                    actionTimer.Start();
                    ResetMiningCheckTimer();
                }
            }
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
                colorSamples.Clear();
                isMiningStopped = false;

                if (double.TryParse(textBoxLoopInterval.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double interval) && interval > 0)
                {
                    actionTimer.Interval = (int)(interval * 1000);
                    actionTimer.Start();
                    miningCheckTimer.Start();
                    PerformAutomationSteps(); // Immediate first run
                }
                else
                {
                    MessageBox.Show("Please enter a valid positive number for seconds");
                    isRunning = false;
                    buttonStartStopButton.Text = "Start";
                }
            }
            else
            {
                actionTimer.Stop();
                miningCheckTimer.Stop();
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
            var steps = random.Next(30, 60); // More steps for smoother curves
            var baseOffset = random.Next(15, 30); // Base curve amount

            // Create control points for Bézier curve
            var control1 = new Point(
                start.X + random.Next(-baseOffset, baseOffset),
                start.Y + random.Next(-baseOffset, baseOffset)
            );

            var control2 = new Point(
                target.X + random.Next(-baseOffset, baseOffset),
                target.Y + random.Next(-baseOffset, baseOffset)
            );

            for (int i = 0; i <= steps; i++)
            {
                double t = (double)i / steps;

                // Cubic Bézier curve calculation
                double u = 1 - t;
                double tt = t * t;
                double uu = u * u;
                double uuu = uu * u;
                double ttt = tt * t;

                // Add some randomness to the final position
                var finalTarget = new Point(
                    target.X + random.Next(-3, 3),
                    target.Y + random.Next(-3, 3)
                );

                Point point = new Point(
                    (int)(uuu * start.X + 3 * uu * t * control1.X + 3 * u * tt * control2.X + ttt * finalTarget.X),
                    (int)(uuu * start.Y + 3 * uu * t * control1.Y + 3 * u * tt * control2.Y + ttt * finalTarget.Y)
                );

                // Add small random delays and micro-movements
                Cursor.Position = point;
                Thread.Sleep(random.Next(8, 20)); // Vary speed during movement

                // Random tiny pauses (human-like hesitation)
                if (random.Next(100) < 5) // 5% chance of micro-pause
                    Thread.Sleep(random.Next(30, 80));
            }

            // Final adjustment to ensure we reach exact target
            Cursor.Position = target;
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
            LabelStatus.Text = $"{message}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mouseTrackerTimer.Stop();
            actionTimer.Stop();
        }
    }
}
