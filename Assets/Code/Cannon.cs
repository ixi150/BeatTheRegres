using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] AudioEqualizer equalizer;
    [SerializeField] int bandIndex;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] float bulletLifetime = 10;
    [SerializeField] float minInterval = 0.1f;

    float lastTime;
    static int bulletsFired;

    private void Start()
    {
        equalizer.BandPeeked[bandIndex].AddListener(OnBandPeek);
    }

    void OnBandPeek()
    {
        if (Time.time - lastTime < minInterval)
        {
            return;
        }

        var prefab = bulletsFired == 0 ? coinPrefab : bulletPrefab;
        var bullet = Instantiate(prefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, bulletLifetime);
        lastTime = Time.time;

        bulletsFired = (bulletsFired + 1) % 3;
    }
}
