using Autofac;

namespace YZ.Framework.DapperExt
{
    //public class IocContainer
    //{
    //    public static IContainer Instance
    //    {
    //        get
    //        {
    //            ContainerBuilder builder = new ContainerBuilder();
    //            builder.Register<IDapperContext>(c => new DapperContext("dbDefault", DbType.MsSql));
    //            builder.RegisterGeneric(typeof(RespositoryBase<>)).As(typeof(IRespositoryBase<>));
    //            return builder.Build();
    //        }
    //    }
    //}

    public static class IocContainer
    {
        public static IContainer Instance
        {
            get
            {
                ContainerBuilder builder = new ContainerBuilder();
                builder.Register<IDapperContext>(c => new DapperContext("dbDefault", DbType.MsSql));
                builder.RegisterGeneric(typeof(RespositoryBase<>)).As(typeof(IRespositoryBase<>));
                return builder.Build();
            }
        }

        private static IContainer _IContainer = null;
        static IocContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register<IDapperContext>(c => new DapperContext("dbDefault", DbType.MsSql));
            builder.RegisterGeneric(typeof(RespositoryBase<>)).As(typeof(IRespositoryBase<>));
            _IContainer = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _IContainer.Resolve<T>();
        }
    }

}
