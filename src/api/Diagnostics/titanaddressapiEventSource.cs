using System;
using System.Diagnostics.Tracing;

namespace TitanTemplate.titanaddressapi.Diagnostics
{
    [EventSource(Name = "titanaddressapi")]
    public class titanaddressapiEventSource : EventSource
    {
        public readonly static titanaddressapiEventSource Log = new titanaddressapiEventSource();

        #region General

        [Event(1000, Message = "State changed - {0}", Level = EventLevel.Informational)]
        public void StateChanged(string newState)
        {
            WriteEvent(1000, newState);
        }

        #endregion

        #region 2900 Test Controller Errors

        [NonEvent]
        public void PutAsyncError(Exception e)
        {
            if(IsEnabled())
            {
                PutAsyncError(e.Message, e.Source, e.StackTrace, e.GetType().Name);
            }
        }

        [Event(2900, Message = "Error on put request - {0}", Level = EventLevel.Error)]
        public void PutAsyncError(string exceptionMessage, string exceptionSource, string stackTrace, string exceptionType)
        {
            WriteEvent(2900, exceptionMessage, exceptionSource, stackTrace, exceptionType);
        }

        #endregion
    }
}
