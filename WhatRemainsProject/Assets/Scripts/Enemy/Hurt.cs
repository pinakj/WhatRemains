using UnityEngine;

namespace WhatRemains.Enemy.AI
{
    public class Hurt : MonoBehaviour
    {
        public float cooldown_hurt = 0.5f;

        public int health;

        public GameObject enemy;

        private float _timer;

        private bool _bInDamaging;

        private void Awake()
        {
            _bInDamaging = false;
            if (this.enemy == null)
            {
                throw new System.NullReferenceException(nameof(this.enemy));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Sword")
            {
                _bInDamaging = true;
                _timer = this.cooldown_hurt;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Sword")
            {
                _bInDamaging = false;
            }
        }

        private void Update()
        {
            if (_bInDamaging)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0.0f)
                {
                    health--;
                    if (health <= 0)
                    {
                        Debug.Log("The enemy will die.");
                        GameObject.Destroy(this.enemy);
                    }
                    else
                    {
                        _timer = this.cooldown_hurt;
                    }
                }
            }
        }
    }

}
