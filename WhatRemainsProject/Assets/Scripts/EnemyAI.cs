using UnityEngine;

namespace WhatRemains.Enemy.AI
{
	public class EnemyAI : MonoBehaviour
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
			this.StartIdle();
		}

		// Update is called once per frame
		private void Update()
		{
			// Move the enemy towards the movepoints 
			if (Vector3.Distance(this.transform.position, this.movePoints[_randomMoveSpot].position) < this.deadzoneDistance)
			{
				this.StopMoving();
				this.StartIdle();
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
				this.StopIdle();
				this.StartMoving();
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

		private void StartMoving()
		{
			if (!_alreadyMoving)
			{
				_alreadyMoving = true;
				this.animator.SetBool("Moving", true);
			}
		}

		private void StopMoving()
		{
			if (_alreadyMoving)
			{
				_alreadyMoving = false;
				this.animator.SetBool("Moving", false);
			}
		}

		private void StartIdle()
		{
			if (!_alreadyIdled)
			{
				_alreadyIdled = true;
				this.animator.SetBool("Idle", true);
			}
		}

		private void StopIdle()
		{
			if (_alreadyIdled)
			{
				_alreadyIdled = false;
				this.animator.SetBool("Idle", false);
			}
		}
	}

}