using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDetectZone : MonoBehaviour
{
    private StateManager _stateManager;
    private SimpleSkeletonIA _simpleSkeletonIA;

    private void Awake()
    {
        _stateManager = GetComponentInParent<StateManager>();
        _simpleSkeletonIA = GetComponentInParent<SimpleSkeletonIA>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Player") && !_stateManager.Death)
        {
            _simpleSkeletonIA.OnFocusEnemy();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && !_stateManager.Death)
        {
            if (collision.transform.CompareTag("Player"))
            {

                if (collision.GetComponent<StateManager>().Death)
                {
                    _simpleSkeletonIA.EndFocusEnemy();
                }
                _simpleSkeletonIA.DirMove = collision.transform.position - transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Player") && !_stateManager.Death)
        {
            _simpleSkeletonIA.EndFocusEnemy();
            _simpleSkeletonIA.DirMove = Vector3.zero;
        }
    }
}
