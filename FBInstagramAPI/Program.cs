using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace FBInstagramAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var listLink = "";

            Console.WriteLine("API Type : facebook - f, instagram - i");
            var apiType = Console.ReadLine();

            Console.WriteLine("AccessToken");
            var accessToken = Console.ReadLine();

            if (apiType == "i")
            {
                Console.WriteLine("User Id");
                var userId = Console.ReadLine();

                listLink = $"https://graph.instagram.com/{userId}/media?fields=caption,media_url&access_token={accessToken}"; 
            }
            else
                listLink = $"https://graph.facebook.com/v11.0/me/posts?fields=message,permalink_url&access_token={accessToken}";

            var medias = new List<Media>();
            var posts = new List<string>();
            var errorIds = new List<string>();

            var client = new WebClient();

            do
            {
                Console.WriteLine("request list => " + listLink);

                var response = client.DownloadString(listLink);
                var media = JsonSerializer.Deserialize<Media>(response);

                medias.Add(media);

                IEnumerable<string> text;
                
                if (apiType == "i")
                    text = media.data
                        .Where(item => !string.IsNullOrEmpty(item.caption))
                        .Select(item => $"id: {item.id}\nurl: {item.media_url}\n\t{item.caption}");
                else
                    text = media.data
                        .Where(item => !string.IsNullOrEmpty(item.message))
                        .Select(item => $"id: {item.id}\nurl: {item.permalink_url}\n\t{item.message}");

                posts.AddRange(text);

                //foreach (var item in media.data)
                //{
                //    var mediaLink = $"https://graph.instagram.com/{item.id}?fields=caption&access_token={accessToken}";
                //    Console.WriteLine("request media => " + mediaLink);

                //    try
                //    {
                //        response = client.DownloadString(mediaLink);
                //        var caption = JsonSerializer.Deserialize<Dictionary<string, string>>(response);

                //        if (caption.ContainsKey("caption"))
                //            captions.Add($"id: {item.id}\n\t{caption["caption"]}");
                //    }
                //    catch (Exception ex)
                //    {
                //        errorIds.Add(item.id);
                //    }
                //}

                if (media.paging != null)
                    listLink = media.paging.next;
                else
                    listLink = string.Empty;
            } while (!string.IsNullOrEmpty(listLink));

            Console.WriteLine("Result:");
            Console.WriteLine(string.Join("\n----------------\n", posts));
            //Console.WriteLine("Error Ids:");
            //Console.WriteLine(string.Join(", ", errorIds));

            Console.ReadLine();
        }
    }

    class Media
    {
        public Data[] data { get; set; }
        public Paging paging { get; set; }
    }

    class Data
    {
        public string id { get; set; }
        public string caption { get; set; }
        public string message { get; set; }
        public string permalink_url { get; set; }
        public string media_url { get; set; }
    }
    
    class Paging
    {
        public string next { get; set; }
    }
}
