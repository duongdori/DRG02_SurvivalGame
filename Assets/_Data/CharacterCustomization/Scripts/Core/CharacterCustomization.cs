using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


namespace CharacterCustomization
{
    public class CharacterCustomization : MonoBehaviour
    {
        #region Character Object List

        // list of enabled objects on character
        [SerializeField] private List<GameObject> enabledObjects = new List<GameObject>();

        // character object lists
        // male list
        [SerializeField] private CharacterObjectGroups male;

        // female list
        [SerializeField] private CharacterObjectGroups female;

        [SerializeField] private CharacterObjectGroups currentGender;

        // universal list
        [SerializeField] private CharacterObjectListsAllGender allGender;
        
        [SerializeField] private List<CustomizationElement> allElements;
        
        private Gender _gender;
        
        #endregion

        #region Camera Variables

        // reference to camera transform, used for rotation around the model during or after a randomization (this is sourced from Camera.main, so the main camera must be in the scene for this to work)
        private Transform camHolder;

        // cam rotation x
        private float x = 16;

        // cam rotation y
        private float y = -30;

        #endregion

        private void Start()
        {
            // rebuild all lists
            BuildLists();

            ClearEnableObjects();

            // setting up the camera position, rotation, and reference for use
            SetupCamera();
        }

        private void Update()
        {
            if (camHolder)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    x += 1 * Input.GetAxis("Mouse X");
                    y -= 1 * Input.GetAxis("Mouse Y");
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    x -= 1 * Input.GetAxis("Horizontal");
                    y -= 1 * Input.GetAxis("Vertical");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        private void LateUpdate()
        {
            // method for handling the camera rotation around the character
            if (camHolder)
            {
                y = Mathf.Clamp(y, -45, 15);
                camHolder.eulerAngles = new Vector3(y, x, 0.0f);
            }
        }

        private void SetupCamera()
        {
            Transform cam = Camera.main.transform;
            if (cam == null) return;
            
            cam.position = transform.position + new Vector3(0, 0.3f, 2);
            cam.rotation = Quaternion.Euler(0, -180, 0);
            camHolder = new GameObject("CameraHolder").transform;
            camHolder.position = transform.position + new Vector3(0, 1, 0);
            cam.LookAt(camHolder);
            cam.SetParent(camHolder);
        }

        private void ClearEnableObjects()
        {
            if (enabledObjects.Count != 0)
            {
                foreach (GameObject g in enabledObjects)
                {
                    g.SetActive(false);
                }
            }

            enabledObjects.Clear();
        }

        public void RemoveEnableObject(GameObject obj)
        {
            enabledObjects.Remove(obj);
        }
        public void SetGender(Gender gender)
        {
            _gender = gender;

            switch (gender)
            {
                case Gender.Male:
                    currentGender = male;
                    break;
                case Gender.Female:
                    currentGender = female;
                    break;
            }
        }

        public void UpdateElements()
        {
            if(allElements.Count <= 0) return;
            
            Type type = typeof(CharacterObjectGroups);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].FieldType == typeof(List<GameObject>))
                {
                    List<GameObject> gameObjects = (List<GameObject>)fields[i].GetValue(currentGender);
                    allElements[i].SetElements(gameObjects);
                    allElements[i].SetCharacterCustomizer(this);
                }
            }
            
            allElements[allElements.Count - 1].SetElements(allGender.all_Hair);
            allElements[allElements.Count - 1].SetCharacterCustomizer(this);
        }

        public void InitBaseCharacter()
        {
            switch (_gender)
            {
                case Gender.Male:
                    ActivateItem(allGender.all_Hair[0]);
                    ActivateItem(male.headAllElements[0]);
                    ActivateItem(male.eyebrow[0]);
                    ActivateItem(male.facialHair[0]);
                    ActivateItem(male.torso[0]);
                    ActivateItem(male.armUpperRight[0]);
                    ActivateItem(male.armUpperLeft[0]);
                    ActivateItem(male.armLowerRight[0]);
                    ActivateItem(male.armLowerLeft[0]);
                    ActivateItem(male.handRight[0]);
                    ActivateItem(male.handLeft[0]);
                    ActivateItem(male.hips[0]);
                    ActivateItem(male.legRight[0]);
                    ActivateItem(male.legLeft[0]);
                    return;
                
                case Gender.Female:
                    ActivateItem(allGender.all_Hair[0]);
                    ActivateItem(female.headAllElements[0]);
                    ActivateItem(female.eyebrow[0]);
                    ActivateItem(female.torso[0]);
                    ActivateItem(female.armUpperRight[0]);
                    ActivateItem(female.armUpperLeft[0]);
                    ActivateItem(female.armLowerRight[0]);
                    ActivateItem(female.armLowerLeft[0]);
                    ActivateItem(female.handRight[0]);
                    ActivateItem(female.handLeft[0]);
                    ActivateItem(female.hips[0]);
                    ActivateItem(female.legRight[0]);
                    ActivateItem(female.legLeft[0]);
                    break;
            }
        }

        // enable game object and add it to the enabled objects list
        public void ActivateItem(GameObject go)
        {
            // enable item
            go.SetActive(true);

            // add item to the enabled items list
            enabledObjects.Add(go);
        }

        private Color ConvertColor(int r, int g, int b)
        {
            return new Color(r / 255.0f, g / 255.0f, b / 255.0f, 1);
        }

        // method for rolling percentages (returns true/false)
        private bool GetPercent(int pct)
        {
            bool p = false;
            int roll = Random.Range(0, 100);
            if (roll <= pct)
            {
                p = true;
            }

            return p;
        }

        // build all item lists for use in randomization
        private void BuildLists()
        {
            //build out male lists
            BuildList(male.headAllElements, "Male_Head_All_Elements");
            BuildList(male.eyebrow, "Male_01_Eyebrows");
            BuildList(male.facialHair, "Male_02_FacialHair");
            BuildList(male.torso, "Male_03_Torso");
            BuildList(male.armUpperRight, "Male_04_Arm_Upper_Right");
            BuildList(male.armUpperLeft, "Male_05_Arm_Upper_Left");
            BuildList(male.armLowerRight, "Male_06_Arm_Lower_Right");
            BuildList(male.armLowerLeft, "Male_07_Arm_Lower_Left");
            BuildList(male.handRight, "Male_08_Hand_Right");
            BuildList(male.handLeft, "Male_09_Hand_Left");
            BuildList(male.hips, "Male_10_Hips");
            BuildList(male.legRight, "Male_11_Leg_Right");
            BuildList(male.legLeft, "Male_12_Leg_Left");

            //build out female lists
            BuildList(female.headAllElements, "Female_Head_All_Elements");
            BuildList(female.eyebrow, "Female_01_Eyebrows");
            BuildList(female.facialHair, "Female_02_FacialHair");
            BuildList(female.torso, "Female_03_Torso");
            BuildList(female.armUpperRight, "Female_04_Arm_Upper_Right");
            BuildList(female.armUpperLeft, "Female_05_Arm_Upper_Left");
            BuildList(female.armLowerRight, "Female_06_Arm_Lower_Right");
            BuildList(female.armLowerLeft, "Female_07_Arm_Lower_Left");
            BuildList(female.handRight, "Female_08_Hand_Right");
            BuildList(female.handLeft, "Female_09_Hand_Left");
            BuildList(female.hips, "Female_10_Hips");
            BuildList(female.legRight, "Female_11_Leg_Right");
            BuildList(female.legLeft, "Female_12_Leg_Left");

            // build out all gender lists
            BuildList(allGender.all_Hair, "All_01_Hair");
            BuildList(allGender.all_Head_Attachment, "All_02_Head_Attachment");
            BuildList(allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
            BuildList(allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
            BuildList(allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair");
            BuildList(allGender.chest_Attachment, "All_03_Chest_Attachment");
            BuildList(allGender.back_Attachment, "All_04_Back_Attachment");
            BuildList(allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
            BuildList(allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
            BuildList(allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
            BuildList(allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
            BuildList(allGender.hips_Attachment, "All_09_Hips_Attachment");
            BuildList(allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
            BuildList(allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
            BuildList(allGender.elf_Ear, "Elf_Ear");
        }

        // called from the BuildLists method
        private void BuildList(List<GameObject> targetList, string characterPart)
        {
            Transform[] rootTransform = gameObject.GetComponentsInChildren<Transform>();
            
            // declare target root transform
            Transform targetRoot = null;

            // find character parts parent object in the scene
            foreach (Transform t in rootTransform)
            {
                if (t.gameObject.name == characterPart)
                {
                    targetRoot = t;
                    break;
                }
            }

            // clears targeted list of all objects
            targetList.Clear();

            // cycle through all child objects of the parent object
            for (int i = 0; i < targetRoot.childCount; i++)
            {
                // get child gameObject index i
                GameObject go = targetRoot.GetChild(i).gameObject;

                // disable child object
                go.SetActive(false);

                // add object to the targeted object list
                targetList.Add(go);
            }
        }
    }
}
