using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using OM_Logger.Destinations;
using OM_Logger.Messages;

namespace OM_Logger
{
    // new change
    // more change
    public enum TypeDestination
    {
        TypeConsole,
        TypeFile
    }

    public class Category
    {
        private string _Name;
        private Exception LastException;
        private IList<Destination> listDestination = new List<Destination>();
        public string Name
        {
            get
            {
                return _Name;
            }
            private set
            {
                _Name = value;
            }
        }
        /*
            Summary:
                Main constructor
        
            Parameters:
            value:
                Name of category
        
        */
        public Category(string nameCategory)
        {
            Name = nameCategory;
        }
        /*
            Summary:
                Add Destination in container of output.
        
            Parameters:
            value:
                enum Type of output.
        */

        public void CloseDestination()
        {
            foreach (Destination item in listDestination)
            {
                item.Close();
            }
        }
        public void AddDestination(TypeDestination Type)
        {
            Destination destination;
            if (Type == TypeDestination.TypeFile)
                destination = new FileDestination();
            else if (Type == TypeDestination.TypeConsole)
                destination = new ConsoleDestination();
            else return;

            destination.Category = Name;
            destination.Initialize(Name);
            listDestination.Add(destination);
        }
        /*
            Summary:
                Send to output message(String)
        
            Parameters:
            value:
                Message to output.
        
        */
        public void SendString(string MessageToSend)
        {
            StringMessage SM = new StringMessage();
            SM.Message = MessageToSend;
            SM.InitializeNewMessage();
            foreach (Destination d in listDestination)
            {
                d.SendString(SM);
            }
        }
        /*
            Summary:
                Send to output template Dictionary.
        
            Parameters:
            value:
                Dictionary to output.
        */
        public void SendToDictionary<K, V>(dynamic Dictionary)
        {
            foreach (KeyValuePair<K, V> pair in Dictionary)
            {
                if (Convert.ToString(pair.Key) != null && pair.Value != null)
                    SendValue(Convert.ToString(pair.Key), pair.Value);
            }
        }
        /*
            Summary:
                Send to output template List.
        
            Parameters:
            value:
                List contains to output.
        */
        public void SendList(string Message, dynamic List)
        {
            foreach (var item in List)
                SendValue(Message, item);
        }
        /*
            Summary:
                Send to output pair <message, some object>
        
            Parameters:
            value:
                message (string)
            value:
                some object (with method toString())
        
        */
        public void SendValue(string Message, object Value)
        {
            ValueMessage VM = new ValueMessage();
            VM.InitializeNewMessage();
            VM.Message = Message;
            VM.Value = Value;
            foreach (Destination d in listDestination)
            {
                d.SendValue(VM);
            }
        }
        /*
            Summary:
                It provides for the possibility of displaying attachments.
        */
        public void EnterMethod()
        {
            EnterMethod(1);
        }
        /*
            Summary:
                It provides for the possibility of displaying attachments.
        */
        public void EnterMethod(int MethodOffSet)
        {
            EnterMethodMessage EMM = new EnterMethodMessage();
            EMM._MethodOffset = MethodOffSet + 2;
            EMM.InitializeNewMessage();
            foreach (Destination d in listDestination)
            {
                d.EnterMethod(EMM);
            }
        }
        /*
            Summary:
                It provides for the possibility of displaying attachments.
        */
        public void ExitMethod()
        {
            ExitMethod(1);
        }
        /*
            Summary:
                It provides for the possibility of displaying attachments.
        */
        public void ExitMethod(int MethodOffSet)
        {
            ExitMethodMessage EMM = new ExitMethodMessage();
            EMM._MethodOffset = MethodOffSet + 2;
            EMM.InitializeNewMessage();
            foreach (Destination d in listDestination)
            {
                d.ExitMethod(EMM);
            }
        }
        /*
            Summary:
                Send message whit word 'error'
        
            Parameters:
            value:
                message of output.
        */
        public void SendError(string Message)
        {
            ErrorMessage EM = new ErrorMessage();
            EM.InitializeNewMessage();
            EM.Message = Message;
            foreach (Destination d in listDestination)
            {
                d.SendError(EM);
            }
        }
        /*
            Summary:
                Output Exception
        
            Parameters:
            value:
                A pointer to a class Exception.
        
        */
        public void SendError(Exception Error)
        {
            if (Error != LastException)
            {
                LastException = Error;
                ErrorMessage EM = new ErrorMessage();
                EM.InitializeNewMessage();
                EM.ExceptionObject = Error;
                foreach (Destination d in listDestination)
                {
                    d.SendError(EM);
                }
            }
        }
    }
}
