using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Congehou
{
    public class ClockUI : MonoBehaviour
    {
        public TextMeshProUGUI clockValue;
        public RectTransform clockPointer;

        private TimeManager m_TimeManager;

        // Start is called before the first frame update
        void Awake()
        {
            m_TimeManager = PlayerCharacter.PlayerInstance.timeManager;
            m_TimeManager.onClockValueChange += UpdateClock;
        }

        public void UpdateClock()
        {
            clockValue.text = m_TimeManager.CurrentClockValue.ToString();
            clockPointer.eulerAngles = new Vector3(0, 0, m_TimeManager.CurrentClockValue);
        }
    }
}
