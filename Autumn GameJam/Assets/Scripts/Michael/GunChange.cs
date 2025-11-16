using Unity.VisualScripting;
using UnityEngine;

public class GunChange : MonoBehaviour
{
    public GameObject currentGun;
    public GameObject newGun;
    public GameObject AR;
    public GameObject Pistol;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the GunChangeTrigger");
            currentGun.SetActive(!currentGun.activeSelf);
            newGun.SetActive(!newGun.activeSelf);
            //AR.SetActive(!AR.activeSelf);
            //Pistol.SetActive(!Pistol.activeSelf);

            Destroy(gameObject);

            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            

        }
        
    }
}
