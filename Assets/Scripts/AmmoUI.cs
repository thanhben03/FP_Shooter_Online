using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public static AmmoUI Instance;
    [SerializeField] TextMeshProUGUI ammoText;

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


    public void UpdateUI(int amount)
    {
        ammoText.text = "Ammo: " + amount;
    }
}
