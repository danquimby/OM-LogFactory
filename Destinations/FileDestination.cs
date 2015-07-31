// ***********************************************************************
// Assembly         : OM-Logger
// Author           : DanQuimby
// Created          : 07-30-2015
//
// Last Modified By : DanQuimby
// Last Modified On : 07-31-2015
// ***********************************************************************
// <copyright file="FileDestination.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    /// Class FileDestination.
    /// </summary>
    public class FileDestination : Destination
    {
        /// <summary>
        /// The output
        /// </summary>
        private StreamWriter Output;
        /// <summary>
        /// The absolute path
        /// </summary>
        private string AbsolutePath;

        /// <summary>
        /// The log directory
        /// </summary>
        public string LogDirectory = "log";
        /// <summary>
        /// The overwrite
        /// </summary>
        public bool Overwrite = true;
        /// <summary>
        /// The automatic flush
        /// </summary>
        public bool AutoFlush = true;
        /// <summary>
        /// The indent string
        /// </summary>
        public string IndentString = "  ";
        /// <summary>
        /// The indent number
        /// </summary>
        private int IndentNumber = 0;

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <param name="Number">The number.</param>
        /// <param name="Length">The length.</param>
        /// <returns>System.String.</returns>
        private string GetNumber(int Number, int Length)
        {
            int TempLength = Length;
            string TempString = Number.ToString();
            TempString = TempString.Trim();
            TempLength -= TempString.Length;
            for (int i = 0; i < TempLength; i++)
                TempString = "0" + TempString;
            return TempString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetIndentString()
        {
            return GetIndentString(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Shrink"></param>
        /// <param name="IndentChar"></param>
        /// <returns></returns>
        private string GetIndentString(int Shrink, char IndentChar = ' ')
        {
            string TempString = "";
            for (int i = 0; i < Shrink; i++)
                TempString += IndentChar;
            return TempString;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            if (Assembly.GetEntryAssembly() != null && !Assembly.GetEntryAssembly().GlobalAssemblyCache)
                AbsolutePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), LogDirectory);
            else
                AbsolutePath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(LogFactory)).Location), "Logfiles"), LogDirectory);

            string TempFileName = Path.Combine(AbsolutePath, Category + ".log");
            if (Directory.Exists(AbsolutePath))
            {
                int TempInt = 0;
                while (File.Exists(TempFileName))
                {
                    TempInt++;
                    TempFileName = Path.Combine(AbsolutePath, Category + "." + GetNumber(TempInt, 3) + ".log");
                }
            }
            else
            {
                Directory.CreateDirectory(AbsolutePath);
            }
            Output = new StreamWriter(TempFileName, !Overwrite);
            Output.AutoFlush = AutoFlush;
        }

        /// <summary>
        /// Enters the method.
        /// </summary>
        /// <param name="EMM">The emm.</param>
        public override void EnterMethod(EnterMethodMessage EMM)
        {
            Output.WriteLine("{0}{1} >{2}",
              EMM.UTCTime.ToString("dd-MM-yyyy") + " " + EMM.UTCTime.ToLongTimeString(),
              GetIndentString(IndentNumber),
              EMM.MethodName);
            IndentNumber += 2;
        }

        /// <summary>
        /// Exits the method.
        /// </summary>
        /// <param name="EMM">The emm.</param>
        public override void ExitMethod(ExitMethodMessage EMM)
        {
            IndentNumber -= 2;
            Output.WriteLine("{0}{1} <{2}",
              EMM.UTCTime.ToString("dd-MM-yyyy") + " " + EMM.UTCTime.ToLongTimeString(),
              GetIndentString(IndentNumber),
              EMM.MethodName);
        }

        /// <summary>
        /// Sends the string.
        /// </summary>
        /// <param name="SM">The sm.</param>
        public override void SendString(StringMessage SM)
        {
            ArrayList TempArray = new ArrayList(SM.Message.Split('\r', '\n'));
            string TempString = new String(' ', SM.UTCTime.ToString("dd-MM-yyyy").Length + 1 + SM.UTCTime.ToLongTimeString().Length);
            TempString += "";
            TempString += GetIndentString();
            Output.WriteLine("{0}{1} {2}",
              SM.UTCTime.ToString("dd-MM-yyyy") + " " + SM.UTCTime.ToLongTimeString(),
              GetIndentString(),
              TempArray[0].ToString());
            TempArray.RemoveAt(0);
            foreach (String s in TempArray)
            {
                if (s.Trim('\r', '\n').Length != 0)
                {
                    Output.WriteLine("{0} {1}",
                      TempString,
                      s);
                }
            }
        }

        /// <summary>
        /// Sends the value.
        /// </summary>
        /// <param name="VM">The vm.</param>
        public override void SendValue(ValueMessage VM)
        {
            if (VM.Value == null)
            {
                Output.WriteLine("{0}{1} {2} = **NULL**",
                  VM.UTCTime.ToString("dd-MM-yyyy") + " " + VM.UTCTime.ToLongTimeString(),
                  GetIndentString(),
                  VM.Message);
            }
            else
            {
                ArrayList TempArray = new ArrayList(VM.Value.ToString().Split('\r', '\n'));
                string TempString = new String(' ', VM.UTCTime.ToString("dd-MM-yyyy").Length + 1 + VM.UTCTime.ToLongTimeString().Length);
                TempString += "";
                TempString += GetIndentString();
                TempString += " " + new String(' ', VM.Message.Length) + "  ";
                Output.WriteLine("{0}{1} {2} = '{3}'",
                  VM.UTCTime.ToString("dd-MM-yyyy") + " " + VM.UTCTime.ToLongTimeString(),
                  GetIndentString(),
                  VM.Message,
                  TempArray[0].ToString());
                TempArray.RemoveAt(0);
                foreach (String s in TempArray)
                {
                    if (s.Trim('\r', '\n').Length != 0)
                    {
                        Output.WriteLine("{0} '{1}'",
                          TempString,
                          s);
                    }
                }
            }
        }

        /// <summary>
        /// Sends the error.
        /// </summary>
        /// <param name="EM">The em.</param>
        public override void SendError(ErrorMessage EM)
        {
            ArrayList TempArray;
            if (EM.ExceptionObject == null)
                TempArray = new ArrayList(EM.Message.Split('\r', '\n'));
            else
                TempArray = new ArrayList(EM.ExceptionObject.ToString().Split('\r', '\n'));

            string TempString = new String(' ', EM.UTCTime.ToString("dd-MM-yyyy").Length + 1 + EM.UTCTime.ToLongTimeString().Length);
            TempString += "";
            TempString += GetIndentString();
            TempString += "       ";
            Output.WriteLine("{0}{1} ERROR: {2}",
              EM.UTCTime.ToString("dd-MM-yyyy") + " " + EM.UTCTime.ToLongTimeString(),
              GetIndentString(),
              TempArray[0].ToString());
            TempArray.RemoveAt(0);
            foreach (String s in TempArray)
            {
                if (s.Trim('\r', '\n').Length != 0)
                {
                    Output.WriteLine("{0} {1}",
                      TempString,
                      s);
                }
            }
        }
    }
}
