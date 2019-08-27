using UnityEngine;

namespace WhatRemains.Enemy.AI
{
    public class Attack : MonoBehaviour
    {
        public float dis_attack;

        public Animator animator;

        // Start is called before the first frame update
        private void Awake()
        {
            if (this.animator == null)
            {
                throw new System.NullReferenceException(nameof(this.animator));
            }
        }

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
        }

        private void OnTriggerExit(Collider other)
        {
        }
    }

}