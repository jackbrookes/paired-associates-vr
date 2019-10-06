using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment : MonoBehaviour
{
    public ObjectPairs objectPairs;

    public void GenerateExperiment(UXF.Session session)
    {        
        UXF.Block block = session.CreateBlock();

        foreach (Pair pair in objectPairs.pairs)
        {
            UXF.Trial trial = block.CreateTrial();
            trial.settings.SetValue("pair", pair);
            trial.settings.SetValue("object_a", pair.prefabA.name);
            trial.settings.SetValue("object_b", pair.prefabB.name);
        }

        bool recall = session.number > 1;
        
        if (recall)
        {
            block.settings.SetValue("recall", true);
            foreach (UXF.Trial trial in block.trials)
            {
                Pair trialPair = (Pair) trial.settings.GetObject("pair");
                System.Random trialRng = new System.Random(trial.number); // consistent shuffles between participants

                List<Pair> options = new List<Pair>(4);
                options.Add(trialPair); // "correct" answer
                
                // now add three wrong answers
                List<Pair> pairsCopy = new List<Pair>(objectPairs.pairs);
                UXF.Extensions.Shuffle(pairsCopy, trialRng); // random SELECTION of options
                foreach (Pair optionPair in pairsCopy)
                {
                    if (optionPair != trialPair) // cant have the correct object as a wrong option
                    {
                        options.Add(optionPair);
                        if (options.Count >= 4) break;
                    }
                }

                // shuffle the options and store the settings
                UXF.Extensions.Shuffle(options, trialRng); // random ORDER of options
                int j = 1;
                foreach (Pair option in options)
                {
                    // we use prefab B as the options (A is always the stimulus)
                    trial.settings.SetValue(string.Format("option_object_{0}", j), option.prefabB);
                    trial.settings.SetValue(string.Format("option_{0}", j), option.prefabB.name);
                    j++;
                }
            }
        }
        else
        {
            block.settings.SetValue("recall", false);
            for (int i = 1; i < 5; i++) 
                block.settings.SetValue(string.Format("option_{0}", i), "");
        }


        // different shiffle per person
        UXF.Extensions.Shuffle(block.trials);
    }

    IEnumerator DelayThenStart(UXF.Trial trial)
    {
        float delayTime = trial.settings.GetFloat("delay_time");
        yield return new WaitForSeconds(delayTime);
        trial.session.BeginNextTrialSafe();
        trial.session.EndIfLastTrial(trial);
    }

}
