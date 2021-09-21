using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Акваріум_формс
{
    public partial class Form2 : Form
    {
       
     
        public string MyName
        {
            get
            {
                string str = textBox1.Text;
                if (str.Length < 1) str = "Anonymous";
                return str;
            }
        }

        public double Weight
        {
            get
            {
                double w;
                try
                {
                    w = Convert.ToDouble(textBox2.Text);
                    if ((w < 0) || (w > 50)) w = 10;
                }
                catch (Exception e)
                {
                    w = 10;
                }
                return w;
            }
        }

        public int Energy
        {
            get
            {
                int d;
                try
                {
                    d = Convert.ToInt16(textBox3.Text);
                    if ((d < 0) || (d > 100)) d = 50;
                }
                catch (Exception e)
                {
                    d = 100;
                }
                return d;
            }
        }
       
        public int X
        {
            get
            {
                int _x;
                try
                {
                    _x = Convert.ToInt16(textBox4.Text);
                }
                catch (Exception e)
                {
                    _x = 0;
                }
                return _x;
            }
        }

        public int Y
        {
            get
            {
                int _y;
                try
                {
                    _y = Convert.ToInt16(textBox5.Text);
                }
                catch (Exception e)
                {
                    _y = 0;
                }
                return _y;
            }
        }
       
        public bool Active
        {
            get
            {
                return checkBox1.Checked;
            }
        }
     
        public int TypeOfMicroObject
        {
            get
            {
                if (radioButton1.Checked == true)
                {
                   
                    return 1;
                }
                else
                {
                    return 3;
                }
            }
        }
        public string Sex
        {
            get
            {
                if(comboBox1.Text!="girl" && comboBox1.Text != "boy")
                {
                    return "boy";
                }
                if (comboBox1.SelectedItem.ToString() != "girl" )

                {
                    return "boy";
                }
                if (comboBox1.SelectedItem.ToString() == "girl")
                {
                    return "girl";
                }
               
                else
                {
                    return "boy";
                }
            }
        }
        public int Age
        {
            get
            {
                if (comboBox2.Text != "1" && comboBox2.Text != "2" && comboBox2.Text != "3")
                {
                    return 1;
                }
                if (comboBox2.SelectedItem.ToString() == "2")
                {
                    return 2;
                }
                if (comboBox2.SelectedItem.ToString() == "3")
                {
                    return 3;
                }
                else
                {
                   return 1;
                }
            }
        }
        public Form2()
        {
            InitializeComponent();
        }


    }
    
}
