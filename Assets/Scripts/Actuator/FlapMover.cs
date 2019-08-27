using System;
using UnityEngine;

namespace Game
{
    public class FlapMover : MonoBehaviour
    {
        [SerializeField] private Vector2 flapForce = Vector2.up * 5;

        private Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Flap()
        {
            rigidbody2D.velocity = flapForce;
        }
    }
}