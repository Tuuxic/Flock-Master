using GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    // Base class for all GameEvents
    public abstract class GameEvent
    { 
        public virtual bool isValid() { return true; }
    }

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

    public class GameOverEvent : GameEvent { }

}


