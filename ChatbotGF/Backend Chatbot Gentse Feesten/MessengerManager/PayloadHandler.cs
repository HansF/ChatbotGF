﻿using Chatbot_GF.Data;
using Chatbot_GF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Chatbot_GF.BotData.MessengerData;

namespace Chatbot_GF.MessengerManager
{
    public class PayloadHandler
    {
        private UserManager umanager;
        private ReplyManager rmanager;

        public PayloadHandler()
        {
            umanager = UserManager.GetInstance();
            rmanager = new ReplyManager();
            
        }

        public void handle(Messaging message)
        {
            switch (message.postback.payload)
            {
                case "GET_STARTED_PAYLOAD":
                    umanager.startUser(message.sender.id);
                    break;
                case "SEND_LOCATION_CHOICE":
                    rmanager.SendLocationsChoice(message.sender.id);
                    break;
                case "GET_USER_LOCATION":
                    rmanager.SendGetLocationButton(message.sender.id);
                    break;
                case "DEVELOPER_DEFINED_SEARCHFALSE":
                    rmanager.SendInfoForEnding(message.sender.id);
                    break;
                default:
                    //contains information for user
                    handleInformation(message.sender.id, message.postback.payload);
                    break;
            }

        }

        private void handleInformation(long id, string payload)
        {
            //payload indicates which category data in messengers has been given
            int pos = payload.IndexOf("-");
            string category = payload.Substring(0, pos);
            string value = payload.Substring(pos + 1);
            Console.WriteLine(category + " " + value);
            switch (category)
            {
                case "GET_EVENT_HERE_NOW":
                    rmanager.SendLocationQuery(id, int.Parse(value));
                    break;
                case "DEVELOPER_DEFINED_LOCATION":
                    umanager.setUserLocation(id, value);
                    break;
                case "DEVELOPER_DEFINED_TIME":
                    umanager.setUserTime(id, value);
                    break;
                case "DEVELOPER_DEFINED_SEARCH":
                    umanager.searchResults(id);
                    break;
                case "DEVELOPER_DEFINED_COORDINATES":
                    string[] data = value.Split(':');
                    SearchableLocation location = DataConstants.GetClosestLocation(new Coordinates { lat = double.Parse(data[1]), lon = double.Parse(data[0]) });
                    rmanager.SendLocationResult(id, location);
                    break;
                case "DEVELOPER_DEFINED_DESCRIPTION":
                    // rmanager.SendTextMessage(id, value);
                    break;
                case "DEVELOPER_DEFINED_HOURS":
                    int loc = value.IndexOf("-_-");
                    string date = value.Substring(0, loc);
                    string name = value.Substring(loc + 3 );
                    rmanager.SendTextMessage(id, name + ": " + date);
                    break;
                default:
                   //do nothing
                    break;
            }

        }

        public void HandleLocation(Coordinates coords)
        {

        }
    }
}
