using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenObject : MonoBehaviour
{
    private float minXValueWorld;
    private float maxXValueWorld;
    private float minYValueWorld;
    private float maxYValueWorld;
    private float threshold = 2f;

    private Vector3 maxXValueScreenWithPlayerSize;
    private Vector3 minXValueScreenWithPlayerSize;
    private Vector3 maxYValueScreenWithPlayerSize;
    private Vector3 minYValueScreenWithPlayerSize;
    private Vector3 maxXValueWorldWithPlayerSize;
    private Vector3 minXValueWorldWithPlayerSize;
    private Vector3 maxYValueWorldWithPlayerSize;
    private Vector3 minYValueWorldWithPlayerSize;

    void Start()
    {
        minXValueWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        maxXValueWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        minYValueWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        maxYValueWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        maxXValueWorldWithPlayerSize = new Vector3(maxXValueWorld + threshold / 4, 0, 0);
        minXValueWorldWithPlayerSize = new Vector3(minXValueWorld - threshold / 4, 0, 0);
        maxYValueWorldWithPlayerSize = new Vector3(0, maxYValueWorld + threshold / 4, 0);
        minYValueWorldWithPlayerSize = new Vector3(0, minYValueWorld - threshold / 4, 0);
        maxXValueScreenWithPlayerSize = Camera.main.WorldToScreenPoint(maxXValueWorldWithPlayerSize);
        minXValueScreenWithPlayerSize = Camera.main.WorldToScreenPoint(minXValueWorldWithPlayerSize);
        maxYValueScreenWithPlayerSize = Camera.main.WorldToScreenPoint(maxYValueWorldWithPlayerSize);
        minYValueScreenWithPlayerSize = Camera.main.WorldToScreenPoint(minYValueWorldWithPlayerSize);
    }

    private void FixedUpdate()
    {
        OffscreenCheck();
    }

    private void OffscreenCheck()
    {

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        if (screenPos.x < minXValueScreenWithPlayerSize.x)
        {
            this.transform.position = new Vector3(
                maxXValueWorldWithPlayerSize.x,
                this.transform.position.y,
                this.transform.position.z);
        }

        if (screenPos.x > maxXValueScreenWithPlayerSize.x)
        {
            this.transform.position = new Vector3(
                minXValueWorldWithPlayerSize.x,
                this.transform.position.y,
                this.transform.position.z);
        }

        if (screenPos.y < minYValueScreenWithPlayerSize.y)
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                maxYValueWorldWithPlayerSize.y,
                this.transform.position.z);
        }

        if (screenPos.y > maxYValueScreenWithPlayerSize.y)
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                minYValueWorldWithPlayerSize.y,
                this.transform.position.z);
        }

    }
}
