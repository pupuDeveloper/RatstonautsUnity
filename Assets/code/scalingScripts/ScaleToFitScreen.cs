using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject panel;
    [SerializeField] scaleLayoutItems layoutScaleScript;
    float worldScreenHeight;
    float worldScreenWidth;
    float xBuffer;
    float yBuffer;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.safeArea.y * Screen.safeArea.x;


        //below 2 lines scale things differently, both half wrong and half right, trying to combine their effects
        //transform.localScale = new Vector3((canvas.GetComponent<RectTransform>().rect.width / sr.sprite.bounds.size.x), canvas.GetComponent<RectTransform>().rect.height / sr.sprite.bounds.size.y, 1);
        //transform.localScale = new Vector3((worldScreenWidth / sr.sprite.bounds.size.x), (worldScreenHeight / sr.sprite.bounds.size.y), 1);

        transform.localScale = new Vector3(((canvas.GetComponent<RectTransform>().rect.width) / sr.sprite.bounds.size.x) * 1.1f, ((canvas.GetComponent<RectTransform>().rect.height) / sr.sprite.bounds.size.y) * 1.1f, 1);
        //transform.localScale = new Vector3((worldScreenWidth / sr.sprite.bounds.size.x), (worldScreenHeight / sr.sprite.bounds.size.y), 1);
        layoutScaleScript.Scale(panel.GetComponent<RectTransform>().rect.width, panel.GetComponent<RectTransform>().rect.height - (Mathf.Abs(panel.GetComponent<RectTransform>().sizeDelta.y * canvas.GetComponent<RectTransform>().localScale.y)));
        Debug.Log(Mathf.Abs(panel.GetComponent<RectTransform>().sizeDelta.y));
        Debug.Log(canvas.GetComponent<RectTransform>().localScale.y);
        Debug.Log(panel.GetComponent<RectTransform>().anchoredPosition);
    }

    public float getX()
    {
        return canvas.GetComponent<RectTransform>().rect.width;
    }
    public float getXBuffer()
    {
        return xBuffer;
    }
    public float getY()
    {
        return canvas.GetComponent<RectTransform>().rect.height;
    }

} // class
