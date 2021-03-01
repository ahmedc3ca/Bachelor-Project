using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ObjectSpawning : NetworkBehaviour 
{

    [SerializeField] List<GameObject> leftWords = new List<GameObject>();
    [SerializeField] List<GameObject> rightWords = new List<GameObject>();
    [SerializeField] GameObject leftSpawnGroup;
    [SerializeField] GameObject rightSpawnGroup;

   
    // Executed when server is created
    public override void OnStartServer(){
        base.OnStartServer();
        if(isServer){
            SpawnAllTokens(); // Spawn all tokens on shelfs
        }
    } 

    void Update(){
        // Spawn is only managed by host
        if(isServer){
            int i = 0;

            bool noSpawn = false;

            // Check on left shelf if an token is missing
            // If yes, spawn a new one
            foreach (Transform spawnPoint in leftSpawnGroup.transform)
            {
                // Check if token already in place
                Collider[] hitColliders = Physics.OverlapSphere(spawnPoint.position, 0.2f);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.transform.tag == "Token"){
                        noSpawn = true;
                    }     
                }
                // If no token, spawn a new one
                if (!noSpawn){
                        SpawnGivenObject(leftWords[i], spawnPoint);
                }
                noSpawn = false;
                i++;
            }

            i = 0;

            // Check on right shelf if an token is missing
            // If yes, spawn a new one 
            foreach (Transform spawnPoint in rightSpawnGroup.transform)
            {
                // Check if token already in place
                Collider[] hitColliders = Physics.OverlapSphere(spawnPoint.position, 0.2f);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.transform.tag == "Token"){
                        noSpawn = true;
                    }     
                }
                // If no token, spawn a new one
                if (!noSpawn){
                        SpawnGivenObject(rightWords[i], spawnPoint);
                }
                noSpawn = false;
                i++;
            }
        }
    }

    // Spawn all tokens on shelfs
    void SpawnAllTokens(){
        int i = 0;

        foreach (Transform spawnPoint in leftSpawnGroup.transform)
        {
            SpawnGivenObject(leftWords[i], spawnPoint);
            i++;
        }

        i = 0;

        foreach (Transform spawnPoint in rightSpawnGroup.transform)
        {
            SpawnGivenObject(rightWords[i], spawnPoint);
            i++;
        }
    }


    // Spawn a prefab given its index and spawnPoint
    void SpawnGivenObject(GameObject obj, Transform point){
        GameObject go = GameObject.Instantiate(obj, point.position, point.rotation) as GameObject;
        NetworkServer.Spawn(go); 
    }
}
