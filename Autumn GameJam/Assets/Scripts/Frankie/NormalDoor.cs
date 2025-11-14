using UnityEngine;

public class NormalDoor : MonoBehaviour
{
    public float playerDistanceToUse = 3f;
    public Transform player; 
    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Animator>().SetTrigger("OpenDoor");
        }  
    }
}
