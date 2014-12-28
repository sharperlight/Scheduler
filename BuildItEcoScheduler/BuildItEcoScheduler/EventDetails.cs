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
    public partial class EventDetails : Form
    {

        private DBLibrary _DBL;
        private DateTime displayMonth;
        private BIELibrary.DateLibrary.CalendarStore calendarStore;
        private int cId;
        private int pId;
        private bool update;
        

        //Job doesn't exist
        public EventDetails(DBLibrary _DBL, int cId, DateTime dateSelected)
        {
            InitializeComponent();
            this._DBL = _DBL;
            displayMonth = new DateTime(dateSelected.Year, dateSelected.Month, 1);
            update = false;
            this.cId = cId;
            buttonCU.Text = "Create";
            BasicStartUp();
        }

        //Job does exist
        public EventDetails(DBLibrary _DBL, int pId)
        {
            InitializeComponent();
            this._DBL = _DBL;
            this.pId = pId;
            update = true;
            buttonCU.Text = "Update";
            BasicStartUp();
        }

        private void BasicStartUp()
        {
            string errorMessage;

            this.calendarStore = new DateLibrary.CalendarStore();

            //Generate Builders Combobox
            string[] nameList = _DBL.GenerateBuilderDB(out errorMessage);
            if (nameList == null) ShowError(errorMessage);
            comboBoxBuilderName.Items.AddRange(nameList);

            if (update)
            {
                //Set Details
                DataTable details = _DBL.GetJobDetails(pId, out errorMessage);
                if (details == null) ShowError(errorMessage);
                PopulateDetailsFields(details);
                //set ComboBox
                string builderName = _DBL.GetBuilderName(pId);
                if (builderName == null) ShowError(errorMessage);
                int builderNameIndex = comboBoxBuilderName.Items.IndexOf(builderName);
                comboBoxBuilderName.SelectedIndex = builderNameIndex;
            }

            //Get Contractor Name Label
            string cName;
            cName = _DBL.GetContractorNameUsingCId(cId, out errorMessage);
            if (cName == null) ShowError(errorMessage);
            labelName.Text = cName;

            //generate Calendar
            InitialCalendarSetup();
            dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(grid_CellClick);
        }

        private void InitialCalendarSetup()
        {
            string errorMessage;
            InitialeCalendarProperties();
            PopulateCalendar();
            DataTable dataT = _DBL.AddOtherJobsToCalendar(displayMonth, cId, out errorMessage);
            if (dataT == null) ShowError(errorMessage);
            AddOtherCalendarJobs(dataT);
            if (update)
            {
                DataTable currentJobList = _DBL.CurrentJobToCalendar(pId, out errorMessage);
                if (currentJobList == null) ShowError(errorMessage);
                HighlightCurrentJobs(currentJobList);
            }
        }

        private void InitialeCalendarProperties()
        {
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "Sun";
            dataGridView1.Columns[1].Name = "Mon";
            dataGridView1.Columns[2].Name = "Tues";
            dataGridView1.Columns[3].Name = "Wed";
            dataGridView1.Columns[4].Name = "Thur";
            dataGridView1.Columns[5].Name = "Fri";
            dataGridView1.Columns[6].Name = "Sat";
            int widthSize = 33;
            dataGridView1.Columns[0].Width = widthSize;
            dataGridView1.Columns[1].Width = widthSize;
            dataGridView1.Columns[2].Width = widthSize;
            dataGridView1.Columns[3].Width = widthSize;
            dataGridView1.Columns[4].Width = widthSize;
            dataGridView1.Columns[5].Width = widthSize;
            dataGridView1.Columns[6].Width = widthSize;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.BackgroundColor = Color.White;
            

        }

        private void PopulateCalendar()
        {
            DateTime startofMonth = new DateTime(displayMonth.Year, displayMonth.Month, 1);
            int dayPosition = _DBL._DL.GetDayPosition(startofMonth.DayOfWeek.ToString());
            int numDaysofMonth = DateTime.DaysInMonth(displayMonth.Year, displayMonth.Month);
            labelMonth.Text = startofMonth.ToString("MMMM");
            labelYear.Text = startofMonth.Year.ToString();
            string[] set = new string[7];
            //initial
            int counter = 1;
            for (int i = 0; i < 7; i++)
            {
                if (i < dayPosition)
                {
                    set[i] = "";
                }
                else
                {
                    set[i] = counter.ToString();
                    counter++;
                }
            }
            if (dayPosition == 6)
            {
                if (numDaysofMonth >= 30) set[0] = "30";
                if (numDaysofMonth > 30) set[1] = "31";
            }
            if (dayPosition == 5 && numDaysofMonth == 31) set[0] = "31";
            dataGridView1.Rows.Add(set);
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (counter > numDaysofMonth)
                    {
                        set[j] = "";
                    }
                    else
                    {
                        set[j] = counter.ToString();
                        counter++;
                    }

                }
                dataGridView1.Rows.Add(set);
            }
            disableEmptyCells();
            
        }

        private void disableEmptyCells()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString() == string.Empty)
                    {
                        cell.ReadOnly = true;
                    }
                }
            }
        }

        private void AddOtherCalendarJobs(DataTable dataT)
        {
            foreach(DataRow dataTDR in dataT.Rows)
            {
                int day = Convert.ToDateTime(dataTDR[0]).Day;
                bool flagFound = false;
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (flagFound) break;
                    foreach (DataGridViewCell dc in dr.Cells)
                    {
                        if (flagFound) break;
                        else if (dc.Value.ToString() == string.Empty) continue;
                        else if (Convert.ToInt16(dc.Value) == day)
                        {
                            dc.Style.BackColor = Color.Green;
                            flagFound = !flagFound;
                            break;
                        }
                    }
                }
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = dataGridView1.CurrentCell.ColumnIndex;
            int row = e.RowIndex;
            int day;
            DateTime selectedDate;

            if (dataGridView1[col, row].Style.BackColor == Color.Blue)
            {
                dataGridView1[col, row].Style.BackColor = Color.White;
                day = Convert.ToInt16(dataGridView1[col, row].Value);
                selectedDate = new DateTime(displayMonth.Year, displayMonth.Month, day);
                calendarStore.store.Remove(selectedDate);
            }
            else if (dataGridView1[col, row].Style.BackColor == Color.Green)
            {
                dataGridView1[col, row].Style.BackColor = Color.Crimson;
                day = Convert.ToInt16(dataGridView1[col, row].Value);
                selectedDate = new DateTime(displayMonth.Year, displayMonth.Month, day);
                calendarStore.store.AddLast(selectedDate);
            }
            else if (dataGridView1[col, row].Style.BackColor == Color.Crimson)
            {
                dataGridView1[col, row].Style.BackColor = Color.Green;
                day = Convert.ToInt16(dataGridView1[col, row].Value);
                selectedDate = new DateTime(displayMonth.Year, displayMonth.Month, day);
                calendarStore.store.Remove(selectedDate);
            }
            else
            {
                dataGridView1[col, row].Style.BackColor = Color.Blue;
                day = Convert.ToInt16(dataGridView1[col, row].Value);
                selectedDate = new DateTime(displayMonth.Year, displayMonth.Month, day);
                calendarStore.store.AddLast(selectedDate);
            }
            dataGridView1.ClearSelection();
        }

        private void PopulateDetailsFields(DataTable details)
        {
            DataRow dr = details.Rows[0];
            textBoxCost.Text = dr[0].ToString();
            textBoxAddress.Text = dr[1].ToString();
            textBoxDescription.Text = dr[2].ToString();
            this.displayMonth = Convert.ToDateTime(dr[3].ToString());
            this.cId = Convert.ToInt16(dr[4].ToString());
            if (Convert.ToBoolean(dr[5].ToString())) checkBoxConfirmed.Checked = true;
            if (Convert.ToBoolean(dr[6].ToString())) checkBoxCompleted.Checked = true;
        }

        private void HighlightCurrentJobs(DataTable currentJobList)
        {
            foreach (DataRow currentDR in currentJobList.Rows)
            {
                DateTime JobDate = Convert.ToDateTime(currentDR[0]);
                if (JobDate.Month != displayMonth.Month) break;
                int day = JobDate.Day;
                bool flagFound = false;
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (flagFound) break;
                    foreach (DataGridViewCell dc in dr.Cells)
                    {
                        if (flagFound) break;
                        else if (dc.Value.ToString() == string.Empty) continue;
                        else if (Convert.ToInt16(dc.Value) == day)
                        {
                            dc.Style.BackColor = Color.Blue;
                            flagFound = !flagFound;
                            calendarStore.store.AddLast(JobDate);
                            break;
                        }
                    }
                }
            }
        }

        private bool CheckDetails()
        {
            if (comboBoxBuilderName.SelectedIndex < 0)
            {
                MessageBox.Show("Select a Builder");
                return false;
            }
            if (textBoxAddress.Text.Trim().Length == 0)
            {
                MessageBox.Show("Add an address");
                return false;
            }
            return true;
        }


        private void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage);
            Application.Exit();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            this.displayMonth = displayMonth.AddMonths(-1);
            InitialCalendarSetup();

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            this.displayMonth = displayMonth.AddMonths(1);
            InitialCalendarSetup();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonCU_Click(object sender, EventArgs e)
        {
            if (!CheckDetails()) return;
            string errorMessage;
            int jId = -1;
            int bId = _DBL.GetBIdFromBName(comboBoxBuilderName.SelectedItem.ToString(), out errorMessage);
            if (bId == -1) ShowError(errorMessage);
            if (update)
            {
                if (_DBL.RemoveDateList(pId, out errorMessage)) ShowError(errorMessage);
                jId = _DBL.GetJIdFromPId(pId, out errorMessage);
                if (jId == -1) ShowError(errorMessage);
            }

            decimal price = Convert.ToDecimal(textBoxCost.Text);
            string description = textBoxDescription.Text;
            string address = textBoxAddress.Text;
            int confirmed = Convert.ToInt16(checkBoxConfirmed.Checked);
            int completed = Convert.ToInt16(checkBoxCompleted.Checked);

            if (!_DBL.JobCU(bId, jId, address, out errorMessage)) ShowError(errorMessage);
            if(!update) jId =  _DBL.GetLastJIdAdded(out errorMessage);
            if(jId == -1) ShowError(errorMessage);
            if (!_DBL.JobPartCU(jId, cId, description, price, confirmed, completed, update, out errorMessage)) ShowError(errorMessage);
            if (!update) this.pId = _DBL.GetLastPIdAdded(out errorMessage);
            if (pId == -1) ShowError(errorMessage);
            if (!_DBL.AddDateToList(calendarStore, pId, out errorMessage)) ShowError(errorMessage);
        }

    }
}
