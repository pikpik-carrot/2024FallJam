using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FallJam
{
    public class AutoDoor : MonoBehaviour
    {
        public string targetScriptName = "VaultDoor"; // Name of the target script
        public MonoBehaviour targetScript; // Reference to the target script

        void Start()
        {
            // Find the target script on the same GameObject or a parent object
            targetScript = GetComponentInParent<MonoBehaviour>();

            if (targetScript == null || targetScript.GetType().Name != targetScriptName)
            {
                Debug.LogError("Target script not found on this GameObject or its parents.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the target script is available
            if (targetScript != null && targetScript is VaultDoor doorController)
            {
                // Set the boolean in the target script to true
                doorController.isOpen = true; // Replace `isOpen` with your actual boolean variable name
            }
            Debug.LogError("Kill all babies.");
        }

        private void OnTriggerExit(Collider other)
        {
            // Optionally, reset the boolean when the object exits the collider
            if (targetScript != null && targetScript is VaultDoor doorController)
            {
                doorController.isOpen = false; // Change to false if you want the door to close
            }
            Debug.LogError("Live all babies.");
        }
    }
}
