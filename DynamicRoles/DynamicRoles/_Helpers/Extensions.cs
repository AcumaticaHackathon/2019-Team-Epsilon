using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicRoles
{
    public static  class Extensions
    {
        public static List<T> GetAsList<T>(this PXResultset<T> resultSet)
           where T : class, IBqlTable, new()
        {
            List<T> toReturn = new List<T>();
            if (resultSet != null && resultSet.Any())
            {
                foreach (var result in resultSet)
                {
                    toReturn.Add(result.GetItem<T>());
                }
            }

            return toReturn;
        }
    }
}
