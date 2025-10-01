using System;
using HelloWorld.AdvancedTopics.ExtClass;


namespace HelloWorld.AdvancedTopics.ExtClass
{
  public class EventsAndDelegatesMailService    //This class is a subscriber to the VideoEncoded events
    {
      
        public void OnVideoEncoded(object source, EventsAndDelegatesVideoEventArgs e)
        {
            Console.WriteLine("MailService: Sending an email..." + e.Video.Title);
        }
    }
}
