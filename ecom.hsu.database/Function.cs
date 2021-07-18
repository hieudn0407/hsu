using ecom.hsu.dtos.Response;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ecom.hsu.database
{
    public class Funtion
    {

        //private readonly string connStrings = "Server=localhost;Port=5432;User Id = postgres; Password=hieupro123;Database=postgres;";
        private readonly string connStrings = "Server=3.226.134.153;Port=5432;User Id=kyixppcdumylow;Password=a5e3a21dd6f8fd1cf05c669a99186d4ceae2997bb0150982eb2d2555662fc2b1;Database=d8vc94jd5keves;SSL Mode=Require;Trust Server Certificate=true";
        private NpgsqlConnection Connection;

        public Funtion()
        {
            Connection = new NpgsqlConnection(connStrings);
        }

        public ResponseResult<T> GetAll<T>(string function_name, Dictionary<string, object> param = null) where T : new()
        {
            ResponseResult<T> ResponseResult = new ResponseResult<T>();

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        string cursor;

                        using (var cmd = new NpgsqlCommand(function_name, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (param != null)
                            {
                                foreach (var item in param)
                                {
                                    cmd.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
                                }
                            }
                            var reader = cmd.ExecuteReader();
                            reader.Read();
                            cursor = reader.GetString(0);

                            reader.Close();
                        }

                        using (var cmd2 = new NpgsqlCommand())
                        {
                            cmd2.Connection = conn;
                            cmd2.CommandText = $@"FETCH ALL FROM ""{cursor}""";
                            using (IDataReader reader1 = cmd2.ExecuteReader())
                            {
                                var data = MapDataToBusinessEntityCollection<T>(reader1);
                                reader1.Close();

                                ResponseResult.status = true;
                                ResponseResult.message = "OK";
                                ResponseResult.exception = null;
                                ResponseResult.data = data;
                            }
                        }
                        tran.Commit();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ResponseResult.status = false;
                ResponseResult.message = "FAIL";
                ResponseResult.exception = ex;
                ResponseResult.data = null;
            }

            return ResponseResult;
        }

        public List<T> MapDataToBusinessEntityCollection<T>(IDataReader dr) where T : new()
        {
            Type businessEntityType = typeof(T);
            List<T> entitys = new List<T>();
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = businessEntityType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }
            while (dr.Read())
            {
                T newObject = new T();
                for (int index = 0; index < dr.FieldCount; index++)
                {
                    PropertyInfo info = (PropertyInfo)
                                        hashtable[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite && !Convert.IsDBNull(dr[index]))
                    {
                        var a = dr.GetValue(index);
                        info.SetValue(newObject, dr.GetValue(index), null);
                    }
                }
                entitys.Add(newObject);
            }
            dr.Close();
            return entitys;
        }
    }
}
