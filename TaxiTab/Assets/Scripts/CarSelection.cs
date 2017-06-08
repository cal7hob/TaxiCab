using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinHead_Developer
{
    public class CarSelection : MonoBehaviour
    {

        // Use this for initialization
        public GameObject[] Cars;
        int movement = 0;
        public Camera Cam;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Cam.gameObject.transform.position = Vector3.MoveTowards(Cam.transform.position, Cars[movement].transform.position, Time.deltaTime * 20f);
        }

        public void LoadLevel()
        {
            GameManager.Instance.SelectedCar = movement;
            GameManager.Instance.Play(3);
        }

        //Activates iTS Manager and iTS Spawner at Runtime
        public void LoadTraffic()
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Traffic"))
            {
                item.SetActive(true);
                Debug.Log(item.name + "   " + item.activeInHierarchy + "    " + item.activeSelf);
            }
        }
        public void NextCar()
        {

            if (movement < Cars.Length - 1)
            {
                movement++;
            }

        }
        public void PreviousCar()
        {
            if (movement > 0)
            {
                movement--;
            }
        }
    }
}
