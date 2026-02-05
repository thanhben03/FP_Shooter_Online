using UnityEngine;

public class ZoomInUI : MonoBehaviour
{
    public static ZoomInUI Instance;
    public GameObject zoomInImage;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUI(bool status)
    {
        zoomInImage.SetActive(status);
    }
}
