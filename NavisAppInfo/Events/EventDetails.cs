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
   public class EventDetails : IDisposable
   {
      private Delegate _delegate = null;

      public EventInfo EventInfo { get; private set; }
      public string EventName { get; private set; }
      public Type CallerType { get; private set; }
      public object CallerValue { get; private set; }
      public DateTime LastRaised { get; private set; }
      public Delegate Delegate { get { return _delegate; } }

      public EventDetails(Type callerType, object callerValue, string eventName, EventInfo info)
      {
         EventName = eventName;
         CallerType = callerType;
         CallerValue = callerValue;
         EventInfo = info;
         LastRaised = DateTime.MinValue;
      }

      /// <summary>
      /// Event Handler triggered by the event being attached to
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void EventHandler(object sender, EventArgs e)
      {
         LastRaised = DateTime.Now;
         if (EventRaised != null)
         {
            EventDetailsArgs eda = new EventDetailsArgs(this, e);
            EventRaised(sender, eda);
         }
      }

      public delegate void EventDetailsEventHandler(object sender, EventDetailsArgs e);
      public event EventDetailsEventHandler EventRaised;
 
      /// <summary>
      /// Creates an event handler
      /// </summary>
      /// <param name="callerType"></param>
      /// <param name="callerValue"></param>
      /// <param name="info"></param>
      /// <returns></returns>
      public Delegate CreateEventHandler(Type callerType, object callerValue, EventInfo info)
      {
         Delegate d = null;
         try
         {
            Type handlerType = info.EventHandlerType;

            MethodInfo miHandler = typeof(EventDetails).GetMethod("EventHandler",
               BindingFlags.NonPublic | BindingFlags.Instance);

            d = Delegate.CreateDelegate(handlerType, this, miHandler);

         }
         catch (Exception)
         {
            //All exceptions, we simply return no delegate
         }

         return d;
      }

      /// <summary>
      /// Adds the Event Handler
      /// </summary>
      /// <param name="callerType"></param>
      /// <param name="callerValue"></param>
      /// <param name="info"></param>
      /// <returns></returns>
      internal bool AddEventHandler()
      {
         _delegate = CreateEventHandler(CallerType, CallerValue, EventInfo);
         if (_delegate == null)
            return false;
         try
         {
            EventInfo.AddEventHandler(CallerValue, _delegate);
         }
         catch (Exception)
         {
            //All Exceptions, we simply return false
            return false;
         }

         return true;
      }

      /// <summary>
      /// Removes the event handler
      /// </summary>
      /// <param name="callerValue"></param>
      /// <param name="info"></param>
      /// <returns></returns>
      internal bool RemoveEventHandler()
      {
         if (_delegate == null)
            return true;
         try
         {
            EventInfo.RemoveEventHandler(CallerValue, _delegate);
         }
         catch (Exception)
         {
            //All Exceptions, we simply return false
            return false;
         }

         return true;
      }

      public void Dispose()
      {
         RemoveEventHandler();
      }
   }
}
