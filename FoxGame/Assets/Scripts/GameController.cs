using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingPrefabs;
    private GameObject objInHand = null;
    int choiceIndex;

    void Start()
    {
        choiceIndex = 0;

        if (buildingPrefabs.Length > 0)
        {
            Debug.Log("Build: " + buildingPrefabs[choiceIndex].name);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            choiceIndex = (choiceIndex + 1) % buildingPrefabs.Length;
            Debug.Log("Build: " + buildingPrefabs[choiceIndex].name);

            if (objInHand != null)
            {
                Destroy(objInHand);
                objInHand = Instantiate(buildingPrefabs[choiceIndex], transform.position, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !objInHand)
        {
            objInHand = Instantiate(buildingPrefabs[choiceIndex], transform.position, Quaternion.identity);
            //objInHand = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            objInHand = null;
        }
    }
}
