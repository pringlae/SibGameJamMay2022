using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class puse : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;


    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            ispuse = true;
            Button1.SetActive(true);
            //Button2.SetActive(true);
            //Button3.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            ispuse = false;
            Button1.SetActive(false);
            //Button2.SetActive(false);
            //Button3.SetActive(false);
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;
            Button1.SetActive(true);
            //Button2.SetActive(true);
            // Button3.SetActive(true);

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;
            Button1.SetActive(false);
            //Button2.SetActive(false);
            // Button3.SetActive(false);

        }
    }
 /*   public void OnGUI()
    {
        if (guipuse == true)
        {
            Cursor.visible = true;// включаем отображение курсора
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f), "Продолжить"))
            {
                ispuse = false;
                timer = 0;
                Cursor.visible = false;
            }

            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "В Меню"))
            {
                ispuse = false;
                timer = 0;
                SceneManager.LoadScene(0); // здесь при нажатии на кнопку загружается другая сцена, вы можете изменить название сцены на свое

            }
        }
    }*/

    public void ReplayPressed()
    {
        ispuse = false;
        timer = 0;
        Cursor.visible = false;
        SceneManager.LoadScene(1);
        Button1.SetActive(false);
        //Button2.SetActive(false);
        //Button3.SetActive(false);

    }

    public void GoOnPressed()
    {
        ispuse = false;
        timer = 0;
        Cursor.visible = false;
        Button1.SetActive(false);
        //Button2.SetActive(false);
        //Button3.SetActive(false);
    }

    public void MenuPressed()
    {
        ispuse = false;
        timer = 0;
        SceneManager.LoadScene(0);
        Button1.SetActive(false);
        //Button2.SetActive(false);
        //Button3.SetActive(false);
    }
}
