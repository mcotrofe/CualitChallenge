using UnityEngine;
using UnityEngine.Events;

namespace CualitChallenge.Utils
{

    public class TriggerEvent : MonoBehaviour
    {
        public string Tag;
        [SerializeField] UnityEvent onTriggerEnter;
        [SerializeField] UnityEvent onTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tag)) onTriggerEnter.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tag)) onTriggerExit.Invoke();
        }
    }

}