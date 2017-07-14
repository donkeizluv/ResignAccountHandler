using System;

namespace ResignAccountHandlerUI.Log
{
    public delegate void OnNewLogHandler(ILogger logger, NewLogEventArgs e);

    public interface ILogger
    {
        Type ClassType { get; }
        void Log(string log);
        void Log(Exception ex);
        event OnNewLogHandler OnNewLog;
    }
    public class NewLogEventArgs : EventArgs
    {
        public NewLogEventArgs(string log)
        {
            Log = log;
        }
        public NewLogEventArgs(Exception ex)
        {
            Ex = ex;
        }
        public Exception Ex { get; private set; }
        public string Log { get; private set; } = string.Empty;
    }
    public class SimpleLogger : ILogger
    {
        public SimpleLogger(Type type)
        {
            ClassType = type;
        }

        public Type ClassType { get; }
        public event OnNewLogHandler OnNewLog;

        public void Log(string log)
        {
            RaiseNewLogEvent(log);
        }

        public void Log(Exception ex)
        {
            RaiseNewLogEvent(ex);

        }

        private void RaiseNewLogEvent(string log)
        {
            OnNewLog?.Invoke(this, new NewLogEventArgs(log));
        }
        private void RaiseNewLogEvent(Exception ex)
        {
            OnNewLog?.Invoke(this, new NewLogEventArgs(ex));
        }

    }
}