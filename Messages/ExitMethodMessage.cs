// ***********************************************************************
// Assembly         : OM-Logger
// Author           : DanQuimby
// Created          : 07-30-2015
//
// Last Modified By : DanQuimby
// Last Modified On : 07-29-2015
// ***********************************************************************
// <copyright file="ExitMethodMessage.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Diagnostics;

/// <summary>
/// The Messages namespace.
/// </summary>
namespace OM_Logger.Messages
{
    /// <summary>
    /// Class ExitMethodMessage.
    /// </summary>
	public class ExitMethodMessage: AbstractMessage
	{
        /// <summary>
        /// The _ method name
        /// </summary>
    private string _MethodName;
    /// <summary>
    /// The _ method offset
    /// </summary>
    internal int _MethodOffset = 0;

    /// <summary>
    /// Initializes the new message.
    /// </summary>
    protected override void InitNewMessage()
    {
      base.InitNewMessage();
      this.MethodName = this.Stack.GetFrame(_MethodOffset).GetMethod().DeclaringType.FullName + "." + this.Stack.GetFrame(_MethodOffset).GetMethod().Name;
    }

    /// <summary>
    /// Gets or sets the name of the method.
    /// </summary>
    /// <value>The name of the method.</value>
    public string MethodName
    {
      get
      {
        return _MethodName;
      }
      set
      {
        _MethodName = value;
      }
    }
	}
}
