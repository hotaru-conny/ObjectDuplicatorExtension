using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Rigidbody playerRigidbody;
    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
