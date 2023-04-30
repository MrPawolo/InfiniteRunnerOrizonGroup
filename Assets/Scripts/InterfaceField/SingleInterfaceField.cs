using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.InterfaceField
{
    [System.Serializable]
    public class SingleInterfaceField<T>
    {
        public Component component;
        public T Interface { get; private set; }
        public void Validate()
        {
            if (!component)
                return;

            if (component is T)
            {
                Interface = (T)(object)component;
            }
            else
            {
                if (component.TryGetComponent<T>(out var inter))
                {
                    Interface = inter;
                }
                else
                {
                    component = null;
                    Debug.Log("This GameObject Dont have Any Interface Of that type ");
                }
            }
        }
    } 
}
