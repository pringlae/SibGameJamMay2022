using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneAnimalsCheck : MonoBehaviour
{
    public GameObject fox, littleFox;
    public GameObject hare;
    public GameObject beaver;
    public GameObject squirrel;
    public GameObject hunter;


    void Start()
    {
        fox.gameObject.SetActive(Rescued("Fox"));
        littleFox.gameObject.SetActive(Rescued("Fox"));
        hare.gameObject.SetActive(Rescued("Hare"));
        beaver.gameObject.SetActive(Rescued("Beaver"));
        squirrel.gameObject.SetActive(Rescued("Squirrel"));
        hunter.gameObject.SetActive(Rescued("Hunter"));
    }

    private bool Rescued(string name)
    {
        return Animal.rescued.ContainsKey(name) && Animal.rescued[name];
    }
}
