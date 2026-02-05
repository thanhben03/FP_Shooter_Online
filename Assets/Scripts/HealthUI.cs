using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Transform healthContainer;
    [SerializeField] Transform healthTemplate;
    public static HealthUI Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIHealth(int amount)
    {
        foreach (Transform child in healthContainer.transform)
        {
            if (child == healthTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < amount; i++)
        {
            Transform iconTransform = Instantiate(healthTemplate, healthContainer.transform);
            iconTransform.gameObject.SetActive(true);
        }
    }
}
