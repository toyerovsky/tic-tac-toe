using UnityEngine;

namespace Assets.Scripts
{
    public class SkyboxRotationScript : MonoBehaviour
    {
        public float RotationSpeed;
        // Update is called once per frame
        void Update()
        {
            GetComponent<Skybox>().material.SetFloat("_Rotation", Time.time * RotationSpeed);
        }
    }
}
