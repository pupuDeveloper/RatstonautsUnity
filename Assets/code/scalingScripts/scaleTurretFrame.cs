using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleTurretFrame : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] GameObject canvas;
    float worldScreenHeight;
    float worldScreenWidth;
    [SerializeField] private GameObject turret;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.safeArea.y * Screen.safeArea.x;
        transform.localScale = new Vector3(((canvas.GetComponent<RectTransform>().rect.width) / sr.sprite.bounds.size.x) * 1.05f, ((canvas.GetComponent<RectTransform>().rect.height) / sr.sprite.bounds.size.y) * 1.05f, 1);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        //turret.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, canvas.GetComponent<RectTransform>().rect.height * 0.15f);
    }
}
