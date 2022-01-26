using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform player, centerPoint;

    public int ColorID = 0;
    float[] location = { 0, 0 };

    public float bulletForce;

    void Update()
    {
        centerPoint.LookAt(player);
        location[0] = centerPoint.localRotation.y / Mathf.Abs(centerPoint.localRotation.y);
        location[1] = -(centerPoint.localRotation.x / Mathf.Abs(centerPoint.localRotation.x));

        switch (location)
        {
            case var l when l[0] == 1 && l[1] == 1:         //Top-Right corner
                ColorID = 0;
                break;

            case var l when l[0] == -1 && l[1] == 1:        //Top-Left corner
                ColorID = 1;
                break;

            case var l when l[0] == 1 && l[1] == -1:        //Bottom-Right corner
                ColorID = 2;
                break;

            case var l when l[0] == -1 && l[1] == -1:       //Bottom-Left corner
                ColorID = 3;
                break;

            default:
                break;
        }
    }
}
