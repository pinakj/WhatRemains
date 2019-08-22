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
		pController.isVaultable = true;
	}
	private void OnTriggerExit(Collider other)
	{
		pController.isVaultable = false;
	}
}
