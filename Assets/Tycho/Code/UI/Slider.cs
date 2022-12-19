using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class Slider : MonoBehaviour
    {
        public  SliderData Data;
        private Image      Bar;


        public void Start()
        {
            Bar = this.gameObject.transform.Find("Bar").GetComponent<Image>();
        }

        // Smoothly change the element fill to the right value.
        public IEnumerator LerpSlider()
        {
            Data.LerpingBar = false;

            float timer       = 0;

            // Get the fill information.
            float startFill   =  Bar.fillAmount;
            float targetFill  = (Data.FillValues.UIValue    - Data.FillValues.UIMinValue) /
                                (Data.FillValues.UIMaxValue - Data.FillValues.UIMinValue);
            float currentFill = startFill;

            Data.LerpingBar = true;

            while (timer < 1 && Data.LerpingBar)
            {
                timer        += Time.deltaTime * Data.SlideSpeed;
                currentFill   = Mathf.Lerp(startFill, targetFill, timer);
                Bar.fillAmount = currentFill;

                yield return new WaitForEndOfFrame();
            }

            Data.LerpingBar = false;

            yield return null;
        }
    }
}
