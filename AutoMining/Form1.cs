using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Tesseract;
using System.Drawing.Imaging;

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
        private TesseractEngine tesseractEngine; // OCR engine

        // Add these new constants
        private const int COLOR_CHECK_INTERVAL_MIN = 1000;
        private const int COLOR_CHECK_INTERVAL_MAX = 5000;
        private const int COLOR_SAMPLES_COUNT = 5;
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
        private const int MenuItemWidth = 609 - 475; // Width of the menu item (537-367)
        private const int MenuItemHeight = 965 - 949; // Height of the menu item (834-816)

        private Color preDockColor;
        private const int DOCK_VERIFICATION_DELAY = 60000; // 60 seconds
        private Point dockCheckPoint;

        public Form1()
        {
            InitializeComponent();

            // Initialize OCR engine
            // Initialize OCR engine
            try
            {
                tesseractEngine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
                // Remove the character whitelist to allow numbers and symbols
                // tesseractEngine.SetVariable("tessedit_char_whitelist", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OCR initialization failed: {ex.Message}");
            }

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

                    // Keep only last 5 samples
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
                // Store dock check coordinates
                dockCheckPoint = new Point(
                    int.Parse(textBoxMiningModuleX.Text),
                    int.Parse(textBoxMiningModuleY.Text)
                );

                // Capture initial color before docking
                preDockColor = GetPixelColor(dockCheckPoint);

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

                // Wait for docking to complete
                UpdateStatus("Waiting for docking verification...");
                await Task.Delay(DOCK_VERIFICATION_DELAY);

                // Verify docking
                Color postDockColor = GetPixelColor(dockCheckPoint);
                if (preDockColor != postDockColor)
                {
                    UpdateStatus("Docking successful - stopping automation");
                    StopAllOperations();
                    ShutdownComputer();
                }
                else
                {
                    UpdateStatus("Docking failed - initiating shutdown");
                    ShutdownComputer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Docking error: {ex.Message}");
                isDocking = false;
                isMiningStopped = false;
                colorSamples.Clear();
            }
        }

        private Color GetPixelColor(Point point)
        {
            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(point, Point.Empty, new Size(1, 1));
                return bmp.GetPixel(0, 0);
            }
        }

        private void StopAllOperations()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)StopAllOperations);
                return;
            }

            isRunning = false;
            isDocking = false;
            buttonStartStopButton.Text = "Start";
            actionTimer.Stop();
            miningCheckTimer.Stop();
            UpdateStatus("Stopped");
        }

        private void ShutdownComputer()
        {
            try
            {
                StopAllOperations();
                UpdateStatus("Shutting down...");

                if (IsUserAdministrator())
                {
                    Process.Start("shutdown", "/s /t 0");
                }
                else
                {
                    MessageBox.Show("Program needs administrator rights to shut down computer");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Shutdown failed: {ex.Message}");
            }
        }

        private bool IsUserAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
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

            // Step 4.5: Check if "Compress" option is visible
            if (!IsCompressOptionVisible(menuItemRect))
            {
                UpdateStatus("Compress option not found - initiating shutdown");

                // If mining has stopped and compress option is missing, shut down
                if (isMiningStopped)
                {
                    ShutdownComputer();
                    return;
                }

                // Otherwise, just move mouse away and skip this cycle
                UpdateStatus("Skipping compress - no ore");
                HumanLikeMouseMove(new Point(random.Next(1100, 1300), random.Next(500, 700)));
                LeftClick();
                return;
            }

            UpdateStatus("Moving to Context Menu");
            var target2 = GetRandomPointInRectangle(menuItemRect);
            HumanLikeMouseMove(target2);
            Thread.Sleep(random.Next(500, 1000));
            LeftClick();
            Thread.Sleep(random.Next(500, 1000));
            HumanLikeMouseMove(new Point(random.Next(1100, 1300), random.Next(500, 700)));
        }

        private bool IsCompressOptionVisible(Rectangle menuRect)
        {
            try
            {
                if (tesseractEngine == null)
                {
                    MessageBox.Show("OCR engine not initialized");
                    return true;
                }

                // Capture a slightly larger area around the button
                var captureRect = new Rectangle(
                    menuRect.X - 10,
                    menuRect.Y - 2,
                    menuRect.Width + 20,
                    menuRect.Height + 4 // Use constant height instead of rect.Height
                );

                // Draw debug rectangle (red color, 3 seconds)
                //DrawDebugRectangle(captureRect, Color.Red, 3000);

                // Capture the menu area
                using (var bmp = new Bitmap(captureRect.Width, captureRect.Height))
                {
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(captureRect.Location, Point.Empty, captureRect.Size);
                    }

                    // Preprocess image for better OCR
                    using (var processed = PreprocessImage(bmp))
                    {
                        // Perform OCR
                        using (var page = tesseractEngine.Process(processed))
                        {
                            string text = page.GetText();
                            Debug.WriteLine($"OCR Output: {text}"); // Add this for debugging

                            // Case-insensitive check for "compress"
                            return text.IndexOf("compress", StringComparison.OrdinalIgnoreCase) >= 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OCR Error: {ex.Message}");
                return true;
            }
        }


        // Add this method to draw a transparent overlay rectangle
        private void DrawDebugRectangle(Rectangle rect, Color color, int durationMillis)
        {
            // Create a transparent form
            var overlay = new Form()
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = Color.Wheat, // Any color that's not the transparency key
                TransparencyKey = Color.Wheat,
                Opacity = 0.7,
                StartPosition = FormStartPosition.Manual,
                Bounds = Screen.PrimaryScreen.Bounds
            };

            // Draw the rectangle on the form
            overlay.Paint += (s, e) =>
            {
                using (var pen = new Pen(color, 2))
                {
                    e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                }

                // Draw label
                using (var font = new Font("Arial", 12))
                using (var brush = new SolidBrush(color))
                {
                    e.Graphics.DrawString("OCR Area", font, brush, rect.X, rect.Y - 20);
                }
            };

            // Show the overlay
            overlay.Show();

            // Auto-close after duration
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = durationMillis;
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                overlay.Close();
                overlay.Dispose();
            };
            timer.Start();
        }

        private Pix PreprocessImage(Bitmap original)
        {
            // Convert to grayscale with higher contrast
            using (var highContrast = new Bitmap(original.Width, original.Height))
            {
                for (int x = 0; x < original.Width; x++)
                {
                    for (int y = 0; y < original.Height; y++)
                    {
                        Color pixel = original.GetPixel(x, y);

                        // Simple thresholding for better text recognition
                        int avg = (pixel.R + pixel.G + pixel.B) / 3;
                        int value = avg < 150 ? 0 : 255; // Adjust threshold as needed
                        highContrast.SetPixel(x, y, Color.FromArgb(value, value, value));
                    }
                }

                return BitmapToPix(highContrast);
            }
        }
        // Debugging helper to visualize where the program is looking
        private void DrawDebugRectangle(Rectangle rect)
        {
            try
            {
                using (var bmp = new Bitmap(rect.Width + 40, rect.Height + 40))
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(rect.X - 20, rect.Y - 20, 0, 0, bmp.Size);
                    using (var pen = new Pen(Color.Red, 2))
                    {
                        g.DrawRectangle(pen, 20, 20, rect.Width, rect.Height);
                    }
                    bmp.Save("debug_rectangle.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            catch { /* Ignore errors */ }
        }

        private Bitmap PixToBitmap(Pix pix)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                pix.Save("temp_image.png", Tesseract.ImageFormat.Png); // Save Pix to a temporary file  
                return new Bitmap("temp_image.png"); // Load the temporary file into a Bitmap  
            }
        }

        private Pix BitmapToPix(Bitmap bitmap)
        {
            // Save the bitmap to a memory stream
            using (var memoryStream = new System.IO.MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                return Pix.LoadFromMemory(memoryStream.ToArray());
            }
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

            // Clean up OCR engine
            tesseractEngine?.Dispose();
        }
    }
}
