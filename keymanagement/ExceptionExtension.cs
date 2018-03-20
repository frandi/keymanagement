using System;

namespace keymanagement
{
    public static class ExceptionExtension
    {
        public static string GetInnerExceptionMessage(this Exception ex)
        {
            while (true)
            {
                if (ex.InnerException == null) 
                    return ex.Message;

                ex = ex.InnerException;
            }
        }
    }
}
