
注：
同一个底层库，应用到不同项目时，数据库可能会不同。如果我们比较下不同数据库操作类之间的不同点，我们会发现所有的方法都是一致的，就是某些类型不同，
所以，根据子类出现的地方，可以用父类替换的原则，将DataAccess中关于特定数据库的类，换成基类，并将创建特定数据库对象实例的代码统一到IDBClinet中

主要类有：
DataAccess: 使用基类访问数据库，并聚合IDBClient来创建特定数据库对象的实例。
IDBClient: 定义创建特定数据库实例的接口，并转成基类型；
SqlServerClient：定义创建SqlServer对象的实例，并转成基类型；
MySqlCient：定义创建MySql对象的实例，并转成基类型；
DBClientFactory：根据类名动态创建IDBClient的实现类；


如果想要更换数据库时，只需要修改如下代码，并在配置文件中修改下连接字符串和具体的DBClient的类名：
  <connectionStrings>
    <add name="dbDefault" connectionString ="Data Source=.;Initial Catalog=hy;Persist Security Info=True;User ID=sa;Password=sa123!@#;" providerName ="System.Data.SqlClient" />
  </connectionStrings>
  <appSettings>
    <add key = "dbClient" value="SqlServerClient"/>
  </appSettings>


        #region 注释
        //private static string ConnectionString = string.Empty;
        //private static IDBClient DBClient = null;
        //private static DataAccess _Instance = null;

        ////数据访问层实例<单件模式> 
        //public static DataAccess Instance //实例
        //{
        //    get
        //    {
        //        if (_Instance == null) _Instance = new DataAccess();
        //        return _Instance;
        //    }
        //}

        //private DataAccess()//私有构造器
        //{
        //    ConnectionString = ConfigurationManager.ConnectionStrings["dbDefault"].ConnectionString;
        //    DBClient = DBClientFactory.GetDBClient(ConfigurationManager.ConnectionStrings["dbDefault"].ProviderName);
        //}
        #endregion