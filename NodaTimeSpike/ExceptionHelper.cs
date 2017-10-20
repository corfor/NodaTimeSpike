using System;
using System.Text;

namespace NodaTimeSpike
{
    public static class ExceptionHelper
    {
        public static string RecursiveErrorMessage(this Exception ex)
        {
            var sb = new StringBuilder();
            while (true)
            {
                sb.Append(ex.Message);
                if (ex.InnerException == null)
                    break;
                sb.Append(' ');
                ex = ex.InnerException;
            }
            return sb.ToString().AssureEndsWithPeriod();
        }
    }
}