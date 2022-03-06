using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public float LifePoint = 20;
    public bool Death = false;




    private void Update()
    {
        
        if (LifePoint <= 0)
        {

            Death = true;
        }
    }

}
