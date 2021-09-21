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

    class Killer : Fish
    {
        public Bitmap b;
        int bsizewx;
        int bsizewy;
        public Killer(int kind = 3 ,string name = "Rico", string sex = "girl", int age = 1, double weight = 10.0, int e = 70,
            bool active = false, int x = 0, int y = 0, int speed = 3, bool Inside = false, bool haveChild=false )
            : base(kind,name, sex, age, weight, e, active, x, y, speed, Inside,haveChild)
        {
            GetIn = false;
           
            
                if (sex == "boy")
                {
                    if (age == 1)
                    {
                        b = new Bitmap("Predator_Boy.png");
                    }
                    if (age == 2)
                    {
                        b = new Bitmap("Predator_Mature_Boy.png");
                    }
                    if (age == 3)
                    {
                        b = new Bitmap("Predator_Old_Boy.png");
                    }
                }
                if (sex == "girl")
                {
                    if (age == 1)
                    {
                        b = new Bitmap("Predator_Girl.png");
                    }
                    if (age == 2)
                    {
                        b = new Bitmap("Predator_Mature_Girl.png");
                    }
                    if (age == 3)
                    {
                        b = new Bitmap("Predator_Old_Girl.png");
                    }
                }
  
            bsizewx = b.Size.Width;
            bsizewy = b.Size.Height;
            Kills[0] = 0;
    }

        public override void Save(StreamWriter sw)
        {
            base.Save(sw);
            sw.WriteLine(Kills[0]);
        }

        public override void Load(StreamReader sr)
        {
            base.Load(sr);
            Kills[0] = Convert.ToInt32(sr.ReadLine());
            if (kind == 3)

            {
                Aquarium obj = new Aquarium();
                foreach (var VAR in obj.arr)
                {
                    if (kind == 3)
                    {

                    }
                }
                if (sex == "boy")
                {
                    if (age == 1)
                    {
                        b = new Bitmap("Predator_Boy.png");
                    }
                    if (age == 2)
                    {
                        b = new Bitmap("Predator_Mature_Boy.png");
                    }
                    if (age == 3)
                    {
                        b = new Bitmap("Predator_Old_Boy.png");
                    }
                }

            }
            if (sex == "girl")
            {
                if (age == 1)
                {
                    b = new Bitmap("Predator_Girl.png");
                }
                if (age == 2)
                {
                    b = new Bitmap("Predator_Mature_Girl.png");
                }
                if (age == 3)
                {
                    b = new Bitmap("Predator_Old_Girl.png");
                }
            }
        }

        public override void Draw(Graphics gc, bool windowed, int scrx, int scry, int scrwx, int scrwy)
        {
            if (windowed)
            {
                gc.DrawImage(b, (x - scrx) + Killer.imagex, (y - scry) + Killer.imagey, Killer.imagewx, Killer.imagewy);
                Font f = new Font("Arial", 12, FontStyle.Bold);
                Point[] Pt = new Point[]
                {new Point((x-scrx) + Killer.imagex + Killer.imagewx/2-10, (y-scry) + Killer.imagey-10),
                new Point((x-scrx) + Killer.imagex + Killer.imagewx/2+10, (y-scry) + Killer.imagey-10),
                new Point((x-scrx) + Killer.imagex + Killer.imagewx/2, (y-scry) + Killer.imagey)};
                if (active)
                {
                    gc.DrawString(name + " - " + weight + " г, " + Energy + "%, " + Kills[0] + " kill(s)", f, Brushes.Black, (x - scrx - 10) + textx1, (y - scry) + texty1);
                    Brush p = new SolidBrush(Color.Black);
                    gc.FillPolygon(p, Pt);
                }
                else
                {
                    gc.DrawString(name + " - " + weight + " г, " + Energy + "%, " + Kills[0] + " kill(s)", f, Brushes.Red, (x - scrx - 10) + textx1, (y - scry) + texty1);
                    Brush p = new SolidBrush(Color.Red);
                    gc.FillPolygon(p, Pt);
                }
            }
            else
            {
                gc.DrawImage(b, x + Killer.imagex, y + Killer.imagey, Killer.imagewx, Killer.imagewy);
                Font f = new Font("Arial", 12, FontStyle.Bold);
                Point[] Pt = new Point[]
                {new Point(x + Killer.imagex + Killer.imagewx/2-10, y + Killer.imagey-10),
                new Point(x + Killer.imagex + Killer.imagewx/2+10, y + Killer.imagey-10),
                new Point(x + Killer.imagex + Killer.imagewx/2, y + Killer.imagey)};
                if (active)
                {
                    gc.DrawString(name + " - " + weight + " г, " + Energy + "%", f, Brushes.Black, x + textx1, y + texty1);
                    Brush p = new SolidBrush(Color.Black);
                    gc.FillPolygon(p, Pt);
                }
                else
                {
                    gc.DrawString(name + " - " + weight + " г, " + Energy + "%", f, Brushes.Red, x + textx1, y + texty1);
                    Brush p = new SolidBrush(Color.Red);
                    gc.FillPolygon(p, Pt);
                }
            }
        }
    }
}
