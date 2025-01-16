
namespace CanToptan_Fatura_Control
{
    partial class Frm_Karşılaştırma_Ayarları
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Karşılaştırma_Ayarları));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_BayiKdv = new DevExpress.XtraEditors.TextEdit();
            this.Txt_FirmaKDV = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_Tolerans = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_BayiKdv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_FirmaKDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Tolerans.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bayi KDV Hizmet Kodu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Firma KDV Hizmet Kodu";
            // 
            // Txt_BayiKdv
            // 
            this.Txt_BayiKdv.Location = new System.Drawing.Point(195, 36);
            this.Txt_BayiKdv.Name = "Txt_BayiKdv";
            this.Txt_BayiKdv.Size = new System.Drawing.Size(142, 20);
            this.Txt_BayiKdv.TabIndex = 3;
            this.Txt_BayiKdv.TextChanged += new System.EventHandler(this.Txt_BayiKdv_TextChanged);
            // 
            // Txt_FirmaKDV
            // 
            this.Txt_FirmaKDV.Location = new System.Drawing.Point(195, 72);
            this.Txt_FirmaKDV.Name = "Txt_FirmaKDV";
            this.Txt_FirmaKDV.Size = new System.Drawing.Size(142, 20);
            this.Txt_FirmaKDV.TabIndex = 4;
            this.Txt_FirmaKDV.TextChanged += new System.EventHandler(this.Txt_FirmaKDV_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(12, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Karşılaştırma Tolerans Değeri";
            // 
            // Txt_Tolerans
            // 
            this.Txt_Tolerans.Location = new System.Drawing.Point(195, 111);
            this.Txt_Tolerans.Name = "Txt_Tolerans";
            this.Txt_Tolerans.Size = new System.Drawing.Size(142, 20);
            this.Txt_Tolerans.TabIndex = 6;
            this.Txt_Tolerans.TextChanged += new System.EventHandler(this.Txt_Tolerans_TextChanged);
            // 
            // Frm_Karşılaştırma_Ayarları
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 172);
            this.Controls.Add(this.Txt_Tolerans);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Txt_FirmaKDV);
            this.Controls.Add(this.Txt_BayiKdv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Frm_Karşılaştırma_Ayarları.IconOptions.LargeImage")));
            this.MaximizeBox = false;
            this.Name = "Frm_Karşılaştırma_Ayarları";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Karşılaştırma Ayarları";
            this.Load += new System.EventHandler(this.Frm_Karşılaştırma_Ayarları_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Txt_BayiKdv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_FirmaKDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Tolerans.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit Txt_BayiKdv;
        private DevExpress.XtraEditors.TextEdit Txt_FirmaKDV;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit Txt_Tolerans;
    }
}