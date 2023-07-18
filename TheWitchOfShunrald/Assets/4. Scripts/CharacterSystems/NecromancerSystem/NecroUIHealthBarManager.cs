using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sidar
{
    public class NecroUIHealthBarManager : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
        }

        private void Start()
        {
            //SetUIHealthBartToInactive();
        }

        public void SetUIHealthBartToActive()
        {
            slider.gameObject.SetActive(true);
        }
        public void SetUIHealthBartToInactive()
        {
            slider.gameObject.SetActive(false);
        }

        public void SetBossMaxHeatlh(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetBossCurrentHealth(float currentHealth)
        {
            slider.value = currentHealth;
        }
    }
}
