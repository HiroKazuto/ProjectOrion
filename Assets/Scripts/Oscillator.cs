using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}
        float cycles = Time.time / period;//contuinally growing over time

        const float tau = Mathf.PI * 2;//constant value of 6.283
        float rawSinWae = Mathf.Sin(cycles * tau);//going from -1 to 1

        movementFactor = (rawSinWae + 1f) / 2f;//recalculated tp gp from 0 to 1 so its cleaner

        Vector3 offset = movementVector  * movementFactor;
        transform.position = startingPosition + offset;
    }
}
