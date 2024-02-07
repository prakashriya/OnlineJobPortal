using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.User
{
    public partial class Register : System.Web.UI.Page
    {
        
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(str);
                string query = @"Insert into [User] (Username,Password,Name,Address,Mobile,Email,Country) values (@Username,@Password,@Name,@Address,@Mobile,@Email,@Country)";
                cmd = new SqlCommand(query, con);
               
                cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtConfirmPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Registered Successfully ";
                    lblmsg.CssClass = "alert alert.success";
                    Clear();

                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "cannot save records right now, please try after sometimes...!";
                    lblmsg.CssClass = "alert alert.danger";


                }

            }
            catch (SqlException ex)
            {
                if(ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "<b>" +txtUserName.Text.Trim() + "</b> username already exist, try now one..!";
                    lblmsg.CssClass = "alert alert.danger";


                }
                else
                {
                    Response.Write("<Script>alert('" + ex.Message + "');</Script>");

                }
            }
            catch(Exception ex)
            {
                    Response.Write("<Script>alert('" + ex.Message + "');</Script>");

            }
            finally
            {
                con.Close();
            }


        }

        private void Clear()
        {
            txtUserName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty; 
            txtFullName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            ddlCountry.ClearSelection();
        }

      
        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

       
        
    }
}