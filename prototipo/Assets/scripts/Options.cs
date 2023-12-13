using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    [Header("Volumen")]
    
    public Slider slider;
    public float sliderValue;
    public Image horn;
    public Sprite[] imageSound;

    [Header("Resolucion")]
    public Toggle toggle;
    //
    public TMP_Dropdown resolucionesDropDown;
    public TMP_FontAsset nuevaFuente;
    private List<Resolution> resolucionesPersonalizadas = new List<Resolution>();
    Resolution[] resoluciones;
    public TMP_Text aa;

    // Start is called before the first frame update
    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        IMute();
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        RevisarResolucion();
    }
    public void ActiveFULLS(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    void AgregarResolucionPersonalizada(int width, int height)
    {
        Resolution resolucion = new Resolution();
        resolucion.width = width;
        resolucion.height = height;
        resolucionesPersonalizadas.Add(resolucion);
    }
    public void RevisarResolucion()
    {
        Resolution[] a = Screen.resolutions;
        foreach (Resolution b in a)
        {
            if (!ResolucionEstaEnLista(b))
            {
                AgregarResolucionPersonalizada(b.width,b.height);
            }
        }
        resoluciones = resolucionesPersonalizadas.ToArray();
        Debug.Log(resoluciones.Length);
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;

                opciones.Add(opcion);



            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }

        }

        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();
        TMP_Text[] textosDropdown = resolucionesDropDown.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text texto in textosDropdown)
        {
            texto.font = nuevaFuente;
        }
        //
        resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
        //
    }
    bool ResolucionEstaEnLista(Resolution resolucion)
    {
        foreach (Resolution resolucionPersonalizada in resolucionesPersonalizadas)
        {
            if (resolucion.width == resolucionPersonalizada.width &&
                resolucion.height == resolucionPersonalizada.height)
            {
                return true;
            }
        }
        return false;
    }
    public void CambiarResolucion(int indiceResolucion)
    {
        //
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDropDown.value);
        //


        Resolution resolution = resoluciones[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    //

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        IMute();
    }
    public void IMute()
    {
        if (slider.value==1)
        {
            horn.sprite=imageSound[3];
        }
        else if(slider.value>=0.5f)
        {
            horn.sprite = imageSound[2];
        }
        else if(slider.value >= 0.1f)
        {
            horn.sprite = imageSound[1];
        }else if(slider.value == 0)
        {
            horn.sprite = imageSound[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        aa.text = ""+resolucionesPersonalizadas.Count;
    }
}
