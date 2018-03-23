using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Framework.DapperExt;

namespace YZ.Test
{
    public class IocContainer
    {
        public static IContainer Instance
        {
            get
            {
                ContainerBuilder builder = new ContainerBuilder();
                builder.Register<IDapperContext>(c => new DapperContext("dbDefault", DbType.MsSql)).SingleInstance();
                builder.RegisterGeneric(typeof(RespositoryBase<>)).As(typeof(IRespositoryBase<>)).SingleInstance();
                return builder.Build();
            }
        }
    }

}
