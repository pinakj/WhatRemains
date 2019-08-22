using UnityEngine;

namespace WhatRemains.Enemy.AI
{
	public class EnemyAI : MonoBehaviour
	{
		public float moveSpeed;

		public Transform[] movePoints;

		public float deadzoneDistance = 0.4f;

		public float waitTimeInMovepoint = 1.0f;

		private int _randomMoveSpot;

		private float _timer = 0.0f;

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
		}

		// Update is called once per frame
		private void Update()
		{
			// Move the enemy towards the movepoints 
			if (Vector3.Distance(this.transform.position, this.movePoints[_randomMoveSpot].position) < this.deadzoneDistance)
			{
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
				this.transform.position = Vector3.MoveTowards(this.transform.position, this.movePoints[_randomMoveSpot].position, Time.deltaTime * this.moveSpeed);
			}


		}

		private void RandMoveSpot()
		{
			_randomMoveSpot = Random.Range(0, this.movePoints.Length);
			_timer = this.waitTimeInMovepoint;
		}
	}

}