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
    public partial class Frm_pdf : DevExpress.XtraEditors.XtraForm
    {
        public Frm_pdf(string pdfPath)
        {
            InitializeComponent();
            pdfViewer1.LoadDocument(pdfPath);
        }
    }
}
