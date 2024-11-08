using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    //public SpriteRenderer exampleBGSprite;
    [SerializeField] private GameObject canvas;
    void Awake()
    {
        float orthoSize = Screen.height / 2;
        Camera.main.orthographicSize = orthoSize;
    }
}