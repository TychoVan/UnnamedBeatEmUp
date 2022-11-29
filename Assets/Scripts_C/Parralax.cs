using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    private float lenghth, startpos;
    public GameObject cam;
    public float parralaxEffect;

    private void Start()
    {
        startpos = transform.position.x;
        lenghth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parralaxEffect));
        float dist = (cam.transform.position.x * parralaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + lenghth) startpos += lenghth;
        else if (temp < startpos - lenghth) startpos -= lenghth;

        // toturial followed from dani
    }
}
