using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Windows.Forms;

namespace CanToptan_Fatura_Control
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            BonusSkins.Register();
            SkinManager.EnableFormSkins();

           
            using (var splash = new Frm_SplaSh())
            {
                splash.ShowDialog(); 
            }

            Application.Run(new Frm_Ana_sayfa());
        }
    }
}
