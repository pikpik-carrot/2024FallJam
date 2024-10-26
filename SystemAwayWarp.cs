using OWML.ModHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static FallJam.FallJam;

namespace FallJam
{
    internal class SystemAwayWarp: MonoBehaviour
    {
        private bool hasWaited = false;

        [SerializeField]
        private OWTriggerVolume _triggerVolume;

        private void Awake()
        {
            _triggerVolume.OnExit += OnExit;
        }
        private void OnExit(GameObject hitObj)
        {
            if (hitObj.CompareTag("PlayerDetector"))
            {
                FallJam.newHorizons.ChangeCurrentStarSystem("Interstellar Space");
            }
        }
    }
}
