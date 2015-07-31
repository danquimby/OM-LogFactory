// ***********************************************************************
// Assembly         : OM-Logger
// Author           : DanQuimby
// Created          : 07-30-2015
//
// Last Modified By : DanQuimby
// Last Modified On : 07-29-2015
// ***********************************************************************
// <copyright file="StringMessage.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

/// <summary>
/// The Messages namespace.
/// </summary>
namespace OM_Logger.Messages
{
    /// <summary>
    /// Class StringMessage.
    /// </summary>
	public class StringMessage: AbstractMessage
	{
        /// <summary>
        /// The _ message
        /// </summary>
    private string _Message;

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>The message.</value>
    public string Message
    {
      get
      {
        return _Message;
      }
      set
      {
        _Message = value;
      }
    }
	}
}
