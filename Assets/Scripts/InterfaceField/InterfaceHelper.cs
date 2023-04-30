using System.Collections.Generic;

namespace ML.InterfaceField
{
    public static class InterfaceHelper
    {
        public static IEnumerable<T> ReturnEnumarable<T>(IEnumerable<SingleInterfaceField<T>> collection)
        {
            foreach (SingleInterfaceField<T> field in collection)
            {
                yield return field.Interface;
            }
        }
        public static void ValidateInterfaces<T>(ref List<SingleInterfaceField<T>> list)
        {
            if (list == null)
                return;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                list[i].Validate();
            }
        }

        public static void ValidateInterfaces<T>(ref SingleInterfaceField<T>[] array)
        {
            if (array == null)
                return;

            for (int i = array.Length - 1; i >= 0; i--)
            {
                array[i].Validate();
            }
        }
    } 
}
