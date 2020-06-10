using UnityEngine;

namespace TheInput
{
    /// <summary>
    /// Contains global values Types such as Enums, Delegates, etc
    /// </summary>
    public class InputGlobals
    {
        public delegate void EventV2(Vector2 vector2);
        public delegate void EventV3(Vector3 vector3);

        /// <summary>
        /// All no parameters events 
        /// </summary>
        public enum EventsNoParamEnum
        {
            AttackUp,
            AttackDn,
            AttackLt,
            AttackRt,
        }

        /// <summary>
        /// All Vector2 Events
        /// </summary>
        public enum EventsV2Enum
        {
            Look,
            Move,
            Turn,
        }

    }
}