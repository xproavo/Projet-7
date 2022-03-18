using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BeeIA : MonoBehaviour
{
    public float TimeBeforAttack = 4f;
    private bool _canAttack = true;


    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Transform ennemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private AnimateFlying _animateFlying;
    private StateManager _stateManager;

    void Start()
    { //recupere et trace le chemin sans actualiser
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        _animateFlying = GetComponent<AnimateFlying>();
        _stateManager = GetComponent<StateManager>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    { //permet de voir si c'est la fin du trajet
        if (path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }else
        {
            reachedEndOfPath = false;
        }
        // pour avoir un vecteur à 1, pour avoir le prochain vecteur
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        //add une force
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (force.x >= 0.01f)
        {
            ennemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            ennemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _canAttack)
        {
            _animateFlying.Attack("Attack1");
            collision.gameObject.GetComponent<StateManager>().Attack(_stateManager.Damage);
            StartCoroutine(NoAttack());
        }
    }

    private IEnumerator NoAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(TimeBeforAttack);
        _canAttack = true;
    }

}
