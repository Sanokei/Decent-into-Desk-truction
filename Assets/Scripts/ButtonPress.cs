using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public Animation pushAnimation;
    public Animation doorCloseAnimation;
    public Animation doorOpenAnimation;
    public GameObject door;
    public GameObject player;
    public GameObject waveCollapsePrefab;
    public GameObject monsters;
    public int currentLevel = 1;

    private bool doorOpen = false;

    private void Update()
    {
        if (IsPlayerLookingAtButton() && Input.GetKeyDown(KeyCode.E))
        {
            pushAnimation.Play();
            doorCloseAnimation.Play();
            LoadLevel();
        }

        if (doorOpen && !IsEnemiesAlive())
        {
            doorCloseAnimation.Play();
            doorOpen = false;
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject == player)
    //     {
    //         doorOpenAnimation.Play();
    //         doorOpen = true;
    //     }
    // }

    private bool IsPlayerLookingAtButton()
    {
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            return hit.collider.gameObject.CompareTag("button");
        }

        return false;
    }

    private void LoadLevel()
    {
        // Instantiate(waveCollapsePrefab, transform.position, transform.rotation);

        // Add the monsters to the scene considering the current level
        // Add code here

        doorOpenAnimation.Play();
        doorOpen = true;
    }

    private bool IsEnemiesAlive()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        return enemies.Length > 0;
    }
}