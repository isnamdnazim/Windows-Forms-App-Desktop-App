using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.EContactClasses
{
    internal class contactClass
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool IsPrinted { get; set; }

        static string myConSting = ConfigurationManager.ConnectionStrings["EContactConString"].ConnectionString;

        //selecting data from database
        public DataTable select()
        {
            //step 1 database Connection
            SqlConnection con = new SqlConnection(myConSting);
            DataTable dt = new DataTable();
            try
            {
                // step 2 writing a sql query
                string sql = "select * from tbl_contact";
                // creating cmd using sql and con
                SqlCommand cmd = new SqlCommand(sql, con);
                // creating sql dataAdapter using cmd
                SqlDataAdapter adapter= new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex) 
            {

            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        // inserting data into database
        public bool insert(contactClass cobj)
        {
            // creating a default return type and setting its value to false
            bool isSuccess = false;
            // step 1 connect database
            SqlConnection con = new SqlConnection(myConSting);
            try
            {
                // step 2 create a sql query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName,ContactNumber,Address,Gender,IsPrinted) values(@FirstName, @LastName,@ContactNumber,@Address,@Gender,@IsPrinted)";
                // creating sql cmd useing sql and con
                SqlCommand cmd = new SqlCommand(sql, con);
                // create parameter to add data
                cmd.Parameters.AddWithValue("@FirstName", cobj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", cobj.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", cobj.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", cobj.Address);
                cmd.Parameters.AddWithValue("@Gender", cobj.Gender);
                cmd.Parameters.AddWithValue("@IsPrinted", cobj.IsPrinted);

                // open the connection
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows will be greter then zero
                if(rows > 0)
                {
                    isSuccess= true;
                }
                else
                {
                    isSuccess= false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }

        // update data into the database
        public bool update (contactClass cobj)
        {
            // creating a default return type and setting its value to false
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myConSting);
            try
            {
                // sql query to update database
                //cobj.IsPrinted= true;
                string sql = "UPDATE tbl_contact set FirstName=@FirstName,LastName=@LastName,ContactNumber=@ContactNumber,Address=@Address,Gender=@Gender,@IsPrinted WHERE ContactId=@ContactId";
                // creating sql cmd 
                SqlCommand cmd = new SqlCommand(sql, con);
                // create parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", cobj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", cobj.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", cobj.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", cobj.Address);
                cmd.Parameters.AddWithValue("@Gender", cobj.Gender);
                cmd.Parameters.AddWithValue("@ContactId", cobj.ContactId);
                cmd.Parameters.AddWithValue("@IsPrinted", true);
                // open database connection
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows will be greter then zero
                if (rows > 0)
                {
                    isSuccess= true;
                }
                else
                {
                    isSuccess= false;
                }
            }
            catch(Exception ex)
            {

            }
            finally 
            { 
                con.Close(); 
            }

            return isSuccess;
        }

        // update IsPrinted Value
        //public bool updateIsPrinted(contactClass cobj)
        //{
        //    bool isSuccess = false;
        //    SqlConnection con = new SqlConnection(myConSting);
        //    try
        //    {
        //        // sql query to update database
        //        string sql = "UPDATE tbl_contact setIsPrinted=@IsPrinted WHERE isPrinted=0";
        //        // creating sql cmd 
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cobj.IsPrinted= true;
        //        con.Open();
        //        int rows = cmd.ExecuteNonQuery();
        //        // if the query runs successfully then the value of rows will be greter then zero
        //        if (rows > 0)
        //        {
        //            isSuccess = true;
        //        }
        //        else
        //        {
        //            isSuccess = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return isSuccess;
        //}


        // delete method for delete data from database

        public bool Delete (contactClass cobj)
        {
            // creating a default return type and setting its value to false
            bool isSuccess = false;
            // create sql connection
            SqlConnection con = new SqlConnection(myConSting);
            try
            {
                // Sql to delete data
                string sql = "DELETE FROM tbl_contact WHERE ContactId = @ContactId";
                // create a sql cmd
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ContactId", cobj.ContactId);
                // open the connection
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows will be greter then zero
                if(rows > 0)
                {
                    isSuccess= true;
                }
                else
                {
                    isSuccess= false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }

    }
}
