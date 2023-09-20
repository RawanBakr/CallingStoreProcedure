using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace StoreProcedure
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGeteployeeid ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                if (radioButton1.Checked == false) { cmd.Parameters.AddWithValue("@gender", radioButton2.Text); }
                else
                {
                    cmd.Parameters.AddWithValue("@gender", radioButton1.Text);
                }
                cmd.Parameters.AddWithValue("@salary", textBox2.Text);

                SqlParameter outputparameter = new SqlParameter();
                outputparameter.ParameterName = "@Employeeid";
                outputparameter.DbType =DbType.Int32;
                outputparameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputparameter);

                conn.Open();
                cmd.ExecuteNonQuery();
                string @empid= outputparameter.Value.ToString();
                Console.WriteLine("Employee ID is = "+ @empid);
            }
        }
    }
}