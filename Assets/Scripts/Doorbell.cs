using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using SojaExiles;

public class Doorbell : MonoBehaviour
{
    public AudioSource doorbellSound;
    public opencloseDoor doorScript;

    // This method will be called when the doorbell is interacted with
    public void OnPoke()
    {
        if (doorbellSound != null)
        {
            doorbellSound.Play();
        }
        if (doorScript != null)
        {
            doorScript.ToggleDoor();
        }
    }
}
