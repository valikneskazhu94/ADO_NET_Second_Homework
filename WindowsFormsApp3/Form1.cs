using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        SqlConnection connect = null;
        SqlCommand command = null;
        SqlDataReader reader = null;
        string connection = null;
        public Form1()
        {
            connection = @"Data Source=COMP801\SQLEXPRESS;Initial Catalog=Sales;Integrated Security=True;";
            InitializeComponent();
            AddTableData();
        }

        private void AddTableData()
        {
            connect = new SqlConnection(connection);
            string select = "select table_name from INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'base table' and table_name not like 'sys%';";

            try
            {
                connect.Open();
                command = new SqlCommand(select, connect);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["table_name"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect?.Close();
                command?.Clone();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from "+comboBox1.Text,connect);

            DataSet set = new DataSet();
            dataGridView1.DataSource = null;
            adapter.Fill(set);

            dataGridView1.DataSource = set.Tables[0];
        }
    }
}
