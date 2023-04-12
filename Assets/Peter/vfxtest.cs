using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxtest : MonoBehaviour
{
    [SerializeField] GameObject spark;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject boom;
    [SerializeField] GameObject sparkle;

    private void Start()
    {
        spark.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            StartCoroutine(Spark());
        }
        if (Input.GetKeyDown("2"))
        {
            Instantiate(sparkle, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
        }
        if (Input.GetKeyDown("3"))
        {
            Instantiate(boom, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
            Instantiate(smoke, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
        }
    }

    IEnumerator Spark()
    {
        spark.SetActive(true);
        yield return new WaitForSeconds(5f);
        spark.SetActive(false);
    }
}
