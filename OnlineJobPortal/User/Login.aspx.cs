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
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        string str = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        String username, password = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLoginType.SelectedValue == "Admin")
                {
                    username = ConfigurationManager.AppSettings["username"];

                    password = ConfigurationManager.AppSettings["password"];
                    if (username == txtUserName.Text.Trim() && password == txtPassword.Text.Trim())
                    {
                        Session["admin"] = username;
                        Response.Redirect("../Admin/Dashboard.aspx", false);
                        
                    }
                    else
                    {
                        showErrorMsg("Admin");
                    }
                }
                else
                {
                    con = new SqlConnection(str);
                    string query = @"Select * from [User] where Username = @Username ana Password = @Password";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        Session["user"] = sdr["Username"].ToString();
                        Session["userId"] = sdr["UserId"].ToString();
                        Response.Redirect("Default.aspx", false);

                    }
                    else
                    {
                        showErrorMsg("User");

                    }
                    con.Close();

                }

            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('" + ex.Message + "');</Script>");
                con.Close();

            }
        }
        private void showErrorMsg(string userType)
        {

            lblmsg.Visible = true;
            lblmsg.Text = "<b>" + userType + "</b> credential are incorrect..!";
            lblmsg.CssClass = "alert alert.danger";

        }
    }
}