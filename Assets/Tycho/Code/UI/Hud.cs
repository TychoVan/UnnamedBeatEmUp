using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private SliderData[] sliderElements;




        public void ChangeSliderFill(int sliderNr)
        {
            SliderData sliderData       = sliderElements[sliderNr];
            if (!sliderData.Slider.Data.LerpingBar) {
                 sliderData.Slider.Data = sliderData;

                StartCoroutine(sliderData.Slider.LerpSlider());
            }
        }


    }
}
