using UnityEngine;

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
            this.RandMoveSpot();
            this.canMoving = true;
            this.canIdling = true;
        }

        // Update is called once per frame
        private void Update()
        {
            // Move the enemy towards the movepoints 
            if (Vector3.Distance(this.transform.position, this.movePoints[_randomMoveSpot].position) < this.deadzoneDistance)
            {
                this.Idling();
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
                this.Moving();
                var movePoint = this.movePoints[_randomMoveSpot];
                this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint.position, Time.deltaTime * this.moveSpeed);
                var supposedDir = (movePoint.position - this.transform.position).normalized;
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
            if (this.canMoving)
            {
                this.canMoving = false;
                this.canIdling = true;
                this.animator.SetTrigger("Moving");
            }
        }

        private void Idling()
        {
            if (canIdling)
            {
                this.canIdling = false;
                this.canMoving = true;
                this.animator.SetTrigger("Idling");
            }
        }
    }

}