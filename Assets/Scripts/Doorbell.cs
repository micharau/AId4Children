using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using SojaExiles;

public class Doorbell : MonoBehaviour
{
    public AudioSource doorbellSound;
    // public opencloseDoor doorScript;
    public SojaExiles.opencloseDoor doorScript;
    public float interactionCooldown = 5.0f; // Time in seconds after which the doorbell can be interacted with again
    private bool canInteract = true;

    //Co-Routine for the door opening with delay
    IEnumerator ToggleDoorWithDelay()
    {
        yield return new WaitForSeconds(5);
        doorScript.ToggleDoor();
    }
    //Co-Routine for interaction cooldown
    IEnumerator Cooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactionCooldown);
        canInteract = true;
    }

    //This method will be called when the doorbell is interacted with
    public void OnPoke()
    {
        if (!canInteract)
            return;

        if (doorbellSound != null)
        {
            doorbellSound.Play();
        }
        if (doorScript != null)
        {   
            //open the door with delay
            //doorScript.ToggleDoor();
            StartCoroutine(ToggleDoorWithDelay());
        }
        //Start interaction cooldown
        StartCoroutine(Cooldown());
    }
}
