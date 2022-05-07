
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
            this.label1 = new System.Windows.Forms.Label();
            this.RedComponent = new System.Windows.Forms.Label();
            this.GreenComponent = new System.Windows.Forms.Label();
            this.BlueComponent = new System.Windows.Forms.Label();
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
            this.ToggleImage.Size = new System.Drawing.Size(80, 17);
            this.ToggleImage.TabIndex = 7;
            this.ToggleImage.Text = "Hide Image";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Color";
            // 
            // RedComponent
            // 
            this.RedComponent.AutoSize = true;
            this.RedComponent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedComponent.ForeColor = System.Drawing.Color.Red;
            this.RedComponent.Location = new System.Drawing.Point(130, 141);
            this.RedComponent.Name = "RedComponent";
            this.RedComponent.Size = new System.Drawing.Size(17, 16);
            this.RedComponent.TabIndex = 10;
            this.RedComponent.Text = "R";
            // 
            // GreenComponent
            // 
            this.GreenComponent.AutoSize = true;
            this.GreenComponent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenComponent.ForeColor = System.Drawing.Color.Lime;
            this.GreenComponent.Location = new System.Drawing.Point(129, 157);
            this.GreenComponent.Name = "GreenComponent";
            this.GreenComponent.Size = new System.Drawing.Size(18, 16);
            this.GreenComponent.TabIndex = 11;
            this.GreenComponent.Text = "G";
            // 
            // BlueComponent
            // 
            this.BlueComponent.AutoSize = true;
            this.BlueComponent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlueComponent.ForeColor = System.Drawing.Color.Blue;
            this.BlueComponent.Location = new System.Drawing.Point(130, 173);
            this.BlueComponent.Name = "BlueComponent";
            this.BlueComponent.Size = new System.Drawing.Size(17, 16);
            this.BlueComponent.TabIndex = 12;
            this.BlueComponent.Text = "B";
            // 
            // MouseTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 292);
            this.Controls.Add(this.BlueComponent);
            this.Controls.Add(this.GreenComponent);
            this.Controls.Add(this.RedComponent);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RedComponent;
        private System.Windows.Forms.Label GreenComponent;
        private System.Windows.Forms.Label BlueComponent;
    }
}

