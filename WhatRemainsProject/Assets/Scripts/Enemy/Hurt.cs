using UnityEngine;

namespace WhatRemains.Enemy.AI
{
    public class Hurt : MonoBehaviour
    {
        public float cooldown_hurt = 0.5f;

        public int health;

        public GameObject enemy;

        public ParticleSystem particleBlood;

        private float _timer;

        private bool _bTouchedSword;

        private bool _bInCoolDown;

        private void Awake()
        {
            _bTouchedSword = false;
            if (this.enemy == null)
            {
                throw new System.NullReferenceException(nameof(this.enemy));
            }
            if (this.particleBlood == null)
            {
                throw new System.NullReferenceException(nameof(this.particleBlood));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Sword")
            {
                _bTouchedSword = true;
                _timer = this.cooldown_hurt;
                this.particleBlood.Play();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Sword")
            {
                _bTouchedSword = false;
                this.particleBlood.Stop();
            }
        }

        private void Update()
        {
            if (_bInCoolDown)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _bInCoolDown = false;
                }
            }
            else
            {
                if (_bTouchedSword)
                {
                    this.health--;
                    if (this.health <= 0)
                    {
                        Debug.Log("The enemy will die.");
                        GameObject.Destroy(this.enemy);
                    }
                    _bInCoolDown = true;
                    _timer = this.cooldown_hurt;
                }
            }
        }
    }
}
