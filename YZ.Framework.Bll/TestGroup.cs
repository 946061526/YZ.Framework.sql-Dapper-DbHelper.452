using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Framework.SysManage.Model;
using SqlSugar;
using YZ.Framework.SqlSugarDao;

namespace YZ.Framework.Bll
{
    public class TestGroup : ITestGroup
    {
        public long Add(GroupInfo entity, bool ignorePk = true)
        {
            using (SqlSugarClient db = SugarDao.GetInstance())
            {
                IInsertable<GroupInfo> insertObj = db.Insertable(entity);
                if (ignorePk)
                {
                    insertObj = insertObj.IgnoreColumns(it => it == "Id");
                }
                return insertObj.ExecuteReturnBigIdentity();
            }
        }

        public long Add(List<GroupInfo> entitys, bool ignorePk = true)
        {
            using (SqlSugarClient db = SugarDao.GetInstance())
            {
                IInsertable<GroupInfo> insertObj = db.Insertable(entitys);
                if (ignorePk)
                {
                    insertObj = insertObj.IgnoreColumns(it => it == "Id");
                }
                var t = insertObj.ExecuteReturnBigIdentityAsync();
                t.Wait();
                return t.Result;
            }
        }

        public int Delete(int[] primaryKeys, bool isLogic)
        {
            using (SqlSugarClient db = SugarDao.GetInstance())
            {
                if (isLogic)
                {
                    return 1;
                }
                else
                    return db.Deleteable<GroupInfo>(primaryKeys).ExecuteCommand();
            }
        }

        public int Delete(int primaryKey, bool isLogic)
        {
            using (SqlSugarClient db = SugarDao.GetInstance())
            {
                if (isLogic)
                {
                    return 1;
                }
                else
                    return db.Deleteable<GroupInfo>(primaryKey).ExecuteCommand();
            }
        }

        public void Dispose()
        {
            
        }

        public int Edit(GroupInfo entity)
        {
            using (SqlSugarClient db = SugarDao.GetInstance())
            {
                return db.Updateable(entity).ExecuteCommand();
            }
        }

        public GroupInfo Get(int id)
        {
            using (SqlSugarClient db = SugarDao.GetInstance())
            {
                //var sdb = new SimpleClient<GroupInfo>(db);
                //sdb.GetById(1);

                return db.Queryable<GroupInfo>().Single(it => it.Id == id);
            }
        }

        public List<GroupInfo> GetList(int id, string name, int pageIndex, int pageSize, string orderFileds, ref int total)
        {
            using (SqlSugarClient db = SugarDao.GetInstance())
            {
                //var sdb = new SimpleClient<GroupInfo>(db);
                //sdb.GetPageList()

                var qable = db.Queryable<GroupInfo>();
                if (!string.IsNullOrEmpty(name))
                {
                    qable = qable.Where(it => it.GroupName.Contains(name));
                }
                if (id>0)
                {
                    qable = qable.Where(it => it.Id == id);
                }
                if (!string.IsNullOrEmpty(orderFileds))//无需担心注入
                {
                    qable = qable.OrderBy(orderFileds);
                }
                total = qable.Count();
                return qable.ToPageList(pageIndex, pageSize);
            }
        }
    }
}
