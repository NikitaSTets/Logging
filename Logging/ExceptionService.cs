using System;

namespace Logging
{
    public class ExceptionService
    {
        public void ThrowException()
        {
            var rand = new Random();
            var randomValue = rand.Next(1, 100);

            if (randomValue > 20)
            {
                throw new Exception("LoOL", new ArgumentOutOfRangeException());
            }
        }
    }
}