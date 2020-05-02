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
        public PlayerMovement player;

        private MarkerDetector markerDetector;
        // Start is called before the first frame update
        void Start()
        {
            markerDetector = new MarkerDetector();
        }

        protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
        {
            var texture = new Texture2D(input.width, input.height);
            texture.SetPixels(input.GetPixels());
            var img = Unity.TextureToMat(texture, Unity.TextureConversionParams.Default);
            ProcessFrame(img, img.Cols, img.Rows);
            output = Unity.MatToTexture(img, output);
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
                    Matrix4x4 transformMatrix = markerDetector.TransfromMatrixForIndex(count);
                    Vector3 position = MatrixHelper.GetPosition(transformMatrix);
                    Quaternion rotation = MatrixHelper.GetQuaternion(transformMatrix);
                    Vector3 scale = MatrixHelper.GetScale(transformMatrix);
                    Debug.Log("Detected" + position.x + " - " + position.y + " - " + position.z);
                    player.Yaw(rotation.eulerAngles.z - 180);
                    player.Pitch(position.z * 360/7 - 150);
                    //player.Pitch()
                }
                count++;
            }
        }
    }
}
