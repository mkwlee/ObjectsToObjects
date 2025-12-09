using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TV
{
    public bool power { get; private set; }
    public int channel { get; private set; }
    public enum inputType { HDM1, HDMI2, HDMI3, RCA };
    public inputType input { get; private set; }

    public virtual bool TVPowerToggle()
    {
        return power = power!;
    }

    public virtual bool ChangeChannel(int UpOrDown)
    {
        if (IsValidChannel(channel+UpOrDown))
        {
            channel += UpOrDown;
            return true;
        }
        else
        {
            return false;
        }
    } 

    private bool IsValidChannel(int channelNum)
    {
        /// Would check channel is valid
        return true;
    }
}


public class Remote : TV
{
    public override bool ChangeChannel(int UpOrDown)
    {
        return base.ChangeChannel(UpOrDown);
    }
}