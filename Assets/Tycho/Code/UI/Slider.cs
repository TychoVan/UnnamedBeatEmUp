using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    [System.Serializable]
    public class Slider
    {
        public  float       SlideSpeed              = 2;
        public bool         LerpingBar              = false;

        public GameObject   Element;
        public Image        Bar;

        [SerializeInterface(typeof(I_ScoreValue))]
        [SerializeField]
        private Object _FillValues;

        public I_ScoreValue FillValues => _FillValues as I_ScoreValue;
    }
}
    