// main

using System;
using System.Collections.Generic;
using YoutubeExplode;

namespace DLYoutube
{
    public class Program
    {
        public static void Main()
        {
            //commands --help --channel --version
            var args = Environment.GetCommandLineArgs();
            var commands = new List<string>(args);
            commands.RemoveAt(0);
            var storage = new Storage();
            var downloadBusiness = new DownloadUtils(storage);
            if (commands.Contains("--help"))
            {
                Console.WriteLine("DLYoutube:");
                Console.WriteLine("Usage:");
                Console.WriteLine("DLYoutube [options] [<args> … ]");
                Console.WriteLine("Options:");
                Console.WriteLine("--channel <channel> url pour télécharger un channel complet");
                Console.WriteLine("--version Display informamtion");
            }
            else if (commands.Contains("--version"))
            {
                Console.WriteLine("DLYoutube 1.0.0");
            }
            else if (commands.Contains("--channel"))
            {
                var index = commands.IndexOf("--channel");
                var channelId = commands[index + 1];
                downloadBusiness.DownloadChannelUtils(channelId);
            }
            // else dowanload all vidéos in args, convert all args to a list of url
            else
            {
                downloadBusiness.DownloadVideoUtils(commands.ToArray());
            }
        }
    }
} 