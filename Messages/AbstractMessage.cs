// ***********************************************************************
// Assembly         : OM-Logger
// Author           : DanQuimby
// Created          : 07-30-2015
//
// Last Modified By : DanQuimby
// Last Modified On : 07-29-2015
// ***********************************************************************
// <copyright file="AbstractMessage.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Diagnostics;
using System.Reflection;

/// <summary>
/// The Messages namespace.
/// </summary>
namespace OM_Logger.Messages
{
    /// <summary>
    /// Summary description for AbstractMessage.
    /// </summary>
    public class AbstractMessage
    {
        /// <summary>
        /// The _ machine name
        /// </summary>
        private string _MachineName;
        /// <summary>
        /// The _ application name
        /// </summary>
        private string _AppName;
        /// <summary>
        /// The _ application domain friendly name
        /// </summary>
        private string _AppDomainFriendlyName;
        /// <summary>
        /// The _ UTC time
        /// </summary>
        private DateTime _UTCTime;
        /// <summary>
        /// The _ os platform
        /// </summary>
        private string _OSPlatform;
        /// <summary>
        /// The _ os version
        /// </summary>
        private string _OSVersion;
        /// <summary>
        /// The _ stack
        /// </summary>
        private StackTrace _Stack;
        /// <summary>
        /// The _ user domain name
        /// </summary>
        private string _UserDomainName;
        /// <summary>
        /// The _ user name
        /// </summary>
        private string _UserName;
        /// <summary>
        /// The _ user interactive
        /// </summary>
        private bool _UserInteractive;
        /// <summary>
        /// The _ runtime version
        /// </summary>
        private string _RuntimeVersion;

        /// <summary>
        /// Initializes the new message.
        /// </summary>
        protected virtual void InitNewMessage()
        {
        }

        /// <summary>
        /// Initializes the new message.
        /// </summary>
        public void InitializeNewMessage()
        {
            _MachineName = Environment.MachineName;
            if (Assembly.GetEntryAssembly() != null)
            {
                _AppName = Assembly.GetEntryAssembly().Location;
            }
            else
            {
                _AppName = "<unknown>";
            }
            _AppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName;
            _UTCTime = DateTime.UtcNow;
            _OSPlatform = Environment.OSVersion.Platform.ToString();
            _OSVersion = Environment.OSVersion.Version.ToString();
            _Stack = new StackTrace(0);
            _UserDomainName = Environment.UserDomainName;
            _UserName = Environment.UserName;
            _UserInteractive = Environment.UserInteractive;
            _RuntimeVersion = Environment.Version.ToString();
            InitNewMessage();
        }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName
        {
            get
            {
                return _MachineName;
            }
            set
            {
                _MachineName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string AppName
        {
            get
            {
                return _AppName;
            }
            set
            {
                _AppName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the application domain friendly.
        /// </summary>
        /// <value>The name of the application domain friendly.</value>
        public string AppDomainFriendlyName
        {
            get
            {
                return _AppDomainFriendlyName;
            }
            set
            {
                _AppDomainFriendlyName = value;
            }
        }

        /// <summary>
        /// Gets or sets the UTC time.
        /// </summary>
        /// <value>The UTC time.</value>
        public DateTime UTCTime
        {
            get
            {
                return _UTCTime;
            }
            set
            {
                _UTCTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the os platform.
        /// </summary>
        /// <value>The os platform.</value>
        public string OSPlatform
        {
            get
            {
                return _OSPlatform;
            }
            set
            {
                _OSPlatform = value;
            }
        }

        /// <summary>
        /// Gets or sets the os version.
        /// </summary>
        /// <value>The os version.</value>
        public string OSVersion
        {
            get
            {
                return _OSVersion;
            }
            set
            {
                _OSVersion = value;
            }
        }

        /// <summary>
        /// Gets or sets the stack.
        /// </summary>
        /// <value>The stack.</value>
        public StackTrace Stack
        {
            get
            {
                return _Stack;
            }
            set
            {
                _Stack = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the user domain.
        /// </summary>
        /// <value>The name of the user domain.</value>
        public string UserDomainName
        {
            get
            {
                return _UserDomainName;
            }
            set
            {
                _UserDomainName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [user interactive].
        /// </summary>
        /// <value><c>true</c> if [user interactive]; otherwise, <c>false</c>.</value>
        public bool UserInteractive
        {
            get
            {
                return _UserInteractive;
            }
            set
            {
                UserInteractive = value;
            }
        }

        /// <summary>
        /// Gets or sets the runtime version.
        /// </summary>
        /// <value>The runtime version.</value>
        public string RuntimeVersion
        {
            get
            {
                return _RuntimeVersion;
            }
            set
            {
                _RuntimeVersion = value;
            }
        }
    }
}
