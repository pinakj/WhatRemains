﻿using UnityEngine;

namespace WhatRemains.Enemy.AI
{
    public class Patrol : MonoBehaviour
    {
        public float moveSpeed;

        public float rotateAngleSpeed = 5.0f;

        public Animator animator;

        public Transform[] movePoints;

        public float deadzoneDistance = 0.4f;

        public float waitTimeInMovepoint = 1.0f;

        private int _randomMoveSpot;

        private float _timer = 0.0f;

        private bool _alreadyMoving = true;

        private bool _alreadyIdled = false;

        public bool canMoving { get; private set; }

        public bool canIdling { get; private set; }

        public bool canPatrol { get; private set; }

        private void Awake()
        {
            if (this.movePoints.Length == 0)
            {
                throw new System.Exception("The move points' count should not be 0.");
            }
            _timer = 0.0f;
        }

        // Start is called before the first frame update
        private void Start()
        {
            this.StartPatroling();
        }

        public void StartPatroling()
        {
            this.canPatrol = true;
            this.RandMoveSpot();
        }

        public void StopPatroling()
        {
            this.canPatrol = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!this.canPatrol)
            {
                return;
            }
            // Move the enemy towards the movepoints 
            var targetPosition = new Vector3(this.movePoints[_randomMoveSpot].position.x, this.transform.position.y, this.movePoints[_randomMoveSpot].position.z);
            if (Vector3.Distance(this.transform.position, targetPosition) < this.deadzoneDistance)
            {
                //this.Idling();
                // Wait for the certain seconds
                if (_timer <= 0)
                {
                    this.RandMoveSpot();
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
            }
            else
            {
                //this.Moving();
                var movePoint = this.movePoints[_randomMoveSpot];
                this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * this.moveSpeed);
                var supposedDir = (targetPosition - this.transform.position).normalized;
                var supposedRot = Quaternion.LookRotation(supposedDir);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, supposedRot, Time.deltaTime * this.rotateAngleSpeed);
            }
        }

        private void RandMoveSpot()
        {
            _randomMoveSpot = Random.Range(0, this.movePoints.Length);
            _timer = this.waitTimeInMovepoint;
        }

        private void Moving()
        {
            if (!this.animator.GetBool("Moving"))
            {
                this.animator.SetBool("Moving", true);
                this.animator.SetBool("Idling", false);
            }
        }

        private void Idling()
        {
            if (!this.animator.GetBool("Idling"))
            {
                this.animator.SetBool("Idling", true);
                this.animator.SetBool("Moving", false);
            }
        }
    }

}