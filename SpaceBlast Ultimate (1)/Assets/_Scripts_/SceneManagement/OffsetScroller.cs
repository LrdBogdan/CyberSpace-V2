using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScroller : MonoBehaviour //Bogdan C. SU17A\\
{

    Material material;
    Vector2 offset;

    public int xVelocity, yVelocity;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }

    private void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
