using System;
using System.Threading;

namespace HelloWorld.AdvancedTopics.ExtClass
{
   public class EventsAndDelegatesVideoEncoder
    {
        public void Encode(EventsAndDelegatesVideo video)
        {
            Console.WriteLine("Encoding Video...");
            Thread.Sleep(3000);

            OnVideoEncoded(video);
        }


        /*To allow a class to publish an event we need to:
        1. Define a delegate (determines the signature and subscriber that will be called.
        2. Define an event based on that delegate
        3. Raise the event
        */

        //OLD WAY (create custom): 
        //1. define a delegate hich determines the shape or the signature of the method in the subscriber:
        // public delegate void VideoEncodedEventHandler(object source, EventsAndDelegatesVideoEventArgs args); //EventArgs is any addigional data to be sent
        //2. define an event based on that delegate:
        //  public event EventHandler<EventsAndDelegatesVideoEventArgs> VideoEncoded;
        //----END OLD WAY-----


        //NEW WAY to create a custom delegate:
        //EventHandler - packaged in .NET
        //EventHandler<TEventArgs> - packaged in .NET
        public event EventHandler<EventsAndDelegatesVideoEventArgs> VideoEncoded;


        // 3. Raise the event
        protected virtual void OnVideoEncoded(EventsAndDelegatesVideo video) //When raising an event in .net it should be "protected" "virtual" "void" and have a prefix with the word "On"
        {
            if (VideoEncoded != null)
            {
                VideoEncoded(this, new EventsAndDelegatesVideoEventArgs() { Video = video }); //"EventArgs.Empty" means no additional args will be passed
            }
        }


       
    }
}
