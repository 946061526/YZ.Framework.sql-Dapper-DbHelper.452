using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using YZ.Framework.DataAccess;
using System.Linq;

namespace YZ.Test.TestDb
{
    /// <summary>
    /// 测试类
    /// </summary>
    /// <returns></returns>
    public class TestDataAccess
    {
        public DataTable getdt()
        {
            DataTable dt = DataAccess.ExecuteDataTableBySql("select * from dbo.GroupInfo", null);
            return dt;
        }
        public List<GroupInfo> getList()
        {
            List<GroupInfo> list = DataAccess.GetList<GroupInfo>("select id,groupName from dbo.GroupInfo", null);
            return list;
        }
        public List<GroupInfo> getListPage()
        {
            List<GroupInfo> list = DataAccess.GetList<GroupInfo>("select * from dbo.GroupInfo", 1, 2, "order by id asc", null);
            return list;
        }
        public int insert()
        {
            DbParameter[] dbParameter = new DbParameter[2];
            dbParameter[0] = DataAccess.CreateParameterInput("@groupName", "admin", DbType.String, 0);
            dbParameter[1] = DataAccess.CreateParameterInput("@remark", "remark", DbType.String, 0);
            return DataAccess.ExecuteNonQueryBySql("insert into dbo.GroupInfo(groupName, remark) values(@groupName,@remark)", dbParameter);
        }
        public int update()
        {
            DbParameter[] dbParameter = new DbParameter[2];
            dbParameter[0] = DataAccess.CreateParameterInput("@groupName", "superadmin", DbType.String, 0);
            dbParameter[1] = DataAccess.CreateParameterInput("@id", "1", DbType.String, 0);
            return DataAccess.ExecuteNonQueryBySql("update dbo.GroupInfo set groupName=@groupName where id=@id", dbParameter);
        }

        public int delete()
        {
            DbParameter[] dbParameter = new DbParameter[1];
            dbParameter[0] = DataAccess.CreateParameterInput("@id", "2", DbType.String, 0);
            return DataAccess.ExecuteNonQueryBySql("delete dbo.GroupInfo where id=@id", dbParameter);
        }

        public string GetObj()
        {
            var sql = "select groupName from GroupInfo where id=1";
            return DataAccess.ExecuteScalarBySql<string>(sql, null);
        }

        public int GetID()
        {
            var sql = "select top 1 id from GroupInfo ";
            return DataAccess.ExecuteScalarBySql<int>(sql, null);
        }


        /// <summary>
        /// 单个事务操作，只需要把相关联的代码放在BeginTransaction和CommitTransaction中间，如果发生异常调用RollbackTransaction即可
        /// </summary>
        private void Tran1()
        {
            try
            {
                DataAccess.BeginTransaction();
                // add 
                DataAccess.ExecuteNonQueryBySql("insert into dbo.GroupInfo(groupName, remark) values('groupName','remark')");

                //detele by pk
                DataAccess.ExecuteNonQueryBySql("delete dbo.GroupInfo where id=5");

                Console.WriteLine(string.Format("success and commited"));
                DataAccess.CommitTransaction();
            }
            catch
            {
                Console.WriteLine(string.Format("exception and rollback"));
                DataAccess.RollbackTransaction();
            }
        }
        //当调用 BeginTransaction时创建TransConnection对象。之后的多个DbCommand命令都从这个事务上拿连接。因为TransConnectionObj添加了ThreadStatic属性，所以它是线程唯一的，不会影响其它线程上的事务；所有方法执行完后，调用CommitTransaction 就提交事务，并关闭连接；如果发生异常，则调用RollbackTransaction，就会回滚所有命令，并关闭连接。





        /// <summary>
        /// 嵌套事务
        /// </summary>
        private void Tran2()
        {
            try
            {
                DataAccess.BeginTransaction();

                // add 
                DataAccess.ExecuteNonQueryBySql("insert into dbo.GroupInfo(groupName, remark) values('groupName','remark')");

                Transaction2();

                //detele by pk
                DataAccess.ExecuteNonQueryBySql("delete dbo.GroupInfo where id=5");

                Console.WriteLine(string.Format("success and commited"));
                DataAccess.CommitTransaction();
            }
            catch
            {
                Console.WriteLine(string.Format("exception and rollback"));
                DataAccess.RollbackTransaction();
            }

            Console.ReadLine();
        }
        private static void Transaction2()
        {
            try
            {
                DataAccess.BeginTransaction();
                //update model
                DataAccess.ExecuteNonQueryBySql("update dbo.GroupInfo set groupName=@groupName where id=1");
                //throw new Exception("");

                DbParameter param = DataAccess.CreateParameterInput("@id", "1", DbType.String);
                DbDataReader reader = DataAccess.ExecuteReaderBySql("select * from dbo.DeptInfo where id=@id", param);
                while (reader.Read())
                {
                    Console.WriteLine(reader["deptName"]);
                }

                reader.Close();

                DataAccess.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("exception and rollback: {0}", ex.Message));
                DataAccess.RollbackTransaction();
                throw;
            }
        }

    }
}
