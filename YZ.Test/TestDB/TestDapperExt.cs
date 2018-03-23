using System.Collections.Generic;
using YZ.Framework.DapperExt;

namespace YZ.Test.TestDb
{
    public class TestDapperExt
    {
        #region ext1
        private IDapperContext context = new DapperContext("dbDefault");
        private IRespositoryBase<GroupInfo> userRespo;

        public TestDapperExt()
        {
            userRespo = new RespositoryBase<GroupInfo>(context);
        }

        public GroupInfo get()
        {
            var g = userRespo.Get(x => x.id, 1);
            return g;
        }

        public List<GroupInfo> GetList()
        {
            var list = userRespo.GetList(x => x.id > 0, null) as List<GroupInfo>;
            return list;
        }

        public void insert()
        {
            var u = new GroupInfo() { groupName = "test1", remark = "r1" };
            //指定主键id
            var b = //userRespo.Insert(u);
            //不指定主键id
            userRespo.Insert(u, x => x.id);
        }

        public void update()
        {
            //var f1 = new DbFiled<GroupInfo>(x => x.groupName, "update");
            //var f2 = new DbFiled<GroupInfo>(x => x.remark, "update");
            DbFiled<GroupInfo>[] fs = {
                new DbFiled<GroupInfo>(x => x.groupName, "update2"),
                new DbFiled<GroupInfo>(x => x.remark, "update2")
            };

            var b = userRespo.Update(x => x.id == 5, fs);
        }

        public void getPage()
        {
            var total = 0;
            Sorting<GroupInfo>[] sorts = { new Sorting<GroupInfo>(x => x.id, YZ.Framework.DapperExt.SortType.Desc) };
            var b = userRespo.GetPage(x => x.id > 0, sorts, 1, 5, false, ref total);
        }

        public void delete()
        {
            var b = userRespo.Delete(x => x.id == 4);
        }

        #endregion


        public void GetById()
        {
            //GroupInfo t = ContextFactory.CreateDbSet<GroupInfo>().GetById(1);

            //var g = new S.Frame.DataCore.Dapper.DapperContext<GroupInfo>().GetById(1);

        }


    }

    public class GroupInfo
    {
        public int id { get; set; }

        public string groupName { get; set; }

        public string remark { get; set; }
    }
}
