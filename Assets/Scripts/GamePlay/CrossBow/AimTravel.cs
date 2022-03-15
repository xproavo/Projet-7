using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTravel : MonoBehaviour
{
    public float distanceToParent = 1f;

    private CrossBow _parentCrossBowScript;

    private void Awake()
    {
        _parentCrossBowScript = GetComponentInParent<CrossBow>();
        transform.localPosition = new Vector3(distanceToParent + _parentCrossBowScript.AjustArrowSpawnPos.x, _parentCrossBowScript.AjustArrowSpawnPos.y);
    }




    
}
