
namespace MouseTool
{
    partial class MouseTool
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MouseTool));
            this.CoordsX = new System.Windows.Forms.Label();
            this.CoordsY = new System.Windows.Forms.Label();
            this.ScreenSection = new System.Windows.Forms.PictureBox();
            this.ZoomLevelBar = new System.Windows.Forms.TrackBar();
            this.Text2 = new System.Windows.Forms.Label();
            this.ToggleImage = new System.Windows.Forms.CheckBox();
            this.Text1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomLevelBar)).BeginInit();
            this.SuspendLayout();
            // 
            // CoordsX
            // 
            this.CoordsX.AutoSize = true;
            this.CoordsX.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CoordsX.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.CoordsX.Location = new System.Drawing.Point(12, 141);
            this.CoordsX.Name = "CoordsX";
            this.CoordsX.Size = new System.Drawing.Size(24, 24);
            this.CoordsX.TabIndex = 0;
            this.CoordsX.Text = "X";
            // 
            // CoordsY
            // 
            this.CoordsY.AutoSize = true;
            this.CoordsY.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CoordsY.ForeColor = System.Drawing.Color.Crimson;
            this.CoordsY.Location = new System.Drawing.Point(12, 165);
            this.CoordsY.Name = "CoordsY";
            this.CoordsY.Size = new System.Drawing.Size(24, 24);
            this.CoordsY.TabIndex = 1;
            this.CoordsY.Text = "Y";
            // 
            // ScreenSection
            // 
            this.ScreenSection.Location = new System.Drawing.Point(12, 12);
            this.ScreenSection.Name = "ScreenSection";
            this.ScreenSection.Size = new System.Drawing.Size(178, 100);
            this.ScreenSection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ScreenSection.TabIndex = 2;
            this.ScreenSection.TabStop = false;
            // 
            // ZoomLevelBar
            // 
            this.ZoomLevelBar.LargeChange = 1;
            this.ZoomLevelBar.Location = new System.Drawing.Point(16, 217);
            this.ZoomLevelBar.Maximum = 5;
            this.ZoomLevelBar.Minimum = 1;
            this.ZoomLevelBar.Name = "ZoomLevelBar";
            this.ZoomLevelBar.Size = new System.Drawing.Size(171, 45);
            this.ZoomLevelBar.TabIndex = 3;
            this.ZoomLevelBar.Value = 3;
            this.ZoomLevelBar.Scroll += new System.EventHandler(this.ZoomLevelScroll);
            // 
            // Text2
            // 
            this.Text2.AutoSize = true;
            this.Text2.Location = new System.Drawing.Point(13, 201);
            this.Text2.Name = "Text2";
            this.Text2.Size = new System.Drawing.Size(34, 13);
            this.Text2.TabIndex = 4;
            this.Text2.Text = "Zoom";
            // 
            // ToggleImage
            // 
            this.ToggleImage.AutoSize = true;
            this.ToggleImage.Location = new System.Drawing.Point(16, 268);
            this.ToggleImage.Name = "ToggleImage";
            this.ToggleImage.Size = new System.Drawing.Size(179, 17);
            this.ToggleImage.TabIndex = 7;
            this.ToggleImage.Text = "Hide Image (Better performance)";
            this.ToggleImage.UseVisualStyleBackColor = true;
            this.ToggleImage.CheckedChanged += new System.EventHandler(this.zoom_toggle_CheckedChanged);
            // 
            // Text1
            // 
            this.Text1.AutoSize = true;
            this.Text1.Location = new System.Drawing.Point(13, 128);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(79, 13);
            this.Text1.TabIndex = 8;
            this.Text1.Text = "Mouse Position";
            // 
            // MouseTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 292);
            this.Controls.Add(this.Text1);
            this.Controls.Add(this.ToggleImage);
            this.Controls.Add(this.Text2);
            this.Controls.Add(this.ZoomLevelBar);
            this.Controls.Add(this.ScreenSection);
            this.Controls.Add(this.CoordsY);
            this.Controls.Add(this.CoordsX);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MouseTool";
            this.Text = "MouseTool";
            ((System.ComponentModel.ISupportInitialize)(this.ScreenSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomLevelBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CoordsX;
        private System.Windows.Forms.Label CoordsY;
        private System.Windows.Forms.PictureBox ScreenSection;
        private System.Windows.Forms.TrackBar ZoomLevelBar;
        private System.Windows.Forms.Label Text2;
        private System.Windows.Forms.CheckBox ToggleImage;
        private System.Windows.Forms.Label Text1;
    }
}

