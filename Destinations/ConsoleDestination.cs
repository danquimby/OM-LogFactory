using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM_Logger.Messages;

namespace OM_Logger.Destinations
{
    public class ConsoleDestination : Destination
    {
        // number Indent
        private int IndentNumber = 0;
        public int IndentSize = 2;


        private string GetIndentString(int ShrinkIt, char IndentChar = ' ')
        {

            string OneIndentString = "";
            for (int i = 1; i <= ShrinkIt; i++)
                OneIndentString += IndentChar;

            string TempString = "";
            for (int i = 1; i <= LogFactory.Indent; i++)
                TempString += OneIndentString;

            if (TempString.Length < ShrinkIt)
            {
                TempString = "";
            }
            else
            {
                if (ShrinkIt != 0)
                {
                    TempString = TempString.Remove(0, (IndentSize / ShrinkIt));
                }
            }
            return TempString;
        }

        private string GetSingleIndentString(int Shrink, char IndentChar = ' ')
        {
            string OneIndentString = "";
            for (int i = 1; i <= Shrink; i++)
                OneIndentString += IndentChar;
            if (Shrink != 0)
            {
                OneIndentString = OneIndentString.Remove(0, (IndentSize / Shrink));
            }

            return OneIndentString;
        }

        private string GetIndentString()
        {
            return GetIndentString(0);
        }

        public override void EnterMethod(EnterMethodMessage EMM)
        {
            IndentNumber += 2;
            Console.WriteLine("LOG: " + GetIndentString() + GetSingleIndentString(IndentNumber) + ">" + EMM.MethodName);
        }

        public override void ExitMethod(ExitMethodMessage EMM)
        {
            Console.WriteLine("LOG: " + GetSingleIndentString(IndentNumber) + "<" + EMM.MethodName);
            IndentNumber -= 2;
        }

        public override void SendString(StringMessage SM)
        {
            Console.WriteLine("LOG: " + GetSingleIndentString(IndentNumber, '.') + SM.Message);
        }

        public override void SendValue(ValueMessage VM)
        {
            if (VM.Value == null)
            {
                Console.WriteLine("LOG: " + GetIndentString() + String.Format("{0} = NULL", VM.Message));
            }
            else
            {
                Console.WriteLine("LOG: " + GetIndentString() + String.Format("{0} = '{1}'", VM.Message, VM.Value.ToString()));
            }
        }

        public override void SendError(ErrorMessage EM)
        {
            Console.WriteLine("LOG: " + GetIndentString() + " ERROR: " + EM.Message);
        }
    }
}
