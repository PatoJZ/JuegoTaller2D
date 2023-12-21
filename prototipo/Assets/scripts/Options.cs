using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public GameObject panel;

    [Header("Cambio de Escena")]
    public string nameSceneOptions;
    public string nameSceneNivel;
    public Vector3 scale;
    [Header("Sonidos")]

    public AudioClip click;
    
    [Header("Volumen")]
    
    public Slider slider;
    public float sliderValue;
    public Image horn;
    public Sprite[] imageSound;

    [Header("Resolucion")]
    
    public TMP_Dropdown resolucionesDropDown;
    public TMP_FontAsset nuevaFuente;
    private List<Resolution> resolucionesPersonalizadas = new List<Resolution>();
    private List<Resolution> resolucionesPersonalizadasADetalle = new List<Resolution>();
    Resolution[] resoluciones;
    

    // Start is called before the first frame update
    private void Awake()
    {
        Screen.fullScreen = true;
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        AudioListener.volume = slider.value;
        IMute();
        AgregarResolucionPersonalizadaADetalle(640, 480);
        AgregarResolucionPersonalizadaADetalle(720, 480);
        AgregarResolucionPersonalizadaADetalle(720, 576);
        AgregarResolucionPersonalizadaADetalle(1024, 768);
        AgregarResolucionPersonalizadaADetalle(1366, 768);
        AgregarResolucionPersonalizadaADetalle(1600, 900);
        AgregarResolucionPersonalizadaADetalle(1920, 1080);
        AgregarResolucionPersonalizadaADetalle(1920, 1200);
        AgregarResolucionPersonalizadaADetalle(2560, 1440);

        RevisarResolucion();

    }
    void AgregarResolucionPersonalizadaADetalle(int width, int height)
    {
        Resolution resolucion = new Resolution();
        resolucion.width = width;
        resolucion.height = height;
        resolucionesPersonalizadasADetalle.Add(resolucion);
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
            if (!ResolucionEstaEnLista(b)&& ResolucionPredeterminadas(b))
            {
                AgregarResolucionPersonalizada(b.width,b.height);
            }
        }
        resoluciones = resolucionesPersonalizadas.ToArray();
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            
            opciones.Add(opcion);



            if ((Screen.fullScreen && resoluciones[i].width == 1920 && resoluciones[i].height == 1080)|| 
                (!Screen.fullScreen && resoluciones[i].width == 1920 && resoluciones[i].height == 1080))
            {
                resolucionActual = i;
                Debug.Log(opcion);
                //Debug.Log(opcion);
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
        //resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
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
    bool ResolucionPredeterminadas(Resolution resolucion)
    {
        foreach (Resolution resolucionPersonalizada in resolucionesPersonalizadasADetalle)
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
        ControllerSound.instance.ExecuteSound(click);
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
    public void ActiveOptions()
    {
        ControllerSound.instance.ExecuteSound(click);
        panel.SetActive(true);
        panel.gameObject.GetComponent<Image>().color = Color.white;
        scale = new Vector3(3, 3, 1);
        panel.gameObject.GetComponent<RectTransform>().localScale = scale;
    }
    public void ActiveOptionsInicio()
    {
        ControllerSound.instance.ExecuteSound(click);
        panel.SetActive(true);
        panel.gameObject.GetComponent<Image>().color = Color.black;
        scale = new Vector3(6, 6, 1);
        panel.gameObject.GetComponent<RectTransform>().localScale = scale;
    }
    public void DesactiveOptions()
    {
        ControllerSound.instance.ExecuteSound(click);
        panel.SetActive(false);
    }
    public void DesactiveOptionsInicio()
    {
        panel.gameObject.GetComponent<Image>().color = Color.white;
        scale = new Vector3(3, 3, 1);
        panel.gameObject.GetComponent<RectTransform>().localScale = scale;
        panel.SetActive(false);
    }


    // Update is called once per frame
    private void Update()
    {
        
    }
}
