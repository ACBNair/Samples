using System;
using System.Collections.Generic;
using System.Text;

namespace UsingInterfaces.Nature
{
    public class Car : IMovable
    {
        #region IMovable Member

        public void Move()
        {
            Console.WriteLine("Der Auto f�hrt zur n�chsten Ampel.");
        }

        #endregion
    }
}
