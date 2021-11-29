using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentBehavior : MonoBehaviour
{
    public List<Tuple<float, float>> steps = new List<Tuple<float, float>>();
    //public list for orientation
    public List<int> orientation = new List<int>();
    public Vector3 currPosition;
    public Vector3 targetPosition;

    public int currOrientation;
    public int targetOrientation;

    public int speed = 1;

    private int cont = 2;
    private float t = 1;


    //function to rotate object based on input number
    public void Rotate(int num)
    {
        if (num == 0)
        {
            transform.Rotate(0, 90, 0);
        }
        else if (num == 1)
        {
            transform.Rotate(0, 180, 0);
        }
        else if (num == 2)
        {
            transform.Rotate(0, 270, 0);
        }
        else if (num == 3)
        {
            transform.Rotate(0, 360, 0);
        }
    }


    void Start()
    {
        currPosition = transform.position;
        targetPosition = new Vector3(steps[1].Item1, 0, steps[1].Item2);

        currOrientation = orientation[0];
        targetOrientation = orientation[1];
        Rotate(currOrientation);
    }

    void Update()
    {
        //move agent to next step position with speed usiing lerp
        transform.position = Vector3.Lerp(currPosition, targetPosition, t);

        t += Time.deltaTime * speed;

        if(transform.position == targetPosition && cont <steps.Count)
        {
            t = 0;
            currPosition = new Vector3(transform.position.x, 0, transform.position.z);
            targetPosition = new Vector3(steps[cont].Item1, 0, steps[cont].Item2);

            //change orientation
            currOrientation = targetOrientation;
            targetOrientation = orientation[cont];
            Rotate(currOrientation);


            cont++;
        }
    }
}
