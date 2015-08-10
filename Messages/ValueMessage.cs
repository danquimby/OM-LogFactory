// ***********************************************************************
// Assembly         : OM-Logger
// Author           : DanQuimby
// Created          : 07-30-2015
//
// Last Modified By : DanQuimby
// Last Modified On : 07-29-2015
// ***********************************************************************
// <copyright file="ValueMessage.cs" company="">
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
    /// Class ValueMessage.
    /// </summary>
    public class ValueMessage : AbstractMessage
    {
        /// <summary>
        /// The _ message
        /// </summary>
        private string _Message;
        /// <summary>
        /// The _ value
        /// </summary>
        private object _Value;
        /// <summary>
        /// The _ type
        /// </summary>
        private Type _Type;

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

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    if (_Value == null)
                    {
                        _Type = null;
                    }
                    else
                    {
                        _Type = _Value.GetType();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type
        {
            get
            {
                return _Type;
            }
        }
    }
}
