using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE
{
    public partial class determine_pin_locations_window : Form
    {
        List<Rectangle> pin_locations;
        Bitmap socketimage_to_be_processed;
        public determine_pin_locations_window()
        {
            InitializeComponent();
            pin_locations = new List<Rectangle>();
            //socketimage_to_be_processed = new Bitmap("dummy filename");
        }

        private void determine_pin_locations_selectimage_Click(object sender, EventArgs e)
        {

        }

        private void determine_pin_locations_resetallpins_Click(object sender, EventArgs e)
        {

        }

        private void determine_pin_locations_image_MouseClick(object sender, MouseEventArgs e)
        {
            Point sPt = scaledPoint(determine_pin_locations_image, e.Location);
            Bitmap bmp = (Bitmap)determine_pin_locations_image.Image;
            Color c0 = bmp.GetPixel(sPt.X, sPt.Y);
            if(c0 != Color.Red)
            {
                Fill4(bmp, sPt, c0, Color.Red);
                determine_pin_locations_image.Image = bmp;
            }
            
        }
        static void Fill4(Bitmap bmp, Point pt, Color c0, Color c1)
        {
            Color cx = bmp.GetPixel(pt.X, pt.Y);
            if (cx.GetBrightness() < 0.01f) return;  // optional, to prevent filling a black grid
            Rectangle bmpRect = new Rectangle(Point.Empty, bmp.Size);
            Stack<Point> stack = new Stack<Point>();
            int x0 = pt.X;
            int y0 = pt.Y;

            stack.Push(new Point(x0, y0));
            while (stack.Any())
            {
                Point p = stack.Pop();
                if (!bmpRect.Contains(p)) continue;
                cx = bmp.GetPixel(p.X, p.Y);
                if (cx == c0)
                {
                    bmp.SetPixel(p.X, p.Y, c1);
                    stack.Push(new Point(p.X, p.Y + 1));
                    stack.Push(new Point(p.X, p.Y - 1));
                    stack.Push(new Point(p.X + 1, p.Y));
                    stack.Push(new Point(p.X - 1, p.Y));
                }
            }
        }
        static Point scaledPoint(PictureEdit pbox, Point pt)
        {
            Size si = pbox.Image.Size;
            Size sp = pbox.ClientSize;
            int left = 0;
            int top = 0;
            if (1f * si.Width / si.Height < 1f * sp.Width / sp.Height)
                left = (sp.Width - si.Width * sp.Height / si.Height) / 2;
            else
                top = (sp.Height - si.Height * sp.Width / si.Width) / 2;
            pt = new Point(pt.X - left, pt.Y - top);
            float scaleX = 1f * pbox.Image.Width / (pbox.ClientSize.Width - 2 * left);
            float scaleY = 1f * pbox.Image.Height / (pbox.ClientSize.Height - 2 * top);
            return new Point((int)(pt.X * scaleX), (int)(pt.Y * scaleY));
        }

    }
}
