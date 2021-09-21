using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace Акваріум_формс
{
    class Algaes
    {
        public int ax, ay;
        public static int adx=200, ady=200;
        public bool empty;
        Aquarium vinzoo;
        Bitmap b;
        int bsizewx;
        int bsizewy;
        public  static List<Fish> pen = new List<Fish>();
       
        public Algaes(Aquarium vinzoo, int ax=300, int ay=200, bool empty = true)
        {
            this.vinzoo = vinzoo;
            this.ax = ax;
            this.ay = ay;
            this.empty = empty;

            b = new Bitmap("Algaes.jpg");
            bsizewx = b.Size.Width;
            bsizewy = b.Size.Height;
        }

        public void Save(StreamWriter sw)
        {
            sw.WriteLine("{0},{1},{2}", ax, ay, empty);
            if (empty == false)
            {
                foreach (var VARIABLE in pen)
                {
                    VARIABLE.Save(sw);
                }
            }
        }
       
        public int Load(StreamReader sr)
        {
            string s = sr.ReadLine();
            string[] ars = s.Split(',');

            if (ars.Length != 3) return -1;

            ax = Convert.ToInt32(ars[0]);
            ay = Convert.ToInt32(ars[1]);
            empty = Convert.ToBoolean(ars[2]);
            if (empty == false)
            {
                s = sr.ReadLine();
                if (s == "Fish")
                {
                    pen.Add(new Fish());
                    pen[pen.Count - 1].Load(sr);
                }
               
                if (s == "Killer")
                {
                    pen.Add(new Killer());
                    pen[pen.Count - 1].Load(sr);
                }
            }
            return 0;
        }
        
        public void AddFish(Fish obj)
        {
            pen.Add(obj);
            obj++;
        }

        public bool CheckInside(Fish obj)
        {
            if (obj.InAviary(ax, ay, adx, ady) && empty == true ) return true;

            return false;
        }
     
        public void Draw(Graphics gc, bool windowed, int scrx, int scry, int scrwx, int scrwy)
        {
            if (windowed)
            {
                gc.DrawImage(b, (ax - scrx), (ay - scry), adx, ady);
                if (empty)
                {
                    Font f = new Font("Arial", 10, FontStyle.Bold);
                    gc.DrawString("emty", f, Brushes.Black, (ax - scrx) + 80, (ay - scry));
                }
                else
                {
                    foreach (var i in pen)
                    {
                        i.Draw(gc, true, scrx, scry, scrwx, scrwy);
                    }
                } 
            }
            else
            {
                gc.DrawImage(b, ax, ay, adx, ady);
                if (empty)
                {
                    Font f = new Font("Arial", 10, FontStyle.Bold);
                    gc.DrawString("emty", f, Brushes.Black, ax + 80, ay);
                }
                else
                {
                    foreach (var i in pen)
                    {
                        i.Draw(gc, false, scrx, scry, scrwx, scrwy);
                    }
                }
            }
        }
    }
}
