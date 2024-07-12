using System;

[Serializable]
public struct PlayerData
{
    //information being passed will tell server the users position
    public string id;
    public string xPos;
    public string yPos;
    public string zPos;
    public double timestamp;
}