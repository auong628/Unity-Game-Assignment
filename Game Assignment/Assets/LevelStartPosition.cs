using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPosition : MonoBehaviour
{
    public string spawnPointName = "Level2StartPoint";
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawnPoint = GameObject.Find(spawnPointName);
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
}
