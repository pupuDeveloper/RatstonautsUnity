using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] GameObject canvas;
    float worldScreenHeight;
    float worldScreenWidth;
    float xBuffer;
    float yBuffer;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        //below 2 lines scale things differently, both half wrong and half right, trying to combine their effects
        //transform.localScale = new Vector3((canvas.GetComponent<RectTransform>().rect.width / sr.sprite.bounds.size.x), canvas.GetComponent<RectTransform>().rect.height / sr.sprite.bounds.size.y, 1);
        //transform.localScale = new Vector3((worldScreenWidth / sr.sprite.bounds.size.x), (worldScreenHeight / sr.sprite.bounds.size.y), 1);

        xBuffer = canvas.GetComponent<RectTransform>().rect.width * 0.02f;
        yBuffer = canvas.GetComponent<RectTransform>().rect.height * 0.02f;
        Debug.Log(xBuffer +" " +yBuffer);

        transform.localScale = new Vector3(((canvas.GetComponent<RectTransform>().rect.width + xBuffer) / sr.sprite.bounds.size.x), (canvas.GetComponent<RectTransform>().rect.height) / sr.sprite.bounds.size.y, 1);
        //transform.localScale = new Vector3((worldScreenWidth / sr.sprite.bounds.size.x), (worldScreenHeight / sr.sprite.bounds.size.y), 1);
        this.gameObject.GetComponentInParent<scaleLayoutItems>().Scale(canvas.GetComponent<RectTransform>().rect.width + xBuffer, canvas.GetComponent<RectTransform>().rect.height);

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
