using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageIA : MonoBehaviour
{
    public GameObject[] WayPoints;
    public float TimeBeforeTeleport = 5;

    public float CoulDown;


    public bool _playerOnTheZone = false;
    private bool attack = true;

    private Vector3 _targetPos;

    private Attack _attack;
    private StateManager _stateManager;
    private MoveManager _moveManager;
    private AnimateManager _animateManager;

    private GameObject _player;

    public GameObject FireBallPrefab;

    private Vector2 _dirAttack;


    // Start is called before the first frame update
    void Start()
    {
        _attack = GetComponent<Attack>();
        _stateManager = GetComponent<StateManager>();
        _moveManager = GetComponent<MoveManager>();
        _animateManager = GetComponent<AnimateManager>();

        _player = GameObject.FindGameObjectWithTag("Player");

        _stateManager.OnTakeDamage += isDeathStopCoroutine;

        StartCoroutine(TeleportMage());
    }

    private void isDeathStopCoroutine(float damage)
    {
        if (_stateManager.Death)
            StopAllCoroutines();
    }

    private IEnumerator TeleportMage()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeBeforeTeleport);
            _animateManager.TP();
        }

    }

    public void TPSkeleton()
    {
        gameObject.transform.position = WayPoints[Random.Range(0, WayPoints.Length - 1)].transform.position;
    }

    public void InZone(bool isInZone)
    {
        _playerOnTheZone = isInZone;
    }



    private void FixedUpdate()
    {
        if (_playerOnTheZone)
        {
            if (attack)
            {
                _dirAttack = _player.transform.position - transform.position;
                var distance = Vector3.Distance(transform.position, _player.transform.position);
                _attack.DoAttackWithFireBall(_dirAttack, distance, _stateManager.EnemyLayer);
                StartCoroutine(AttackCoulDown());
            }
        }
    }

    public void ThrowFireBall()
    {
        var _dirSee = new Vector3(_moveManager.DirectionSee().x, _moveManager.DirectionSee().y);
        GameObject clone = GameObject.Instantiate(FireBallPrefab, transform.position + _dirSee, Quaternion.identity);
        clone.gameObject.GetComponent<FireBall>().ChangeTheDirToMove(_dirAttack.normalized);
    }

    private IEnumerator AttackCoulDown()
    {
        attack = false;
        yield return new WaitForSeconds(CoulDown);
        attack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        var dir = _player.transform.position - transform.position;
        var distance = Vector3.Distance(transform.position, _player.transform.position);

        Gizmos.DrawLine(transform.position,  transform.position + dir );
    }
}
