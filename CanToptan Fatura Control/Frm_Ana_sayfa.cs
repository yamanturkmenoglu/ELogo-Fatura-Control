using CanToptan_Fatura_Control.EArşiv;
using CanToptan_Fatura_Control.irsaliye;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CanToptan_Fatura_Control
{
    public partial class Frm_Ana_sayfa : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Frm_Ana_sayfa()
        {
            InitializeComponent();
        }
        Frm_Giden_Efatura Frm;
        private void Btn_GidenEFatura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Frm == null || Frm.IsDisposed)
            {
                Frm = new Frm_Giden_Efatura();
                Frm.MdiParent = this;
                Frm.Show();
            }
        }
        Frm_Firma_Tanımlama Frm1;
        private void Btn_FTanımla_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

           
                Frm1 = new Frm_Firma_Tanımlama();
               
                Frm1.Show();
            
        }
        Frm_Firma_Bilgileri_Güncelle Frm2;
        private void Btn_BglDuzenle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Frm2 = new Frm_Firma_Bilgileri_Güncelle();

            Frm2.Show();
        }

        Frm_Karşılaştırma_Ayarları Frm3;
        private void Btn_Karsilastirme_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm3 = new Frm_Karşılaştırma_Ayarları();
            Frm3.Show();
        }

        Frm_Gelen_Efatura Frm4;
        private void Btn_Gelen_Efatura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Frm4 == null || Frm4.IsDisposed)
            {
                Frm4 = new Frm_Gelen_Efatura();
                Frm4.MdiParent = this;
                Frm4.Show();
            }
        }
        Frm_Giden_Eirsaliye Frm6;
        private void Btn_Giden_İrsaliye_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Frm6 == null || Frm6.IsDisposed)
            {
                Frm6 = new Frm_Giden_Eirsaliye();
                Frm6.MdiParent = this;
                Frm6.Show();
            }
        }
        Frm_Gelen_Eirsaliye Frm7;
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Frm7 == null || Frm7.IsDisposed)
            {
                Frm7 = new Frm_Gelen_Eirsaliye();
                Frm7.MdiParent = this;
                Frm7.Show();
            }
        }
        Frm_Giden_Earsiv Frm5;
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Frm5 == null || Frm5.IsDisposed)
            {
                Frm5 = new Frm_Giden_Earsiv();
                Frm5.MdiParent = this;
                Frm5.Show();
            }
        }
    }
}
