using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnVacPickup : MonoBehaviour
{
    public delegate void OnVaccumPickup();
    public OnVaccumPickup OnVaccumPickupEvent;
    public Animator DoorLeft;
    public Animator DoorRight;
    public GameObject UI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorLeft.SetTrigger("OpenDoor");
            DoorRight.SetTrigger("OpenDoor");
            UI.SetActive(true);
            Destroy(gameObject);
        }
    }
}