using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Акваріум_формс
{
    public partial class Fishes  : Form
    {
      
        BufferedGraphicsContext bgc;
        BufferedGraphics bg;
        Bitmap buffered_bitmap;
        private static int viewport_bx;
        private static int viewport_by;
        private static int viewport_wx;
        private static int viewport_wy;  
        Aquarium vin;
        private MyVScrollBar myVScrollBar;
        private MyHScrollBar myHScrollBar;
        private static int ScrollBarWidth=25;
        Timer timer = new Timer();
        public static bool already = false;
        public static Fishes form1;
        delegate void Zoo_Proc();
        static Zoo_Proc myproc = new Zoo_Proc(Aquarium.ZooMove);
        IAsyncResult iar;


        public void Organize_Graphics(Graphics g)
        {
            lock (Fishes.form1)
            {
                g.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);
                vin.Draw(g, true);

                Graphics gbmp = Graphics.FromImage(buffered_bitmap);
                gbmp.FillRectangle(Brushes.White, 0, 0, Aquarium.wx, Aquarium.wy);
                vin.Draw(gbmp, false);

                g.DrawImage(buffered_bitmap, Fishes.viewport_bx, Fishes.viewport_by, Fishes.viewport_wx, Fishes.viewport_wy);
                g.DrawRectangle(Pens.Black, Fishes.viewport_bx, Fishes.viewport_by, Fishes.viewport_wx, Fishes.viewport_wy);
            }            
        }

        public Fishes()
        {
            InitializeComponent();
            Fishes.form1 = this;
            vin = new Aquarium();

            this.DoubleBuffered = false;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint , true);    
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
                
            bgc = BufferedGraphicsManager.Current;
            bgc.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            bg = bgc.Allocate(this.CreateGraphics(),new Rectangle(0, 0, this.Width, this.Height));

            Fishes.viewport_bx = this.Width - 234;
            Fishes.viewport_by = 25;
            Fishes.viewport_wx = 200;
            Fishes.viewport_wy = 200;

            buffered_bitmap = new Bitmap(Aquarium.wx, Aquarium.wy);

            Organize_Graphics(bg.Graphics);

            myVScrollBar = new MyVScrollBar();
            myVScrollBar.Left = this.ClientRectangle.Size.Width - Fishes.ScrollBarWidth;
            myVScrollBar.Top = 25;
            myVScrollBar.Height = this.ClientRectangle.Size.Height - Fishes.ScrollBarWidth;
            myVScrollBar.Width = Fishes.ScrollBarWidth;
            myVScrollBar.ValueChanged += this.vScrollBar1_ValueChanged;
            myVScrollBar.Anchor = AnchorStyles.Right;
            this.Controls.Add(myVScrollBar);

            myHScrollBar = new MyHScrollBar();
            myHScrollBar.Left =0;
            myHScrollBar.Top=this.ClientRectangle.Size.Height- Fishes.ScrollBarWidth;
            myHScrollBar.Height = Fishes.ScrollBarWidth;
            myHScrollBar.Width = this.ClientRectangle.Size.Width - Fishes.ScrollBarWidth;
            myHScrollBar.ValueChanged += this.hScrollBar1_ValueChanged;
            myHScrollBar.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.Controls.Add(myHScrollBar);

            myHScrollBar.Minimum = 0;
            myHScrollBar.Maximum = Aquarium.wx-750;
            myVScrollBar.Minimum = 0;
            myVScrollBar.Maximum = Aquarium.wy-750;

            timer.Tick += new EventHandler(timer_Tick); 
            timer.Interval = 100;                
            timer.Enabled = true;              
            timer.Start(); 
        }

        delegate void delegate_form_Invalidate();

        public void form_Invalidate()
        {
            bool InvokeRequired;
            lock (Fishes.form1)
            {
                InvokeRequired = Fishes.form1.InvokeRequired;
            }

            if (!InvokeRequired)
            {
                Organize_Graphics(bg.Graphics);
                bg.Render(Graphics.FromHwnd(this.Handle));
            }
            else
            {
                delegate_form_Invalidate d = new delegate_form_Invalidate(form_Invalidate);
                this.Invoke(d);
            }

        }
        
        void timer_Tick(object sender, EventArgs e)
        {
            lock (Fishes.form1)
            {
                vin.Zoo_TimerTick(e);
                Organize_Graphics(bg.Graphics);
                bg.Render(Graphics.FromHwnd(this.Handle));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            vin.SetScreenSize(this.Width, this.Height); 
            bg.Render(e.Graphics);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            vin.SetScreenSize(this.Width, this.Height);

            if (e.Button == MouseButtons.Left)
            {
                if ((e.Location.X > Fishes.viewport_bx) &&
                    (e.Location.X < (Fishes.viewport_bx + Fishes.viewport_wx)) &&
                    (e.Location.Y > Fishes.viewport_by) &&
                    (e.Location.Y < (Fishes.viewport_by + Fishes.viewport_wy))
                    )
                {
                    double partX = ((double)(e.Location.X - Fishes.viewport_bx)) / ((double)Fishes.viewport_wx);
                    double partY = ((double)(e.Location.Y - Fishes.viewport_by)) / ((double)Fishes.viewport_wy);
                    vin.Adjust_Viewport(partX, partY);
                }
                else
                    vin.Zoo_MouseClick(e);
            }
            Organize_Graphics(bg.Graphics);
            bg.Render(Graphics.FromHwnd(this.Handle));
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Fishes.already)
            {
                Fishes.already = true;

                iar = Fishes.myproc.BeginInvoke(null, null);
            }


            vin.SetScreenSize(this.Width, this.Height);
            vin.Zoo_Keydown(e);
            Organize_Graphics(bg.Graphics);
            bg.Render(Graphics.FromHwnd(this.Handle));

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override bool IsInputKey(Keys keyData)
        {            
            if ((keyData == Keys.Up) || (keyData == Keys.Down)
                 || (keyData == Keys.Left) || keyData == Keys.Right)
            {
                return true;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            vin.SetVScroll(myVScrollBar.Value);
            vin.SetScreenSize(this.Width, this.Height);
            Organize_Graphics(bg.Graphics);
            bg.Render(Graphics.FromHwnd(this.Handle));
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            vin.SetHScroll(myHScrollBar.Value);
            vin.SetScreenSize(this.Width, this.Height);
            Organize_Graphics(bg.Graphics);
            bg.Render(Graphics.FromHwnd(this.Handle));
        }

        private void serializeTotxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
              SaveFileDialog saveFileDialog1 = new SaveFileDialog();
              saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
              saveFileDialog1.RestoreDirectory = false;
              if (saveFileDialog1.ShowDialog() == DialogResult.OK)
              {
                  string fileToWrite = saveFileDialog1.FileName;
                  StreamWriter myWriter = new StreamWriter(fileToWrite);
                  vin.Save(myWriter);
                  myWriter.Close();
              }
        } 

        private void deserializeFromTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileToRead = openFileDialog1.FileName;
                StreamReader myReader = new StreamReader(fileToRead);
                bool needstart = Fishes.already;
                vin.Load(myReader);
                myReader.Close();
            }
        }

        private void slowlyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer.Start(); 
            timer.Enabled = false;
            timer.Interval = 500;
            timer.Enabled = true;
        }

        private void fastToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer.Start(); 
            timer.Enabled = false;
            timer.Interval = 100;
            timer.Enabled = true;
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            Aquarium.timer2.Stop();
            Aquarium.timer3.Stop();
        }
       
    }

    public class MyVScrollBar: VScrollBar
    {
        protected override bool IsInputKey(Keys keyData)
        {
            if ( (keyData == Keys.Up) || (keyData == Keys.Down)
                 || (keyData == Keys.Left) || keyData == Keys.Right )
            {
                return false;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }
    }

    public class MyHScrollBar : HScrollBar
    {
        protected override bool IsInputKey(Keys keyData)
        {
            if ((keyData == Keys.Up) || (keyData == Keys.Down)
                 || (keyData == Keys.Left) || keyData == Keys.Right)
            {
                return false;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }


    }
}
