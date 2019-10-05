using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChildChange : MonoBehaviour
{
    private SpriteRenderer parentSprRender;
    private SpriteRenderer sprRenderer;

    void Start()
    {       
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        parentSprRender = transform.parent.GetComponent<SpriteRenderer>();
        sprRenderer.sprite = parentSprRender.sprite;
    }
}
