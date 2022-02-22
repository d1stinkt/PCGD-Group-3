using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    public Transform originPointDown, endPointDown, originPointLeft,
                     endPointLeft, originPointRight, endPointRight,
                     originPointUp, endPointUp;

    public bool wallDetectedDown, wallDetectedLeft, wallDetectedRight, wallDetectUp;

    public static bool pathOpenDown, pathOpenLeft, pathOpenRight, pathOpenUp;
    private int wallLayer = 1 << 7;

    private void Update()
    {
        WallDetector();
    }

    void WallDetector()
    {
        //LayerMask wall = LayerMask.GetMask("Wall");
        Debug.DrawLine(originPointDown.position, endPointDown.position, Color.black);
        Debug.DrawLine(originPointLeft.position, endPointLeft.position, Color.black);
        Debug.DrawLine(originPointRight.position, endPointRight.position, Color.black);
        Debug.DrawLine(originPointUp.position, endPointUp.position, Color.black);
        wallDetectedDown = Physics2D.Linecast(originPointDown.position, endPointDown.position, wallLayer);
        wallDetectedLeft = Physics2D.Linecast(originPointLeft.position, endPointLeft.position, wallLayer);
        wallDetectedRight = Physics2D.Linecast(originPointRight.position, endPointRight.position, wallLayer);
        wallDetectUp = Physics2D.Linecast(originPointUp.position, endPointUp.position, wallLayer);
        CheckResults();
    }

    void CheckResults()
    {
        if (wallDetectedDown == true)
        {
            pathOpenDown = false;
        }
        else { pathOpenDown = true; }

        if (wallDetectedLeft == true)
        {
            pathOpenLeft = false;
        }
        else { pathOpenLeft = true; }

        if (wallDetectedRight == true)
        {
            pathOpenRight = false;
        }
        else { pathOpenRight = true; }

        if (wallDetectUp == true)
        {
            pathOpenUp = false;
        }
        else { pathOpenUp = true; }
    }
}