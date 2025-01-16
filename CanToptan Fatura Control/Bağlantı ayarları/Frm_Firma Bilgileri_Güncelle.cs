using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanToptan_Fatura_Control
{
    public partial class Frm_Firma_Bilgileri_Güncelle : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Firma_Bilgileri_Güncelle()
        {
            InitializeComponent();
        }
        SqlConnectionClass bgl = new SqlConnectionClass();

        private void Frm_Firma_Bilgileri_Güncelle_Load(object sender, EventArgs e)
        {
            FillType();
            CmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
        }
        private void FillType()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    string query = "SELECT ID, Tanım FROM MEFA_Connection";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);


                            CmbType.DataSource = dataTable;
                            CmbType.DisplayMember = "Tanım";
                            CmbType.ValueMember = "ID";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Veri yüklenirken bir hata oluştu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbType.SelectedValue != null)
            {
                // Seçilen değeri kontrol et
                if (int.TryParse(CmbType.SelectedValue.ToString(), out int selectedId))
                {
                    try
                    {
                        using (SqlConnection conn = bgl.baglanti()) 
                        {
                            string query = "SELECT * FROM MEFA_Connection WHERE ID = @ID";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", selectedId);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read()) 
                                    {
                                        txtServerNo.Text = reader["ServerName"].ToString();
                                        txtBaseName.Text = reader["DatabaseName"].ToString();
                                        txtUserNo.Text = reader["UserName"].ToString();
                                        txtPass.Text = reader["UserPass"].ToString();
                                        txtFirma.Text = reader["FirmaNo"].ToString();
                                        txtDonem.Text = reader["DonemNo"].ToString();
                                        txtEUser.Text = reader["ELogoUser"].ToString();
                                        txtEPass.Text = reader["ElogoPass"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Veri yüklenirken bir hata oluştu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Btn_Kaydet_Click(object sender, EventArgs e)
        {
            if (CmbType.SelectedValue != null)
            {
                // Seçilen ID'yi kontrol et
                if (int.TryParse(CmbType.SelectedValue.ToString(), out int selectedId))
                {
                    try
                    {
                        using (SqlConnection conn = bgl.baglanti())
                        {
                            string updateQuery = @"
                        UPDATE MEFA_Connection 
                        SET 
                            ServerName = @ServerName, 
                            DatabaseName = @DatabaseName, 
                            UserName = @UserName, 
                            UserPass = @UserPass, 
                            FirmaNo = @FirmaNo, 
                            DonemNo = @DonemNo, 
                            ELogoUser = @ELogoUser, 
                            ElogoPass = @ELogoPass 
                        WHERE ID = @ID";

                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                // Güncellenen alanları parametrelerle bağla
                                cmd.Parameters.AddWithValue("@ServerName", txtServerNo.Text);
                                cmd.Parameters.AddWithValue("@DatabaseName", txtBaseName.Text);
                                cmd.Parameters.AddWithValue("@UserName", txtUserNo.Text);
                                cmd.Parameters.AddWithValue("@UserPass", txtPass.Text);
                                cmd.Parameters.AddWithValue("@FirmaNo", txtFirma.Text);
                                cmd.Parameters.AddWithValue("@DonemNo", txtDonem.Text);
                                cmd.Parameters.AddWithValue("@ELogoUser", txtEUser.Text);
                                cmd.Parameters.AddWithValue("@ELogoPass", txtEPass.Text);
                                cmd.Parameters.AddWithValue("@ID", selectedId);

                                cmd.ExecuteNonQuery();
                            }

                            XtraMessageBox.Show("Kayıt başarıyla güncellendi.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Kayıt güncellenirken bir hata oluştu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçin.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Btn_Sil_Click(object sender, EventArgs e)
        {

            if (CmbType.SelectedValue != null)
            {
                // Seçilen ID'yi kontrol et
                if (int.TryParse(CmbType.SelectedValue.ToString(), out int selectedId))
                {
                    try
                    {
                        using (SqlConnection conn = bgl.baglanti())
                        {
                            string deleteQuery = "DELETE FROM MEFA_Connection WHERE ID = @ID";

                            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", selectedId);
                                cmd.ExecuteNonQuery();
                            }

                            XtraMessageBox.Show("Kayıt başarıyla silindi.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillType(); // ComboBox'u güncelle
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Kayıt silinirken bir hata oluştu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen silinecek bir kayıt seçin.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}