using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using WindowsFormsApp2.EContactClasses;
using System.IO;
using Font = iTextSharp.text.Font;
using System.Drawing;
using iTextSharp.text.pdf.security;
using Microsoft.Build.Tasks;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

namespace WindowsFormsApp2
{
    public partial class EContact : Form
    {
        public EContact()
        {
            InitializeComponent();
        }
        contactClass cobj = new contactClass();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // get the value from input fields
            cobj.FirstName = txtBoxFirstName.Text;
            cobj.LastName= txtBoxLastName.Text;
            cobj.ContactNumber= txtBoxContactNumber.Text;
            cobj.Address= txtBoxAddress.Text;
            cobj.Gender = cmbGender.Text;

            // insert the data into databse
            bool success = cobj.insert(cobj);
            if(success== true)
            {
                // successfully inserted
                MessageBox.Show("New Contact Successfully Inserted");
                // cal the clear method here
                clear();
            }
            else
            {
                // Failed to add contact
                MessageBox.Show("Failed to Insert Contact, Try Again...");
            }

            // Load data on datagrid table
            DataTable dt = cobj.select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EContact_Load(object sender, EventArgs e)
        {
            // Load data on datagrid table
            DataTable dt = cobj.select();
            dgvContactList.DataSource = dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // call the clear method here
            clear();
        }

        // method to clear all fields after adding data
        public void clear()
        {
            txtboxContactId.Text = "";
            txtBoxFirstName.Text = "";
            txtBoxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // get the data from textBoxes
            cobj.ContactId = int.Parse(txtboxContactId.Text);
            cobj.FirstName = txtBoxFirstName.Text;
            cobj.LastName = txtBoxLastName.Text;
            cobj.ContactNumber = txtBoxContactNumber.Text;
            cobj.Address = txtBoxAddress.Text;
            cobj.Gender = cmbGender.Text;

            // upadate data into database
            bool success = cobj.update(cobj);
            if(success== true)
            {
                // updated successfully 
                MessageBox.Show("Contact has been successfully Updated");
                // Load data on datagrid table
                DataTable dt = cobj.select();
                dgvContactList.DataSource = dt;
                // call the clear method
                clear();
            }
            else
            {
                // Failed to update
                MessageBox.Show("Contact Failed to update");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // get data from Data grid view and load it to in text box
            // identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxContactId.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // get the contact id from the application
            cobj.ContactId = Convert.ToInt32(txtboxContactId.Text);
            bool success = cobj.Delete(cobj);
            if (success)
            {
                // successfully deleted
                MessageBox.Show("Contact information deleted successfully");
                // refresh the datagrid table
                clear();
                DataTable dt = cobj.select();
                dgvContactList.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Contact Delete Failed");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* Make Pdf with Itextsharp from database data by selecting data with query */


           /* SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["EContactConString"].ToString());

            SqlCommand insert = new SqlCommand();
            insert.CommandText = "select FirstName,LastName from tbl_contact where IsPrinted = 0";
            insert.Connection = cn;
            cn.Open();
            SqlDataReader dr = null;
            dr = insert.ExecuteReader();
            while (dr.Read())
            {
                int i = 0;
                string path = Application.StartupPath + ""; ;
                path = path + "/" + dr[i] + ".pdf";
                Document doc1 = new Document(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(doc1, new FileStream(path, FileMode.Create));
                doc1.Open();
                Font georgia1 = FontFactory.GetFont("georgia", 12f);
                Chunk c1 = new Chunk(" " + "FirstName:   " + dr[0], georgia1);
                Chunk c2 = new Chunk(" " + "LastName:   " + dr[1], georgia1);

                Phrase ph1 = new Phrase();
                Phrase ph2 = new Phrase();

                ph1.Add(c1);
                ph2.Add(c2);

                Paragraph p1 = new Paragraph();
                p1.Add(ph1);
                Paragraph p2 = new Paragraph();
                p2.Add(ph2);

                doc1.Add(p1);
                doc1.Add(p2);

                doc1.CloseDocument();
                doc1.Close();
                doc1.Dispose();
                ++i;
                cobj.IsPrinted = false;
                // upadate data into database
                bool success = cobj.update(cobj);
                if (success == true)
                {
                    // updated successfully 
                    MessageBox.Show(dr[i] + "Pdf Downloaded");
                    // Load data on datagrid table
                    DataTable dt = cobj.select();
                    dgvContactList.DataSource = dt;
                    // call the clear method
                    clear();
                }
                else
                {
                    // Failed to update
                    MessageBox.Show("Contact Failed to Download");
                }

            }
            dr.Close();
            cn.Close(); */

            if (dgvContactList.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Result.pdf";
                bool ErrorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Unable to wride data in disk" + ex.Message);
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(dgvContactList.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            foreach (DataGridViewColumn col in dgvContactList.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow viewRow in dgvContactList.Rows)
                            {
                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {
                                    pTable.AddCell(dcell.Value.ToString());
                                }
                            }
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Data Export Successfully", "info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while exporting Data" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record Found", "Info");
            }
        }

        //private void RDLCExportToPdf()
        //{
        //    string deviceInfo = "";
        //    string streamIds;
        //    Microsoft.Build.Tasks.Warning[] warnings;

        //    string mimeType = string.Empty;
        //    string encoding = string.Empty;
        //    string extension = string.Empty;

        //    ReportViewer viewer = new ReportViewer();
        //    viewer.ProcessingMode = ProcessingMode.Local;
        //    viewer.LocalReport.ReportPath = "PdfReport.rdlc";
        //    viewer.LocalReport.DataSources.Add(new ReportDataSource("EContactDS", GetContactInfo()));
        //}

        //private List<contactClass> GetContactInfo()
        //{
        //    return new List<contactClass> { 

        //        new contactClass{FirstName = Firs}

        //        };
        //}
    }
}
