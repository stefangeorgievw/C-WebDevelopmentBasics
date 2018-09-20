using System;
using System.Net;

namespace ValidateURL
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var urlInput = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(urlInput);
            try
            {
                Uri uri = new Uri(decodedUrl);

                if (string.IsNullOrEmpty(uri.Scheme) || string.IsNullOrEmpty(uri.Host) || string.IsNullOrEmpty(uri.AbsolutePath) || uri.Port < 0)
                {

                    Console.WriteLine("Invalid URL");
                }
                else if (uri.Scheme == "http" && uri.Port == 80 || uri.Scheme == "https" && uri.Port == 443)
                {
                    Console.WriteLine($"Protocol: {uri.Scheme}");
                    Console.WriteLine($"Host: {uri.Host}");
                    Console.WriteLine($"Port: {uri.Port}");
                    Console.WriteLine($"Path: {uri.AbsolutePath}");

                    if (!string.IsNullOrEmpty(uri.Query))
                    {
                        Console.WriteLine($"Query: {uri.Query}");
                    }
                    if (!string.IsNullOrEmpty(uri.Fragment))
                    {
                        Console.WriteLine($"Fragment: {uri.Fragment}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid URL");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Invalid URL");
            }
           
        }
    }
}
