using System.Drawing;
using System.Windows.Forms;

namespace DesenvolvedorRelacional.Apresentacao
{
    public class Mascara : Panel
    {
        public Mascara()
        {
        }

        protected Graphics Graficos { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graficos = e.Graphics;

            Graficos.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Graficos.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            Graficos.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            Graficos.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }

    }
}
