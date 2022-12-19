using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    [System.Serializable]
    public class SliderData
    {
        public string       Name;

        public  float       SlideSpeed              = 2;
        public bool         LerpingBar              = false;

        [field: SerializeField] public Slider Slider { get; private set; }

        [SerializeInterface(typeof(I_ScoreValue))]
        [SerializeField] private Object _FillValues;

        public I_ScoreValue FillValues => _FillValues as I_ScoreValue;
    }
}
    