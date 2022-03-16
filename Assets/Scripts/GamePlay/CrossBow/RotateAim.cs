using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAim : MonoBehaviour
{
    public float Speed;
    public float RotationTime;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotation());
    }

    private void Update()
    {
        transform.Rotate(0, 0, Speed * Time.deltaTime);
    }
    private IEnumerator Rotation()
    {
        var firstPass = RotationTime / 2;

        yield return new WaitForSeconds(firstPass);

        while (true)
        {
            Speed *= -1;

            yield return new WaitForSeconds(RotationTime);

        }
    }

}
