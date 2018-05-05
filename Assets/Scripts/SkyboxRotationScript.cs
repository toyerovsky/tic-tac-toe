using UnityEngine;

namespace Assets.Scripts
{
    public class SkyboxRotationScript : MonoBehaviour
    {
        public float RotationSpeed;

        private void Update()
        {
            GetComponent<Skybox>().material.SetFloat("_Rotation", Time.time * RotationSpeed);
        }
    }
}
