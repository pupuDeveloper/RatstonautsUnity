using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] GameObject canvas;
    [SerializeField] scaleLayoutItems layoutScaleScript;
    [SerializeField] GameObject topBar;
    [SerializeField] GameObject bottomBar;
    float worldScreenHeight;
    float worldScreenWidth;
    float xBuffer;
    float yBuffer;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.safeArea.y * Screen.safeArea.x;

        transform.localScale = new Vector3(((canvas.GetComponent<RectTransform>().rect.width) / sr.sprite.bounds.size.x) * 1.1f, ((canvas.GetComponent<RectTransform>().rect.height) / sr.sprite.bounds.size.y) * 1.1f, 1);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        float height = canvas.GetComponent<RectTransform>().rect.height - Mathf.Abs(bottomBar.GetComponent<RectTransform>().rect.height) - Mathf.Abs(topBar.GetComponent<RectTransform>().rect.height);
        layoutScaleScript.Scale(canvas.GetComponent<RectTransform>().rect.width, height);
    }
    public float getX()
    {
        return canvas.GetComponent<RectTransform>().rect.width;
    }
    public float getXBuffer()
    {
        return xBuffer;
    }
    public float getYBuffer()
    {
        return topBar.GetComponent<RectTransform>().rect.height;
    }
    public float getY()
    {
        return canvas.GetComponent<RectTransform>().rect.height;
    }
}
