using UnityEngine;

public class SelectColorUI : MonoBehaviour
{
    [SerializeField] Transform listColorContainer;
    [SerializeField] Transform itemTemplate;
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        foreach (Transform t in listColorContainer)
        {
            t.gameObject.SetActive(false);
        }

        for (int i = 0; i < GameManager.Instance.Colors.Length;i++)
        {
            SelectSingleColor item = Instantiate(itemTemplate.gameObject, listColorContainer).GetComponent<SelectSingleColor>();
            item.SetData(GameManager.Instance.Colors[i], i);
            item.gameObject.SetActive(true);
        }
    }
}
