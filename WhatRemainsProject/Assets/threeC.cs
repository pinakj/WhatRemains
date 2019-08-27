using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threeC : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed = 7f;

	private Vector3 forward;
	private Vector3 right;

    private Animator Charanimator;

	public bool isVaultable;
    // Start is called before the first frame update
    void Start()
    {
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        //Rongda
        Charanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.anyKey)
		{
			MovePlayer();

            //Rongda stuff
        }

        if (isVaultable)
		{
			Debug.Log("Player can vault!");
		}
		else
		{
			Debug.Log("Player can't vault!");
		}

        //Rongda
        //Characater Movement.

        if (Input.GetAxis("HorizontalKey") != 0 || Input.GetAxis("VerticalKey") != 0)
        {
            Charanimator.SetBool("isRun", true);

        }
        else
        {
            Charanimator.SetBool("isRun", false);

        }

        //Rongda
        //Character Die

        if (Input.GetKeyUp(KeyCode.X))
        {
            Charanimator.SetBool("isDied", true);
        }

        //Rongda
        //Character Attack

        if (Input.GetMouseButtonDown(0))
        {
            Charanimator.SetBool("isAttack", true);
        }

        else
        {
            Charanimator.SetBool("isAttack", false);

        }

        //Rongda
        //Vault

        if (Input.GetKey(KeyCode.Space))
        {
            Charanimator.SetBool("IsJumped", true);


        }

        else
        {
            Charanimator.SetBool("IsJumped", false);

            Charanimator.SetBool("isIdle", true);

        }



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
