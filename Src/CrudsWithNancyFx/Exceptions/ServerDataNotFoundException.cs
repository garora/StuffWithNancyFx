
using System;
namespace CrudsWithNancyFx.Exceptions
{
    public class ServerDataNotFoundException : Exception
    {
        public ServerDataNotFoundException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }
    }
}