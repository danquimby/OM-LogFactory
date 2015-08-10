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
using System.IO;
using System.Reflection;
using OM_Logger.Messages;

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
        private StreamWriter OutputStream;
        /// <summary>
        /// The absolute path
        /// </summary>
        private string AbsolutePath;
        /// <summary>
        /// The absolute path with number
        /// </summary>
        private string AbsolutePathWithNumber;
        /// <summary>
        /// The log directory
        /// </summary>
        public const string LogDirectory = "log";
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
        public const string IndentString = "  ";
        /// <summary>
        /// The write message
        /// </summary>
        private bool WriteMessage = false;
        /// <summary>
        /// The indent number
        /// </summary>
        private int IndentNumber;
        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <param name="Number">The number.</param>
        /// <param name="Length">The length.</param>
        /// <returns>System.String.</returns>
        private string GetNumber(int Number, int Length)
        {
            int tempLength = Length;
            string tempString = Number.ToString();
            tempString = tempString.Trim();
            tempLength -= tempString.Length;
            for (int i = 0; i < tempLength; i++)
                tempString = "0" + tempString;
            return tempString;
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
            string tempString = "";
            for (int i = 0; i < Shrink; i++)
                tempString += IndentChar;

            return tempString;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize(string NameCategory)
        {
            base.Initialize(NameCategory);
            if (Assembly.GetEntryAssembly() != null && !Assembly.GetEntryAssembly().GlobalAssemblyCache)
                AbsolutePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), LogDirectory);
            else
                AbsolutePath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(LogFactory)).Location), "Logfiles"), LogDirectory);
            AbsolutePath = Path.Combine(AbsolutePath, NameCategory);

            AbsolutePathWithNumber = Path.Combine(AbsolutePath, Category + ".log");
            if (Directory.Exists(AbsolutePath))
            {
                int tempInt = 0;
                while (File.Exists(AbsolutePathWithNumber))
                {
                    tempInt++;
                    AbsolutePathWithNumber = Path.Combine(AbsolutePath, Category + "." + GetNumber(tempInt, 3) + ".log");
                }
            }
            else
            {
                Directory.CreateDirectory(AbsolutePath);
            }
            OutputStream = new StreamWriter(AbsolutePathWithNumber, !Overwrite);
            OutputStream.AutoFlush = AutoFlush;
        }
        /// <summary>
        /// Enters the method.
        /// </summary>
        /// <param name="EMM">The emm.</param>
        public override void EnterMethod(EnterMethodMessage EMM)
        {
            OutputStream.WriteLine("{0}{1} >{2}",
              EMM.UTCTime.ToString("dd-MM-yyyy") + " " + EMM.UTCTime.ToLongTimeString(),
              GetIndentString(IndentNumber),
              EMM.MethodName);
            IndentNumber += 2;
            WriteMessage = true;
        }
        /// <summary>
        /// Exits the method.
        /// </summary>
        /// <param name="EMM">The emm.</param>
        public override void ExitMethod(ExitMethodMessage EMM)
        {
            IndentNumber -= 2;
            OutputStream.WriteLine("{0}{1} <{2}",
              EMM.UTCTime.ToString("dd-MM-yyyy") + " " + EMM.UTCTime.ToLongTimeString(),
              GetIndentString(IndentNumber),
              EMM.MethodName);
            WriteMessage = true;
        }
        /// <summary>
        /// Sends the string.
        /// </summary>
        /// <param name="SM">The sm.</param>
        public override void SendString(StringMessage SM)
        {
            ArrayList tempArray = new ArrayList(SM.Message.Split('\r', '\n'));
            string tempString = new String(' ', SM.UTCTime.ToString("dd-MM-yyyy").Length + 1 + SM.UTCTime.ToLongTimeString().Length);
            tempString += "";
            tempString += GetIndentString();
            OutputStream.WriteLine("{0}{1} {2}",
              SM.UTCTime.ToString("dd-MM-yyyy") + " " + SM.UTCTime.ToLongTimeString(),
              GetIndentString(),
              tempArray[0].ToString());
            tempArray.RemoveAt(0);

            foreach (String s in tempArray)
            {
                if (s.Trim('\r', '\n').Length != 0)
                {
                    OutputStream.WriteLine("{0} {1}",
                      tempString,
                      s);
                }
            }
            WriteMessage = true;
        }
        /// <summary>
        /// Sends the value.
        /// </summary>
        /// <param name="VM">The vm.</param>
        public override void SendValue(ValueMessage VM)
        {
            if (VM.Value == null)
            {
                OutputStream.WriteLine("{0}{1} {2} = **NULL**",
                  VM.UTCTime.ToString("dd-MM-yyyy") + " " + VM.UTCTime.ToLongTimeString(),
                  GetIndentString(),
                  VM.Message);
                WriteMessage = true;
            }
            else
            {
                ArrayList tempArray = new ArrayList(VM.Value.ToString().Split('\r', '\n'));
                string tempString = new String(' ', VM.UTCTime.ToString("dd-MM-yyyy").Length + 1 + VM.UTCTime.ToLongTimeString().Length);
                tempString += "";
                tempString += GetIndentString();
                tempString += " " + new String(' ', VM.Message.Length) + "  ";
                OutputStream.WriteLine("{0}{1} {2} = '{3}'",
                  VM.UTCTime.ToString("dd-MM-yyyy") + " " + VM.UTCTime.ToLongTimeString(),
                  GetIndentString(),
                  VM.Message,
                  tempArray[0].ToString());
                tempArray.RemoveAt(0);
                foreach (String s in tempArray)
                {
                    if (s.Trim('\r', '\n').Length != 0)
                    {
                        OutputStream.WriteLine("{0} '{1}'",
                          tempString,
                          s);
                    }
                }
                WriteMessage = true;
            }
        }
        /// <summary>
        /// Sends the error.
        /// </summary>
        /// <param name="EM">The em.</param>
        public override void SendError(ErrorMessage EM)
        {
            ArrayList tempArray = EM.ExceptionObject == null ? 
                new ArrayList(EM.Message.Split('\r', '\n')) : 
                new ArrayList(EM.ExceptionObject.ToString().Split('\r', '\n'));

            string tempString = new String(' ', EM.UTCTime.ToString("dd-MM-yyyy").Length + 1 + EM.UTCTime.ToLongTimeString().Length);
            tempString += "";
            tempString += GetIndentString();
            tempString += "       ";
            OutputStream.WriteLine("{0}{1} ERROR: {2}",
              EM.UTCTime.ToString("dd-MM-yyyy") + " " + EM.UTCTime.ToLongTimeString(),
              GetIndentString(),
              tempArray[0].ToString());
            tempArray.RemoveAt(0);

            foreach (String s in tempArray)
            {
                if (s.Trim('\r', '\n').Length != 0)
                {
                    OutputStream.WriteLine("{0} {1}",
                      tempString,
                      s);
                }
            }
            WriteMessage = true;
        }
        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            if (!WriteMessage)
            {
                if (File.Exists(AbsolutePathWithNumber))
                {
                    OutputStream.Close();
                    File.Delete(AbsolutePathWithNumber);
                }
            }            
        }
    }
}
