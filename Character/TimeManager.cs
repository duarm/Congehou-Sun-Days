using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Congehou
{
    #region Singleton
    public class TimeManager : MonoBehaviour
    {
        #region Singleton

        static protected TimeManager s_Instance;
        static public TimeManager Instance { get { return s_Instance; } }
        #endregion

        protected static bool quitting;
        #endregion

        public int maxTimeClock = 360;
        public float rechargeRate = 0.03f;
        public float decreaseRate = 0.03f;
        
        public UnityEvent onStopTime;
        public UnityEvent onFlowTime;
        
        public float TimeFlow { get; private set; }
        public float CurrentClockValue { get { return m_CurrentClockValue; } }

        private int m_CurrentClockValue;
        private WaitForSeconds m_RechargeRate;
        private WaitForSeconds m_DecreaseRate;
        private Coroutine m_CurrentCoroutine;

        protected HashSet<ITimeObject> m_TimeObjects = new HashSet<ITimeObject>();

        public delegate void ClockValueChangeCallback();
        public ClockValueChangeCallback onClockValueChange;

        void OnDestroy()
        {
            if (s_Instance == this)
                quitting = true;
        }

        private void Awake()
        {
            s_Instance = this;

            m_DecreaseRate = new WaitForSeconds(decreaseRate);
            m_RechargeRate = new WaitForSeconds(rechargeRate);
        }

        public static void RegisterTimeAffectedObject(ITimeObject timeAffectedObject)
        {
            Instance.Register(timeAffectedObject);
        }

        public static void UnregisterTimeAffectedObject(ITimeObject timeAffectedObject)
        {
            if (!quitting)
            {
                Instance.Unregister(timeAffectedObject);
            }
        }

        public void Register(ITimeObject timeAffectedObject)
        {
            m_TimeObjects.Add(timeAffectedObject);
        }

        public void Unregister(ITimeObject timeAffectedObject)
        {
            m_TimeObjects.Remove(timeAffectedObject);
        }

        private void Start()
        {
            m_CurrentClockValue = maxTimeClock;
            onClockValueChange.Invoke();
            TimeFlow = 1;
        }

        public void SpeedUp()
        {
            foreach(ITimeObject timeObject in m_TimeObjects)
            {
                timeObject.SectionCall();
            }
        }

        public void StopTime()
        {
            TimeFlow = 0;

            foreach(var obj in m_TimeObjects)
            {
                obj.OnTimeStop();
            }

            onStopTime.Invoke();
            if(m_CurrentCoroutine != null)
                StopCoroutine(m_CurrentCoroutine);
            m_CurrentCoroutine = StartCoroutine(DecreaseClock());
        }

        public void FlowTime()
        {
            TimeFlow = 1;

            foreach(var obj in m_TimeObjects)
            {
                obj.OnTimeFlow();
            }

            onFlowTime.Invoke();
            if(m_CurrentCoroutine != null)
                StopCoroutine(m_CurrentCoroutine);
            m_CurrentCoroutine = StartCoroutine(RechargeClock());
        }
        
        IEnumerator DecreaseClock()
        {
            while(m_CurrentClockValue > 0)
            {
                m_CurrentClockValue--;
                onClockValueChange.Invoke();
                yield return m_DecreaseRate;
            }
            FlowTime();
        }

        IEnumerator RechargeClock()
        {
            while(m_CurrentClockValue < maxTimeClock)
            {
                m_CurrentClockValue++;
                onClockValueChange.Invoke();
                yield return m_RechargeRate;
            }
        }
    }
    
    public interface ITimeObject
    {
        void OnTimeStop();
        void OnTimeFlow();
        void SectionCall();
    }
}
