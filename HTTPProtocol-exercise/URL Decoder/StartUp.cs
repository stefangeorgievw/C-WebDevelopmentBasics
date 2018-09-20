using System;
using System.Net;

namespace URL_Decoder
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var urlInput = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(urlInput);
            Console.WriteLine(decodedUrl);
        }
    }
}
