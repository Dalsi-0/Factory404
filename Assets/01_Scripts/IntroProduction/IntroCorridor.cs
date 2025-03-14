using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCorridor : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private Transform spawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("IntroAgent"))
        {
            other.GetComponent<IntroAgent>().PassivityUpdateNavmesh();

            StartCoroutine(WaitOneFrame(other));
        }
    }

    IEnumerator WaitOneFrame(Collider other)
    {
        yield return null;

        IntroCorridorSpawner.Instance.SpawnerNewCorridors(spawnPoint);
        IntroCorridorSpawner.Instance.SetCurrentCorridor(gameObject);
        other.GetComponent<IntroAgent>().SetNewDestination(destination.position);
    }

}
