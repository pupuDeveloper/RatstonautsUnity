using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    public SpriteRenderer exampleBGSprite;
    void Awake()
    {
        float orthoSize = exampleBGSprite.bounds.size.x * Screen.height / Screen.width * 0.5f;
        Camera.main.orthographicSize = orthoSize;
    }
}