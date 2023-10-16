using Dapper;
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

namespace WindowsFormsAppTest
{
    public partial class Form1 : Form
    {
        private readonly AppDbContext _context;
        private readonly SqlConnection _sqlConnection;
        public Form1()
        {
            InitializeComponent();
            AppConfigService appConfigService=new AppConfigService();
            _context = new AppDbContext(appConfigService.GetDbConnection());
            _sqlConnection = new SqlConnection(appConfigService.GetDbConnection());


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            

            MessageBox.Show("Hello");
            }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
             BlogDataModel blog = new BlogDataModel()
                         {
                             BlogTitle = txtTitle.Text,
                             BlogAuthor=txtAuthor.Text,
                             BlogContent=txtContent.Text
                         };


            #region EF
            /*_context.Blogs.Add(blog);
                         var result = _context.SaveChanges();
                         */
            #endregion


            #region Dapper
            /* string query = $@"INSERT INTO [dbo].[Blog]
            ([BlogTitle]
            ,[BlogAuthor]
            ,[BlogContent])
      VALUES
            (@BlogTitle
            ,@BlogAuthor
            ,@BlogContent)";

             IDbConnection db = new SqlConnection(_sqlConnection.ConnectionString);
             var result = db.Execute(query, blog);  */

            #endregion

            #region ADO
            SqlConnection connection = new SqlConnection(_sqlConnection.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Blog] ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES(@BlogTitle,@BlogAuthor,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            #endregion


            string message = result > 0 ? "Save Sucess" : "Save error";
            MessageBox.Show(message, "Alert", MessageBoxButtons.OK, result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            txtTitle.Clear();
            txtAuthor.Clear();
            txtContent.Clear();
            txtTitle.Focus();
        }
    }
}
