using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;

		// Add this line to declare an AudioClip field.
        public AudioClip doorSound;

        // Add this line to hold a reference to the AudioSource component.
        private AudioSource audioSource;

		void Start()
		{
			open = false;

			// Initialize the audioSource variable.
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                // Optionally add an AudioSource component if it is not attached.
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

		/*
		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 15)
					{
						if (open == false)
						{
							if (Input.GetMouseButtonDown(0))
							{
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StartCoroutine(closing());
								}
							}

						}

					}
				}

			}
		}
		*/

		public void ToggleDoor()
        {
            if (open == false)
            {
                StartCoroutine(opening());
            }
            else
            {
                StartCoroutine(closing());
            }
        }

		IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("Opening");
			open = true;

			// Play the door sound if it is assigned.
            if (doorSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(doorSound);
            }

			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			open = false;

		            // Play the door sound if it is assigned.
            if (doorSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(doorSound);
            }

			yield return new WaitForSeconds(.5f);
		}
	}
}