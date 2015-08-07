// ***********************************************************************
// Assembly         : OM-Logger
// Author           : DanQuimby
// Created          : 07-30-2015
//
// Last Modified By : DanQuimby
// Last Modified On : 07-29-2015
// ***********************************************************************
// <copyright file="ErrorMessage.cs" company="">
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
    /// Class ErrorMessage.
    /// </summary>
	public class ErrorMessage: AbstractMessage
	{
        /// <summary>
        /// The _ exception object
        /// </summary>
    private Exception _ExceptionObject;
    /// <summary>
    /// The _ message
    /// </summary>
    private string _Message = string.Empty;

    /// <summary>
    /// Gets or sets the exception object.
    /// </summary>
    /// <value>The exception object.</value>
    public Exception ExceptionObject
    {
      get
      {
        return _ExceptionObject;
      }
      set
      {
        _ExceptionObject = value;
      }
    }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>The message.</value>
    public string Message
    {
      get
      {
        if (_Message == string.Empty
          & _ExceptionObject != null)
          return _ExceptionObject.ToString();
        else
          return _Message;
      }
      set
      {
        _Message = value;           
      }
    }
	}
}