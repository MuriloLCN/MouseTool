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

        int MarginRelief;

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
            MarginRelief = this.Width - ScreenSection.Width;
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
        /// 
        private void TurnOffImage()
        {
            IsImageActive = false;
            ScreenSection.Image = null;
            ToggleImage.Checked = true;
        }
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

                if (WindowState == FormWindowState.Minimized || this.Width < ScreenSection.Width + MarginRelief || this.Height < ScreenSection.Height + 31)
                {
                    TurnOffImage();
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

                    Rectangle ImageSpace = new Rectangle(InitialPoint.X, InitialPoint.Y, 2 * SizeX, 2 * SizeY);

                    Bitmap bmp = new Bitmap(ImageSpace.Width, ImageSpace.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    g.CopyFromScreen(ImageSpace.Left, ImageSpace.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

                    Color FocusColor;
                    FocusColor = StaticFunctions.getColorAndDrawCenter(bmp, ImageSpace, out bmp);


                    UpdateImage(bmp);
                    UpdateColor(FocusColor.R, FocusColor.G, FocusColor.B);

                    bmp.Dispose();
                    g.Dispose();
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
        /// <summary>
        /// Gets the color from where the mouse is and draws a red pixel on it.
        /// </summary>
        /// <param name="b">The original image passed in</param>
        /// <param name="r">The rectangle used to crop the image</param>
        /// <param name="p">The bitmap to be replaced with the edited version</param>
        /// <returns>The color of the pixel at the middle of the screen</returns>
        public static Color getColorAndDrawCenter(Bitmap b, Rectangle r, out Bitmap p)
        {

            Point MousePosition = Cursor.Position;
            Rectangle ScreenSize = Screen.PrimaryScreen.Bounds;

            int MidX = GetCorrectPosition(MousePosition.X, r.Width, r.X, ScreenSize.Width);
            int MidY = GetCorrectPosition(MousePosition.Y, r.Height, r.Y, ScreenSize.Height);

            Color middleColor;
            try
            {
                middleColor = b.GetPixel(MidX, MidY);
            }
            catch (ArgumentOutOfRangeException)
            {
                MidX = (int)(Math.Floor((double)(b.Size.Width / 2)));
                MidY = (int)(Math.Floor((double)(b.Size.Height / 2)));
                middleColor = b.GetPixel(MidX, MidY);
            }
            b.SetPixel(MidX, MidY, Color.Red);
            p = (Bitmap)b.Clone();

            return middleColor;
        }

        /// <summary>
        /// Returns the correct position to get color from and draw red pixel. It can either be the current mouse position
        /// in case it's close to the screen edge or the middle of the drawable rectangle if it's not.
        /// </summary>
        /// <param name="CursorPos">Current mouse position in the checking axis (MousePos.x/.y)</param>
        /// <param name="CheckableWall">Size of the drawable rectangle in that direction (Rect.width/.height)</param>
        /// <param name="DiffParam">Starting point of the drawable rectangle in that direction (Rect.x/.y)</param>
        /// <param name="DirectionScreenLimit">The limit of the screen in that direction (ScreenBounds.width/.height)</param>
        /// <returns>The constrained mouse position if on edge, the middle of the drawable rectangle if not or if an exception happens</returns>
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
