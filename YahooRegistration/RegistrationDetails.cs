using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooRegistration
{
    class RegistrationDetails : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { "UserFirst", "UserLast", "Password", "Email", "0000000000","January","01","2020", "Male"};
            yield return new object[] { "UserFirst2", "UserLast2", "Password2", "Email2", "1111111111", "February", "13", "1990", "Female" };
            yield return new object[] { "UserFirst3", "UserLast3", "Password3", "Email3", "999999999", "May", "07", "1990","Non-Binary" };
            yield return new object[] { "UserFirst4", "UserLast4", "Password4!@#$%", "UniQu3EMa1L5320111", "4089665241", "December", "17", "1989", "Non-Binary" };
        }
    }
}
