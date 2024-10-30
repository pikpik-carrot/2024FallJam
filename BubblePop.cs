using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FallJam
{
    public class BubblePop : MonoBehaviour
    {
        public ParticleSystem particleEffect;          // Assign the particle effect in the inspector
        public GameObject triggerObject;               // Assign any object to be triggered (e.g., a sound, animation)
        public MonoBehaviour componentToToggle;        // Assign the component you want to toggle on trigger
        public AudioClip collisionSound;               // Assign the audio clip to play on collision
        public float floatSpeed = 1f;                  // Speed at which the object floats upwards
        public float wobbleAmount = 0.1f;              // Amount of wobble on the X-axis
        public float shrinkSpeed = 5f;                 // Speed at which the object shrinks
        public float triggerDelay = 3f;                // Time delay before auto-triggering
        public float growthSpeed = 2f;                 // Speed at which the object grows to full size

        private AudioSource audioSource;               // Audio source to play sounds
        private bool hasTriggered = false;             // To track if the collider was triggered
        private Vector3 initialPosition;
        private Vector3 targetScale;                   // Target scale for the growth effect
        private bool isGrowing = true;                 // Flag to indicate growth phase

        private void Start()
        {
            // Store the starting position for wobbling and floating effect
            initialPosition = transform.position;

            // Save the initial target scale and start from scale zero
            targetScale = transform.localScale;
            transform.localScale = Vector3.zero;

            // Disable the specified component at the start
            if (componentToToggle != null)
            {
                componentToToggle.enabled = false;
            }

            // Add an AudioSource component and assign it
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = collisionSound; // Assign the audio clip
            audioSource.spatialBlend = 1;
            audioSource.volume = 0.01f;
            audioSource.maxDistance = 1;


            // Other audiostuff

            // Start the delayed trigger
            StartCoroutine(TriggerAfterDelay());
        }

        private IEnumerator TriggerAfterDelay()
        {
            yield return new WaitForSeconds(triggerDelay);

            // Trigger actions once the delay is complete
            TriggerActions();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the object's tag is NOT the specified tag, e.g., "Enemy"
            if (!other.CompareTag("Sun")) // Replace "Enemy" with the tag you want to ignore
            {
                TriggerActions();
            }
        }

        private void TriggerActions()
        {
            if (hasTriggered) return; // Ensure it only triggers once
            hasTriggered = true;

            // Play collision sound
            if (audioSource != null && collisionSound != null)
            {
                audioSource.Play();
            }

            // Trigger any additional action on the assigned GameObject
            if (triggerObject != null)
            {
                triggerObject.SetActive(true);
            }

            // Emit particle effect if assigned
            if (particleEffect != null)
            {
                ParticleSystem instantiatedParticle = Instantiate(particleEffect, transform.position, Quaternion.identity);
                instantiatedParticle.Play();

                // Destroy the particle system after its duration
                Destroy(instantiatedParticle.gameObject, instantiatedParticle.main.duration);
            }

            // Enable the specified component
            if (componentToToggle != null)
            {
                componentToToggle.enabled = true;
            }

            // Optionally, destroy the game object after a delay if desired
            Destroy(gameObject, 3f);
        }

        private void Update()
        {
            // Floating effect with wobble
            float wobble = Mathf.Sin(Time.time * 2f) * wobbleAmount;
            Vector3 wobbleMovement = transform.right * wobble;
            Vector3 upwardMovement = transform.up * (floatSpeed * Time.deltaTime);

            // Apply movement
            transform.position += upwardMovement + wobbleMovement;

            if (isGrowing)
            {
                // Smoothly scale the object from zero to its target size
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, growthSpeed * Time.deltaTime);

                // Once the object is close to its target scale, stop growing
                if ((transform.localScale - targetScale).sqrMagnitude < 0.001f)
                {
                    transform.localScale = targetScale;
                    isGrowing = false;
                }
            }

            if (hasTriggered)
            {

                // Shrinking effect
                if (triggerObject != null)
                {
                    triggerObject.transform.localScale = Vector3.Lerp(triggerObject.transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);

                    if (triggerObject.transform.localScale.magnitude < 0.01f)
                    {
                        Destroy(triggerObject);
                    }
                }
            }
        }
    }
}
