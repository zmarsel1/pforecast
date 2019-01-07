using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Documents
{
    public class DBDocumentLocker
    {
        public DBDocumentLocker() { Locked = false; }

        string LockKey = string.Empty;
        SqlConnection session = null;

        public bool Locked { get; protected set; }
        public SqlConnection Session { get { return session; } }
        
        public bool Lock(string key, string connection) //результат = удалось ли заблокироать
        {
            if (Locked) return false; //если заблокирован выходим
            LockKey = key;
            try
            {
                session = new SqlConnection(connection);
                session.Open();

                SqlCommand locker = new SqlCommand("sp_getapplock", Session);
                locker.CommandType = CommandType.StoredProcedure;
                locker.Parameters.Add(new SqlParameter("@Resource", key));
                locker.Parameters.Add(new SqlParameter("@LockMode", "Exclusive"));
                locker.Parameters.Add(new SqlParameter("@LockOwner", "Session"));
                locker.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                SqlCommand mode = new SqlCommand();
                mode.CommandText = string.Format("SELECT APPLOCK_TEST('public', '{0}', 'Exclusive', 'Session');", LockKey);
                mode.Connection = session;

                try
                {
                    int grantble = (int)mode.ExecuteScalar();
                    if (grantble == 1)
                    {
                        locker.ExecuteNonQuery();
                        int r = (int)locker.Parameters["@RetVal"].Value;
                        Locked = (r == 0 || r == 1);
                        return Locked;
                    }
                    else
                        return false;
                }
                catch (Exception exception) // не удалось заблокировать строку на запись
                {
                    Locked = false; //только для чтения
                    //throw new Exception("Не удалось загрузить документ с заданным номером.");
                }
            }
            catch
            {

            }
            return false;
        }
        public virtual void Unlock()
        {
            if (Locked)
            {
                try
                {
                    SqlCommand locker = new SqlCommand("sp_releaseapplock", session);
                    locker.CommandType = CommandType.StoredProcedure;
                    locker.Parameters.Add(new SqlParameter("@Resource", LockKey));
                    locker.Parameters.Add(new SqlParameter("@LockOwner", "Session"));
                    locker.ExecuteNonQuery();
                    session.Close();
                }
                catch (Exception exception)
                {

                }
                Locked = false;
            }
        }
    }
}
