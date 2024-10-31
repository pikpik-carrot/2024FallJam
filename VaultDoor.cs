using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FallJam
{
public class VaultDoor : MonoBehaviour
{
    public Animator animator;
    public bool isOpen;

    private bool previousState;

    private void Start()
    {
        previousState = isOpen;
    }

    // Hell is real
    // I saw it...
    //              -Textured_Turtle

    private void FixedUpdate()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on VaultDoor GameObject.");
            return;
        }

        if (previousState != isOpen)
        {
            // Detect change in isOpen and trigger animation accordingly
            if (isOpen)
            {
                animator.Play("DoorOpen", 0, 0f); // Start from beginning
                animator.speed = 1f; // Normal speed for opening
                Debug.Log("Playing DoorOpen animation forward.");
            }
            else
            {
                animator.Play("DoorOpen", 0, 1f); // Start at end for reverse
                animator.speed = -1f; // Normal speed for opening
                Debug.Log("Playing DoorOpen animation in reverse.");
            }

            previousState = isOpen; // Update the previous state
        }

        // Handle the closing animation
        if (!isOpen && animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
        {
            float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            // Manually adjust the normalized time for a smooth closing effect
            if (normalizedTime > 0)
            {
                normalizedTime -= Time.deltaTime / animator.runtimeAnimatorController.animationClips[0].length; // Adjust based on animation length
                animator.Play("DoorOpen", 0, normalizedTime);
            }
            else
            {
                // Stop when fully closed
                animator.speed = 0f; // Stop animation at the end
                Debug.Log("Animation stopped on the last frame.");
            }
        }
    }
}
}
