internal class CameraSettingsRepresentation : CameraSettingRepresentation
{
    public string cameraTrackTarget { get; set; }
    public object cameraZDepth { get; set; }
    public float minX { get; set; }
    public float minY { get; set; }
    public float maxX { get; set; }
    public float maxY { get; set; }
    public float trackingSpeed { get; set; }
}