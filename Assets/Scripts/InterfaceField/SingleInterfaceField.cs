using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.InterfaceField
{
    [System.Serializable]
    public class SingleInterfaceField<T>
    {
        public Component Interface;

        public void Validate()
        {
            if (Interface == null)
                return;


            if (Interface is T)
                return;

            if(Interface.TryGetComponent<T>(out var inter))
            {
                Component[] components = Interface.GetComponents<Component>();

                foreach(Component component in components)
                {
                    if(component is T)
                    {
                        Interface = component;
                        break;
                    }
                }
            }
            else
            {
                Interface = null;
                Debug.Log("This GameObject Dont have Any Interface Of that type ");
            }
        }
    } 
}
