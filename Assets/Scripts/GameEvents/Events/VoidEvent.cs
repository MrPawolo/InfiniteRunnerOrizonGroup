
using UnityEngine;

namespace ML.GameEvents
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "ML/GameEvents/Void Event")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        /// <summary>
        /// null check is not required "?."
        /// </summary>
        public void Invoke() => Invoke(new Void());
    }
}
