using UnityEngine;

namespace CualitChallenge.Characters.AI
{

    public class TestDummy : MonoBehaviour
    {
        void Start()
        {
            GetComponentInChildren<AIBrain>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}