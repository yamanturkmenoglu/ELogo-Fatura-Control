using DevExpress.XtraEditors;
using System;

using System.Data.SqlClient;

using System.Windows.Forms;

namespace CanToptan_Fatura_Control
{
    public partial class Frm_Firma_Tanımlama : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Firma_Tanımlama()
        {
            InitializeComponent();
        }
        SqlConnectionClass bgl = new SqlConnectionClass();
        private void Btn_Kaydet_Click(object sender, EventArgs e)
        {
           
            string serverName = txtServerNo.Text;
            string dbName = txtBaseName.Text;
            string user = txtUserNo.Text;
            string password = txtPass.Text;
            string firmaNo = txtFirma.Text;
            string donemNo = txtDonem.Text;
            string tanim = txtTanim.Text;
            string eUser = txtEUser.Text;
            string ePass = txtEPass.Text;

            
            if (string.IsNullOrWhiteSpace(serverName) || string.IsNullOrWhiteSpace(dbName) || string.IsNullOrWhiteSpace(user) ||
               string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(firmaNo) || string.IsNullOrWhiteSpace(donemNo) ||
               string.IsNullOrWhiteSpace(tanim) || string.IsNullOrWhiteSpace(eUser) || string.IsNullOrWhiteSpace(ePass))
            {
                XtraMessageBox.Show("Lütfen tüm alanları doldurun.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = bgl.baglanti()) 
                {
                    
                    string checkTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MEFA_Connection')
                    BEGIN
                        CREATE TABLE MEFA_Connection (
                            ID INT IDENTITY(1,1) PRIMARY KEY,
                            Tanım NVARCHAR(100) NOT NULL,
                            [ServerName] NVARCHAR(100) NOT NULL,
                            [UserName] NVARCHAR(100) NOT NULL,
                            [UserPass] NVARCHAR(100) NOT NULL,
                            [DatabaseName] NVARCHAR(100) NOT NULL,
                            [FirmaNo] NVARCHAR(50) NOT NULL,
                            DonemNo NVARCHAR(50) NOT NULL,
                            [ELogoUser] NVARCHAR(100) NOT NULL,
                            [ElogoPass] NVARCHAR(100) NOT NULL
                        )
                    END";

                    using (SqlCommand cmd = new SqlCommand(checkTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                   
                    string insertDataQuery = @"
                    INSERT INTO MEFA_Connection (Tanım, [ServerName], [UserName], [UserPass], [DatabaseName], [FirmaNo], DonemNo, [ELogoUser], [ElogoPass])
                    VALUES (@Tanim, @ServerName, @User, @Password, @DbName, @FirmaNo, @DonemNo, @EUser, @EPass)";

                    using (SqlCommand cmd = new SqlCommand(insertDataQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Tanim", tanim);
                        cmd.Parameters.AddWithValue("@ServerName", serverName);
                        cmd.Parameters.AddWithValue("@User", user);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@DbName", dbName);
                        cmd.Parameters.AddWithValue("@FirmaNo", firmaNo);
                        cmd.Parameters.AddWithValue("@DonemNo", donemNo);
                        cmd.Parameters.AddWithValue("@EUser", eUser);
                        cmd.Parameters.AddWithValue("@EPass", ePass);

                        cmd.ExecuteNonQuery();
                    }

                    XtraMessageBox.Show("Bağlantı başarıyla tamamlandı.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   
                    ClearFormFields();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Bağlantı başarısız: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFormFields()
        {
            txtServerNo.Text = string.Empty;
            txtBaseName.Text = string.Empty;
            txtUserNo.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtFirma.Text = string.Empty;
            txtDonem.Text = string.Empty;
            txtTanim.Text = string.Empty;
            txtEUser.Text = string.Empty;
            txtEPass.Text = string.Empty;
        }
    }
}