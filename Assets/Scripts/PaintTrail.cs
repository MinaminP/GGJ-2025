using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTrail : MonoBehaviour
{
    [SerializeField] LineRenderer paintTrail;
    [SerializeField] Vector3 paintTrailPreviousPos;
    [SerializeField] float trailDistance;
    [SerializeField] GameObject paintDroplet;
    // Start is called before the first frame update
    void Start()
    {
        paintTrail.positionCount = 1;
        paintTrailPreviousPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CreateTrail();
    }

    void CreateTrail()
    {
        if (Vector3.Distance(paintDroplet.transform.position, paintTrailPreviousPos) > trailDistance)
        {
            if (paintTrailPreviousPos == transform.position)
            {
                paintTrail.SetPosition(0, paintDroplet.transform.position);
            }
            else
            {
                paintTrail.positionCount++;
                paintTrail.SetPosition(paintTrail.positionCount - 1, paintDroplet.transform.position);
            }

            paintTrailPreviousPos = paintDroplet.transform.position;
        }
    }
}
