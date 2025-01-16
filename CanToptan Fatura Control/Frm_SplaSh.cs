using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanToptan_Fatura_Control
{
    public partial class Frm_SplaSh : Form
    {
        public Frm_SplaSh()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            panel2.Width += 3;
            if (panel2.Width >= 607)
            {
                timer1.Stop();
               
                this.Hide();
               
            }
        }
    }
    }

