using HelloWorld.AdvancedTopics.ExtClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.AdvancedTopics
{
    class clsEventsAndDelegates
    {
        public void runEventsAndDelegates()
        {
            var video = new EventsAndDelegatesVideo { Title = "Joe's Video 1" };
            var videoEncoder = new EventsAndDelegatesVideoEncoder(); //publisher
           var mailService = new EventsAndDelegatesMailService(); //subscriber
            var messageService = new EventsAndDelegatesMessageService(); //subscriber

            videoEncoder.VideoEncoded += mailService.OnVideoEncoded; //This is the subsription, you don't need the parenthesis () because you're only doing a pointer.  Look at "Delegatees" course for += explain.
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded; //This is the subsription, you don't need the parenthesis () because you're only doing a pointer.  Look at "Delegatees" course for += explain.

            videoEncoder.Encode(video);
        }
    }
}
