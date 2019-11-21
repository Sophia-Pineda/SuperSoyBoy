using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class LevelDataRepresentation : MonoBehaviour
{
    public Vector2 playerStartPosition;
    public CameraSettingRepresentation cameraSettings;
    public LevelItemRepresenation[] levelItems;
}
