using GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{

    public class PlayerDamageEvent : GameEvent
    {
        public readonly int damage;
        public PlayerDamageEvent(int damage)
        {
            this.damage = damage;
        }

        public override bool isValid()
        {
            return damage >= 0;
        }
    }

}


