using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialTimer : MonoBehaviour
{
    public UXF.Session session;
    
    public void BeginNextTrialSafeAfterDelay(UXF.Trial trial)
    {
        if (trial == session.LastTrial) return;
        
        session.Invoke("BeginNextTrialSafe", trial.settings.GetFloat("delay_time"));
    }

    public void ConditionalEndCurrentTrialAfterDelay(UXF.Trial trial)
    {
        if (trial.settings.GetBool("recall")) return;
        
        session.Invoke("EndCurrentTrial", trial.settings.GetFloat("display_time"));
    }
}
