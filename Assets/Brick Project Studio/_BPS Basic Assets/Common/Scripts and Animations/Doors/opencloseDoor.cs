using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; // Namespace für VR-Interaktionen

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;

        private List<InputDevice> rightHandDevices = new List<InputDevice>();
        private bool triggerValue;

        private Vector3 controllerPosition
        {
            get
            {
                if (rightHandDevices.Count > 0)
                {
                    Vector3 position;
                    rightHandDevices[0].TryGetFeatureValue(CommonUsages.devicePosition, out position);
                    return position;
                }
                return Vector3.zero;
            }
        }

        private Vector3 controllerForward
        {
            get
            {
                if (rightHandDevices.Count > 0)
                {
                    Quaternion rotation;
                    rightHandDevices[0].TryGetFeatureValue(CommonUsages.deviceRotation, out rotation);
                    return rotation * Vector3.forward;
                }
                return Vector3.zero;
            }
        }

        public float maxDistance = 2.0f;

        void Start()
		{
			open = false;
            // Suche nach dem rechten Hand Controller
            var desiredCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandDevices);
        }

        void Update()
        {   
            // Controllerposition und -richtung abfragen, Raycast durchführen
            RaycastHit hit;
            if (Physics.Raycast(controllerPosition, controllerForward, out hit, maxDistance))
            {
                // Überprüfen, ob das getroffene Objekt diese Tür ist
                if (hit.collider.gameObject == this.gameObject)
                {
                    TryGetTriggerValue();
                    // Triggerwert abfragen
                    if (triggerValue) // Wenn der Trigger gedrückt wurde:
                    {
                        if (!open) // wenn die Tür geschlossen ist, öffne sie
                        {
                            StartCoroutine(opening());
                        }
                        else // wenn die Tür offen ist, schließe sie
                        {
                            StartCoroutine(closing());
                        }
                    }
                }
            }
        }

        bool TryGetTriggerValue()
        {
            triggerValue = false;

            // Sicherstellen ob ein Gerät gefunden wurde
            if (rightHandDevices.Count > 0)
            {
                // Tastenabfrage
                if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    return true; // Trigger wurde gedrückt
                }
            }

            return false; // Trigger wurde nicht gedrückt
        }

        IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("Opening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}