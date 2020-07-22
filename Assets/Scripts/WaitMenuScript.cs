namespace PaperPlaneTools.AR
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Video;
    using OpenCvSharp;
    using System.Runtime.InteropServices;
    using System;
    using UnityEngine.UI;

    public class WaitMenuScript : WebCamera
    {
        //Para obtener información acerca de los valores máximos del tracking de AR
        private Vector3 positionMax;
        private Vector3 positionMin;
        private Vector3 rotationMax;
        private Vector3 rotationMin;
        /* Una prueba de los valores de AR para un rango de una mano y el uso de la webcam ha sido:
         * (3.3, 1.1, 13.4) - (-3.5, -1.5, 0.0)
         * (360.0, 253.4, 359.1) - (0.0, 0.0, 0.0)
         * 
         */
        //Propuesta 10 
        public int pitchFactor;
        //timer
        public float tiempo_start;
        public float tiempo_end;

        public VideoPlayer videoPlayer;
        public int idMarker;

        private MarkerDetector markerDetector;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        // Start is called before the first frame update
        void Start()
        {
            markerDetector = new MarkerDetector();
        }

        void Update()
        {

            if (idMarker == 23)
            {
                tiempo_start += Time.deltaTime;
                tiempo_end = tiempo_start + 7;

                if (tiempo_start >= tiempo_end)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
                }
            }
            
        }
        protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
        {
            var texture = new Texture2D(input.width, input.height);
            texture.SetPixels(input.GetPixels());
            var img = Unity.TextureToMat(texture, Unity.TextureConversionParams.Default);
            ProcessFrame(img, img.Cols, img.Rows);
            output = Unity.MatToTexture(img, output);
            UnityEngine.Object.Destroy(texture);
            return true;
        }

        private void ProcessFrame(Mat mat, int width, int height)
        {
            List<int> markerIds = markerDetector.Detect(mat, width, height);
            int count = 0;
            foreach (int id in markerIds)
            {

                if (id == 23)
                {
                    idMarker = 23;
                    videoPlayer.Play();
                }
                count++;
            }
        }
    }
}
