using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace CanToptan_Fatura_Control
{
    public partial class Frm_Karşılaştırma_Ayarları : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Karşılaştırma_Ayarları()
        {
            InitializeComponent();
        }

        private void Frm_Karşılaştırma_Ayarları_Load(object sender, EventArgs e)
        {
           
            Txt_BayiKdv.Text = Properties.Settings.Default.BayiKdv;
            Txt_FirmaKDV.Text = Properties.Settings.Default.FirmaKdv;
            Txt_Tolerans.Text = Properties.Settings.Default.Tolerans;
        }

        private void Txt_BayiKdv_TextChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.BayiKdv = Txt_BayiKdv.Text;
            Properties.Settings.Default.Save();
        }

        private void Txt_FirmaKDV_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FirmaKdv = Txt_FirmaKDV.Text;
            Properties.Settings.Default.Save();
        }

        private void Txt_Tolerans_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Tolerans = Txt_Tolerans.Text;
            Properties.Settings.Default.Save();
        }
    }
}
