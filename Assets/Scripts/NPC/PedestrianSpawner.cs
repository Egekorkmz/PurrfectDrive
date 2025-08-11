using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] pedestrianPrefabs;          // birden fazla prefab!

    [Header("Spawn settings")]
    public int  amountToSpawn = 10;
    public float delayFirst   = 0f;
    public float delayBetween = .2f;

    // ------------------------------------------------------------
    void Start () => StartCoroutine(SpawnRoutine());

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(delayFirst);

        int spawned     = 0;
        int waypointCnt = transform.childCount;
        if (waypointCnt == 0 || pedestrianPrefabs.Length == 0) yield break;

        while (spawned < amountToSpawn)
        {
            // --- 1. Rastgele prefab seç
            GameObject prefab = pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)];

            // --- 2. Instantiate
            GameObject go = Instantiate(prefab);

            // --- 3. Rastgele waypoint seç
            Transform wpT = transform.GetChild(Random.Range(0, waypointCnt));
            Waypoint  wp  = wpT.GetComponent<Waypoint>();

            // --- 4. NPCWalker'ı yapılandır
            NPCWalker walker = go.GetComponent<NPCWalker>();
            walker.firstWaypoint = wp;
            //go.transform.position = wpT.position;

            Vector3 spawnPos = wp.GetPosition();                       // şerit içinde rastgele
            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                spawnPos = hit.position;                               // garanti navmesh’te
            go.transform.position = spawnPos;

            spawned++;
            yield return new WaitForSeconds(delayBetween);
        }
    }
}
