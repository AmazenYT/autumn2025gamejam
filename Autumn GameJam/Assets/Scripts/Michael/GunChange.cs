using Unity.VisualScripting;
using UnityEngine;

public class GunChange : MonoBehaviour
{
    public GameObject currentGun;
    public GameObject newGun;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the GunChangeTrigger");
            currentGun.SetActive(!currentGun.activeSelf);
            newGun.SetActive(!newGun.activeSelf);

            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited the GunChangeTrigger");

            

        }
        
    }
}
