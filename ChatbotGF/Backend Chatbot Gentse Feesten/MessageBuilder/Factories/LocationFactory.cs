﻿using Chatbot_GF.Data;
using Chatbot_GF.MessageBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot_GF.MessageBuilder.Factories
{
    public class LocationFactory
    {
        public static GenericMessage makeLocationButton(long id,string lang)
        {
            List<SimpleQuickReply> quick_replies = new List<SimpleQuickReply>();
            quick_replies.Add(new SimpleQuickReply("location"));
            String hmess = DataConstants.GetMessage("Pick_map", lang);
            return new GenericMessage(id, hmess, quick_replies);
        }

        public static GenericMessage MakeLocationResponse(long id, double lat, double lon, string lang)
        {
            string image_url = $"https://maps.googleapis.com/maps/api/staticmap?size=764x400&center={lat},{lon}&zoom=15&markers={lat},{lon}";
            string item_url = $"http://maps.apple.com/maps?q={lat},{lon}&z=16";
            LocationElement element = new LocationElement(DataConstants.GetMessage("CLOSEST_TOILET", lang), image_url, "", null, item_url);
            List<Element> elements = new List<Element>();
            elements.Add(element);
            IPayload payload = new PayloadMessage("generic", elements, true, "horizontal");
            Attachment attachment = new Attachment("template", payload);
            return new GenericMessage(id, attachment);
        }
    }
}
