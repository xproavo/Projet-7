using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    private MoveManager _moveManager;

    private void Awake()
    {
        _moveManager = GetComponentInParent<MoveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveManager.DirectionSee() == Vector2.left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }else if (_moveManager.DirectionSee() == Vector2.right)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
