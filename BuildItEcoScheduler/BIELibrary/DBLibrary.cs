using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace BIELibrary
{
    public class DBLibrary
    {

        public SqlConnection cnn;
        public DateLibrary _DL;

        public DBLibrary()
        {
            string errorMessage;
            if (!ConnectToDB(out errorMessage)) Debug.Write(errorMessage);
            _DL = new DateLibrary();
                
        }

        private bool ConnectToDB(out string errorMessage)
        {
            string connString = null;
            errorMessage = "";
            connString = "Data Source=localhost;Initial Catalog=BuildItEco; User ID=YUKIROBSON;Password=morioka";
            cnn = new SqlConnection(connString);
            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                errorMessage = ("Couldn't open connection");
                return false;
            }
            return true;
        }

        public int[] GetContractorId(out string errorMessage)
        {
            errorMessage = "";
            int numCon = GetNumContractors(out errorMessage);
            if (numCon == -1) Debug.Write(errorMessage + "\n");

            int[] cIdList = new int[numCon];

            string search = "SELECT cId FROM Contractor";
            SqlCommand sqlSearch = new SqlCommand(search, cnn);
            SqlDataReader dRead;

            try
            {
                dRead = sqlSearch.ExecuteReader();
                int counter = 0;
                while (dRead.Read())
                {
                    cIdList[counter] = Convert.ToInt16(dRead[0]);
                    counter++;
                }
                dRead.Close();
                return cIdList;
            }
             catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting cId";
                return null;
            }
        }

        public int GetNumContractors(out string errorMessage)
        {
            errorMessage = "";
            string search = "SELECT COUNT(cId) FROM Contractor";
            SqlCommand sqlSearch = new SqlCommand(search, cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
             catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting number of Contractors";
                return -1;
            }
            dRead.Read();
            int num = Convert.ToInt16(dRead[0]);
            dRead.Close();
            return num;
        }

        public string GetContractorNameUsingCId(int cId, out string errorMessage)
        {
            errorMessage = "";
            string search = "SELECT name FROM Contractor " +
                            "WHERE cId = " + cId;
            SqlCommand sqlSearch = new SqlCommand(search, cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting Contractor name using cId";
                return null;
            }
            dRead.Read();
            string name = dRead[0].ToString();
            dRead.Close();
            return name;
        }

        public string GetContractorNameUsingPId(int pId, out string errorMessage)
        {
            errorMessage = "";
            string search = "SELECT CN.name FROM Contractor CN, JobPart JP " +
                            "WHERE JP.cId = CN.cId AND " +
                            "JP.pId = " + pId ;
            SqlCommand sqlSearch = new SqlCommand(search, cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
                //** needs a check if fails to find
                dRead.Read();
                string name = dRead[0].ToString();
                dRead.Close();
                return name;
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting Contractor name using pId";
                return null;
            }
        }

        public DataTable PopulateContratorGrid(int cId, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("Day");
            string startDateS = _DL.currentFortNightStartDate.ToString("yyyy/MM/dd");
            string endDateS = _DL.currentFortNightEndDate.ToString("yyyy/MM/dd");
            string search = "SELECT DT.date, BD.name, JS.address, LSJP.pId, LSJP.confirmed, LSJP.completed" +
                            " FROM [Date] DT" +
                            " FULL JOIN Contractor CN ON CN.cId = " + cId +
                            " FULL OUTER JOIN" +
                            " (select LS.dId,LS.lId,JP.* from List LS full outer join JobPart JP ON JP.pId = LS.pId) LSJP" +
                            " ON LSJP.cId  = CN.cId and LSJP.dId = DT.dId" +
                            " FULL JOIN Job JS ON LSJP.jId = JS.jId" +
                            " FULL JOIN Builder BD ON JS.bId = BD.bId" +
                            " WHERE DT.date BETWEEN '" + startDateS + "' AND ' " + endDateS + "'";
            SqlCommand sqlSearch = new SqlCommand(search, cnn);
            SqlDataReader dataReader;

            try
            {
                dataReader = sqlSearch.ExecuteReader();
                dt.Load(dataReader);
                
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting Contractor grid data";
                return null;
            }
            dataReader.Close();
            
            DateTime dd;
            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                dd = Convert.ToDateTime(row.ItemArray[1]);
                dt.Rows[index][0] = dd.DayOfWeek.ToString();
                index++;
            }

            return dt;
        }
        public string[] GenerateBuilderDB(out string errorMessage)
        {
            errorMessage = "";
            SqlCommand getNumCon = new SqlCommand("SELECT COUNT(*) FROM Builder", cnn);
            SqlDataReader dRead;
            try
            {
                dRead = getNumCon.ExecuteReader();
                dRead.Read();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while get number of builders data";
                return null;
            }
            int num = dRead.GetInt32(0);
            String[] nameList = new String[num];
            dRead.Close();
            SqlCommand getName = new SqlCommand("SELECT name FROM Builder", cnn);
            try
            {
                dRead = getName.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting builders data";
                return null;
            }
            int counter = 0;
            while (dRead.Read())
            {
                nameList[counter] = dRead.GetString(0);
                counter++;
            }
            dRead.Close();
            return nameList;
        }

        public string GetBuilderName(int pId)
        {
            SqlCommand getName = new SqlCommand("SELECT BD.name FROM Builder BD, Job JS, JobPart JP WHERE JP.pId =" + pId + " AND JS.jId = JP.jId AND JS.bId = BD.bId", cnn);
            SqlDataReader dRead = getName.ExecuteReader();
            dRead.Read();
            String builderName = dRead[0].ToString();
            dRead.Close();
            return builderName;
        }

        public DataTable AddOtherJobsToCalendar(DateTime displayMonth, int cId, out string errorMessage)
        {
            errorMessage = "";
            DateTime endofMonth = new DateTime(displayMonth.Year, displayMonth.Month, DateTime.DaysInMonth(displayMonth.Year, displayMonth.Month));
            string startDate = new DateTime(endofMonth.Year, endofMonth.Month, 1).ToString("yyyy/MM/dd");
            string endDate = endofMonth.ToString("yyyy/MM/dd");
            string search = "SELECT DS.date FROM Date DS, List LS, Contractor CN, JobPart JP" +
                            " WHERE CN.cId = " + cId +
                            " AND CN.cId = JP.cId" +
                            " AND JP.pId = LS.pId AND" +
                            " LS.dId = DS.dId" +
                            " AND DS.date BETWEEN '" + startDate + "' AND ' " + endDate + "'";

            SqlCommand sqlSearch = new SqlCommand(search, cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting other jobs to calendar data";
                return null;
            }
            DataTable dataT = new DataTable();
            dataT.Load(dRead);
            dRead.Close();
            return dataT;
        }

        public DataTable CurrentJobToCalendar(int pId, out string errorMessage)
        {
            errorMessage = "";
            string search = "SELECT DS.date FROM Date DS, List LS" +
                            " WHERE LS.pId = " + pId +
                            " AND LS.dId = DS.dId";
            SqlCommand sqlSearch = new SqlCommand(search, cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting current jobs for calendar data";
                return null;
            }
            DataTable dataT = new DataTable();
            dataT.Load(dRead);
            dRead.Close();
            return dataT;
        }

        public DataTable GetJobDetails(int pId, out string errorMessage)
        {
            errorMessage = "";
            string search = "SELECT JP.price, JS.address, JP.description, DT.date, CN.cId, JP.confirmed, JP.Completed, CN.cId, JP.pId " +
                            "FROM Job JS, Contractor CN, List LS, JobPart JP, Date DT " +
                            "WHERE JP.pId = " + pId + " " +
                            "AND JP.cId = CN.cId AND JP.jId = JS.jId " +
                            "AND JP.pId = LS.pId AND LS.dId = DT.dId"; 
            SqlCommand sqlSearch = new SqlCommand(search, cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while collecting job details data";
                return null;
            }
            DataTable dataT = new DataTable();
            dataT.Load(dRead);
            dRead.Close();
            return dataT;
        }

        public bool RemoveDateList(int pId, out string errorMessage)
        {
            errorMessage = "";
            string cmdSql = "DELETE FROM List WHERE pId = " + pId;
            SqlCommand cmd = new SqlCommand(cmdSql);
            cmd.Connection = cnn;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while deleting list for jobpart";
                return false;
            }
            return true;
        }

        public int GetJIdFromPId(int pId, out string errorMessage)
        {
            errorMessage = "";
            SqlCommand sqlSearch = new SqlCommand("SELECT JP.jId FROM JobPart JP WHERE JP.pId = " + pId, cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while getting jId from pid";
                return -1;
            }
            dRead.Read();
            int jId = Convert.ToInt16(dRead[0]);
            dRead.Close();
            return jId;
        }

        public int GetBIdFromBName(string bName, out string errorMessage)
        {
            System.Diagnostics.Debug.WriteLine("\n " + bName + "\n");
            errorMessage = "";
            SqlCommand sqlSearch = new SqlCommand("SELECT BD.bId FROM Builder BD WHERE BD.name = '" + bName + "'", cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while getting bId from bname";
                return -1;
            }
            dRead.Read();
            int bId = Convert.ToInt16(dRead[0]);
            dRead.Close();
            return bId;
        }

        public bool JobCU(int bId, int jId, string address, out string errorMessage)
        {
            errorMessage = "";
            SqlCommand cmd;
            string sqlCmd;
            if (jId != -1) //update
            {
                sqlCmd = "UPDATE Job SET bId = " + bId +
                         ", address = '" + address + "'" +
                         " WHERE jId = " + jId;
            }
            else //create
            {
                sqlCmd = "INSERT INTO Job VALUES(" + bId + ", '" + address + "')";       
            }
            cmd = new SqlCommand(sqlCmd);
            cmd.Connection = cnn;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while updating/creating Job";
                return false;
            }
            return true;

        }

        public int GetLastJIdAdded(out string errorMessage)
        {
            errorMessage = "";
            SqlCommand sqlSearch = new SqlCommand("SELECT TOP 1 jId FROM Job ORDER BY jId DESC", cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while getting the last jId";
                return -1;
            }
            dRead.Read();
            int jId = Convert.ToInt16(dRead[0]);
            dRead.Close();
            return jId;
        }

        public int GetLastPIdAdded(out string errorMessage)
        {
            errorMessage = "";
            SqlCommand sqlSearch = new SqlCommand("SELECT TOP 1 pId FROM JobPart ORDER BY pId DESC", cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while getting the last pid";
                return -1;
            }
            dRead.Read();
            int pId = Convert.ToInt16(dRead[0]);
            dRead.Close();
            return pId;
        }

        public bool JobPartCU(int jId, int cId, string description, decimal price, int confirmed, int completed, bool update, out string errorMessage)
        {
            errorMessage = "";
            string sqlCmd;
            if (update)
            {
                sqlCmd = "UPDATE JobPart SET description = '" + description + "'" +
                       ", price = " + price +
                        ", confirmed = " + confirmed +
                        ", completed = " + completed +
                        " WHERE cId = " + cId +
                        " AND jId = " + jId;
            }
            else
            {
                sqlCmd = "INSERT INTO JobPart VALUES(" + jId +
                        ", " + cId +
                        ",'" + description + "'" +
                        ", " + price +
                        ", " + confirmed +
                        ", " + completed + ")";
            }
            SqlCommand cmd = new SqlCommand(sqlCmd);
            cmd.Connection = cnn;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while create/update job part";
                return false;
            }
            return true;
        }

        public int GetDId(string sqlDate, out string errorMessage)
        {
            errorMessage = "";
            SqlCommand sqlSearch = new SqlCommand("SELECT dId FROM Date WHERE date ='" + sqlDate + "'", cnn);
            SqlDataReader dRead;
            try
            {
                dRead = sqlSearch.ExecuteReader();
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while getting dId";
                return -1;
            }
            dRead.Read();
            int dId = Convert.ToInt16(dRead[0]);
            dRead.Close();
            return dId;
        }

        public bool AddDateToList(DateLibrary.CalendarStore calendarStore, int pId, out string errorMessage)
        {
            errorMessage = "";
            string sqlCmd;
            string dt;
            int dId;
            SqlCommand cmd;
            while (calendarStore.store.Count > 0)
            {
                dt = _DL.DateTimeConvertSQL(calendarStore.store.Last.Value);
                dId = GetDId(dt, out errorMessage);
                if (dId == -1) return false;
                calendarStore.store.RemoveLast();
                sqlCmd = "INSERT INTO List VALUES(" + pId + ", " + dId + ")";
                cmd = new SqlCommand(sqlCmd);
                cmd.Connection = cnn;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    errorMessage = "Error occurred while added date to list with num" + calendarStore.store.Count;
                    return false;
                }
            }
            return true;
        }

    }
}
