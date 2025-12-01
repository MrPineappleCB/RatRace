using UnityEngine;
using UnityEngine.UI;

public class RickySlider : MonoBehaviour
{
    public GameObject player;
    public GameObject end;
    public Slider slider;
    bool firstcheck = false;
    float fulldist;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstcheck == false)
        {
            fulldist = end.transform.position.x - player.transform.position.x;
            firstcheck = true;
        }
        
        float distance = end.transform.position.x - player.transform.position.x;
        float slidervalue = 100 - ((distance/fulldist) * 100);
        slider.value = slidervalue;

    }
}
