using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float deltaMove = 20, minX, maxX;
    [SerializeField] ParticleSystem deatheffect;
    public static event System.Action Minused = () => { };
    public static event System.Action Plused = () => { };

    private void Awake()
    {
        Minused = Plused = null;
    }

    private void Start()
    {
        Score.ScoreLowerThanZero += OnScoreLowerThanZero;
    }

    void OnScoreLowerThanZero()
    {
        Instantiate(deatheffect, transform.position, deatheffect.transform.rotation);
        Destroy(gameObject);
    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Right"))
        {
            TryMove(deltaMove);
        }
        else if (Input.GetButtonDown("Left"))
        {
            TryMove(-deltaMove);
        }
    }

    void TryMove(float offset)
    {
        var pos = transform.position;
        var x = pos.x;
        x += offset;
        if (x >= minX && x <= maxX)
        {
            pos.x = x;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minus"))
        {
            Minused();
        }
        else if (other.CompareTag("Plus"))
        {
            Plused();
        }
    }
}
