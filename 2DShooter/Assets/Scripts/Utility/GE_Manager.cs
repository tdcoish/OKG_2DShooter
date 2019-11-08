/*************************************************************************************
Got tired of that shitty thing where you have to put a bajillion "add listener" components
onto what you want. Here, you're just going to register your game object, as well as the 
events you want to listen to.

Unfortunately, it doesn't seem like we have a good way of shoving hashes, so we have to
actually store the raw pointers, then check if they're valid all the time. Rough.

Got something online, I think it's decent, although it's not going to let me pass data
in these events. Hopefully that's fixable.

Credit to: https://forum.unity.com/threads/using-delegate-events-from-other-scripts.382176/ 

As I suspected, we're not removing the handlers when we die, which is really irritating.
This might be unusable.
*************************************************************************************/
using UnityEngine;
using System.Collections.Generic;

public enum TDC_GE { GE_PCDeath, GE_EDeath, GE_PCK_AM, GE_PCK_HLT }; // ... Other events
public static class TDC_EventManager
{
    // Stores the delegates that get called when an event is fired
    private static Dictionary<TDC_GE, System.Action> eventTable
                 = new Dictionary<TDC_GE, System.Action>();
 
    // Adds a delegate to get called for a specific event
    public static void FAddHandler(TDC_GE evnt, System.Action action)
    {
        if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
        else eventTable[evnt] += action;
    }

    public static void FRemoveHandler(TDC_GE evnt, System.Action action)
    {
        if (eventTable[evnt] != null)
            eventTable[evnt] -= action;
        if (eventTable[evnt] == null)
            eventTable.Remove(evnt);
    }

    public static void FRemoveAllHandlers()
    {
        System.Array values = System.Enum.GetValues(typeof(TDC_GE));

        foreach(TDC_GE val in values)
        {
            eventTable[val] = null;
        }
    }
 
    // Fires the event
    public static void FBroadcast(TDC_GE evnt)
    {
        if(!eventTable.ContainsKey(evnt)){
            return;
        }
        
        if (eventTable[evnt] != null){
            eventTable[evnt]();
        } 
    }
}


 // Used in other classes:
        // // Adds a method to be called when the event occurs
        // EventManager.AddHandler(EVENT.MyEvent1, MyMethod);
        // // Calls the event and runs any methods added
        // EventManager.Broadcast(EVENT.MyEvent1);