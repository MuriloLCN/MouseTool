using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseTool
{
    public partial class MouseTool : Form
    {
        // Measures for screen and zoom
        static int MonitorWidth = Screen.PrimaryScreen.Bounds.Width;
        static int MonitorHeight = Screen.PrimaryScreen.Bounds.Height;

        static int OriginalMeasureX = 80;
        static int OriginalMeasureY = 45;

        int SizeX = OriginalMeasureX;
        int SizeY = OriginalMeasureY;

        bool NeedsZoomLevelUpdate = false;

        double defaultSliderValue = 3.0;

        private System.Windows.Forms.Timer MainEventTimer;

        public void InitializeTimer()
        {
            MainEventTimer = new System.Windows.Forms.Timer();
            MainEventTimer.Tick += new EventHandler(TimerTick);
            MainEventTimer.Interval = 7; // Updates every 7ms (~144 FPS)
            MainEventTimer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            try
            {
                // Only updates the values if there has been a change in the slider
                if (NeedsZoomLevelUpdate)
                {
                    double zoom = ZoomLevelBar.Value / defaultSliderValue;

                    SizeX = (int)Math.Floor(zoom * OriginalMeasureX);
                    SizeY = (int)Math.Floor(zoom * OriginalMeasureY);

                    NeedsZoomLevelUpdate = false;
                }

                Point MousePosition = System.Windows.Forms.Cursor.Position;

                if (!ToggleImage.Checked)
                {
                    Point InitialPoint = new Point(MousePosition.X - SizeX, MousePosition.Y - SizeY);
                    Point EndPoint = new Point(MousePosition.X + SizeX, MousePosition.Y + SizeY);

                    // Avoids getting empty pixels from outside the screen space
                    if (InitialPoint.X < 0)
                    {
                        InitialPoint.X = 0;
                    }
                    if (InitialPoint.Y < 0)
                    {
                        InitialPoint.Y = 0;
                    }
                    if (EndPoint.X > MonitorWidth)
                    {  
                        InitialPoint.X = MonitorWidth - SizeX * 2 - 1;
                    }
                    if (EndPoint.Y >= MonitorHeight)
                    {
                        InitialPoint.Y = MonitorHeight - SizeY * 2 - 1;
                    }

                
                    Bitmap FullScreenshot = GetScreenshot();
                    Rectangle ImageSpace = new Rectangle(InitialPoint.X, InitialPoint.Y, 2 * SizeX, 2 * SizeY);
                    Bitmap CroppedSection = StaticFunctions.CropRectangleFromImage(FullScreenshot, ImageSpace);

                    UpdateImage(CroppedSection);
                }
                UpdateText($"X:{MousePosition.X}", $"Y:{MousePosition.Y}");

                /*/ 
                 * The garbage collector is called every-so-often normally, but due to the fast refresh rate,
                 * the memory usage would end up skyrocketing then falling every two or so seconds (always >200MB still).
                 * 
                 * Calling it explicitly makes it stay at <30MB all the time.
                 * 
                 * Using the hide image button lowers memory usage to around 8MB and also lowers substantially the CPU usage.
                /*/

                GC.Collect();
            }
            catch (Exception e)
            {
                UpdateText("An error has occured:", $"{e}");
                GC.Collect();
            }
        }

        private Bitmap GetScreenshot()
        {   
            Bitmap bitmap = new Bitmap(MonitorWidth, MonitorHeight);
            Graphics gra = Graphics.FromImage((Image)bitmap);
            gra.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            return bitmap;
        }

        private void UpdateImage(Image img)
        {
            ScreenSection.Image = img;
            // Screen_Section.Update();
            ScreenSection.Refresh();
        }

        private void UpdateText(string x, string y)
        {
            CoordsX.Text = x;
            CoordsY.Text = y;
        }

        public MouseTool()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void ZoomLevelScroll(object sender, EventArgs e)
        {
            NeedsZoomLevelUpdate = true;
        }

        private void zoom_toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (ToggleImage.Checked)
            {
                ScreenSection.Visible = false;
            }
            else
            {
                ScreenSection.Visible = true;
            }
        }
    }
    static class StaticFunctions
    {
        // From here: https://stackoverflow.com/a/7939908/16110236
        public static Bitmap CropRectangleFromImage(this Bitmap b, Rectangle r)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(b, -r.X, -r.Y);
                Color red = Color.Red;
                nb.SetPixel((int)Math.Floor((double)r.Width / 2),(int)Math.Floor((double)r.Height/2),red);
                return nb;
            }
        }
    }
}