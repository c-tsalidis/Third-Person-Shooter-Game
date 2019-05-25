using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class GradientColor : MonoBehaviour
    {
        #region Private non-serializefield variables
        private float i = 0; // counter to control the lerp
        #endregion

        #region Private serializeField variables
        [SerializeField]
        private Material skybox;
        [SerializeField]
        private Color startingColor;
        [SerializeField]
        private Color endingColor;
        [SerializeField]
        private float rate = 10; // number of times per second the new color is chosen 
        [SerializeField]
        private Color [] colors;
        #endregion

        #region Public variables

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            startingColor = new Color(Random.value, Random.value, Random.value);
            endingColor = new Color(Random.value, Random.value, Random.value);
        }

        // Update is called once per frame
        void Update()
        {
            Color newColor;
            i += Time.deltaTime * rate;
            // int randomIndex = Random.Range(0, colors.Length);


            // newColor = Color.Lerp(startingColor, endingColor, Mathf.PingPong(Time.time, 1));
            newColor = Color.Lerp(startingColor, endingColor, i);

            // changing the skybox color
            if (RenderSettings.skybox.HasProperty("_Tint"))
            {
                RenderSettings.skybox.SetColor("_Tint", newColor);
                DynamicGI.UpdateEnvironment();
            }
            else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            {
                RenderSettings.skybox.SetColor("_SkyTint", newColor);
                DynamicGI.UpdateEnvironment();
            }
            else if (RenderSettings.skybox.HasProperty("_Ground"))
            {
                RenderSettings.skybox.SetColor("_Ground", newColor);
                DynamicGI.UpdateEnvironment();
            }


            if(i >= 1)
            {
                i = 0;
                // startingColor = GetComponent<Renderer>().material.color;
                startingColor = new Color(Random.value, Random.value, Random.value);
                endingColor = new Color(Random.value, Random.value, Random.value);
                /*
                Color aux = startingColor;
                aux = endingColor;
                endingColor = startingColor;
                startingColor = aux;
                 */
            }

        }

        #endregion

        #region Custom methods

        #endregion
    }
}
