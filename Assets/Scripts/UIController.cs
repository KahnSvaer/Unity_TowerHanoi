using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static int Rings;
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text sliderVal;

    public void StartGame() 
    {
        SceneManager.LoadScene(1);
        Rings=5;
        sliderVal.SetText("5");
    }

    private void Update() {
        Rings = (int)slider.GetComponent <Slider> ().value;
        sliderVal.SetText(Rings.ToString());
    }
    public static int getRings()
    {
        return Rings;
    }
}
