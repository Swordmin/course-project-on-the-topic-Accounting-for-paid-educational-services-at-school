using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace SchollPaidEducationalServices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Initialization();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить договор?", "Удаление", MessageBoxButtons.YesNo);
                if (zz == DialogResult.Yes)
                {


                    string script = ($"DELETE FROM statement WHERE ID = {value2} ");
                    MSDataFill(script, _connectData, dataGridView1);
                }
            }
            catch { MessageBox.Show("Неверно введены данные"); }
        }
        private DataTable _table;
        public MySqlConnection _mycon;
        public MySqlCommand _mycom;
        private string _connectData = "Server=localhost;Database=ychet;Uid=root;pwd=12345";
        public DataSet ds;

        private void Initialization()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            string  script = "Select id,fullname as ФИО,subject as Предмет from teachers";
            string script2 = "Select id,fullnameparents as ФИОродителя,fullnamechildren as ФИОученика,subject as предмет,class as класс,date as дата,address as адрес,phone as телефон from statement";
            string script4 = "Select service.id,service.title as Название, service.priceperlesson as Цена_за_занятие, service.thedateofthe as Дата_проведения, service.numberofclasses as количество_занятий,teachers.fullname as ФИО, `Groups`.numbergroups as Номер_группы  from service join teachers on teachers.id = service.teachers join `Groups` on `Groups`.id = service.groups_ID";
            string script3 = "Select  `Groups`.id, `Groups`.numberstudents as Номер_Студента, `Groups`.numbergroups as Номер_группы, statement.fullnamechildren as ФИО_ученика  from `Groups` join statement on  `Groups`.FullNameStudents = statement.id";
            MSDataFill(script3, _connectData, dataGridView2);
            MSDataFill(script4, _connectData, dataGridView4);
            MSDataFill(script, _connectData, dataGridView6);
            MSDataFill(script2, _connectData, dataGridView1);
            dataGridView6.Columns[0].Visible = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView4.Columns[0].Visible = false;
            dataGridView2.Columns[0].Visible = false;

            MSAdapter($"SELECT ID  FROM `Groups` WHERE numbergroups='{comboBox11.Text}'", comboBox12, "id", "id"); //Это для услуг
            MSAdapter($"SELECT ID  FROM teachers WHERE fullname='{comboBox8.Text}'", comboBox9, "id", "id");

            //MSDataAdapterFill("SELECT ID,fullnameChildren  FROM statement", comboBox3, _table, "fullnameChildren", "id");

            //const string connStr1 = "Server=localhost;Database=YCHET;Uid=root;pwd=12345;charset=utf8";
            //DataTable patientTable = new DataTable();
            //MySqlConnection myConnection = new MySqlConnection(connStr1);
            //{
            //    MySqlCommand command = new MySqlCommand("SELECT ID,fullnamechildren  FROM statement", myConnection);
            //    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            //    adapter.Fill(patientTable);
            //}
            //comboBox3.DataSource = patientTable;
            //comboBox3.DisplayMember = "fullnamechildren";
            //comboBox3.ValueMember = "id";
            Adapter(comboBox3,"SELECT ID,fullnamechildren  FROM statement", "fullnamechildren","id");//Группы

            Adapter(comboBox8, "SELECT ID,fullname  FROM teachers", "fullname", "id");//Услуги
            Adapter(comboBox11, "SELECT ID,numbergroups  FROM `Groups`", "numbergroups", "id");

           
        }
        public static void Adapter(ComboBox comboBox,string script,string member, string value  ) 
        {
            const string connStr1 = "Server=localhost;Database=YCHET;Uid=root;pwd=12345;charset=utf8";
            DataTable patientTable = new DataTable();
            MySqlConnection myConnection = new MySqlConnection(connStr1);
            {
                MySqlCommand command = new MySqlCommand(script, myConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(patientTable);
            }
            comboBox.DataSource = patientTable;
            comboBox.DisplayMember = member;
            comboBox.ValueMember = value;
        }
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "ychet";
            string username = "root";
            string password = "12345";
            try
            {
                return GetDBConnection(host, port, database, username, password);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return null;
        }
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection SqlConnection = new MySqlConnection(connString);

            return SqlConnection;
        }
        public string value1;
        public string value2;
        public string value3;
        public string value4;
        private void MSDataFill(string script, string connect, DataGridView dataGridView)
        {
            try
            {
                _table = new DataTable();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(script, _mycon);
                ms_data.Fill(_table);
                dataGridView.DataSource = _table;
                _mycon.Close();
                //_table.Clear();
            }
            catch (Exception exeption)
            {
                MessageBox.Show("" + exeption);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
                      
            
                string script = "Select id,fullname as ФИО,subject as Предмет from teachers";
                MSDataFill(script, _connectData, dataGridView6);
            dataGridView6.Columns[0].Visible = false;
            //MSDataAdapterFill("SELECT ID,Class  FROM Class", comboBox25, _table, "id", "Class");



        }

        private void button19_Click(object sender, EventArgs e)
        {
            string script = "Select * from Attendance";
            MSDataFill(script, _connectData, dataGridView5);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
            string script = "Select service.id,service.title as Название, service.priceperlesson as Цена_за_занятие, service.thedateofthe as Дата_проведения, service.numberofclasses as количество_занятий,teachers.fullname as ФИО, `Groups`.numbergroups as Номер_группы  from service join teachers on teachers.id = service.teachers join `Groups` on `Groups`.id = service.groups_ID";
            MSDataFill(script, _connectData, dataGridView4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string script = "Select id,fullnameparents as ФИОродителя,fullnamechildren as ФИОученика,subject as предмет,class as класс,date as дата,address as адрес,phone as телефон from statement";
            MSDataFill(script, _connectData, dataGridView1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Adapter(comboBox11, "SELECT ID,numbergroups  FROM `Groups`", "numbergroups", "id");
            const string connStr1 = "Server=localhost;Database=YCHET;Uid=root;pwd=12345;charset=utf8";
            DataTable patientTable = new DataTable();
            MySqlConnection myConnection = new MySqlConnection(connStr1);
            {
                MySqlCommand command = new MySqlCommand("SELECT ID,fullnamechildren  FROM statement", myConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(patientTable);
            }
            comboBox3.DataSource = patientTable;
            comboBox3.DisplayMember = "fullnamechildren";
            comboBox3.ValueMember = "id";
            string script = "Select  `Groups`.id, `Groups`.numberstudents as Номер_Студента, `Groups`.numbergroups as Номер_группы, statement.fullnamechildren as ФИО_ученика  from `Groups` join statement on  `Groups`.FullNameStudents = statement.id";
            MSDataFill(script, _connectData, dataGridView2);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string script = "Select * from contract";
            MSDataFill(script, _connectData, dataGridView3);
        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            string script = $"insert into teachers(fullname,subject) value('{textBox7.Text}','{textBox8.Text}')";
            MSDataFill(script, _connectData, dataGridView6);
        }

        private void dataGridView6_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            value1 = dataGridView6.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox7.Text = dataGridView6.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox8.Text = dataGridView6.Rows[e.RowIndex].Cells[2].Value.ToString();

        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить договор?", "Удаление", MessageBoxButtons.YesNo);
                if (zz == DialogResult.Yes)
                {


                    string script = ($"DELETE FROM teachers WHERE ID = {value1} ");
                    MSDataFill(script, _connectData, dataGridView6);
                }
            }
            catch { MessageBox.Show("Неверно введены данные"); }

        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                string script = ($"UPDATE teachers SET  fullname='{textBox7.Text}',subject='{textBox8.Text}' WHERE ID = {value1} ");
                MSDataFill(script, _connectData, dataGridView5);
            }
            catch { MessageBox.Show("Неверно введены данные"); }

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            string script = ("SELECT fullname as ФИО,subject as Предмет FROM teachers WHERE (((fullname)Like \"%" + textBox17.Text + "%\"));");
            MSDataFill(script, _connectData, dataGridView6);

        }
        private void MSDataAdapterFill(string cmdText, ComboBox comboBox = null, DataTable dataTable = null, string displayNubmer = null, string valueNumber = null)
        {
            try
            {
                MySqlConnection myConnection = GetDBConnection();
                {
                    _table = new DataTable();
                    MySqlCommand command = new MySqlCommand(cmdText, _mycon);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(_table);
                    //_table.Clear();
                }
                if (comboBox != null)
                {
                    comboBox.DataSource = dataTable;
                    comboBox.DisplayMember = displayNubmer;
                    comboBox.ValueMember = valueNumber;
                }
                //
            }
            catch (Exception exeption)
            {
                MessageBox.Show("" + exeption);
            }
        }
        public  static void MSAdapter(string script,ComboBox comboBox,string member, string value) 
        {
            const string connStr1 = "Server=localhost;Database=YCHET;Uid=root;pwd=12345;charset=utf8";
            DataTable patientTable = new DataTable();
            MySqlConnection myConnection = new MySqlConnection(connStr1);
            {
                MySqlCommand command = new MySqlCommand(script, myConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(patientTable);
            }
            comboBox.DataSource = patientTable;
            comboBox.DisplayMember = member;
            comboBox.ValueMember = value;
        }
        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM teachers WHERE fullname='{comboBox8.Text}'", comboBox9, "id", "id");
            //MSDataAdapterFill($"SELECT ID  FROM teachers WHERE fullname='{comboBox8.Text}'", comboBox9, _table, "id", "id");
            //const string connStr1 = "Server=localhost;Database=YCHET;Uid=root;pwd=12345;charset=utf8";
            //DataTable patientTable = new DataTable();
            //MySqlConnection myConnection = new MySqlConnection(connStr1);
            //{
            //    MySqlCommand command = new MySqlCommand($"SELECT ID  FROM teachers WHERE fullname='{comboBox8.Text}'", myConnection);
            //    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            //    adapter.Fill(patientTable);
            //}
            //comboBox9.DataSource = patientTable;
            //comboBox9.DisplayMember = "id";
            //comboBox9.ValueMember = "id";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string pablo = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            value2 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
           pablo = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pablo = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string script = $"insert into statement(fullnameparents,fullnamechildren,subject,class,date,address,phone) value('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{pablo}','{textBox5.Text}','{textBox6.Text}')";
            MSDataFill(script, _connectData, dataGridView6);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string pablo = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string script = ($"UPDATE statement SET fullnameparents = '{textBox1.Text}',fullnamechildren = '{textBox2.Text}', subject = '{textBox3.Text}',class = '{textBox4.Text}',date = '{pablo}',address = '{textBox5.Text}',phone = '{textBox6.Text}', WHERE ID = {value2} ");
                MSDataFill(script, _connectData, dataGridView1);
            }
            catch { MessageBox.Show("Неверно введены данные"); }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            const string connStr1 = "Server=localhost;Database=YCHET;Uid=root;pwd=12345;charset=utf8";
            DataTable patientTable = new DataTable();
            MySqlConnection myConnection = new MySqlConnection(connStr1);
            {
                MySqlCommand command = new MySqlCommand($"SELECT ID  FROM statement WHERE fullnamechildren='{comboBox3.Text}'", myConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(patientTable);
            }
            comboBox10.DataSource = patientTable;
            comboBox10.DisplayMember = "id";
            comboBox10.ValueMember = "id";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try 
            {

                string script = $"insert into  `Groups`(numberstudents, numbergroups, FullNameStudents) value ({textBox15.Text},{textBox16.Text},{comboBox10.Text})";
                MSDataFill(script, _connectData, dataGridView2);
            }
            catch (Exception exeption)
            {
                MessageBox.Show("" + exeption);
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить договор?", "Удаление", MessageBoxButtons.YesNo);
                if (zz == DialogResult.Yes)
                {


                    string script = ($"DELETE FROM `Groups` WHERE ID = {value3} ");
                    MSDataFill(script, _connectData, dataGridView2);
                }
            }
            catch { MessageBox.Show("Неверно введены данные"); }
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            value3 = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox15.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox16.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox3.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
  
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string script = ($"UPDATE `Groups` SET numberstudents = '{textBox15.Text}',numbergroups = '{textBox16.Text}', FullNameStudents = '{comboBox10.Text}'  WHERE ID = {value3} ");
            MSDataFill(script, _connectData, dataGridView2);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string pablo = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            string script = ($"Insert into service(id,title,priceperlesson,thedateofthe,numberofclasses,teachers,groups_id) value (null,'{textBox9.Text}',{textBox11.Text},'{pablo}',{textBox12.Text},{comboBox9.Text},{comboBox12.Text}) ");
            MSDataFill(script, _connectData, dataGridView4);
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
       
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            MSAdapter($"SELECT ID  FROM `Groups` WHERE numbergroups='{comboBox11.Text}'", comboBox12, "id", "id");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить договор?", "Удаление", MessageBoxButtons.YesNo);
                if (zz == DialogResult.Yes)
                {


                    string script = ($"DELETE FROM Service WHERE ID = {value4} ");
                    MSDataFill(script, _connectData, dataGridView4);
                }
            }
            catch { MessageBox.Show("Неверно введены данные"); }
        }

        private void dataGridView4_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string pablo = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            value4 = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox9.Text = dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox11.Text = dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString();
            pablo = dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox12.Text = dataGridView4.Rows[e.RowIndex].Cells[4].Value.ToString();
            comboBox8.Text = dataGridView4.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox11.Text = dataGridView4.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string pablo = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            string script = ($"Update service set title='{textBox9.Text}',priceperlesson={textBox11.Text},thedateofthe='{pablo}',numberofclasses={textBox12.Text},teachers={comboBox9.Text},groups_id={comboBox12.Text} where id = {value4}");
            MSDataFill(script, _connectData, dataGridView4);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
