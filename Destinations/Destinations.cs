using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OM_Logger.Messages;

namespace OM_Logger.Destinations
{
    public abstract class Destination
    {
        public string Category;

        public virtual void Initialize()
        {}
        public abstract void EnterMethod(EnterMethodMessage EMM);
        public abstract void ExitMethod(ExitMethodMessage EMM);
        public abstract void SendString(StringMessage SM);
        public abstract void SendValue(ValueMessage VM);
        public abstract void SendError(ErrorMessage EM);
    }
}
