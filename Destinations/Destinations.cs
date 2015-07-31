// ***********************************************************************
// Assembly         : OM-Logger
// Author           : DanQuimby
// Created          : 07-30-2015
//
// Last Modified By : DanQuimby
// Last Modified On : 07-31-2015
// ***********************************************************************
// <copyright file="Destinations.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OM_Logger.Messages;

/// <summary>
/// The Destinations namespace.
/// </summary>
namespace OM_Logger.Destinations
{
    /// <summary>
    /// Class Destination.
    /// </summary>
    public abstract class Destination
    {
        /// <summary>
        /// The category
        /// </summary>
        public string Category;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {}

        /// <summary>
        /// Enters the method.
        /// </summary>
        /// <param name="EMM">The emm.</param>
        public abstract void EnterMethod(EnterMethodMessage EMM);

        /// <summary>
        /// Exits the method.
        /// </summary>
        /// <param name="EMM">The emm.</param>
        public abstract void ExitMethod(ExitMethodMessage EMM);

        /// <summary>
        /// Sends the string.
        /// </summary>
        /// <param name="SM">The sm.</param>
        public abstract void SendString(StringMessage SM);

        /// <summary>
        /// Sends the value.
        /// </summary>
        /// <param name="VM">The vm.</param>
        public abstract void SendValue(ValueMessage VM);

        /// <summary>
        /// Sends the error.
        /// </summary>
        /// <param name="EM">The em.</param>
        public abstract void SendError(ErrorMessage EM);
    }
}
