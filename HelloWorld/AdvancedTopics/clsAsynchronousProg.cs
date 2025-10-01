using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.AdvancedTopics
{
    public class clsAsynchronousProg
    {
        // In Asynchronous Program Execution - when a function is called, program execution continues to the next line,
        //without wiating for the funtion to complete.  Used to be multi-threading and callbacks, now use "Async/Await"

        public async void runAsynchronousProg()
        {

            //Console.WriteLine("main started");
            //DownloadHtml("http://www.wikipedia.com");
            //Console.WriteLine("main finished");

            //Console.WriteLine("async started");
            // var testAsync = await DownloadHtmlAsync("http://www.wikipedia.com");
            //Console.WriteLine("main finished");

            Console.WriteLine("async download started");
            var htmlAsync = GetHTMLAsync("http://www.wikipedia.com");
            var gethtml = await htmlAsync;
            Console.WriteLine("async download finished");

            Console.WriteLine("download started");
          var html = GetHTML("http://www.wikipedia.com");
            Console.WriteLine(html);
            Console.WriteLine("download finished");

     
        }


        public async Task<string> GetHTMLAsync(string url)
        {
            var myWebClient = new WebClient();
            return await myWebClient.DownloadStringTaskAsync(url);
        }
        public string GetHTML(string url)
        {
        var myWebClient = new WebClient();
        return myWebClient.DownloadString(url);
        }





        public async Task DownloadHtmlAsync(string url)
        {

            var webClient = new WebClient();
            var html = await webClient.DownloadStringTaskAsync(url); //await does NOT mean "wait" it is just a marker for the compiler to return control to the caller of the method.

            using (var streamWriter = new StreamWriter(@"C:\asynctest.html"))
            {
                Console.WriteLine("running async");
  
               await streamWriter.WriteAsync(html);
            }

        }

        public void DownloadHtml(string url)
        {
            var webClient = new WebClient();
            var html = webClient.DownloadString(url);

            using (var streamWriter = new StreamWriter(@"C:\test.html"))
            {
                streamWriter.Write(html);
            }
        }
        }
}
