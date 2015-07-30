using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OM_Logger.Messages;

namespace OM_Logger.Destinations
{
    public class FileDestination : Destination
    {
        private StreamWriter Output;
        private string AbsolutePath;

        public string LogDirectory = "log";
        public bool Overwrite = true;
        public bool AutoFlush = true;
        public string IndentString = "  ";
        private int IndentNumber = 0;

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

        private string GetIndentString()
        {
            return GetIndentString(0);
        }

        private string GetIndentString(int Shrink, char IndentChar = ' ')
        {
            string TempString = "";
            for (int i = 0; i < Shrink; i++)
                TempString += IndentChar;
            return TempString;
        }

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

        public override void EnterMethod(EnterMethodMessage EMM)
        {
            Output.WriteLine("{0}{1} >{2}",
              EMM.UTCTime.ToString("dd-MM-yyyy") + " " + EMM.UTCTime.ToLongTimeString(),
              GetIndentString(IndentNumber),
              EMM.MethodName);
            IndentNumber += 2;
        }

        public override void ExitMethod(ExitMethodMessage EMM)
        {
            IndentNumber -= 2;
            Output.WriteLine("{0}{1} <{2}",
              EMM.UTCTime.ToString("dd-MM-yyyy") + " " + EMM.UTCTime.ToLongTimeString(),
              GetIndentString(IndentNumber),
              EMM.MethodName);
        }

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
