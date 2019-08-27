using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float jumpCooldown;
	[SerializeField]
	private float jumpSpeed;
	[SerializeField]
	private float jumpHeight;

	private float jumpInterval;
	private float maxHeight;
	private float fallMultiplier;
	private float distToGround;

	private Vector3 forward;
	private Vector3 right;

	private Rigidbody playerRB;

	public bool isVaultable;
	public bool isFront;
	public bool isBack;
	public bool isRight;
	public bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

		jumpInterval = 0.5f;
		maxHeight = 0f;
		fallMultiplier = 3.25f;
		playerRB = GetComponent<Rigidbody>();
		distToGround = GetComponent<BoxCollider>().bounds.extents.y;

		isVaultable = false;
		isFront = false;
		isBack = false;
		isLeft = false;
		isRight = false;

    }

    // Update is called once per frame
    void Update()
    {
		print(isGrounded());
		if (Input.GetKeyDown(KeyCode.Space) && isVaultable && isGrounded())
		{
			StartCoroutine(Jump());

		}
		else
		{
			MovePlayer();
		}

		if (isGrounded())
		{
			playerRB.useGravity = true;
		}
    }

	public bool isGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
	private IEnumerator Jump()
	{
		float originalHeight = transform.position.y;
		float maxHeight = originalHeight + jumpHeight;
		playerRB.useGravity = false;
		yield return null;

		while (transform.position.y < maxHeight)
		{
			if (isFront == true)
			{
				print("Here");
				transform.position += Vector3.right * Time.deltaTime * jumpSpeed;

			}
			else if (isBack == true)
			{
				transform.position += Vector3.left * Time.deltaTime * jumpSpeed ;
			}
			else if (isLeft == true)
			{
				transform.position += Vector3.forward * Time.deltaTime * jumpSpeed;
			}
			else if (isRight == true)
			{
				transform.position += Vector3.back * Time.deltaTime * jumpSpeed;
			}
			transform.position += Vector3.up * Time.deltaTime * jumpSpeed;

			yield return null;

		}

		while (transform.position.y > originalHeight)
		{
			if (isFront == true && !isGrounded())
			{
				transform.position -= Vector3.left * Time.deltaTime * jumpSpeed;
			}
			else if (isBack == true && !isGrounded())
			{
				transform.position -= Vector3.right * Time.deltaTime * jumpSpeed;
			}
			else if (isLeft == true && !isGrounded())
			{
				transform.position -= Vector3.back * Time.deltaTime * jumpSpeed;
			}
			else if (isRight == true && !isGrounded())
			{
				transform.position -= Vector3.forward * Time.deltaTime * jumpSpeed;
			}
			transform.position -= Vector3.up * Time.deltaTime * jumpSpeed;

			if (isGrounded())
			{
				if (isFront == true)
				{
					isFront = false;
				}

				else if (isLeft == true)
				{
					isLeft = false;
				}

				else if (isRight == true)
				{
					isRight = false;
				}

				else if (isBack == true)
				{
					isBack = false;
				}
			}
			yield return null;
		}

		//playerRB.useGravity = true;
		yield return null;
	}

		

	private void MovePlayer()
	{
		Vector3 dir = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
		Vector3 rMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
		Vector3 fMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

		Vector3 heading = Vector3.Normalize(rMovement + fMovement);
		transform.forward = heading;
		transform.position += rMovement;
		transform.position += fMovement;
	}
}
