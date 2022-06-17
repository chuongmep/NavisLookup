//------------------------------------------------------------------
// NavisWorks Sample code
//------------------------------------------------------------------

// (C) Copyright 2009 by Autodesk Inc.

// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.

// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//------------------------------------------------------------------
//
// This sample illustrates the various properties available in the API
//
//------------------------------------------------------------------

using System.Reflection;

namespace AppInfo.Events
{
   public static class EventHandlers
   {
      /// <summary>
      /// Stores the events to handlers relationships
      /// </summary>
      static Dictionary<string, Dictionary<object, EventDetails>> eventHandlers = new Dictionary<string, Dictionary<object, EventDetails>>();

      /// <summary>
      /// Gets a usable key combination from the Type and the Event
      /// </summary>
      /// <param name="eventName"></param>
      /// <param name="callerType"></param>
      /// <returns></returns>
      private static string GetKey(string eventName, Type callerType)
      {
         string key = string.Format("{0}_{1}",
            eventName,
            callerType.ToString());
         return key;
      }

      /// <summary>
      /// Adds an event handler to the event
      /// </summary>
      /// <param name="callerType"></param>
      /// <param name="callerValue"></param>
      /// <param name="eventName"></param>
      /// <param name="info"></param>
      /// <returns></returns>
      public static bool AddEventHandler(Type callerType, object callerValue, string eventName, EventInfo info)
      {
         if (ContainsKey(callerType, callerValue, eventName))
            return true;

         //Create a new EventDetails object
         EventDetails eventDetails =
            new EventDetails(callerType, callerValue, eventName, info);
         eventDetails.EventRaised += eventDetails_EventRaised;

         //Add the event handler
         bool retval = eventDetails.AddEventHandler();
         
         if (retval)
         {
            //Get the key for the event and type
            string key = GetKey(eventName, callerType);

            Dictionary<object, EventDetails> eventTypeDict = null;

            //Check if we've subscribed to event for any object
            if (!eventHandlers.TryGetValue(key, out eventTypeDict))
            {
               eventTypeDict = new Dictionary<object, EventDetails>();
               eventHandlers.Add(key, eventTypeDict);
            }

            if ((callerValue == null && eventTypeDict.Count == 0)//null value, must be a static class
               || callerValue != null //non null value, set to object
               )
            {
               //Add the link
               eventTypeDict.Add((callerValue == null) ? callerType : callerValue, eventDetails);
            }
         }

         return retval;
      }

      static void eventDetails_EventRaised(object sender, EventDetailsArgs e)
      {
         if (EventRaised != null)
         {
            EventRaised(sender, e);
         }
      }

      public delegate void EventDetailsEventHandler(object sender, EventDetailsArgs e);
      public static event EventDetailsEventHandler EventRaised;

      /// <summary>
      /// Removes an event handler form the event
      /// </summary>
      /// <param name="callerType"></param>
      /// <param name="callerValue"></param>
      /// <param name="eventName"></param>
      /// <param name="info"></param>
      /// <returns></returns>
      public static bool RemoveEventHandler(Type callerType, object callerValue, string eventName, EventInfo info)
      {
         Dictionary<object, EventDetails> eventTypeDict = null;
         EventDetails eventDetails = null;

         //Get the key
         string key = GetKey(eventName, callerType);

         //find the event handler
         if (eventHandlers.TryGetValue(key, out eventTypeDict) &&
            eventTypeDict.TryGetValue((callerValue==null)?callerType:callerValue, out eventDetails))
         {
            //remove the event handler
            eventDetails.RemoveEventHandler();

            //remove the  link
            eventHandlers.Remove(key);
         }
         return true;
      }

      internal static void RemoveAllEventHandlers()
      {
         foreach (Dictionary<object, EventDetails> dict in eventHandlers.Values)
         {
            foreach (EventDetails eventDetail in dict.Values)
            {
               eventDetail.Dispose();
            }
            dict.Clear();
         }
         eventHandlers.Clear();
      }

      /// <summary>
      /// returns true when the event has an event handler
      /// </summary>
      /// <param name="callerType"></param>
      /// <param name="callerValue"></param>
      /// <param name="eventName"></param>
      /// <returns></returns>
      public static bool ContainsKey(Type callerType, object callerValue, string eventName)
      {
         Dictionary<object, EventDetails> eventTypeDict = null;
         string key = GetKey(eventName, callerType);

         if (eventHandlers.TryGetValue(key, out eventTypeDict) &&
            eventTypeDict.ContainsKey((callerValue == null) ? callerType : callerValue))
         {
            return true;
         }
         return false;
      }

      /// <summary>
      /// Gets the Status text for a particular event
      /// </summary>
      /// <param name="callerType">The parent class Type of the event</param>
      /// <param name="callerValue">the instance of the class</param>
      /// <param name="eventName">The event</param>
      /// <param name="statusText">out value giving the status of the event</param>
       /// <returns>true when event is being handled, false otherwise</returns>
      public static bool GetStatusText(Type callerType, object callerValue, string eventName, out string statusText)
      {
         Dictionary<object, EventDetails> eventTypeDict = null;
         EventDetails eventDetails = null;
         bool retVal = false;
         statusText = string.Empty;

         //Get the key
         string key = GetKey(eventName, callerType);

         //find the event handler
         if (eventHandlers.TryGetValue(key, out eventTypeDict) &&
            eventTypeDict.TryGetValue((callerValue == null) ? callerType : callerValue, out eventDetails))
         {
            retVal = true;
            if (eventDetails.LastRaised != DateTime.MinValue)
            {
               statusText = eventDetails.LastRaised.ToString();
            }
         }

         return retVal;
      }

      public static bool EventLastHandled(Type callerType, object callerValue, string eventName, out DateTime lastRun)
      {
         Dictionary<object, EventDetails> eventTypeDict = null;
         EventDetails eventDetails = null;

         //Get the key
         string key = GetKey(eventName, callerType);

         //find the event handler
         if (eventHandlers.TryGetValue(key, out eventTypeDict) &&
            eventTypeDict.TryGetValue((callerValue == null) ? callerType : callerValue, out eventDetails))
         {
            lastRun = eventDetails.LastRaised;
            return lastRun ==  DateTime.MinValue;
         }
         else
         {
            lastRun = DateTime.MinValue;
         }
         return false;
      }
   }
}
