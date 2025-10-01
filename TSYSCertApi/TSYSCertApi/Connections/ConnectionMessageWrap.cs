using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSYSCertApi.Connections
{
    public class ConnectionMessageWrap
    {
        public char STX { get; set; }
        public char ETX { get; set; }

        public ConnectionMessageWrap()
        {
         this.STX = (char)2;
        this.ETX = (char)3;
    }

        protected char ReturnLRC { get; set; }
        public char CalculateLongitudinalRedundancyCheck(string source)
        {
            int result = 0;
            for (int i = 0; i < source.Length; i++)
            {
                result = result ^ (Byte)(Encoding.ASCII.GetBytes(source.Substring(i, 1))[0]);
            }
            return (Char)result;
        }
    }
}
