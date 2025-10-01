using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.AdvancedTopics.ExtClass
{
   public class EventsAndDelegatesMessageService //This class is a subscriber to the VideoEncoded events
    {
        public void OnVideoEncoded(object source, EventsAndDelegatesVideoEventArgs e)
        {
            Console.WriteLine("MessageService: Sending an sms message... " + e.Video.Title);
        }
    }

}
