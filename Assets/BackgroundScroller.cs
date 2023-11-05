using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    private Renderer renderer;
    private Vector2 offset;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += new Vector2(scrollSpeed * Time.deltaTime, 0);
        renderer.material.SetTextureOffset("_MainTex", offset);
    }
}
