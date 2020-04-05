using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneNumber : MonoBehaviour
{
    void Start()
    {
        var level = SceneManager.GetActiveScene().buildIndex + 1;
        GetComponent<Text>().text = level + ".";
    }
}
