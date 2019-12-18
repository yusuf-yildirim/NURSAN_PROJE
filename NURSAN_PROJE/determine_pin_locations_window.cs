using DevExpress.XtraEditors;
using NURSAN_PROJE.SQL;
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
        List<String> pin_locations;
        Bitmap socketimage_to_be_processed;
        string socketID_to_be_processed;
        DBeng db;
        public determine_pin_locations_window(string SocketID)
        {
            db = new DBeng();
            InitializeComponent();
            pin_locations = new List<String>();
            //socketimage_to_be_processed = new Bitmap("dummy filename");
            socketID_to_be_processed = SocketID;


        }
        public determine_pin_locations_window(string SocketID,Bitmap img)
        {
            db = new DBeng();
            InitializeComponent();
            pin_locations = new List<String>();
            socketimage_to_be_processed = img;
            determine_pin_locations_image.Image = img;
            socketID_to_be_processed = SocketID;


        }


        private void determine_pin_locations_selectimage_Click(object sender, EventArgs e)
        {

            xtraOpenFileDialog1.InitialDirectory = "c:\\";
            xtraOpenFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            xtraOpenFileDialog1.FilterIndex = 2;
            xtraOpenFileDialog1.RestoreDirectory = true;
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                determine_pin_locations_image.Image = new Bitmap(xtraOpenFileDialog1.FileName);
            }
        }

        private void determine_pin_locations_resetallpins_Click(object sender, EventArgs e)
        {
            determine_pin_locations_image.Image = new Bitmap(xtraOpenFileDialog1.FileName);
            pin_locations.Clear();
            determine_pin_locations_determinedpins.Items.Clear();
            pincount = 0;
        }
        int pincount = 0;
        private void determine_pin_locations_image_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (determine_pin_locations_image.Image != null)
                {
                    Point sPt = scaledPoint(determine_pin_locations_image, e.Location);
                    Bitmap bmp = (Bitmap)determine_pin_locations_image.Image;
                    Color c0 = bmp.GetPixel(sPt.X, sPt.Y);
                    if (c0 != Color.FromArgb(255, 255, 0, 0))
                    {
                        Console.WriteLine("girdi");
                        pin_locations.Add(sPt.X.ToString() + " " + sPt.Y.ToString());
                        determine_pin_locations_determinedpins.Items.Add(++pincount + ". pin = " + sPt.X.ToString() + " " + sPt.Y.ToString());
                        Fill4(bmp, sPt, c0, Color.Red);
                        determine_pin_locations_image.Image = bmp;
                    }
                    else
                    {
                        Fill4(bmp, sPt, c0, Color.White);
                        determine_pin_locations_image.Image = bmp;
                    }

                }
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
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

        private void determine_pin_locations_undo_Click(object sender, EventArgs e)
        {
            try
            {

                if (determine_pin_locations_image.Image != null)
                {
                    Point undo_pin_point = new Point(Convert.ToInt32(pin_locations[pin_locations.Count-1].Split(' ')[0]) , Convert.ToInt32(pin_locations[pin_locations.Count-1].Split(' ')[1]));
                    Bitmap bmp = (Bitmap)determine_pin_locations_image.Image;
                    Color c0 = bmp.GetPixel(undo_pin_point.X, undo_pin_point.Y);
                    if (c0 != Color.FromArgb(255, 255, 0, 0))
                    {
                        Console.WriteLine("girdi");
                        Fill4(bmp, undo_pin_point, c0, Color.Red);
                        determine_pin_locations_image.Image = bmp;
                    }
                    else
                    {
                        Fill4(bmp, undo_pin_point, c0, Color.White);
                        determine_pin_locations_image.Image = bmp;
                    }
                    pin_locations.RemoveAt(pin_locations.Count-1);
                    determine_pin_locations_determinedpins.Items.RemoveAt(determine_pin_locations_determinedpins.Items.Count-1);
                    if(pincount > 0)
                    {
                        pincount--;
                    }

                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void Determine_pin_locations_SavePins_Click(object sender, EventArgs e)
        {
            db.set_socket_image(socketID_to_be_processed, determine_pin_locations_image.Image);
        }
    }
}
