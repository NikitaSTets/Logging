using System;

namespace Logging
{
    public class ExceptionService
    {
        public void ThrowException()
        {
            throw new Exception("LoOL", new ArgumentOutOfRangeException());
        }
    }
}