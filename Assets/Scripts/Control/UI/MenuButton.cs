using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    Button menuButton;

	void Awake () {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(LoadMenu);
	}

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
