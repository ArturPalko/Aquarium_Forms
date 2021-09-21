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
using System.Windows;



namespace lab_3
{
    class Fish : ICloneable, MicroObjInMacro
    {
        public string name;
        public double weight;
        public int speed;
        public int[] Kills = new int[1];
        public int energy;
        public bool Inside;
        public int x, y;
        public bool active;
        public object GetIn;  
        public int age;
        public string sex;
        public int kind;
        public bool haveChild;
        public bool vision;
        public static int imagex = 0;
        public static int imagey = 0;
        public static int imagewx = 150;
        public static int imagewy = 150;
        protected static int textx1 = imagex - 10;
        protected static int texty1 = imagey - 30;
        Bitmap b;
        int bsizewx;
        int bsizewy;
        public int Energy
        {
            get
            {
                return energy;
            }
            set
            {
                if (value > 100)
                {
                    energy = 100;
                }
                if (value < 0)
                {
                    energy = 0;
                }
                if (value >= 0 && value <= 100)
                {
                    energy = value;
                }
            }
        }

        public Fish(int kind=1,string name = "Skipper", string sex = "girl", int age = 1, double weight = 5.0, int e = 50, 
            bool active = false, int x = 0, int y = 0, int speed =2,bool Inside = false, bool haveChild=false,bool vision=true)
        {
            this.name = name;
            this.weight = weight;
            this.sex = sex;
            this.age = age;
            this.kind = kind;
            Energy = e;
            this.x = (x == 0) ? (Program.rnd.Next() % 1400) : (x);
            this.y = (y == 0) ? (Program.rnd.Next() % 1400) : (y);
            this.active = active;
            this.speed = speed;
            this.Inside = Inside;
            this.vision = vision;
            GetIn = false;
            this.haveChild = haveChild;
            if (kind == 1)
            {
                if (sex == "boy")
                {
                    if (age == 1)
                    {
                        b = new Bitmap("Prey_Boy.png");
                    }
                    if (age == 2)
                    {
                        b = new Bitmap("Prey_Mature_Boy.png");
                    }
                    if (age == 3)
                    {
                        b = new Bitmap("Prey_Old_Boy.png");
                    }
                }
                if (sex == "girl")
                {
                    if (age == 1)
                    {
                        b = new Bitmap("Prey_Girl.png");
                    }
                    if (age == 2)
                    {
                        b = new Bitmap("Prey_Mature_Girl.png");
                    }
                    if (age == 3)
                    {
                        b = new Bitmap("Prey_Old_Girl.png");
                    }
                }
            }
            if (kind == 3)
            {

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
            }
           

            
        }

        public virtual void Save(StreamWriter sw)
        {
            sw.WriteLine(GetType().Name);
            sw.WriteLine(kind);
            sw.WriteLine(name);
            sw.WriteLine(weight);
            sw.WriteLine(Energy);
            sw.WriteLine(active);
            sw.WriteLine(sex);
            sw.WriteLine(haveChild);
            sw.WriteLine(vision);
            sw.WriteLine(age);
            sw.WriteLine(x);
            sw.WriteLine(y);
            sw.WriteLine(speed);
            sw.WriteLine(Inside);

        }

        public  virtual void Load(StreamReader sr)
        {
            kind= Convert.ToInt32(sr.ReadLine());
            this.name = sr.ReadLine();
            weight = Convert.ToDouble(sr.ReadLine());
            Energy = Convert.ToInt32(sr.ReadLine());
            active = Convert.ToBoolean(sr.ReadLine());
            sex= sr.ReadLine();
            haveChild = Convert.ToBoolean(sr.ReadLine());
            vision= Convert.ToBoolean(sr.ReadLine());
            age= Convert.ToInt32(sr.ReadLine());
            x = Convert.ToInt32(sr.ReadLine());
            y = Convert.ToInt32(sr.ReadLine());
            speed = Convert.ToInt32(sr.ReadLine());
            Inside = Convert.ToBoolean(sr.ReadLine());
            if (kind == 1)
            {
                if (sex == "boy")
                {
                    if (age == 1)
                    {
                        b = new Bitmap("Prey_Boy.png");
                    }
                    if (age == 2)
                    {
                        b = new Bitmap("Prey_Mature_Boy.png");
                    }
                    if (age == 3)
                    {
                        b = new Bitmap("Prey_Old_Boy.png");
                    }
                }
                if (sex == "girl")
                {
                    if (age == 1)
                    {
                        b = new Bitmap("Prey_Girl.png");
                    }
                    if (age == 2)
                    {
                        b = new Bitmap("Prey_Mature_Girl.png");
                    }
                    if (age == 3)
                    {
                        b = new Bitmap("Prey_Old_Girl.png");
                    }
                }
            }

           


        }

        public bool InAviary(int rx, int ry, int rwx, int rwy)
        {
            if (x < rx - 20) return false;
            if ((x - 20 + Fish.imagewx) > (rx + rwx)) return false;
            if (y < ry - 20) return false;
            if ((y - 20 + Fish.imagewy) > (ry + rwy)) return false;

            return true;
        }

        public static bool operator *(Fish obj1, Fish obj2)
        {
            if ((obj1.age==2)&&(obj2.age==2)&&(obj1.GetType().ToString()== obj2.GetType().ToString()))
            {
                {
                    Rectangle rectangle1 = new Rectangle(obj1.x + 50, obj1.y + 50, Fish.imagewx - 50, Fish.imagewy - 50);
                    Rectangle rectangle2 = new Rectangle(obj2.x + 50, obj2.y + 50, Fish.imagewx - 50, Fish.imagewy - 50);
                    /*Якщо вони  перетинаються, обидва об'єкти активні та не знаходяться у вольєрі*/
                    if (rectangle1.IntersectsWith(rectangle2) && obj1.isActive() == true && obj2.isActive() == true &&
                        obj1.Inside == false && obj2.Inside == false)
                    {
                        return true;
                    }
                }
                
            }
            return false;
        }
        public static bool operator /(Fish obj1, Fish obj2)
        {
                Rectangle rectangle1 = new Rectangle(obj1.x+50, obj1.y+50, Fish.imagewx-50, Fish.imagewy-50);
                Rectangle rectangle2 = new Rectangle(obj2.x+50, obj2.y+50, Fish.imagewx-50, Fish.imagewy-50);
                
                if (rectangle1.IntersectsWith(rectangle2) && obj1.isActive() == true && obj2.isActive() == true &&
                    obj1.Inside == false && obj2.Inside == false)
                {
                    return true;
                }
            
            return false;
        }

        public bool isActive()
        {
            return active;
        }

        public void Inactive()
        {
            active = false;
        }
        public void Nactive()
        {
            active = true;
        }

        public void Move(int dx, int dy)
        {
            if (active)
            {
                x += (speed * dx);
                y += (speed * dy);
                if (x > 5 && x + imagewx < 1600 && y > 50 && y + imagewy < 1600)
                {
                }
                else
                {
                    x -= (speed * dx);
                    y -= (speed * dy);
                }
            }
        }

        public void MouseClick(int mx, int my, int scrx, int scry, int scrwx, int scrwy)
        {
            mx += scrx;
            my += scry;
            if ((mx < x) || (mx > (x + Fish.imagex + Fish.imagewx)) ||
                 (my < y) || (my > (y + Fish.imagey + Fish.imagewy)))
            {
                return;
            }
            active = !active;
        }

        public virtual void Draw(Graphics gc, bool windowed, int scrx, int scry, int scrwx, int scrwy)
        {
            if (windowed)
            {
                gc.DrawImage(b, (x - scrx) + Fish.imagex, (y - scry) + Fish.imagey, Fish.imagewx, Fish.imagewy);
                Font f = new Font("Arial", 12, FontStyle.Bold);
                Point[] Pt = new Point[]
                {new Point((x-scrx) + Fish.imagex + Fish.imagewx/2-10, (y-scry) + Fish.imagey-10),
                new Point((x-scrx) + Fish.imagex + Fish.imagewx/2+10, (y-scry) + Fish.imagey-10),
                new Point((x-scrx) + Fish.imagex + Fish.imagewx/2, (y-scry) + Fish.imagey)};
                if (active)
                {
                    gc.DrawString(name + " - " + weight + " г, " + Energy + "%", f, Brushes.Black, (x - scrx) + textx1, (y - scry) + texty1);
                    Brush p = new SolidBrush(Color.Black);
                    gc.FillPolygon(p, Pt);
                }
                else
                {
                    gc.DrawString(name + " - " + weight + " г, " + Energy + "%", f, Brushes.Green, (x - scrx) + textx1, (y - scry) + texty1);
                    Brush p = new SolidBrush(Color.Green);
                    gc.FillPolygon(p, Pt);
                }
            }
            else
            {
                gc.DrawImage(b, x + Fish.imagex, y + Fish.imagey, Fish.imagewx, Fish.imagewy);
                Font f = new Font("Arial", 12, FontStyle.Bold);
                Point[] Pt = new Point[]
                {new Point(x + Fish.imagex + Fish.imagewx/2-10, y + Fish.imagey-10),
                new Point(x + Fish.imagex + Fish.imagewx/2+10, y + Fish.imagey-10),
                new Point(x + Fish.imagex + Fish.imagewx/2, y + Fish.imagey)};
                if (active)
                {
                    gc.DrawString(name + " - " + weight + " г, " + Energy + "%", f, Brushes.Black, x + textx1, y + texty1);
                    Brush p = new SolidBrush(Color.Black);
                    gc.FillPolygon(p, Pt);
                }
                else
                {
                    gc.DrawString(name + " - " + weight + " г, " + Energy + "%", f, Brushes.Green, x + textx1, y + texty1);
                    Brush p = new SolidBrush(Color.Green);
                    gc.FillPolygon(p, Pt);
                }
            }
        }

        public void Print()
        {
            Console.Write("{0} - риба з вагою {1:0.0}кг енергiєю {2}, показники: ", name, weight, Energy);
        }

        public object Clone()
        {
            Fish tmp = (Fish)this.MemberwiseClone();
            tmp.Kills = (int[])this.Kills.Clone();
            return tmp;
        }

        public static Fish operator ++(Fish obj)
        {
            obj.weight += 3;
            obj.Energy += 2;
            return obj;
        }

        public void GetOut()
        {
            
            x += 200;
            y += 200;
            Inside = false;
            GetIn = false;
        }
    }

    public interface MicroObjInMacro
    {
        void GetOut();
    }

}