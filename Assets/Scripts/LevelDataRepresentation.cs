using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class LevelDataRepresentation
{
    public Vector2 playerStartPosition;
    public CameraSettingRepresentation cameraSettings;
    public LevelItemRepresentation[] levelItems;
}
