using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bloby : MonoBehaviour
{
    public Vector2 RandomTimeToAttack;

    public Vector2 HitForce;
    public GameObject[] Projectil;

    [Range(0f, 100f)]
    public float RageJauge;

    public bool IsAttack;

    public float TimeNoise = 0.5f;
    public float NoiseAmplitude = 1;


    private Animator _animator;
    private StateManager _stateManager;

    private CinemachineVirtualCamera _vcam;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateManager = GetComponent<StateManager>();
    }


    private void Update()
    {
        if (IsAttack) //Fait pour activation manuel le temps de dev l'enemy
        {
            Jump();
            IsAttack = false;
        }
    }

    public void EnemyInZone(bool isInZone)
    {
        if (isInZone)
        {
            StartCoroutine(ItsTimeToAttack());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator ItsTimeToAttack()
    {
        while (true)
        {
            Jump();
            yield return new WaitForSeconds(Random.Range(RandomTimeToAttack.x, RandomTimeToAttack.y));
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("Attack");
    }

    public void Noise()
    {
        StartCoroutine(NoiseEnumerator());
    }

    private IEnumerator NoiseEnumerator()
    {
        GameManager.Instance.CineMachineCam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = NoiseAmplitude;
        yield return new WaitForSeconds(TimeNoise);
        GameManager.Instance.CineMachineCam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        SpawnAttack();
    }

    private void SpawnAttack()
    {
        if (Projectil.Length > 0)
        {
            int randProj = Random.Range(0, Projectil.Length);

            Vector3 pos1 = transform.GetChild(1).transform.position;
            Vector3 pos2 = transform.GetChild(2).transform.position;

            float X = Random.Range(pos1.x, pos2.x);

            GameObject clone = GameObject.Instantiate(Projectil[randProj], new Vector3(X, pos1.y), Quaternion.identity);
            clone.gameObject.GetComponent<Roc>().Init();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            collision.gameObject.GetComponent<StateManager>().Attack(_stateManager.Damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((-collision.gameObject.GetComponent<MoveManager>().DirectionSee().x) * HitForce.x, HitForce.y));
        }
    }

}
