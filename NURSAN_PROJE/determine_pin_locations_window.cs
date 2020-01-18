using DevExpress.XtraEditors;
using NURSAN_PROJE.SQL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace NURSAN_PROJE
{
    public partial class determine_pin_locations_window : Form
    {
        List<String> pin_locations;
        Bitmap socketimage_to_be_processed;
        string socketID_to_be_processed;
        DBeng db;
        PropertyItem propertyItem;
        DataManager manager;
        DataManager.TableUpdater updater;
        public determine_pin_locations_window(string SocketID)
        {
            db = new DBeng();
            manager = new DataManager();
            updater = new DataManager.TableUpdater();
            InitializeComponent();
            pin_locations = new List<String>();
            //socketimage_to_be_processed = new Bitmap("dummy filename");
            socketID_to_be_processed = SocketID;
            //CheckForIllegalCrossThreadCalls = false;


        }
        string[] pin_coordinates;
        Point pin_point;
        Bitmap bitmap_for_pin_processing;
        Color point_color_for_pin_processing;
        public determine_pin_locations_window(string SocketID, Bitmap img)
        {
            manager = new DataManager();
            updater = new DataManager.TableUpdater();
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
            PropertyItem[] propertyItem2 = img.PropertyItems;
            bitmap_to_be_saved = new Bitmap(img);


            for (int i = 0; i < propertyItem2.Length; i++)
            {
                bitmap_to_be_saved.SetPropertyItem(propertyItem2[i]);
                Console.WriteLine(propertyItem2[i]);
            }
            socketimage_to_be_processed = new Bitmap(img);

            db = new DBeng();
            pin_locations = new List<String>();
            determine_pin_locations_image.Image = socketimage_to_be_processed;
            socketID_to_be_processed = SocketID;


            propertyItem = bitmap_to_be_saved.GetPropertyItem(0x010e);


            pin_coordinates = Encoding.UTF8.GetString(propertyItem.Value).Trim().Split(' ');
           


            for (int i = 0; i < pin_coordinates.Length - 2; i = i + 3)
            {
                //Console.WriteLine(i+"==" +pin_coordinates[i + 1]+ " --- " + pin_coordinates[i + 2]+ "///////" + pin_coordinates.Length);
                pin_point = new Point(Convert.ToInt32(pin_coordinates[i + 1]), Convert.ToInt32(pin_coordinates[i + 2]));
                bitmap_for_pin_processing = (Bitmap)determine_pin_locations_image.Image;
                point_color_for_pin_processing = bitmap_for_pin_processing.GetPixel(pin_point.X, pin_point.Y);
                if (point_color_for_pin_processing != Color.FromArgb(255, 255, 0, 0))
                {
                   // Console.WriteLine("girdi");
                    pin_locations.Add(pincount + 1 + " " + pin_point.X.ToString() + " " + pin_point.Y.ToString());
                    determine_pin_locations_determinedpins.Items.Add(++pincount + ". pin = " + pin_point.X.ToString() + " " + pin_point.Y.ToString());
                    Fill4(bitmap_for_pin_processing, pin_point, point_color_for_pin_processing, Color.Red);
                }
                determine_pin_locations_image.Image = bitmap_for_pin_processing;

            }
        }

        Bitmap bitmap_to_be_saved;
        private void determine_pin_locations_selectimage_Click(object sender, EventArgs e)
        {

            xtraOpenFileDialog1.InitialDirectory = "c:\\";
            xtraOpenFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            xtraOpenFileDialog1.FilterIndex = 2;
            xtraOpenFileDialog1.RestoreDirectory = true;
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap_to_be_saved = new Bitmap(xtraOpenFileDialog1.FileName);
                determine_pin_locations_image.Image = (Bitmap)bitmap_to_be_saved.Clone();
            }
        }

        private void determine_pin_locations_resetallpins_Click(object sender, EventArgs e)
        {
            determine_pin_locations_image.Image = (Bitmap)bitmap_to_be_saved.Clone();
            pin_locations.Clear();
            determine_pin_locations_determinedpins.Items.Clear();
            pincount = 0;
        }
        int pincount = 0;
        private void determine_pin_locations_image_MouseClick(object sender, MouseEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            try
            {

                if (determine_pin_locations_image.Image != null)
                {
                    Point sPt = scaledPoint(determine_pin_locations_image, e.Location);
                    Bitmap bmp = (Bitmap)determine_pin_locations_image.Image;
                    Color c0 = bmp.GetPixel(sPt.X, sPt.Y);
                    if (c0 != Color.FromArgb(255, 255, 0, 0))
                    {
                        //Console.WriteLine("girdi");
                        pin_locations.Add(pincount + 1 + " " + sPt.X.ToString() + " " + sPt.Y.ToString());
                        determine_pin_locations_determinedpins.Items.Add(++pincount + ". pin = " + sPt.X.ToString() + " " + sPt.Y.ToString());
                        Bitmap denemeimg = new Bitmap(bmp);
                        Fill4(denemeimg, sPt, c0, Color.Red);
                        //Fill4(bmp, sPt, c0, Color.Red);
                        determine_pin_locations_image.Image = denemeimg;
                    }
                    else
                    {
                        Fill4(bmp, sPt, c0, Color.White);
                        determine_pin_locations_image.Image = bmp;
                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            splashScreenManager1.CloseWaitForm();
        }
        void Fill4(Bitmap bmp, Point pt, Color c0, Color c1)
        {
            Color cx = bmp.GetPixel(pt.X, pt.Y);
            if (cx.GetBrightness() < 0.01f) return;  // optional, to prevent filling a black grid
            Rectangle bmpRect = new Rectangle(Point.Empty, bmp.Size);
            Stack<Point> stack = new Stack<Point>();
            int x0 = pt.X;
            int y0 = pt.Y;
            //TODO  
            stack.Push(new Point(x0, y0));
            int sayacdeneme = 0;
            while (stack.Any())
            {
                sayacdeneme++;
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
            splashScreenManager1.ShowWaitForm();
            try
            {

                if (determine_pin_locations_image.Image != null)
                {
                    Point undo_pin_point = new Point(Convert.ToInt32(pin_locations[pin_locations.Count - 1].Split(' ')[1]), Convert.ToInt32(pin_locations[pin_locations.Count - 1].Split(' ')[2]));
                    Bitmap bmp = (Bitmap)determine_pin_locations_image.Image;
                    Color c0 = bmp.GetPixel(undo_pin_point.X, undo_pin_point.Y);
                    if (c0 != Color.FromArgb(255, 255, 0, 0))
                    {
                       // Console.WriteLine("girdi");
                        Fill4(bmp, undo_pin_point, c0, Color.Red);
                        determine_pin_locations_image.Image = bmp;
                    }
                    else
                    {
                        Fill4(bmp, undo_pin_point, c0, Color.White);
                        determine_pin_locations_image.Image = bmp;
                    }
                    pin_locations.RemoveAt(pin_locations.Count - 1);
                    determine_pin_locations_determinedpins.Items.RemoveAt(determine_pin_locations_determinedpins.Items.Count - 1);
                    if (pincount > 0)
                    {
                        pincount--;
                    }

                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void Determine_pin_locations_SavePins_Click(object sender, EventArgs e)
        {
            try
            {

                PropertyItem propItem = bitmap_to_be_saved.PropertyItems[0];
                SetProperty(ref propItem, 0x010e, "");
                bitmap_to_be_saved.SetPropertyItem(propItem);

                // PropertyItem propItem = bitmap_to_be_saved.GetPropertyItem(0x010e);
                string value_to_be_saved = null;
                for (int i = 0; i < pin_locations.Count; i++)
                {
                    value_to_be_saved += pin_locations[i] + " ";
                }
                propItem.Len = value_to_be_saved.Length + 1;
                byte[] bytesText = Encoding.ASCII.GetBytes(value_to_be_saved);
                propItem.Value = bytesText;
                bitmap_to_be_saved.SetPropertyItem(propItem);

                manager.setSocketImage(socketID_to_be_processed, bitmap_to_be_saved);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RESİM KAYDEDİLEMEDİ - " + ex.Message+" - "+ex.StackTrace);
             
            }

        }
        private void SetProperty(ref System.Drawing.Imaging.PropertyItem prop, int iId, string sTxt)
        {
            int iLen = sTxt.Length + 1;
            byte[] bTxt = new Byte[iLen];
            for (int i = 0; i < iLen - 1; i++)
                bTxt[i] = (byte)sTxt[i];
            bTxt[iLen - 1] = 0x00;
            prop.Id = iId;
            prop.Type = 2;
            prop.Value = bTxt;
            prop.Len = iLen;
        }
    }
}
