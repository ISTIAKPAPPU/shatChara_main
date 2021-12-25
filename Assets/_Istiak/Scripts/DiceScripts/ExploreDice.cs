using System;
using BallScripts;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DiceScripts
{
    public class ExploreDice : MonoBehaviour
    {
        [SerializeField] private float fieldOfImpact;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Ease ease;
        [SerializeField] private int forceRange = 20;
        [SerializeField] private float moveDuration = 1f;

        private void OnEnable()
        {
            ThrowBall.ExploreDice += ExploreDices;
        }

        private void OnDisable()
        {
            ThrowBall.ExploreDice -= ExploreDices;
        }

        private void ExploreDices()
        {
            //  Debug.Log("Explore");
            var dices = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerMask);
            // Debug.Log("Explore"+dices.Length);
            foreach (var dice in dices)
            {
                var diceTransPos = dice.transform.position;
                var direction = diceTransPos - transform.position;
                var endValue = new Vector3(diceTransPos.x + Random.Range(-forceRange, forceRange),
                    diceTransPos.y + Random.Range(-forceRange, forceRange), 0);
                // Debug.Log("endValue :" + endValue);
                dice.transform.DOMove(endValue, moveDuration, false).SetEase(ease);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
        }
    }
}