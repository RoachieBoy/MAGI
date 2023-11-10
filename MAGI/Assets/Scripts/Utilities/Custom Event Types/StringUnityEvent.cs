using System;
using UnityEngine.Events;

namespace Utilities.Custom_Event_Types
{
    [Serializable]
    public class StringUnityEvent : UnityEvent<string> {}
}