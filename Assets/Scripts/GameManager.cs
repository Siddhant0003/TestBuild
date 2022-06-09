using CasualGame.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CasualGame.Manager
{
    
    public class GameManager : MonoBehaviour , IGameManager
    {

        public static GameManager instance = null;
        [Header("game_screen")]
        [SerializeField] GameObject play_InitialScreen;
        public Text score;
        [Header("game_overscreen")]
        public GameObject gameOverScreen;
        [Header("game_character")]
        public GameObject pulpit;
        [SerializeField] GameObject doofus;
        public IObjectPooling objectPooling;
        public Transform generatingPoint;
        public GameObject lastActiveObject;
        public GameObject character;
        public int randomDirection;
        public int lastRandomDirection;

        private void Awake()
        {
            character.GetComponent<Rigidbody>().useGravity = false;
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            play_InitialScreen.SetActive(true);       
            score.enabled = false;
            objectPooling = GetComponent<IObjectPooling>();
            objectPooling.PopulatePulpit(this);
        }

        public void GenerateObject(GameObject gameObject)
        {
            GetRandomGeneratingTransform(gameObject);
        }
        public void GetRandomGeneratingTransform(GameObject obj)
        {
            randomDirection = Random.Range(0, 4);

            if(lastRandomDirection == randomDirection)
            {
                GetRandomGeneratingTransform(obj);
                return;
            }

            switch (randomDirection)
            {
                   //Left
                case 0:
                    generatingPoint.position = new Vector3(lastActiveObject == null? generatingPoint.position.x : generatingPoint.position.x - lastActiveObject.transform.localScale.x, 0, generatingPoint.position.z);
                    break;
                    //Right
                case 1:
                    generatingPoint.position = new Vector3(lastActiveObject == null ? generatingPoint.position.x : generatingPoint.position.x + lastActiveObject.transform.localScale.x, 0, generatingPoint.position.z);
                    break;
                    //forward
                case 2:
                    generatingPoint.position = new Vector3(generatingPoint.position.x, 0, lastActiveObject == null ? generatingPoint.position.z : generatingPoint.position.z + lastActiveObject.transform.localScale.z);
                    break;
                    //backward
                case 3:
                    generatingPoint.position = new Vector3(generatingPoint.position.x, 0, lastActiveObject == null ? generatingPoint.position.z : generatingPoint.position.z - lastActiveObject.transform.localScale.z);
                    break;

            }
            obj.transform.position = generatingPoint.position;
            lastActiveObject = obj;
            obj.SetActive(true);

        }

        public void OnClickPlayButton()
        {
            GenerateObject(objectPooling.GetPooledObject());
            character.GetComponent<Rigidbody>().useGravity = true;
            play_InitialScreen.SetActive(false);       
            score.enabled = true;

        }

        public void OnClickGameOverButton()
        {
            SceneManager.LoadSceneAsync(0);

        }

    }

}

