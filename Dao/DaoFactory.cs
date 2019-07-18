using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public static class DaoFactory
    {
        public static T CreateDao<T>() where T : Dao, new()
        {
            return new T();
        }
    }
}
