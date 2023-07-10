using Pathfinding;
using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    // The players that want to spawn
    public GameObject RedPlayer;
    public GameObject BluePlayer;

    public Transform players;


   

    

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Plane"))
                    {
                        Vector3 spawnPosition = hit.point;
                        GameObject player = Instantiate(BluePlayer, spawnPosition, Quaternion.identity);
                        player.transform.SetParent(players);
                    }
                }
        }
        if (Input.GetMouseButtonDown(1)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.tag + "hi");
                if (hit.collider.gameObject.CompareTag("Plane"))
                {
                    Debug.Log(hit.collider.gameObject.tag+"hi");
                    Vector3 spawnPosition = hit.point;
                    GameObject player= Instantiate(RedPlayer, spawnPosition, Quaternion.identity);
                    player.transform.SetParent(players);
                }
            }
        }
    }
}
