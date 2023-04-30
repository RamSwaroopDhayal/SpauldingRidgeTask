using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpauldingRidgeTask.Models
{

    public class DBFactoryMSSQL : IDisposable
    {
        SqlConnection _con;
        SqlCommand _cmd;
        SqlDataAdapter _da;
        private const string ConnectionString = "DefaultConnection";
        public DBFactoryMSSQL()
        {
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);
        }
        public void OpenConnection()
        {
            try
            {
                if (_con == null)
                {
                    throw new Exception("Connection object is not initialized");
                }
                if (_con.State == ConnectionState.Closed)
                {
                    _con.Close();
                    _con.Open();
                }
                else if (_con.State == ConnectionState.Closed)
                {
                    _con.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlConnection GetConnection
        {
            get
            {
                try
                {
                    if (_con == null)
                    {
                        throw new Exception("Connection object is not intialised. Initalise it with connection string of config file");
                    }
                    else if (_con.ConnectionString.Length == 0)
                    {
                        throw new Exception("Connection string is not initialised");
                    }
                    return _con;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public void CloseConnection()
        {
            try
            {
                if (_con == null)
                {
                    throw new Exception("Connection object is not initialized");
                }
                if (_con.State == ConnectionState.Connecting || _con.State == ConnectionState.Executing || _con.State == ConnectionState.Fetching || _con.State == ConnectionState.Open)
                {
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public DataTable GetTable()
        {

            DataTable dt = new DataTable();
            try
            {
                if (_con == null)
                {
                    throw new Exception("Connection object is not initialized");
                }
                else if (_cmd == null)
                {
                    throw new Exception("Command object is not initialized");
                }
                //this.SaveSqlCommandText();
                _da = new SqlDataAdapter(_cmd);
                _da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                dt.Dispose();

                throw;
            }
        }
        public DataSet GetDataSet()
        {

            DataSet ds = new DataSet();
            try
            {
                if (_con == null)
                {
                    throw new Exception("Connection object is not initialized");
                }
                else if (_cmd == null)
                {
                    throw new Exception("Command object is not initialized");
                }
                //this.SaveSqlCommandText();
                _da = new SqlDataAdapter(_cmd);
                _da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {

                ds.Dispose();
                throw;

            }
            finally
            {
            }
        }
        public SqlCommand CreateCommand(string commandText, DBFactoryCommandType cmdType)
        {
            try
            {
                if (_con == null)
                {
                    throw new Exception("Connection object is not initialized");
                }
                _cmd = new SqlCommand();
                _cmd.CommandText = commandText;
                _cmd.CommandTimeout = 0;
                _cmd.Connection = _con;
                if (Convert.ChangeType(cmdType, cmdType.GetType()).Equals(DBFactoryCommandType.PROCEDURE_COMMAND))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    _cmd.CommandType = CommandType.Text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _cmd;
        }
        public enum DBFactoryCommandType
        {
            // To be used for Stored Procedure
            PROCEDURE_COMMAND
        }
    }

}