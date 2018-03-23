using Autofac;
using System;

namespace YZ.Framework.DapperExt
{
    public class IocContainer
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
    }



    public class ApplicationContainer
    {
        public static IContainer Container { get; set; }
    }
    public class ResolverHelper
    {
        //ResolverHelper.GetResolver<MVCBlog.Service.PostService>();

        public static object GetResolver<TService>()
        {
            var iTypes = typeof(TService).GetInterfaces();
            if (iTypes.Length == 0)
            {
                throw new NotImplementedException("依赖注入的类型没有实现接口");
            }
            Type iType = iTypes[0];
            if (ApplicationContainer.Container.IsRegistered(iType))
            {
                return ApplicationContainer.Container.Resolve(iType);
            }

            var builder = new ContainerBuilder();

            //RegisterServices<TService>(builder, iType);
            //builder.Update(ApplicationContainer.Container);
            builder.Register<IDapperContext>(c => new DapperContext("dbDefault", DbType.MsSql));
            builder.RegisterGeneric(typeof(RespositoryBase<>)).As(typeof(IRespositoryBase<>));
            //return builder.Build();

            return ApplicationContainer.Container.Resolve(iType);
        }
        private static void RegisterServices<TService>(ContainerBuilder builder, Type iType)
        {
            builder.RegisterType<TService>().As(new Type[] { iType });
        }
    }
}
