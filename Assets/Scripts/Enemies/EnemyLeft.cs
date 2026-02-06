using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class EnemyLeft : MonoBehaviourPunCallbacks
{
    public static EnemyLeft Instance { get; private set; }
    [SerializeField] TextMeshProUGUI enemyLeft;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int enemyCount = 0;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("EnemyCount", out object value)) {
            enemyCount = (int)value;
        }
       
        UpdateUI(enemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI(int amount)
    {

        enemyLeft.text = "Enemy Left: " + amount;
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("EnemyCount"))
        {
            int count = (int)propertiesThatChanged["EnemyCount"];
            
            UpdateUI(count);
        }
    }

}
