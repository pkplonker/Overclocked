using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxtest : MonoBehaviour
{
    [SerializeField] GameObject spark;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject boom;
    [SerializeField] GameObject sparkle;
    [SerializeField] GameObject firework;

    AudioSource source;
    [SerializeField] AudioClip sparkleClip;
    [SerializeField] AudioClip boomClip;
    [SerializeField] AudioSource welder;
    [SerializeField] AudioClip fireworkClip;

    private void Start()
    {
        spark.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            StartCoroutine(Spark());
            welder.Play();
        }
        if (Input.GetKeyDown("2"))
        {
            Instantiate(sparkle, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
            source.PlayOneShot(sparkleClip);
        }
        if (Input.GetKeyDown("3"))
        {
            Instantiate(boom, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
            Instantiate(smoke, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
            source.PlayOneShot(boomClip);
        }
        if (Input.GetKeyDown("4"))
        {
            Instantiate(firework, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
            source.PlayOneShot(fireworkClip);
        }
    }

    IEnumerator Spark()
    {
        spark.SetActive(true);
        yield return new WaitForSeconds(5f);
        spark.SetActive(false);
        welder.Stop();
    }
}
