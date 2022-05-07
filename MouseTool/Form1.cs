using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseTool
{
    public partial class MouseTool : Form
    {
        // Toggle image without losing track of element
        bool IsImageActive = true;

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
            MainEventTimer.Interval = 10; // Updates every 10ms (100 FPS)
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

                //if (!ToggleImage.Checked)
                if (IsImageActive)
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
                    Color FocusColor;
                    Bitmap CroppedSection = StaticFunctions.CropRectangleFromImage(FullScreenshot, ImageSpace, out FocusColor);

                    UpdateImage(CroppedSection);
                    UpdateColor(FocusColor.R, FocusColor.G, FocusColor.B);

                    /* Not explicitly calling GC.collect() => Spikes from 200MB to 1.2GB of RAM
                     * Explicitly calling GC.collect() => Consistent ~24MB RAM usage
                     * + Explicitly disposing images here => Consistent ~16MB RAM usage
                     * Will explore more things to optimize, if I find them
                     */
                    FullScreenshot.Dispose();
                    CroppedSection.Dispose();
                    
                }
                UpdateText($"X:{MousePosition.X}", $"Y:{MousePosition.Y}");

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

        private void UpdateColor(int red, int green, int blue)
        {
            RedComponent.Text = $"R: {red}";
            GreenComponent.Text = $"G: {green}";
            BlueComponent.Text = $"B: {blue}";
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
                //ScreenSection.Visible = false;
                IsImageActive = false;
                ScreenSection.Image = null;
            }
            else
            {
                //ScreenSection.Visible = true;
                IsImageActive = true;
            }
        }
    }
    static class StaticFunctions
    {
        // From here: https://stackoverflow.com/a/7939908/16110236
        public static Bitmap CropRectangleFromImage(this Bitmap b, Rectangle r, out Color MiddlePixelColor)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(b, -r.X, -r.Y);
                Color red = Color.Red;
                int MidX = (int)Math.Floor((double)r.Width / 2);
                int MidY = (int)Math.Floor((double)r.Height / 2);

                MiddlePixelColor = nb.GetPixel(MidX, MidY);

                //nb.SetPixel((int)Math.Floor((double)r.Width / 2),(int)Math.Floor((double)r.Height/2),red);
                nb.SetPixel(MidX, MidY, red);
                return nb;
            }
        }
    }
}