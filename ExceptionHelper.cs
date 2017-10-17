using System;

namespace NodaTimeSpike
{
    public static class ExceptionHelper
    {
        public static string RecursiveErrorMessage(this Exception ex)
        {
            var message = ex.Message;

            if (ex.InnerException != null)
                message += " " + RecursiveErrorMessage(ex.InnerException);

            return message.AssureEndsWithPeriod();
        }
    }
}