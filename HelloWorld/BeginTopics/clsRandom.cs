using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsRandom
    {
        public void RunRandom()
        {
            Random randNum = new Random();

            const int passwordLength = 10;

            char[] buffer = new char[passwordLength];
            for (int i = 0; i < passwordLength; i++)
            {
                //Console.Write((char)('a' + randNum.Next(0, 26))); //similar to a password (there are 26 characters in alphabet)
                buffer[i] = (char)('a' + randNum.Next(0, 26));
            }

            string password = new string(buffer);
            Console.WriteLine(password);
        }
    }
}
