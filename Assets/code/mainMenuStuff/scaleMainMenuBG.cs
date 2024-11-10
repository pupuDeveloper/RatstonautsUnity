using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleMainMenuBG : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] GameObject canvas;
    float worldScreenHeight;
    float worldScreenWidth;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.safeArea.y * Screen.safeArea.x;

        transform.localScale = new Vector3(((canvas.GetComponent<RectTransform>().rect.width) / sr.sprite.bounds.size.x) * 1.1f, ((canvas.GetComponent<RectTransform>().rect.height) / sr.sprite.bounds.size.y) * 1.1f, 1);
    }
}
