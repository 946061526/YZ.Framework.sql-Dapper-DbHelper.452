using System.Data.Common;

namespace YZ.Framework.DataAccess
{
    /// <summary>
    /// 保存事务，并记录嵌套事务的嵌套级别
    /// </summary>
    internal class TransConnection
    {
        public TransConnection()
        {
            this.Deeps = 0;
        }

        public DbTransaction DBTransaction { get; set; }

        public int Deeps { get; set; }
    }
}
