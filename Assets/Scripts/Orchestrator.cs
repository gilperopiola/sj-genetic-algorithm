using UnityEngine;

public class Orchestrator : MonoBehaviour
{
    void Start()
    {
        Config.SetLogFile("log.json");
        Config.SetIndividualsPerPopulation(20);
        Config.SetElite(4);
        Config.SetCrossover(Config.CrossoverAlgorithm.OneOfEach);
        Config.SetMutation(10.5f, Config.MutationAlgorithm.ModifiedValue);
        Config.SetJumps(30);
        Config.SetMinMaxJumpFrames(0, 65);

        PopulationHelper.Init();
        PopulationHelper.StartIndividual(0);
    }
}