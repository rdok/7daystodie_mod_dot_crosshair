namespace DotCrosshair.Harmony
{
    public class Logger : ILogger
    {
        public void Info(string message)
        {
            Log.Out(message);
        }

        public void Debug(string message)
        {
#if DEBUG
            Log.Out(message);
#endif
        }

        public void Error(string message)
        {
#if DEBUG
            Log.Error(message);
#endif
        }
    }

    public interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);
    }
}