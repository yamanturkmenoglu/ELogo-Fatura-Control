﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CanToptan_Fatura_Control.irsaliye
{
    public partial class Frm_Gelen_Eirsaliye : Form
    {
        public Frm_Gelen_Eirsaliye()
        {
            InitializeComponent();

            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem multicompareItem = new ToolStripMenuItem("Toplu Karşılaştır");
            ToolStripMenuItem compareItem = new ToolStripMenuItem("Karşılaştır");
            ToolStripMenuItem examineItem = new ToolStripMenuItem("İncele");
            ToolStripMenuItem Excel = new ToolStripMenuItem("Excele Aktar");



            gridControl1.ContextMenuStrip = contextMenu;


            compareItem.Click += CompareItem_Click;
            examineItem.Click += ExamineItem_Click;
            multicompareItem.Click += multiCompareItem_Click;
            Excel.Click += Excel_Click;



            contextMenu.Items.Add(compareItem);
            contextMenu.Items.Add(examineItem);
            contextMenu.Items.Add(multicompareItem);
            contextMenu.Items.Add(Excel);
        }

        private void Frm_Gelen_Eirsaliye_Load(object sender, EventArgs e)
        {
            FillType();
            CmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
        }

        SqlConnectionClass bgl = new SqlConnectionClass();
        private string elogoPass;
        private string elogoUser;
        public string firmaNo;
        public string donemNo;
        public string serverName;
        public string userName;
        public string userPass;
        public string databaseName;
        private int selectedId = -1;


        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbType.SelectedValue != null && int.TryParse(CmbType.SelectedValue.ToString(), out selectedId))
            {
                if (selectedId != -1)
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
                                        elogoPass = reader["ElogoPass"].ToString();
                                        elogoUser = reader["ELogoUser"].ToString();
                                        firmaNo = reader["FirmaNo"].ToString();
                                        donemNo = reader["DonemNo"].ToString();
                                        serverName = reader["ServerName"].ToString();
                                        userName = reader["UserName"].ToString();
                                        userPass = reader["UserPass"].ToString();
                                        databaseName = reader["DatabaseName"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    elogoPass = string.Empty;
                    elogoUser = string.Empty;
                    firmaNo = string.Empty;
                    serverName = string.Empty;
                    databaseName = string.Empty;
                    userName = string.Empty;
                    databaseName = string.Empty;
                    donemNo = string.Empty;
                }
            }
        }


        private void FillType()
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    string query = "SELECT ID, Tanım FROM MEFA_Connection";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                
                DataRow newRow = dataTable.NewRow();
                newRow["ID"] = -1;
                newRow["Tanım"] = "Firma Seç";
                dataTable.Rows.InsertAt(newRow, 0);

                
                CmbType.DataSource = dataTable;
                CmbType.DisplayMember = "Tanım";
                CmbType.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<string> LoginAndGetDocumentList(string username, string password, string beginDate, string endDate)
        {
            string soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                                 xmlns:tem='http://tempuri.org/'
                                 xmlns:efat='http://schemas.datacontract.org/2004/07/eFaturaWebService'>
                                 <soapenv:Header/>
                                 <soapenv:Body>
                                     <tem:Login>
                                         <tem:login>
                                             <efat:passWord>{password}</efat:passWord>
                                             <efat:userName>{username}</efat:userName>
                                         </tem:login>
                                     </tem:Login>
                                 </soapenv:Body>
                                 </soapenv:Envelope>";

            string loginResponse = await SendSoapRequest(soapEnvelope);

            if (loginResponse.Contains("<LoginResult>true</LoginResult>"))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(loginResponse);
                string sessionId = doc.GetElementsByTagName("sessionID")[0].InnerText;



                string documentListResponse = SendFilteredSoapRequest(sessionId, beginDate, endDate);
                return documentListResponse;
            }
            else if (loginResponse.Contains("<faultstring xml:lang=\"tr-TR\">Hatalı kullanıcı adı veya şifre</faultstring>"))
            {
                MessageBox.Show("E-Logo kullanıcı adı veya şifre hatalıdır");
                return null;
            }
            else
            {
                MessageBox.Show("Bilinmeyen bir hata oluştu");
                return null;
            }
        }

        private async Task<string> SendSoapRequest(string soapEnvelope)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                content.Headers.ContentType.CharSet = "utf-8";
                content.Headers.Add("SOAPAction", "http://tempuri.org/IPostBoxService/Login");

                using (HttpResponseMessage response = await client.PostAsync("https://pb.elogo.com.tr/PostboxService.svc", content))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
        private string SendFilteredSoapRequest(string sessionID, string beginDate, string endDate)
        {
            string soapRequest = $@"
     <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                                  xmlns:tem='http://tempuri.org/'
                                  xmlns:arr='http://schemas.microsoft.com/2003/10/Serialization/Arrays'>
                  <soapenv:Header/>
                  <soapenv:Body>
                    <tem:GetDocumentList>
                      <tem:sessionID>{sessionID}</tem:sessionID>
                      <tem:paramList>
                        <arr:string>DOCUMENTTYPE= DESPATCHADVICEDETAIL</arr:string>
                        <arr:string>OPTYPE=2</arr:string>
                        <arr:string>BEGINDATE={beginDate}</arr:string>
                        <arr:string>ENDDATE={endDate}</arr:string>
                        <arr:string>DATEBY=0</arr:string>
                      </tem:paramList>
                    </tem:GetDocumentList>
                  </soapenv:Body>
                </soapenv:Envelope>";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pb.elogo.com.tr/PostboxService.svc");
            request.Headers.Add("SOAPAction", "http://tempuri.org/IPostBoxService/GetDocumentList");
            request.ContentType = "text/xml; charset=utf-8";
            request.Method = "POST";

            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapRequest);
                stream.Write(content, 0, content.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        private DataTable ParseSoapResponse(string soapResponse)
        {
            XDocument doc = XDocument.Parse(soapResponse);
            XNamespace ns = "http://schemas.datacontract.org/2004/07/eFaturaWebService";


            DataTable dt = new DataTable();
            dt.Columns.Add("İrsaliye Numarası");
            dt.Columns.Add("İrsaliye Tipi");
            dt.Columns.Add("Gönderici Unvan");
            dt.Columns.Add("VKN/TCKN");
            dt.Columns.Add("Oluşturma Tarihi");
            dt.Columns.Add("Para Birimi");
            dt.Columns.Add("Senaryo");
            dt.Columns.Add("ETTN");
            dt.Columns.Add("Toplam Tutar");
            dt.Columns.Add("Durum");


            var documents = doc.Descendants(ns + "Document");

            foreach (var document in documents)
            {
                var docInfoElement = document.Descendants(ns + "docInfo").FirstOrDefault();
                if (docInfoElement != null)
                {
                    var jsonStr = docInfoElement.Value;
                    var jsonObj = JObject.Parse(jsonStr);


                    DataRow row = dt.NewRow();
                    row["İrsaliye Numarası"] = jsonObj["ElementId"]?.ToString();

                    row["İrsaliye Tipi"] = jsonObj["DespatchAdviceTypeCode"]?.ToString();

                    row["Gönderici Unvan"] = jsonObj["SupplierPartyName"]?.ToString();
                    row["VKN/TCKN"] = jsonObj["SupplierVknTckn"]?.ToString();


                    string issueDateStr = jsonObj["IssueDate"]?.ToString();
                    if (!string.IsNullOrEmpty(issueDateStr) && issueDateStr.Length >= 10)
                    {
                        row["Oluşturma Tarihi"] = issueDateStr.Substring(0, 10);
                    }
                    else
                    {
                        row["Oluşturma Tarihi"] = issueDateStr;
                    }

                    row["Para Birimi"] = jsonObj["CurrencyUnit"]?.ToString();
                    row["Senaryo"] = jsonObj["ProfileID"]?.ToString();
                    row["ETTN"] = jsonObj["Uuid"]?.ToString();
                    row["Toplam Tutar"] = jsonObj["ValueAmount"]?.ToString();

                    dt.Rows.Add(row);

                }
            }



            return dt;
        }

        private void HandleWebException(WebException ex)
        {
            using (WebResponse response = ex.Response)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)response;
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    MessageBox.Show("Geçersiz session id. Lütfen önce Elogoya giriş yapın !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResponse = reader.ReadToEnd();
                        XDocument doc = XDocument.Parse(soapResponse);
                        XNamespace soapNs = "http://schemas.xmlsoap.org/soap/envelope/";
                        XNamespace detailNs = "http://schemas.datacontract.org/2004/07/eFaturaWebService";

                        XElement faultElement = doc.Descendants(soapNs + "Fault").FirstOrDefault();
                        if (faultElement != null)
                        {
                            XElement faultStringElement = faultElement.Element(soapNs + "faultstring");
                            if (faultStringElement != null)
                            {
                                string faultString = faultStringElement.Value;
                                MessageBox.Show(faultString, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            XElement detailElement = doc.Descendants(detailNs + "ResultType").FirstOrDefault();
                            if (detailElement != null)
                            {
                                XElement resultMsgElement = detailElement.Element(detailNs + "resultMsg");
                                if (resultMsgElement != null)
                                {
                                    string resultMsg = resultMsgElement.Value;
                                    MessageBox.Show(resultMsg, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }
        private async void Btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string username = elogoUser;
                string password = elogoPass;
                string firma = firmaNo;
                string donem = donemNo;

                string beginDate = DtpFirst.Text;
                string endDate = DtpLast.Text;

                string soapResponse = await LoginAndGetDocumentList(username, password, beginDate, endDate);

                if (!string.IsNullOrEmpty(soapResponse))
                {

                    DataTable dt = ParseSoapResponse(soapResponse);


                    gridControl1.DataSource = dt;
                }
            }
            catch (WebException ex)
            {
                HandleWebException(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Excel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControl1.ExportToXlsx(saveFileDialog.FileName);
            }
        }
        private void multiCompareItem_Click(object sender, EventArgs e)
        {
            
            DataTable dataSource = gridControl1.DataSource as DataTable;

            if (dataSource == null)
            {
                MessageBox.Show("GridControl kaynağı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = $"Server={serverName};Database={databaseName};User Id={userName};Password={userPass};";
            string tableInvoice = $"LG_{firmaNo}_{donemNo}_STFICHE";
            string tableStline = $"LG_{firmaNo}_{donemNo}_STLINE";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataRow row in dataSource.Rows)
                {
                    string faturaNo = row["İrsaliye Numarası"]?.ToString();
                    if (string.IsNullOrEmpty(faturaNo)) continue;

                    decimal elogoToplam = Convert.ToDecimal(row["Toplam Tutar"] ?? "0");

                    string query = $@"
               SELECT 
                        STF.FICHENO as [İrsaliye Numarası],
                        STF.NETTOTAL AS [Toplam Tutar]
                    FROM {tableStline} STL
                    LEFT JOIN {tableInvoice} STF ON STF.LOGICALREF = STL.STFICHEREF
                    WHERE STL.TRCODE IN (7, 8, 1, 6) 
                      AND STL.LINETYPE IN (0, 1) 
                      AND STL.LINENET <> 0
                      and STF.GRPCODE = 1
                     AND STF.FICHENO = @FICHENO;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FICHENO", faturaNo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal tigerToplam = Convert.ToDecimal(reader["Toplam Tutar"] ?? 0);

                            decimal tolerans = Convert.ToDecimal(Properties.Settings.Default.Tolerans ?? "0");

                            bool toplamFark = Math.Abs(tigerToplam - elogoToplam) > tolerans;

                            if (!toplamFark)
                            {
                                row["Durum"] = "Fark Yok";
                            }
                            else
                            {
                                row["Durum"] = $"Toplam Tutar Farkı: Tiger = {tigerToplam}, Elogo = {elogoToplam}\n";
                            }
                        }
                        else
                        {
                            row["Durum"] = "Fatura bilgisi bulunamadı.";
                        }
                    }
                }
            }
            gridView1.Columns["Durum"].Width = 500;

            gridControl1.RefreshDataSource();

            MessageBox.Show("Tüm karşılaştırmalar tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CompareItem_Click(object sender, EventArgs e)
        {

            int[] selectedRows = gridView1.GetSelectedRows();
            if (selectedRows.Length > 0)
            {
                int selectedRowIndex = selectedRows[0];
                string faturaNo = gridView1.GetRowCellValue(selectedRowIndex, "İrsaliye Numarası")?.ToString();
                decimal elogoToplam = Convert.ToDecimal(gridView1.GetRowCellValue(selectedRowIndex, "Toplam Tutar") ?? "0");

                string connectionString = $"Server={serverName};Database={databaseName};User Id={userName};Password={userPass};";
                string tableInvoice = $"LG_{firmaNo}_{donemNo}_STFICHE";
                string tableStline = $"LG_{firmaNo}_{donemNo}_STLINE";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = null;
                    query = $@"
                       SELECT 
                        STF.FICHENO as [İrsaliye Numarası],
                        STF.NETTOTAL AS [Toplam Tutar]
                    FROM {tableStline} STL
                    LEFT JOIN {tableInvoice} STF ON STF.LOGICALREF = STL.STFICHEREF
                    WHERE STL.TRCODE IN (7, 8, 1, 6) 
                      AND STL.LINETYPE IN (0, 1) 
                      AND STL.LINENET <> 0
                      and STF.GRPCODE = 1
                     AND STF.FICHENO = @FICHENO;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FICHENO", faturaNo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal tigerToplam = Convert.ToDecimal(reader["Toplam Tutar"] ?? 0);
                           

                            
                            decimal tolerans = Convert.ToDecimal(Properties.Settings.Default.Tolerans ?? "0");

                            bool toplamFark = Math.Abs(tigerToplam - elogoToplam) > tolerans;
                          

                            if (!toplamFark)
                            {
                                MessageBox.Show("Karşılaştırma tamamlandı: Fark yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string farkMesajı = $"Fatura Numarası: {faturaNo}\n";
                                if (toplamFark)
                                    farkMesajı += $"Toplam Tutar Farkı: Tiger = {tigerToplam}, Elogo = {elogoToplam}\n";
                                

                                MessageBox.Show(farkMesajı, "Fark Tespit Edildi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Fatura bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen karşılaştırmak için bir satır seçin.");
            }
        }

        private async void ExamineItem_Click(object sender, EventArgs e)
        {
            string username = elogoUser;
            string password = elogoPass;

            try
            {
               
                string soapEnvelope = $@"
            <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                               xmlns:tem='http://tempuri.org/'
                               xmlns:efat='http://schemas.datacontract.org/2004/07/eFaturaWebService'>
                <soapenv:Header/>
                <soapenv:Body>
                    <tem:Login>
                        <tem:login>
                            <efat:passWord>{password}</efat:passWord>
                            <efat:userName>{username}</efat:userName>
                        </tem:login>
                    </tem:Login>
                </soapenv:Body>
            </soapenv:Envelope>";

                
                string loginResponse = await SendSoapRequestLog(soapEnvelope);

                string sessionId = ExtractSessionId(loginResponse);


                int[] selectedRows = gridView1.GetSelectedRows();
                if (selectedRows.Length > 0)
                {
                    int selectedRowIndex = selectedRows[0];
                    string uuid = gridView1.GetRowCellValue(selectedRowIndex, "ETTN").ToString();

                    if (!string.IsNullOrEmpty(sessionId) && !string.IsNullOrEmpty(uuid))
                    {
                        string documentRequest = $@"
                <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:tem='http://tempuri.org/' xmlns:arr='http://schemas.microsoft.com/2003/10/Serialization/Arrays'>
                    <soapenv:Header/>
                    <soapenv:Body>
                        <tem:GetDocumentData>
                            <tem:sessionID>{sessionId}</tem:sessionID>
                            <tem:uuid>{uuid}</tem:uuid>
                            <tem:paramList>
                                <arr:string>DOCUMENTTYPE=DESPATCHADVICE</arr:string>
                                <arr:string>DATAFORMAT=PDF</arr:string>
                            </tem:paramList>
                        </tem:GetDocumentData>
                    </soapenv:Body>
                </soapenv:Envelope>";

                       
                        string responseString = await SendSoapRequestPDF("https://pb.elogo.com.tr/PostboxService.svc", documentRequest);

                        string base64Zip = ExtractBase64ZipFromResponse(responseString);

                        if (!string.IsNullOrEmpty(base64Zip))
                        {
                            
                            byte[] zipBytes = Convert.FromBase64String(base64Zip);
                            string tempZipPath = Path.Combine(Path.GetTempPath(), "document.zip");

                     
                            File.WriteAllBytes(tempZipPath, zipBytes);

                          
                            string pdfPath = ExtractPdfFromZip(tempZipPath);
                            if (!string.IsNullOrEmpty(pdfPath))
                            {
                             
                                Frm_pdf pdf = new Frm_pdf(pdfPath);
                                pdf.Show();
                            }
                            else
                            {
                                MessageBox.Show("PDF dosyası zip içinde bulunamadı.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Zip verisi bulunamadı.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("SessionID veya uuid alınamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}");
            }
        }



        private async Task<string> SendSoapRequestLog(string soapEnvelope)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                    content.Headers.ContentType.CharSet = "utf-8";
                    content.Headers.Add("SOAPAction", "http://tempuri.org/IPostBoxService/Login");

                    using (HttpResponseMessage response = await client.PostAsync("https://pb.elogo.com.tr/PostboxService.svc", content))
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SOAP isteği gönderilirken hata oluştu", ex);
            }
        }


        private string ExtractSessionId(string response)
        {
            XDocument soapResponse = XDocument.Parse(response);
            XNamespace ns = "http://tempuri.org/";

            return soapResponse.Descendants(ns + "sessionID").FirstOrDefault()?.Value;
        }
        private async Task<string> SendSoapRequestPDF(string url, string soapRequest)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
                content.Headers.Add("SOAPAction", "http://tempuri.org/IPostBoxService/GetDocumentData");

                var response = await httpClient.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
        }
        private string ExtractBase64ZipFromResponse(string responseString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseString);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("a", "http://schemas.datacontract.org/2004/07/eFaturaWebService");

            XmlNode node = doc.SelectSingleNode("//a:binaryData/a:Value", nsmgr);
            return node?.InnerText;
        }

        private string ExtractPdfFromZip(string zipPath)
        {
            string pdfPath = null;
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        pdfPath = Path.Combine(Path.GetTempPath(), entry.FullName);
                        entry.ExtractToFile(pdfPath, true);
                        break;
                    }
                }
            }
            return pdfPath;
        }
      
            

    }
}
