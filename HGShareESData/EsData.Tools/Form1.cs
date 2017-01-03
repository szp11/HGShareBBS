using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EsData.Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string connectionstr = connectionstring.Text;
            string sql = mysql.Text;


            using (var conn = new SqlConnection(connectionstr))
            {
                try
                {
                    conn.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;

                        var ds = new DataSet();
                        var adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);

                        if (ds.Tables.Count > 0)
                        {
                            myjson.Text = JsonConvert.SerializeObject(ds.Tables[0]);
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error");
                    return;
                }
            }  


        }
    }
}
