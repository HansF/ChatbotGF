﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot_GF.MessengerManager
{
    public class FreeTextHandler
    {
        private static IConfiguration ReplyStore;
        private static ReplyManager RMmanager;

        private static void InitReplies()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("replies.json");
            ReplyStore = builder.Build();

            RMmanager = new ReplyManager();
        }


        public static string CheckText(long id,string text, string language)
        {
            if(ReplyStore == null)
            {
                InitReplies();
            }

            string res = GetResponse(text, language);
            if(res != null)
            {
                RMmanager.SendTextMessage(id,res);
            }
            return null;
        }

        private static string RemoveNonAlphanumerics(string text)
        {
            char[] arr = text.Where(c => (char.IsLetterOrDigit(c) ||
                             char.IsWhiteSpace(c) ||
                             c == '-')).ToArray();

            return new string(arr);
        }

        public static string GetResponse(string text, string language)
        {
            InitReplies();
            try
            {
                text = RemoveNonAlphanumerics(text);

                List<string> words = text.ToLower().Split(' ').ToList();
                List<string> KeywordsFound = new List<string>();
                KeywordsFound.Add("keywords");
                int count = 0;
                while (count < words.Count)
                {                    
                    string query = string.Join(":", KeywordsFound);
                    Console.WriteLine("Searching " + query + ":" + words[count]);
                    //Console.WriteLine(ReplyStore["keywords:feestje:waar:response:nl"]);
                    if (ReplyStore.GetSection(query + ":" + words[count]).GetValue<string>("response") != null)
                    {
                        Console.WriteLine(query + ":" + words[count] + "Keyword found!");
                        KeywordsFound.Add(words[count]);
                        words.RemoveAt(count);
                        count = 0; //restart
                    }
                    else if (ReplyStore[query + ":haschildren"] != null && ReplyStore[query + ":haschildren"] == "false")
                    {
                        Console.WriteLine("No children, strop recursion");
                        break; //stop recursion, object has no child keywords
                    }
                    else
                    {
                        count++;
                    }
                }
                Console.WriteLine(string.Join(":", KeywordsFound) + ":response");
                return ReplyStore[string.Join(":", KeywordsFound) + ":response"];
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static bool IsEqual(string first, string second, int tollerance)
        {
            return string.Compare(first, second) < tollerance;
        }

    }
}
