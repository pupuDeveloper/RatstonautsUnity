using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameNotifColor : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    [SerializeField] private Color[] colors;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint;
    public float time;


    private void Start()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Transition();
    }
    private void Transition()
    {
        targetPoint += Time.deltaTime / time;
        spriteRend.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);
        if(targetPoint >= 1f)
        {
            targetPoint = 0f;
            currentColorIndex = targetColorIndex;
            targetColorIndex++;
            if(targetColorIndex == colors.Length)
            {
                targetColorIndex = 0;
            }
        }
    }
}