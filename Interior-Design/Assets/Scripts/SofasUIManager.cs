using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofasUIManager : MonoBehaviour
{
    public GameObject furnitureUIMenu;
    public GameObject uiMenu;
    public GameObject spawnPoint;
    public GameObject[] sofasPrefabs = new GameObject[5];
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        uiMenu.SetActive(false);
        spawnPoint = GameObject.Find("FurnitureSpawnPoint");
    }

    private void Update()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        transform.position = hand.transform.position + offset;
        transform.localRotation = hand.transform.localRotation * Quaternion.Euler(180, 0, 0) * Quaternion.Euler(0, 180, 0);
    }

    public void LeaveWindow()
    {
        uiMenu.SetActive(false);
        furnitureUIMenu.SetActive(true);
    }

    public void SpawnSofa1()
    {
        Instantiate(sofasPrefabs[0], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = false;
    }

    public void SpawnSofa2()
    {
        Instantiate(sofasPrefabs[1], new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z), Quaternion.identity);
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = false;
    }

    public void SpawnSofa3()
    {
        Instantiate(sofasPrefabs[2], new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z), Quaternion.identity);
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = false;
    }

    public void SpawnSofa4()
    {
        Instantiate(sofasPrefabs[3], new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z), Quaternion.identity);
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = false;
    }

    public void SpawnSofa5()
    {
        Instantiate(sofasPrefabs[4], new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z), Quaternion.identity);
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = false;
    }
}
