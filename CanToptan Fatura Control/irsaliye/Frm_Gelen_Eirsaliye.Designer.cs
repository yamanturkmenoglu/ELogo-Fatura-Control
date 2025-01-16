
namespace CanToptan_Fatura_Control.irsaliye
{
    partial class Frm_Gelen_Eirsaliye
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Gelen_Eirsaliye));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.Btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.DtpLast = new System.Windows.Forms.DateTimePicker();
            this.DtpFirst = new System.Windows.Forms.DateTimePicker();
            this.CmbType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 70);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(800, 378);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.Btn_Search);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.DtpLast);
            this.panelControl1.Controls.Add(this.DtpFirst);
            this.panelControl1.Controls.Add(this.CmbType);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Location = new System.Drawing.Point(0, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 68);
            this.panelControl1.TabIndex = 4;
            // 
            // Btn_Search
            // 
            this.Btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Search.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Btn_Search.Appearance.Options.UseFont = true;
            this.Btn_Search.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Search.ImageOptions.Image")));
            this.Btn_Search.Location = new System.Drawing.Point(704, 11);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(84, 38);
            this.Btn_Search.TabIndex = 25;
            this.Btn_Search.Text = "Filtrele";
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 18);
            this.label2.TabIndex = 24;
            this.label2.Text = "Tarih";
            // 
            // DtpLast
            // 
            this.DtpLast.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpLast.Location = new System.Drawing.Point(194, 35);
            this.DtpLast.Name = "DtpLast";
            this.DtpLast.Size = new System.Drawing.Size(99, 21);
            this.DtpLast.TabIndex = 23;
            // 
            // DtpFirst
            // 
            this.DtpFirst.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFirst.Location = new System.Drawing.Point(81, 35);
            this.DtpFirst.Name = "DtpFirst";
            this.DtpFirst.Size = new System.Drawing.Size(95, 21);
            this.DtpFirst.TabIndex = 22;
            // 
            // CmbType
            // 
            this.CmbType.FormattingEnabled = true;
            this.CmbType.Location = new System.Drawing.Point(81, 8);
            this.CmbType.Name = "CmbType";
            this.CmbType.Size = new System.Drawing.Size(212, 21);
            this.CmbType.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(12, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 18);
            this.label7.TabIndex = 20;
            this.label7.Text = "Firma";
            // 
            // Frm_Gelen_Eirsaliye
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "Frm_Gelen_Eirsaliye";
            this.Text = "Gelen E-İrsaliye";
            this.Load += new System.EventHandler(this.Frm_Gelen_Eirsaliye_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton Btn_Search;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtpLast;
        private System.Windows.Forms.DateTimePicker DtpFirst;
        private System.Windows.Forms.ComboBox CmbType;
        private System.Windows.Forms.Label label7;
    }
}