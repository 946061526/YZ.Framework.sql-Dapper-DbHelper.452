using Autofac;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using YZ.Framework.Utility;

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
                //string des = Encrypt.AESEncrypt("123456wer");
                //des = Encrypt.AESDecrypt(des);
                //bool b = des == "123456wer";

                //TestDataAccess();

                //TestDapper();

                setConfig();
                getConfig();

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

            var Get = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().Get(x => x.id > 0 && x.groupName == "superadmin");

            //var total = 0;
            //Framework.DapperExt.Sorting<YZ.Test.TestDb.GroupInfo>[] sorts = { new Framework.DapperExt.Sorting<YZ.Test.TestDb.GroupInfo>(x => x.id, YZ.Framework.DapperExt.SortType.Desc) };
            //var GetPage = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().GetPage(x => x.id > 0, sorts, 1, 5, false, ref total);

            //var GetList = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().GetList(x => x.id > 0, null);
            //if (GetList != null)
            //{
            //    var list = GetList as System.Collections.Generic.List<YZ.Test.TestDb.GroupInfo>;
            //}

            var GetList = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().GetList(x => x.id > 0 && x.groupName != "", null);

            //var SqlQuery = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().QueryBySql("select * from groupInfo where id =1", null);

            //List<string> sqlList = new List<string>() {
            //    "update GroupInfo set groupname='test4' where id=19",
            //    "insert into MenuInfo values('test3-1','test3-1',9,0,'test3-1','test3-1',1,'');",
            //};
            //bool b = IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<YZ.Test.TestDb.GroupInfo>>().ExecTransaction(sqlList);


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
            YZ.Test.TestDB.SysInfo sysInfo = new TestDB.SysInfo
            {
                Ver = "123",
                Name = "tetse"
            };
            YZ.Test.TestDB.DbInfo dbInfo = new TestDB.DbInfo
            {
                Type = "mssql",
                Server = "24423",
                Port = "243",
                Uid = "sdff",
                Pwd = "asdfd",
                DbName = "sdfd"
            };
            YZ.Test.TestDB.SystemConfig config = new TestDB.SystemConfig
            {
                SysInfo = sysInfo,
                DbInfo = dbInfo
            };
            string xmlPath = Path.Combine(Application.StartupPath, "SystemConfig.xml");
            XmlSerializer<YZ.Test.TestDB.SystemConfig>(config, xmlPath);
        }

        private static YZ.Test.TestDB.SystemConfig getConfig()
        {
            YZ.Test.TestDB.SystemConfig config = new TestDB.SystemConfig();
            string xmlPath = Path.Combine(Application.StartupPath, "SystemConfig.xml");
            XmlDocument doc = new XmlDocument();
            if (File.Exists(xmlPath))
            {
                doc.Load(xmlPath);
                config = XmlDeserialize<YZ.Test.TestDB.SystemConfig>(doc.OuterXml);
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
