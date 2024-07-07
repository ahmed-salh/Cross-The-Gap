using Craft2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriveButton : MonoBehaviour
{

    [SerializeField] Button leftButton, rightButton;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        //leftButton.onClick.AddListener(() => { levelManager.Left(); });
        rightButton.onClick.AddListener(() => { levelManager.ActivateControl(); });
    }
}
