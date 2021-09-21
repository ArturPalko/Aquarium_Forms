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
    class Aquarium
    {
        public static int timeleft;
        public List<Fish> arr;
        public List<Algaes> arra;
        public static int textx2 = 600;
        public static int texty2 = 5;
        public static int wx;
        public static int wy;
        public static bool Terminate = false;
        public int scrx;
        public int scry;
        int scrwx;
        int scrwy;
        Bitmap bn;
        int bnsizewx;
        int bnsizewy;
        System.Windows.Forms.Timer timer1;
        public static System.Windows.Forms.Timer timer2;
        public static System.Windows.Forms.Timer timer3;
        delegate void TimerDef(object sender, EventArgs e);
        delegate void TimerDef2(object sender, EventArgs e);
        delegate void TimerDef3(object sender, EventArgs e);
        event TimerDef Added;
        event TimerDef2 Added2;
        event TimerDef3 Added3;
        private static int zoox, zooy, zoowx, zoowy, zoodx;
     
        public Aquarium()
        {
            Aquarium.zoox = 0;
            Aquarium.zooy = 40;
            Aquarium.zoowx = 150;
            Aquarium.zoowy = 150;
            Aquarium.zoodx = 100;
            Aquarium.wx = 1600;
            Aquarium.wy = 1600;
            scrx = 0;
            scry = 0;
            
            arr = new List<Fish>();
            arr.Add(new Killer(sex: "girl", name: "Jessica"));
            arr.Add(new Killer(sex: "girl", name: "Fiona"));
            arr.Add(new Killer(sex:"boy",name: "Edward"));
            arr.Add(new Killer(sex: "boy", name: "Shrek"));
            arr.Add(new Fish(sex: "boy", name: "Richard"));
            arr.Add(new Fish(sex: "boy", name: "Charles"));
            arr.Add(new Fish(sex: "boy", name: "Jack"));
            arr.Add(new Fish(sex: "boy", name: "Patrick"));
            arr.Add(new Fish(sex: "boy", name: "Frank"));
            arr.Add(new Fish(sex: "girl", name: "Melissa"));
            arr.Add(new Fish(sex: "girl", name: "Doris"));
            arr.Add(new Fish(sex: "girl", name: "Nancy"));
            arr.Add(new Fish(sex: "girl", name: "Loraine"));
            arr.Add(new Fish(sex: "girl", name: "Bonnie"));
            
            arra = new List<Algaes>();
            arra.Add(new Algaes(this, 200, 200));
            arra.Add(new Algaes(this, 200, 700));
            arra.Add(new Algaes(this, 200, 1200));
            arra.Add(new Algaes(this, 700, 200));
            arra.Add(new Algaes(this, 700, 700));
            arra.Add(new Algaes(this, 700, 1200));
            arra.Add(new Algaes(this, 1200, 200));
            arra.Add(new Algaes(this, 1200, 700));
            arra.Add(new Algaes(this, 1200, 1200));
           
           bn = new Bitmap("Turtle.png");
           bnsizewx = bn.Size.Width;
           bnsizewy = bn.Size.Height;

            Added += Zoo_TimeTick1;
            Added += Zoo_TimeTick2;

            Added2 += WasteOfEnergy;

            Added3 += GrowingUp;

            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(Added);
            timer1.Interval = 10000;

           timer2 = new System.Windows.Forms.Timer();
            timer2.Tick += new EventHandler(Added2);
            timer2.Interval = 1000;
            
            timer3 = new System.Windows.Forms.Timer();
            timer3.Tick += new EventHandler(Added3);
            timer3.Interval = 30000;


        }
       
        public void Save(StreamWriter sw)
        {
            sw.WriteLine("Aquarium");
            sw.WriteLine(arr.Count);
            foreach (var VARIABLE in arr)
            {
                VARIABLE.Save(sw);
            }
            sw.WriteLine("Aviary");
            sw.WriteLine(arra.Count);
            foreach (var VARIABLE in arra)
            {
                VARIABLE.Save(sw);
            }
            
            lock (Fishes.form1)
            {
                sw.WriteLine("{0}, {1}, {2}, {3}", Fishes.already, zoox, zooy, zoodx);
            }
        }
       
        public int Load(StreamReader sr)
        {
            string s = sr.ReadLine();
            if (s != "Aquarium") return -1;
            foreach (var VAR in arr.ToArray())
            {
                arr.Remove(VAR);
            }
            s = sr.ReadLine();
            int k = Convert.ToInt32(s);
            for (int i = 0; i < k; i++)
            {
                s = sr.ReadLine();
                if (s == "Fish")
                {
                    arr.Add(new Fish());
                    arr[arr.Count - 1].Load(sr);
               
                }
               
                if (s == "Killer")
                {
                    arr.Add(new Killer());
                    arr[arr.Count - 1].Load(sr);
                    
                }
            }
            s = sr.ReadLine();
            if (s != "Aviary") return -2;
            foreach (var VARIABLE in arra)
            {
                foreach (var i in Algaes.pen.ToArray())
                {
                    Algaes.pen.Remove(i);
                }
            }
            s = sr.ReadLine();
            int kk = Convert.ToInt32(s);
            for (int i = 0; i < kk; i++)
            {
                arra.Add(new Algaes(this));
                arra[arra.Count - 1].Load(sr);
            }

            s = sr.ReadLine();
            string[] ars = s.Split(',');
            if (ars.Length != 4) return -3; 
            lock (Fishes.form1)
            {
                Fishes.already = Convert.ToBoolean(ars[0]);
                zoox = Convert.ToInt32(ars[1]);
                zooy = Convert.ToInt32(ars[2]);
                zoodx = Convert.ToInt32(ars[3]);
            }

            return 0;
        }

        public static void ZooMove()
        {
             while(!Terminate)
            {
                 {
                    if (((Aquarium.zoox + Aquarium.zoodx) < 0) || ((Aquarium.zoox + Aquarium.zoowx + Aquarium.zoodx) > Aquarium.wx))     
                    {
                        Aquarium.zoox = -Aquarium.zoodx;
                        Aquarium.timeleft++;
                    }
                    
                    else
                    {
                        Aquarium.zoox += Aquarium.zoodx;
                    }
                    Fishes.form1.form_Invalidate();
                    Thread.Sleep(100);
        
                    
                }
                Fishes.form1.form_Invalidate();
                Thread.Sleep(100);
            }
        }
 
              

        public void Zoo_TimeTick1(object sender, EventArgs e)
        {
            foreach (var VAR in arra.ToArray())
                
            {  if (Algaes.pen.Count != 0)
                {
                    
                    foreach (var VARIABLE in Algaes.pen.ToArray())
                    {
                        if (VARIABLE.isActive())
                        {
                           
                            VARIABLE.GetOut();
                            arr.Add(VARIABLE);
                            Algaes.pen.Remove(VARIABLE);
                           VAR.empty = true;
                        }
                    }
                }
            }
        }
            
            public void Zoo_TimeTick2(object sender, EventArgs e)
        {
            foreach (var VAR in arra)
            {
                    foreach (var VARIABLE in Algaes.pen)
                    {
                      timer1.Stop();
                    }
            }
        }


              

       
        public void Zoo_TimerTick(EventArgs e)
       {/*для кожного пінгвіна із списку зоопарку*/
   
                foreach (var VAR in arr.ToArray()) 
           {/*якщо стан відносно вольєра Inside і стан мікрооб'єкта GetIn (У вольєрі чи ні) - false  */
               if (VAR.Inside == false && (bool)VAR.GetIn == false)
               {/*рандомно переммістити на Х та Y відносно координат*/
                   VAR.Move((Program.rnd.Next(1, 4) - 2) * (Fish.imagewx / 8),
                           (Program.rnd.Next(1, 4) - 2) * (Fish.imagewy / 8));
                    /*Для кожного пінгвіна із списку пінгвінів зоопарку*/
                   foreach (var VARIABLE in arr.ToArray())
                    {/*Створюємо навколо них прямокутники із відповідними параметрами.
                      Якщо вони  перетинаються, обидва об'єкти активні та не знаходяться у вольєрі
                        і властивість Inside==false*/
                        
                        if ((VARIABLE / VAR) && VARIABLE.Inside == false)
                            if(VARIABLE.GetType().ToString()== VAR.GetType().ToString())
                            {
                                if((VARIABLE.haveChild==false) && (VAR.haveChild == false && VARIABLE.kind==VAR.kind && VAR.age==2 && VARIABLE.age==2 &&VARIABLE.sex!=VAR.sex))
                                {
                                if (VARIABLE.kind == 1 && VARIABLE.sex=="girl") 
                                    {
                                     arr.Add(new Fish(VARIABLE.kind,$"{VAR.name}+{VARIABLE.name}", "boy", 1, active:true));
                                    arr.Add(new Fish(VARIABLE.kind,$"{VARIABLE.name}+{VAR.name}", "girl", 1,active:true));
                                     VARIABLE.haveChild = true;
                                     VAR.haveChild = true;
                                    }
                                    if (VARIABLE.kind == 1 && VARIABLE.sex == "boy")
                                    {
                                        arr.Add(new Fish(VARIABLE.kind, $"{VARIABLE.name}+{VAR.name}", "boy", 1, active: true));
                                        arr.Add(new Fish(VARIABLE.kind, $"{VAR.name}+{VARIABLE.name}", "girl", 1, active: true));
                                        
                                        VARIABLE.haveChild = true;
                                        VAR.haveChild = true;
                                    }
                                    if (VARIABLE.kind == 3 && VARIABLE.sex == "girl")
                                    {
                                        arr.Add(new Killer(VARIABLE.kind, $"{VAR.name}+{VARIABLE.name}", "boy", 1, active: true));
                                        arr.Add(new Killer(VARIABLE.kind, $"{VARIABLE.name}+{VAR.name}", "girl", 1, active: true));
                                        VARIABLE.haveChild = true;
                                        VAR.haveChild = true;
                                    }
                                    if (VARIABLE.kind == 3 && VARIABLE.sex == "boy")
                                    {
                                        arr.Add(new Killer(VARIABLE.kind, $"{VARIABLE.name}+{VAR.name}", "boy", 1, active: true));
                                        arr.Add(new Killer(VARIABLE.kind, $"{VAR.name}+{VARIABLE.name}", "girl", 1, active: true));

                                        VARIABLE.haveChild = true;
                                        VAR.haveChild = true;
                                    }
                                    
                                }
                                if (VARIABLE is Killer==true && VAR is Killer ==false && VAR.vision == true) { 
                                }
                               
                            }




                        if ((VARIABLE / VAR) && VARIABLE.Inside == false )
                        {
                           if (VARIABLE is Killer == true && VAR is Killer==false && VAR.vision==false) 
                           {
                               VARIABLE.Kills[0]++;
                                arr.Remove(VAR);
                               VARIABLE.energy += 5;
                               VARIABLE.weight +=VAR.weight;
                           }
       
                           if (VARIABLE is Killer == true && VAR is Killer == false && VAR.vision == true)
                            {
                                VAR.Move(100, 100);
                                VARIABLE.Move(-100, -100);
                                VAR.vision = false;
                           }
                        }
                        timer2.Enabled = true;
                        timer2.Start();
                        timer3.Enabled = true;
                        timer3.Start();
                       
                        if (VAR is Killer == false)
                        {
                            foreach (var i in arra.ToArray())
                            {
                                if (i.CheckInside(VAR))
                                {
                                    i.empty = false;
                                    VAR.Inside = true;
                                    VAR.x = i.ax + 25;
                                    VAR.y = i.ay + 25;
                                    i.AddFish(VAR);
                                    arr.Remove(VAR);
                                    timer1.Enabled = true;
                                    timer1.Start();


                                }

                            }
                        }
                                               
                   }
               }
               
            }
            
               
            
        }
        
                                
                                
    public void WasteOfEnergy(object sender, EventArgs e)
        {
            foreach (var VAR in arr.ToArray())
            {
                if (VAR.active == true )
                {
                    if (VAR.Energy > 0)
                    {
                        VAR.Energy -= 1;
                      
                    }
                    
                    if (VAR.Energy == 0)
                    {
                        arr.Remove(VAR);
                    }
                }
             }
           
        }
        public void GrowingUp(object sender, EventArgs e)
        {
            foreach (var VAR in arr.ToArray())
            {
                if (VAR.active == true)
                {
                    if (VAR.age < 3)
                    {
                        VAR.age += 1;
                        if (VAR.kind == 1)
                        {
                            arr.Add(new Fish(VAR.kind, VAR.name, VAR.sex, VAR.age, VAR.weight, VAR.Energy,
                                                      VAR.active, VAR.x, VAR.y, VAR.speed, VAR.Inside, VAR.haveChild));
                        }
                        else
                        {
                            Fish tmp =new Killer(VAR.kind, VAR.name, VAR.sex, VAR.age, VAR.weight, VAR.Energy,
                                                      VAR.active, VAR.x, VAR.y, VAR.speed, VAR.Inside, VAR.haveChild);
                            tmp.Kills= (int[])VAR.Kills.Clone();
                            arr.Add(tmp);
                        }
                        arr.Remove(VAR);

                    }
                    else
                    {
                        arr.Remove(VAR);
                    }

                }

            }
            foreach (var var in Algaes.pen.ToArray())
            {
                if (var.active == true)
                {
                    if (var.age < 3)
                    {
                        var.age += 1;
                        if (var.kind == 1)
                        {
                            Algaes.pen.Add(new Fish(var.kind, var.name, var.sex, var.age, var.weight, var.Energy,
                                                      var.active, var.x, var.y, var.speed, var.Inside, var.haveChild));
                        }
                        else
                        {

                            arr.Add(new Killer(var.kind, var.name, var.sex, var.age, var.weight, var.Energy,
                                                      var.active, var.x, var.y, var.speed, var.Inside, var.haveChild));

                        }
                        Algaes.pen.Remove(var);
                        

                    }
                    else
                    {
                        Algaes.pen.Remove(var);
                    }
                }
               
            }
        }
        public void SetVScroll(int v) 
        {
            scry = v;
        }

        public void SetHScroll(int v)
        {
            scrx = v;
        }

        public void SetScreenSize(int _wx, int _wy)
        {
            scrwx = _wx;
            scrwy = _wy;
        }
        public void Draw(Graphics gc, bool windowed)
        {
            if (windowed)
            {
               
                Font g = new Font("Arial", 14);
                Brush b = new SolidBrush(Color.Maroon);
                Pen p = new Pen(Color.Maroon, 2);
                Point[] P = new Point[] 
                { new Point(780 - scrx, 40 - scry),
                new Point(875 - scrx, 40-scry),
                new Point(875 - scrx, 60-scry),
                new Point(780-scrx, 60-scry)};
                RectangleF r = new RectangleF((780 - scrx), (40 - scry), (870 - scrx), (70 - scry));
                gc.DrawPolygon(p, P);
                gc.DrawString("Aquarium", g, b, r);
                gc.DrawLine(new Pen(b, 5), (780 - scrx), (40 - scry), (780 - scrx), (100 - scry));
                gc.DrawImage(bn, (zoox - scrx), (zooy - scry), zoowx, zoowy);
                foreach (Algaes el in arra)
                    el.Draw(gc, true, scrx, scry, scrwx, scrwy);
                foreach (Fish el in arr)
                    el.Draw(gc, true, scrx, scry, scrwx, scrwy);
            }
            else
            {
                
                Font g = new Font("Arial", 14);
                Brush b = new SolidBrush(Color.Maroon);
                Pen p = new Pen(Color.Maroon, 2);
                Point[] P = new Point[] 
                { new Point(780, 40),
                new Point(875, 40),
                new Point(875, 60),
                new Point(780, 60)};
                RectangleF r = new RectangleF(780, 40, 870, 70);
                gc.DrawPolygon(p, P);
                gc.DrawString("Aquarium", g, b, r);
                gc.DrawLine(new Pen(b, 5), 780, 40, 780, 100);
                gc.DrawImage(bn, zoox, zooy, zoowx, zoowy);
                foreach (Algaes el in arra)
                    el.Draw(gc, false, scrx, scry, scrwx, scrwy);
                foreach (Fish el in arr)
                    el.Draw(gc, false, scrx, scry, scrwx, scrwy);
            }                        
        }
        public void Adjust_Viewport(double partX, double partY)
        {
            double dx = (Aquarium.wx * partX) - ((double)scrwx) * 0.5;
            scrx = Convert.ToInt32(dx);
            if (scrx < 0) scrx = 0;
            if ((scrx + scrwx) > Aquarium.wx) scrx = Aquarium.wx - scrwx;
                  
            double dy = (Aquarium.wy * partY) - ((double)scrwy) * 0.5;
            scry = Convert.ToInt32(dy);
            if (scry < 0) scry = 0;
            if ((scry + scrwy) > Aquarium.wy) scry = Aquarium.wy - scrwy;
        }

        public void Zoo_MouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (Fish el in arr)
                    el.MouseClick(e.Location.X, e.Location.Y, scrx, scry, scrwx, scrwy);
            }
        }
        public void Zoo_Keydown(KeyEventArgs e)
        {
            const int dx = 30;
            const int dy = 30;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    foreach (Fish i in arr)
                        if (i != null && i.Inside == false) i.Move(0, -dy);
                    break;
                case Keys.Left:
                    foreach (Fish i in arr)
                        if (i != null && i.Inside == false) i.Move(-dx, 0);
                    break;
                case Keys.Right:
                    foreach (Fish i in arr)
                        if (i != null && i.Inside == false) i.Move(dx, 0);
                    break;
                case Keys.Down:
                    foreach (Fish i in arr)
                        if (i != null && i.Inside == false) i.Move(0, dy);
                    break;
                case Keys.C:
                    foreach (var VAR in arr.ToArray())
                    {
                        if (VAR.isActive() == true && VAR is Killer == true && VAR.Inside == false)
                        {
                            Killer k = new Killer();
                            k = (Killer)(VAR as Killer).Clone();
                            k.x += 20;
                            k.y += 20;
                            k.Inactive();
                            arr.Add(k);
                        }
                    }
                    break;
                case Keys.Delete:
                    for (int i = 0; i < arr.Count; i++)
                    {
                        if (arr[i].isActive())
                        {
                            arr.Remove(arr[i]);
                            i--;
                        }

                    }
                    for (int i = 0; i < Algaes.pen.Count; i++)
                    {
                        if (Algaes.pen[i].isActive())
                        {
                            Algaes.pen.Remove(Algaes.pen[i]);
                            i--;
                        }

                    }
                    break;
                case Keys.Escape:
                    for (int i = 0; i < arr.Count; i++)
                    {
                        if (arr[i].isActive())
                        {
                            arr[i].Inactive();
                        }
                    }
                    for (int i = 0; i < Algaes.pen.Count; i++)
                    {
                        if (Algaes.pen[i].isActive())
                        {
                            Algaes.pen[i].Inactive();
                        }
                    }
                    break;
               
                  case Keys.Enter:
                    for (int i = 0; i < arr.Count; i++)
                    {
                        if (arr[i].isActive()==false)
                        {
                            arr[i].Nactive();
                        }
                     }
                    for (int i = 0; i < Algaes.pen.Count; i++)
                    {
                        if (Algaes.pen[i].isActive() == false)
                        {
                            Algaes.pen[i].Nactive();
                        }
                    }
                    break;

                case Keys.NumPad1:
                    for (int i = 0; i < arr.Count; i++)
                    {
                        if (arr[i] is Killer==false)
                        {
                            arr[i].vision = true;
                        }
                    }
                    for (int i = 0; i < Algaes.pen.Count; i++)
                    {
                        Algaes.pen[i].vision = true;
                    }
                    break;
                case Keys.NumPad2:
                    for (int i = 0; i < arr.Count; i++)
                    {
                        if (arr[i] is Killer == false)
                        {
                            arr[i].vision = false;
                        }
                    }
                    for (int i = 0; i < Algaes.pen.Count; i++)
                    {
                        Algaes.pen[i].vision = false;
                    }
                    break;
                case Keys.Insert:
                    {
                        Form2 NewFish = new Form2();
                        
                       


                        if (NewFish.ShowDialog() == DialogResult.OK)
                        {
                            if (NewFish.TypeOfMicroObject == 1)
                            {
                                Fish p = new Fish(NewFish.TypeOfMicroObject, NewFish.MyName, NewFish.Sex, NewFish.Age, NewFish.Weight, NewFish.Energy,
                                                        NewFish.Active, NewFish.X, NewFish.Y);
                                arr.Add(p);
                            }
                            
                          
                             
                            if (NewFish.TypeOfMicroObject == 3)
                            {
                                Killer p = new Killer (NewFish.TypeOfMicroObject, NewFish.MyName, NewFish.Sex, NewFish.Age, NewFish.Weight, NewFish.Energy,
                                                        NewFish.Active, NewFish.X, NewFish.Y);
                                arr.Add(p);
                            }
                        }
                    }
                    break;
            }
        }
    }
}