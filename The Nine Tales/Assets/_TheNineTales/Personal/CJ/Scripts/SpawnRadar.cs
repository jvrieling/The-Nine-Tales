using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRadar : MonoBehaviour
{
    [SerializeField] private GameObject scanner;
    public bool scannerDeployed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !scannerDeployed) {
            Debug.Log("input received");
            Instantiate(scanner, this.transform.position, Quaternion.identity);
        }
    }

    void RefreshAbility() {
        scannerDeployed = false;
    }
}
