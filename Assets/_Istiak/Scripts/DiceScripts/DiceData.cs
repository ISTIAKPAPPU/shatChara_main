using UnityEngine;

namespace DiceScripts
{
    public class DiceData : MonoBehaviour
    {
        public Vector3 initDicePos;
        private void Awake()
        {
            initDicePos = transform.position;
        }
    }
}
