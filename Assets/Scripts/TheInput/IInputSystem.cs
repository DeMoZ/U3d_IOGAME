using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheCamera;
using System;
using UnityEngine.Events;

namespace TheInput
{
    public interface IInputSystem
    {
        void UnsubscribeAll();
        void SubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum attackUp1, UnityAction attackUp2);
        void UnsubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum attackDn1, UnityAction attackDn2);

        void SubscribeVector2Event(InputGlobals.EventsV2Enum enm, InputGlobals.EventV2 callback);
        void UnsubscribeVector2Event(InputGlobals.EventsV2Enum move1, InputGlobals.EventV2 move2);
    }
}