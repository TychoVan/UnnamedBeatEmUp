using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Slider[] sliderElement;




        private void Start()
        {
            for (int i = 0; i < sliderElement.Length; i++) {
                Slider slider = sliderElement[i];

                slider.Bar = slider.Element.transform.Find("Bar").GetComponent<Image>();
                slider.Bar.fillAmount = 0;
            }
        }


        public void ChangeFill(int sliderNr, float amount)
        {
            Slider slider       = sliderElement[sliderNr];


            slider.LerpingBar = false;
            if (!slider.LerpingBar) StartCoroutine(LerpSlider(slider));
        }


        // Smoothly change the element fill to the right value.
        private IEnumerator LerpSlider(Slider slider)
        {
            float timer       = 0;

            // Get the fill information.
            float startFill   = slider.Bar.fillAmount;
            float targetFill  = (slider.FillValues.UIValue    - slider.FillValues.UIMinValue) / 
                                (slider.FillValues.UIMaxValue - slider.FillValues.UIMinValue);
            float currentFill = startFill;

            slider.LerpingBar = true;

            while (timer < 1 && slider.LerpingBar)
            {
                timer                 += Time.deltaTime * slider.SlideSpeed;
                currentFill            = Mathf.Lerp(startFill, targetFill, timer);
                slider.Bar.fillAmount  = currentFill;
            }

            slider.LerpingBar = false;

            yield return null;
        }
    }
}
