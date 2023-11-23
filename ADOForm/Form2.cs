using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
namespace ADOForm
{
    public partial class Form2 : Form
    {
        new  Form1 Parent;
        DBClass dbc = new DBClass();  //*****DBClass 객체 생성

        private void ClearTextBoxes()
        {
            txtid.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtMail.Clear();
        }
        
        public Form2()
        {
            InitializeComponent();
            dbc.DB_Open();//***
        }

        private void UpBtn_Click(object sender, EventArgs e)
        {
            if (NameList.SelectedIndex != 0)
            {
                NameList.SelectedIndex = NameList.SelectedIndex - 1;
            }
            else
            {
                MessageBox.Show("이곳은 레코드의 처음입니다.");
            }
        }

        private void DownBtn_Click(object sender, EventArgs e)
        {
            if (NameList.SelectedIndex != NameList.Items.Count - 1)
            {
                NameList.SelectedIndex = NameList.SelectedIndex + 1;
            }
            else
            {
                MessageBox.Show("이곳은 레코드의 마지막입니다.");
            }
        }

        private void NameList_SelectedIndexChanged(object sender, EventArgs e)
        {
             //DS.Clear();
             //DBAdapter.Fill(DS, "Phone");
            Parent = (Form1)Owner;
            dbc.PhoneTable = dbc.DS.Tables["Phone"];

            DataRow[] ResultRows = dbc.PhoneTable.Select("PName like '%" + Parent.TxtS + "%'");

            DataColumn[] PrimaryKey = new DataColumn[1];
            PrimaryKey[0] = dbc.PhoneTable.Columns["id"];
            dbc.PhoneTable.PrimaryKey = PrimaryKey;

            DataRow currRow = dbc.PhoneTable.Rows.Find(NameList.Text.Substring(0, 2));

            dbc.SelectedKeyValue = Convert.ToInt32(currRow["id"].ToString());
            txtid.Text = currRow["id"].ToString();
            txtName.Text = currRow["PName"].ToString();
            txtMail.Text = currRow["Email"].ToString();
            txtPhone.Text = currRow["Phone"].ToString(); 
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dbc.DS.Clear();
            dbc.DBAdapter.Fill(dbc.DS, "Phone");
            Parent = (Form1)Owner;
            dbc.PhoneTable = dbc.DS.Tables["Phone"];

            DataRow[] ResultRows
                = dbc.PhoneTable.Select("PName like '%" + Parent.TxtS + "%'");

            NameList.Items.Clear();

            foreach (DataRow currRow in ResultRows)
            {
                NameList.Items.Add(currRow["Id"].ToString()
                    + " " + currRow["PName"].ToString());
            }	
        }
    }
}
