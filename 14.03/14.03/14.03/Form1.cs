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

namespace _14._03
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=HOME-ПК; Initial Catalog=Sales; Integrated Security=true;";
        DataTable dataTable = null;
        SqlConnection sqlConnection = null;
        SqlDataReader reader = null;
        public Form1()
        {
            sqlConnection = new SqlConnection(connectionString);
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            try
            {
                sqlConnection.Open();
                string query = "select * from information_schema.tables";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(2));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                if (reader != null)
                    reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = $"select * from {comboBox1.SelectedItem}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                dataGridView1.DataSource = null;
                sqlConnection.Open();
                dataTable = new DataTable();
                reader = sqlCommand.ExecuteReader();
                int line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                        for (int i = 0; i < reader.FieldCount; i++)
                            dataTable.Columns.Add(reader.GetName(i));
                    line++;
                    DataRow row = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        row[i] = reader[i];
                    dataTable.Rows.Add(row);
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
