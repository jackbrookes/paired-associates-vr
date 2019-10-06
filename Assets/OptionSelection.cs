using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelection : MonoBehaviour
{

    public UXF.Session session;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & session.InTrial)
        {
            string selection = transform.GetChild(0).name;
            session.CurrentTrial.result["selected_option"] = selection;
            session.CurrentTrial.result["correct"] = selection == session.CurrentTrial.settings.GetString("object_b");
            session.EndCurrentTrial();
        }
    }

}
