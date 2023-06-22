using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CihanAkpınar
{
    public class DenemeCihan : MonoBehaviour
    {
        public float denemeCan;
        #region
        public static DenemeCihan instance;
        private void Awake()
        {
            instance = this;
        }
        #endregion

        public GameObject player;
    }
}


