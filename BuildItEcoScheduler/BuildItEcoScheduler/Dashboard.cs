using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BIELibrary;

namespace BuildItEcoScheduler
{
    public partial class Dashboard : Form
    {
         DBLibrary _DBLib;
         DateLibrary _DLib;


        public Dashboard()
        {
            InitializeComponent();
            _DBLib = new DBLibrary();
            _DLib = new DateLibrary();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string errorMessage;
            int[] cIdList = _DBLib.GetContractorId(out errorMessage);

            

            if (cIdList == null) ShowError(errorMessage);

            for(int i = 0; i < cIdList.Length; i++)
            {
                GenerateGrid(cIdList[i], i);
                
            }
            
            this.AutoScroll = true;
            DashTableLayout.AutoSize = true;

            TableLayoutRowStyleCollection Dashstyles = DashTableLayout.RowStyles;
            
            foreach (RowStyle row in Dashstyles)
            {
                row.SizeType = SizeType.AutoSize;
            }
            
        }

        private void GenerateGrid(int cId, int num)
        {
            DataGridView grid = new DataGridView();
            string errorMessage;

            TableLayoutWithGridProperties(cId, num, grid);
            DataGridProperties(grid, num);
            DashTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;

            grid.Name = cId.ToString();
            DataTable dt = _DBLib.PopulateContratorGrid(cId, out errorMessage);
            if (dt == null) ShowError(errorMessage);
            grid.DataSource = dt;

            grid.Columns[4].Visible = false;
            grid.Columns[5].Visible = false;
            grid.Columns[6].Visible = false;

            var height = 20;
            var width= 100;
            foreach (DataGridViewRow dr in grid.Rows)
            {
                height += dr.Height;
            }
            foreach (DataGridViewColumn dc in grid.Columns)
            {
                if (dc.Visible) width += dc.Width;
            }
            grid.Height = height + 10;
            grid.Width = width;
            grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(grid_DoubleClick);
            
        }

        private void grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            int rowIndex = e.RowIndex;
            DataGridViewRow dr = grid.Rows[rowIndex];
            EventDetails eventDt;
            //new job
            if (dr.Cells[2].Value.ToString() == string.Empty)
            {
                DateTime jobDate = Convert.ToDateTime(dr.Cells[1].Value);
                int cId = Convert.ToInt16(grid.Name);
                eventDt = new EventDetails(_DBLib, cId, jobDate);
            }
            //existing job
            else
            {
                int pId = Convert.ToInt16(dr.Cells[4].Value.ToString());
                eventDt = new EventDetails(_DBLib, pId);
            }
            eventDt.ShowDialog();
        }

        private void DataGridProperties(DataGridView grid, int num)
        {

            grid.AllowUserToAddRows = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;
        }

        private void TableLayoutWithGridProperties(int cId, int num, DataGridView grid)
        {
            string errorMessage;
            int col;
            int headerRow;
            int dataRow;
            if (num % 2 == 0)
            {
                col = 0;
                headerRow = num;
                dataRow = num + 1;
            }
            else
            {
                col = 1;
                headerRow = num - 1;
                dataRow = num;
            }
            string cName = _DBLib.GetContractorNameUsingCId(cId, out errorMessage);
            if (cName == null)
            {
                ShowError(errorMessage);
            }
            Label cNameLabel = GenNameLabel(cName);

            //DashTableLayout.GetControlFromPosition(col, headerRow);
            //DashTableLayout.GetControlFromPosition(col, dataRow);
            DashTableLayout.Controls.Add(cNameLabel, col, headerRow);
            DashTableLayout.Controls.Add(grid, col, dataRow);

            

        }

        private Label GenNameLabel(string cName)
        {
            Label label = new Label();
            label.Text = cName;
            //** ADD CLICK EVENT 
            return label;
        }

        private void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage);
            Application.Exit();
        }


    }
}
