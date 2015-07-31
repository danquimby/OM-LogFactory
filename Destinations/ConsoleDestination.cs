// ***********************************************************************
// Assembly         : OM-Logger
// Author           : DanQuimby
// Created          : 07-30-2015
//
// Last Modified By : DanQuimby
// Last Modified On : 07-29-2015
// ***********************************************************************
// <copyright file="ConsoleDestination.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM_Logger.Messages;

/// <summary>
/// The Destinations namespace.
/// </summary>
namespace OM_Logger.Destinations
{
    /// <summary>
    /// Class ConsoleDestination.
    /// </summary>
    public class ConsoleDestination : Destination
    {
        // number Indent
        /// <summary>
        /// The indent number
        /// </summary>
        private int IndentNumber = 0;
        /// <summary>
        /// The indent size
        /// </summary>
        public int IndentSize = 2;


        /// <summary>
        /// Gets the indent string.
        /// </summary>
        /// <param name="ShrinkIt">The shrink it.</param>
        /// <param name="IndentChar">The indent character.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Gets the single indent string.
        /// </summary>
        /// <param name="Shrink">The shrink.</param>
        /// <param name="IndentChar">The indent character.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Gets the indent string.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetIndentString()
        {
            return GetIndentString(0);
        }

        /// <summary>
        /// Enters the method.
        /// </summary>
        /// <param name="EMM">The emm.</param>
        public override void EnterMethod(EnterMethodMessage EMM)
        {
            IndentNumber += 2;
            Console.WriteLine("LOG: " + GetIndentString() + GetSingleIndentString(IndentNumber) + ">" + EMM.MethodName);
        }

        /// <summary>
        /// Exits the method.
        /// </summary>
        /// <param name="EMM">The emm.</param>
        public override void ExitMethod(ExitMethodMessage EMM)
        {
            Console.WriteLine("LOG: " + GetSingleIndentString(IndentNumber) + "<" + EMM.MethodName);
            IndentNumber -= 2;
        }

        /// <summary>
        /// Sends the string.
        /// </summary>
        /// <param name="SM">The sm.</param>
        public override void SendString(StringMessage SM)
        {
            Console.WriteLine("LOG: " + GetSingleIndentString(IndentNumber, '.') + SM.Message);
        }

        /// <summary>
        /// Sends the value.
        /// </summary>
        /// <param name="VM">The vm.</param>
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

        /// <summary>
        /// Sends the error.
        /// </summary>
        /// <param name="EM">The em.</param>
        public override void SendError(ErrorMessage EM)
        {
            Console.WriteLine("LOG: " + GetIndentString() + " ERROR: " + EM.Message);
        }
    }
}
