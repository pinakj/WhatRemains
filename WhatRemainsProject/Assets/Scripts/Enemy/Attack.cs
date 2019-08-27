using UnityEngine;

namespace WhatRemains.Enemy.AI
{
    public class Attack : MonoBehaviour
    {
        public float dis_attack = 0.2f;

        public float moveSpeed;

        public Patrol patrol;

        public float rotateAngleSpeed;

        public Animator animator;

        private bool _bCanFollowing = false;

        private Transform _tAttackTarget = null;

        private bool _bFollowing = false;

        // Start is called before the first frame update
        private void Awake()
        {
            if (this.animator == null)
            {
                throw new System.NullReferenceException(nameof(this.animator));
            }
            if (this.patrol == null)
            {
                throw new System.NullReferenceException(nameof(this.patrol));
            }
        }

        private void Start()
        {
            _bCanFollowing = false;
            _bFollowing = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_bCanFollowing)
            {
                var targetPosition = new Vector3(_tAttackTarget.position.x, this.transform.position.y, _tAttackTarget.position.z);
                if (Vector3.Distance(this.transform.position, targetPosition) < this.dis_attack)
                {
                    // Stop moving and start attacking
                    this.Attacking();
                }
                else
                {
                    // Start following
                    this.Following();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                _bCanFollowing = true;
                _tAttackTarget = other.transform;
                this.patrol.StopPatroling();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                _bCanFollowing = false;
                _tAttackTarget = null;
                this.patrol.StartPatroling();
            }
        }

        private void Attacking()
        {
            if (!this.animator.GetBool("Attacking"))
            {
                this.animator.SetBool("Attacking", true);
                this.animator.SetBool("Moving", false);
                this.animator.SetBool("Idling", false);
            }
        }

        private void Following()
        {
            if (!this.animator.GetBool("Moving"))
            {
                this.animator.SetBool("Moving", true);
                this.animator.SetBool("Attacking", false);
                this.animator.SetBool("Idling", false);
            }
            // Follow the player.
            var targetPosition = new Vector3(_tAttackTarget.position.x, this.transform.position.y, _tAttackTarget.position.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * this.moveSpeed);
            // Rotate the enemy to face the player.
            var supposedDir = (targetPosition - this.transform.position).normalized;
            var supposedRot = Quaternion.LookRotation(supposedDir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, supposedRot, Time.deltaTime * this.rotateAngleSpeed);
        }
    }

}