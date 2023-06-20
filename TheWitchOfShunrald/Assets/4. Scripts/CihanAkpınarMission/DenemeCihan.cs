using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenemeCihan : MonoBehaviour
{
    #region
    public static DenemeCihan instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
}
