// ***********************************************************************
// Assembly         : OM-Logger
// Author           : Ignatov Denis (DanQuimby)
// Created          : 07-30-2015
//
// Last Modified By : Ignatov Denis (DanQuimby)
// Last Modified On : 08-10-2015
// ***********************************************************************
// <copyright file="LogFactory.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace OM_Logger
{
    /// <summary>
    /// Class LogFactory. This class cannot be inherited.
    /// </summary>
    public sealed class LogFactory
    {
        /// <summary>
        /// The list categories
        /// </summary>
        private static readonly List<Category> ListCategories = new List<Category>();
        /// <summary>
        /// The indent
        /// </summary>
        internal static int Indent = 0;
        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="NameCategory">The name category.</param>
        /// <returns>Category.</returns>
        public static Category GetCategory(string NameCategory)
        {
            foreach (Category item in ListCategories)
            {
                if (item.Name == NameCategory)
                    return item;
            }
            Category category = new Category(NameCategory);
            ListCategories.Add(category);
            return category;
        }
    }
}
