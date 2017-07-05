﻿using Chatbot_GF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Query;

namespace Chatbot_GF.Data
{
    public class ResultParser
    {

        public static Event GetEvent(SparqlResult res)
        {
            Event e = new Event();
            foreach (String key in res.Variables)
            {
                try
                {
                    switch (key)
                    {
                        case "name":
                            e.name.nl = normalizeString(res[key].ToString());
                            break;
                        case "startdate":
                            e.startDate = res[key].ToString();
                            break;
                        case "endDate":
                            e.endDate = res[key].ToString();
                            break;
                        case "description":
                            e.description.nl = res[key].ToString();
                            break;
                        case "organizer":
                            e.organizer = res[key].ToString();
                            break;
                        case "image":
                            e.image = res[key].ToString();
                            break;
                    }
                }catch(Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
            return e;

                        
        }
        private static string normalizeString(string s)
        {
            int lastIndex = s.LastIndexOf('@');
            if(lastIndex > 0)
            {
                return s.Substring(0,lastIndex);
            }
            else
            {
                return s;
            }
        }
    }
}
