
using System.ComponentModel;
using System.Drawing;

namespace Task_5
{
    [ToolboxItem(true), ToolboxBitmap(typeof(RenderControl), "RenderControl.bmp"), DefaultEvent("")]
    partial class RenderControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RenderControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "RenderControl";
            this.Size = new System.Drawing.Size(480, 300);
            this.TextCodePage = 1251;
            this.Render += new System.EventHandler(this.OnRender);
            this.ContextCreated += new System.EventHandler(this.RenderContextCreated);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
