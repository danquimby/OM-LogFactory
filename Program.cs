// ***********************************************************************
// Assembly         : OM-Logger
// Author           : User
// Created          : 07-29-2015
//
// Last Modified By : User
// Last Modified On : 07-31-2015
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The OM_Logger namespace.
/// </summary>
namespace OM_Logger
{

    /// <summary>
    /// тестовый класс нужен для теста
    /// </summary>
    public class Foo
    {

        /// <summary>
        /// стандартный конструктор
        /// </summary>
        public Foo()
        {
            Category cat = LogFactory.GetCategory("network");
            cat.AddDestination(TypeDestination.TypeFile);
        }

        /// <summary>
        /// куйня какая то для теста
        /// </summary>
        public void prin()
        {
            Category cat = LogFactory.GetCategory("network");
            cat.SendString("ssssss");
        }
    }
    /// <summary>
    /// Gets the R component from ABGR value returned by
    /// <see cref="O:BitMiracle.LibTiff.Classic.Tiff.ReadRGBAImage">ReadRGBAImage</see>.
    /// </summary>
    /// <param name="abgr">The ABGR value.</param>
    /// <returns>The R component from ABGR value.</returns>
    class Program
    {

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Foo f = new Foo();
            Category cat = LogFactory.GetCategory("network");
            cat.EnterMethod();
            f.prin();
            f.prin();
            f.prin();
            cat.ExitMethod();
            
        }
    }
}
