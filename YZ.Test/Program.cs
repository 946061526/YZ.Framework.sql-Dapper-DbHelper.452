using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using YZ.Framework.Utility;
using YZ.Test.TestDb;

namespace YZ.Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Test1());

            try
            {

                //TestDataAccess();

                TestDapper();

                //setConfig();
                //getConfig();


                //IdWorker worker2 = new IdWorker(2);
                //long id = worker2.NextId();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void TestDataAccess()
        {
            /*TestDataAccess*/

            TestDb.TestDataAccess t = new TestDb.TestDataAccess();

            List<GroupInfo> list = t.getList();

            list = t.getListPage();

            //DataTable dt = t.getdt();
            //Console.Write(dt.Rows.Count);

            //var i = t.insert();
            //Console.Write(i);

            //i = t.update();
            //Console.Write(i);

            //i = t.insert();
            //Console.Write(i);

            //i = t.delete();
            //Console.Write(i);

            //var n = t.GetObj();

            var id = t.GetID();
        }

        private static void TestDapper()
        {
            /*TestDapper*/

            //var u = new YZ.Test.TestDb.GroupInfo() { groupName = "test2", remark = "r2" };
            //var Insert = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().Insert(u, x => x.id);

            //var Get = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().Get(x => x.id, 1);

            //var total = 0;
            //Framework.DapperExt.Sorting<YZ.Test.TestDb.GroupInfo>[] sorts = { new Framework.DapperExt.Sorting<YZ.Test.TestDb.GroupInfo>(x => x.id, YZ.Framework.DapperExt.SortType.Desc) };
            //var GetPage = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().GetPage(x => x.id > 0, sorts, 1, 5, false, ref total);

            //var GetList = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().GetList(x => x.id > 0, null);
            //if (GetList != null)
            //    var list = GetList as System.Collections.Generic.List<YZ.Test.TestDb.GroupInfo>;

            //var SqlQuery = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().QueryBySql("select * from groupInfo where id =1", null);

            //List<int> ids = new List<int>();
            //ids.Add(1);
            //ids.Add(2);
            //ids.Add(3);
            //string sid = string.Join(",", ids);
            //var SqlQuery = IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().QueryBySql("select * from groupInfo where id in(" + sid + ")", null);

            //SqlQuery = IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().QueryBySql("select * from groupInfo where id in(" + sid + ")", null);

            List<string> sqlList = new List<string>() {
                "update GroupInfo set groupname='test4' where id=19",
                "insert into MenuInfo values('test3-1','test3-1',9,0,'test3-1','test3-1',1,'');",
            };
            bool b = IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().ExecTransaction(sqlList);


            //TestDb.TestDapperExt dapper = new TestDb.TestDapperExt();
            //dapper.get();

            //dapper.GetList();

            //dapper = new TestDb.TestDapperExt();
            //dapper.getPage();

            //dapper = new TestDb.TestDapperExt();
            //dapper.insert();

            //dapper = new TestDb.TestDapperExt();
            //dapper.update();

            //dapper = new TestDb.TestDapperExt();
            //dapper.delete();}
        }

        private static void setConfig()
        {
            YZ.Test.TestDB.SysConfig config = new TestDB.SysConfig
            {
                uid = "test1",
                pwd = "12345611",
                server = "1.1.1.2",
                db = "testDB1"
            };
            string xmlPath = Path.Combine(Application.StartupPath, "SysConfig.xml");
            XmlSerializer<YZ.Test.TestDB.SysConfig>(config, xmlPath);
        }

        private static YZ.Test.TestDB.SysConfig getConfig()
        {
            YZ.Test.TestDB.SysConfig config = new TestDB.SysConfig();
            string xmlPath = Path.Combine(Application.StartupPath, "SysConfig.xml");
            XmlDocument doc = new XmlDocument();
            if (File.Exists(xmlPath))
            {
                doc.Load(xmlPath);
                config = XmlDeserialize<YZ.Test.TestDB.SysConfig>(doc.OuterXml);
            }
            return config;
        }

        public static void XmlSerializer<T>(T t, string xmlPath)
        {
            if (File.Exists(xmlPath))
            {
                var reader = new XmlSerializer(typeof(T));
                //写入文件
                using (var fs = File.OpenWrite(xmlPath))
                {
                    reader.Serialize(fs, t);
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
        public static T XmlDeserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return default(T);
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(xml);
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(stream);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                T obj = (T)xmlSerializer.Deserialize(reader);
                reader.Close();
                reader.Close();
                writer.Close();
                writer.Dispose();
                return obj;
            }
        }


        public static string XmlSerializer<T>(T t)
        {
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, t, xsn);
            string str = sw.ToString();
            sw.Close();
            sw.Dispose();
            return str;
        }


    }


}
