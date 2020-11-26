using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones
{
    public class Status
    {
        public bool IsError { get; private set; } = false;
        public bool WithException { private get; set; } = false;
        private string message = "";
        private string messageException = "";
        public string Message
        {
            get { IsError = false; if (WithException) { message += "\n\r" + messageException; } return message; }
            private set { message = value; }
        }
        public void SetError(string logError)
        {
            IsError = true;
            Message = logError;
        }
        public void SetError(string logError, string messageException)
        {
            IsError = true;
            Message = logError;
            this.messageException = messageException;
        }
    }
    public class LogError
    {
        private static LStatus Status { get; set; } = new LStatus();

        public static string GetError
        {
            get
            {
                string statuses = null;
                if (Status.Count > 0)
                {
                    statuses = Status[0].Message;
                    Status.RemoveAt(0);
                }
                return statuses;
            }
        }
        public static void SetError(string logError)
        {
            Status.Add(logError);
        }
        public static void SetError(string logError, Exception ex)
        {
            Status.Add(logError, ex);
        }
        public static bool HayErrores
        {
            get
            {
                if (Status.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
    public class LStatus : List<Status>
    {
        public new void Add(Status item)
        {
            base.Add(item);
        }
        public void Add(string logError)
        {
            Status status = new Status();
            status.SetError(logError);
            base.Add(status);
        }
        public void Add(string logError, Exception ex)
        {
            Status status = new Status();
            status.SetError(logError, ex.Message);
            base.Add(status);
        }
        public new int Count
        {
            get
            {
                return base.Count;
            }
        }
    }

}
