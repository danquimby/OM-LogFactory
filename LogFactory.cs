using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_Logger
{
    public sealed class LogFactory
    {
        private static readonly List<Category> listCategorys = new List<Category>();
        internal static int Indent = 0;
        public static Category GetCategory(string nameCategory)
        {
            foreach (Category item in listCategorys)
            {
                if (item.Name == nameCategory)
                    return item;
            }
            Category category = new Category(nameCategory);
            listCategorys.Add(category);
            return category;
        }
    }
}
