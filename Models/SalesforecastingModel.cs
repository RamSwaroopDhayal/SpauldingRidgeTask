using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static SpauldingRidgeTask.Models.DBFactoryMSSQL;

namespace SpauldingRidgeTask.Models
{
    public class SalesforecastingModel
    {
        private DBFactoryMSSQL objDBAccess;
        private DataTable dt;

        public SalesforecastingModel()
        {
            objDBAccess = new DBFactoryMSSQL();
            dt = new DataTable();
        }
        public DataSet GetSalesData(string Year)
        {
            DataSet appData = new DataSet();
            try
            {
                objDBAccess.OpenConnection();
                SqlCommand cmd = objDBAccess.CreateCommand("usp_Sales", DBFactoryCommandType.PROCEDURE_COMMAND);
                cmd.Parameters.AddWithValue("@ActionType", "GetData");
                cmd.Parameters.AddWithValue("@year", Year);
                appData = objDBAccess.GetDataSet();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objDBAccess.CloseConnection();
            }
            return appData;
        }
        public DataSet Getforecasted_SalesData(int Year, float perc_)
        {
            DataSet appData = new DataSet();
            try
            {
                objDBAccess.OpenConnection();
                SqlCommand cmd = objDBAccess.CreateCommand("usp_Sales", DBFactoryCommandType.PROCEDURE_COMMAND);
                cmd.Parameters.AddWithValue("@ActionType", "Get_forecasted_Data");
                cmd.Parameters.AddWithValue("@year", Year);
                cmd.Parameters.AddWithValue("@percentage", perc_);
                appData = objDBAccess.GetDataSet();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objDBAccess.CloseConnection();
            }
            return appData;
        }
        public DataTable Getforecasted_StateData(String State, int Year, float percentage)
        {
            dt.Clear();
            try
            {
                objDBAccess.OpenConnection();
                SqlCommand cmd = objDBAccess.CreateCommand("usp_Sales", DBFactoryCommandType.PROCEDURE_COMMAND);
                cmd.Parameters.AddWithValue("@ActionType", "Get_forecasted_State_Data");
                cmd.Parameters.AddWithValue("@State", State);
                cmd.Parameters.AddWithValue("@year", Year);
                cmd.Parameters.AddWithValue("@percentage", percentage);
                dt = objDBAccess.GetTable();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objDBAccess.CloseConnection();
            }
            return dt;
        }

    }

}