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

        // Current slider value divided by default slider value is the value used to resize the zoom.
        double DefaultSliderValue = 3.0;

        private System.Windows.Forms.Timer MainEventTimer;

        /// <summary>
        /// Starts the ticking for the Timer that updates the elements every tick
        /// </summary>
        public void InitializeTimer()
        {
            MainEventTimer = new System.Windows.Forms.Timer();
            MainEventTimer.Tick += new EventHandler(TimerTick);
            MainEventTimer.Interval = 16; // In ms
            MainEventTimer.Start();
        }

        /// <summary>
        /// Function called by the timer ticking
        /// </summary>
        private void TimerTick(object sender, EventArgs e)
        {
            UpdateGUI();
        }
        
        /// <summary>
        /// Updates the GUI elements every tick and cleans up garbage afterwards
        /// </summary>
        private void UpdateGUI()
        {
            try
            {
                // Only updates the values if there has been a change in the slider
                if (NeedsZoomLevelUpdate)
                {
                    double zoom = ZoomLevelBar.Value / DefaultSliderValue;

                    SizeX = (int)Math.Floor(zoom * OriginalMeasureX);
                    SizeY = (int)Math.Floor(zoom * OriginalMeasureY);

                    NeedsZoomLevelUpdate = false;
                }

                Point MousePosition = System.Windows.Forms.Cursor.Position;

                if (WindowState == FormWindowState.Minimized)
                {
                    IsImageActive = false;
                    ScreenSection.Image = null;
                    ToggleImage.Checked = true;
                }

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
                    if (EndPoint.X >= MonitorWidth)
                    {  
                        InitialPoint.X = MonitorWidth - SizeX * 2 - 1;
                    }
                    if (EndPoint.Y >= MonitorHeight)
                    {
                        InitialPoint.Y = MonitorHeight - SizeY * 2 - 1;
                    }

                
                    Bitmap FullScreenshot = GetScreenshot();
                    // The part of the screen that will get put into the frame
                    Rectangle ImageSpace = new Rectangle(InitialPoint.X, InitialPoint.Y, 2 * SizeX, 2 * SizeY);
                    Color FocusColor;
                    Bitmap CroppedSection = StaticFunctions.CropRectangleFromImage(FullScreenshot, ImageSpace, out FocusColor);

                    UpdateImage(CroppedSection);
                    UpdateColor(FocusColor.R, FocusColor.G, FocusColor.B);

                    /* Not explicitly calling GC.collect() => Spikes from 200MB to 1.2GB of RAM
                     * Explicitly calling GC.collect() => Consistent ~24MB RAM usage
                     * + Explicitly disposing images here => Consistent ~16MB RAM usage
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

        /// <summary>
        /// Gets a screenshot of the entire screen
        /// </summary>
        /// <returns>The image with the screenshot</returns>
        private Bitmap GetScreenshot()
        {   
            Bitmap bitmap = new Bitmap(MonitorWidth, MonitorHeight);
            Graphics gra = Graphics.FromImage((Image)bitmap);
            gra.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            return bitmap;
        }

        /// <summary>
        /// Updates the PictureBox element with an image and refreshes it
        /// </summary>
        /// <param name="img">The image to be placed into the PictureBox element</param>
        private void UpdateImage(Image img)
        {
            ScreenSection.Image = img;
            // Screen_Section.Update();
            ScreenSection.Refresh();
        }

        /// <summary>
        /// Updates the "R","G" and "B" elements with the color value to be displayed in the GUI
        /// </summary>
        /// <param name="red">The RED value</param>
        /// <param name="green">The GREEN value</param>
        /// <param name="blue">The BLUE value</param>
        private void UpdateColor(int red, int green, int blue)
        {
            RedComponent.Text = $"R: {red}";
            GreenComponent.Text = $"G: {green}";
            BlueComponent.Text = $"B: {blue}";
        }

        /// <summary>
        /// Updates the "X" and "Y" texts in the GUI
        /// </summary>
        /// <param name="x">The text that will go into the "X" element</param>
        /// <param name="y">The text that will go into the "Y" element</param>
        public void UpdateText(string x, string y)
        {
            CoordsX.Text = x;
            CoordsY.Text = y;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public MouseTool()
        {
            InitializeComponent();
            InitializeTimer();
        }

        /// <summary>
        /// Signals the ticking function that the zoom level has changed and needs update
        /// </summary>
        private void ZoomLevelScroll(object sender, EventArgs e)
        {
            NeedsZoomLevelUpdate = true;
        }

        /// <summary>
        /// Toggles the image on and off
        /// </summary>
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

        /// <summary>
        /// Returns a cropped section of an image
        /// </summary>
        /// <param name="b">The full image</param>
        /// <param name="r">The rectangle to be cut</param>
        /// <param name="MiddlePixelColor">The color of the pixel in the middle/mouse position</param>
        /// <returns></returns>
        public static Bitmap CropRectangleFromImage(this Bitmap b, Rectangle r, out Color MiddlePixelColor)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(b, -r.X, -r.Y);
                Color red = Color.Red;

                Point MousePosition = Cursor.Position;
                Rectangle ScreenSize = Screen.PrimaryScreen.Bounds;

                int MidX = GetCorrectPosition(MousePosition.X, r.Width, r.X, ScreenSize.Width);
                int MidY = GetCorrectPosition(MousePosition.Y, r.Height, r.Y, ScreenSize.Height);

                try
                {
                    MiddlePixelColor = nb.GetPixel(MidX, MidY);
                }
                // Moving the mouse too fast sometimes causes this, for some reason
                catch (ArgumentOutOfRangeException)
                {
                    MidX = (int)Math.Floor((double)r.Width / 2);
                    MidY = (int)Math.Floor((double)r.Height / 2);
                    MiddlePixelColor = nb.GetPixel(MidX, MidY);
                }

                nb.SetPixel(MidX, MidY, red);
                return nb;
            }
        }

        /// <summary>
        /// Returns the correct position to get color from and draw red pixel. It can either be the current mouse position
        /// in case it's close to the screen edge or the middle of the drawable rectangle if it's not.
        /// </summary>
        /// <param name="CursorPos">Current mouse position in the checking axis (MousePos.x/.y)</param>
        /// <param name="CheckableWall">Size of the drawable rectangle in that direction (Rect.width/.height)</param>
        /// <param name="DiffParam">Starting point of the drawable rectangle in that direction (Rect.x/.y)</param>
        /// <param name="DirectionScreenLimit">The limit of the screen in that direction (ScreenBounds.width/.height)</param>
        /// <returns></returns>
        private static int GetCorrectPosition(int CursorPos, int CheckableWall, int DiffParam, int DirectionScreenLimit)
        {
            if (CursorPos > CheckableWall / 2 && CursorPos < DirectionScreenLimit - CheckableWall / 2)
            {
                return (int)Math.Floor((double)CheckableWall / 2);
            }

            if (CursorPos < CheckableWall / 2)
            {
                int n = CursorPos - DiffParam;
                if (n >= 0)
                {
                    return n;
                }
            }
            if (CursorPos > DirectionScreenLimit - CheckableWall / 2)
            {
                int n = CursorPos - DiffParam - 1;
                if (n >= 0 && n < DirectionScreenLimit)
                {
                    return n;
                }
            }
            return (int)Math.Floor((double)CheckableWall / 2);
        }
    }
}