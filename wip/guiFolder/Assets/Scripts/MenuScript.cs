using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void SwitchScene(string name)
    {
        Application.LoadLevel(name);
    }
}