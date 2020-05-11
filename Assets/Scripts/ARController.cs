namespace PaperPlaneTools.AR
{
    using OpenCvSharp;

    using UnityEngine;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System;
    using System.Collections.Generic;
    using UnityEngine.UI;

    public class ARController : WebCamera
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


        public PlayerMovement player;

        private MarkerDetector markerDetector;
        // Start is called before the first frame update
        void Start()
        {
            /*Eliminar
            positionMax = new Vector3(0, 0, 0);
            positionMin = new Vector3(0, 0, 0);
            rotationMax = new Vector3(0, 0, 0);
            rotationMin = new Vector3(0, 0, 0);
            Fin eliminar*/
            markerDetector = new MarkerDetector();
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
                if (id == 5)
                {
                    Debug.Log("MaxMinPosition: " + positionMax.ToString() + " / " + positionMin.ToString());
                    Debug.Log("MaxMinRotation: " + rotationMax.ToString() + " / " + rotationMin.ToString());
                }
                if (id == 23)
                {                    
                    Matrix4x4 transformMatrix = markerDetector.TransfromMatrixForIndex(count);
                    Vector3 position = MatrixHelper.GetPosition(transformMatrix);
                    Quaternion rotation = MatrixHelper.GetQuaternion(transformMatrix);
                    Vector3 scale = MatrixHelper.GetScale(transformMatrix);
                    //Debug.Log("Position: " + position.x + ", " + position.y + ", " + position.z);
                    //ebug.Log("Rotation: " + rotation.eulerAngles.x + ", " + rotation.eulerAngles.y + ", " + rotation.eulerAngles.z);
                    /*Eliminar: sacado de valores máximos
                    if (position.x > positionMax.x) positionMax.x = position.x;
                    if (position.y > positionMax.y) positionMax.y = position.y;
                    if (position.z > positionMax.z) positionMax.z = position.z;
                    if (position.x < positionMin.x) positionMin.x = position.x;
                    if (position.y < positionMin.y) positionMin.y = position.y;
                    if (position.z < positionMin.z) positionMin.z = position.z;
                    if (rotation.eulerAngles.x > rotationMax.x) rotationMax.x = rotation.eulerAngles.x;
                    if (rotation.eulerAngles.y > rotationMax.y) rotationMax.y = rotation.eulerAngles.y;
                    if (rotation.eulerAngles.z > rotationMax.z) rotationMax.z = rotation.eulerAngles.z;
                    if (rotation.eulerAngles.x < rotationMin.x) rotationMin.x = rotation.eulerAngles.x;
                    if (rotation.eulerAngles.y > rotationMax.y) rotationMax.y = rotation.eulerAngles.y;
                    if (rotation.eulerAngles.z > rotationMax.z) rotationMax.z = rotation.eulerAngles.z;
                    Fin eliminar*/
                    player.Yaw(rotation.eulerAngles.z - 180);
                    player.Pitch(position.z * 360/pitchFactor - 180);
                    //player.Pitch()
                }
                count++;
            }
        }
    }
}
