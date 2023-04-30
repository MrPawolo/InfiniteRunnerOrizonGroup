using UnityEngine;
using TMPro;

namespace ML.UI
{
    public class FillTMP : MonoBehaviour
    {
        [SerializeField]TMP_Text text;
        public void ToText(int val)
        {
            text.text = val.ToString();
        }
        public void ToText(float val)
        {
            text.text = val.ToString();
        }
        public void ToText(string val)
        {
            text.text = val.ToString();
        }
    }
}
