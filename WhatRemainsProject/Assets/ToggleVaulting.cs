using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVaulting : MonoBehaviour
{
	[SerializeField]
	private GameObject player;

	private CharController pController;
    // Start is called before the first frame update
    void Start()
    {
		pController = player.GetComponent<CharController>();
		pController.isVaultable = false;
    }

	private void OnTriggerEnter(Collider other)
	{
		print("HERE");
		if (gameObject.tag == "Right")
		{
			pController.isRight = true;
		}
		else if (gameObject.tag == "Left")
		{
			pController.isLeft = true;
		}
		else if (gameObject.tag == "Front")
		{
			pController.isFront = true;
		}
		else if (gameObject.tag == "Back")
		{
			pController.isBack = true;
		}
		pController.isVaultable = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (gameObject.tag == "Right" && pController.isGrounded())
		{
			pController.isRight = false;
		}
		else if (gameObject.tag == "Left" && pController.isGrounded())
		{
			pController.isLeft = false;
		}
		else if (gameObject.tag == "Front" && pController.isGrounded())
		{
			pController.isFront = false;
		}
		else if (gameObject.tag == "Back" && pController.isGrounded())
		{
			pController.isBack = false;
		}
		pController.isVaultable = false;
	}
}
