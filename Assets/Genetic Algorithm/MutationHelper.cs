using UnityEngine;

public static class MutationHelper
{
    public static void MutatePopulation(Individual[] population)
    {
        for (int i = 0; i < population.Length; i++)
        {
            MutateIndividual(population[i]);
        }
    }

    private static void MutateIndividual(Individual individual)
    {
        int[] currentJumpFrames = individual.dna.jumpFrames;
        int[] newJumpFrames = new int[currentJumpFrames.Length];

        for (int i = 0; i < currentJumpFrames.Length; i++)
        {
            if (Random.Range(0, 10000) < Config.mutationRate * 100)
            {
                individual.wasMutated = true;
                switch (Config.mutationAlgorithm)
                {
                    case Config.MutationAlgorithm.RandomValue:
                        newJumpFrames[i] = DNA.GetRandomJumpFrame();
                        break;
                    case Config.MutationAlgorithm.ModifiedValue:
                        newJumpFrames[i] = (int)(individual.dna.jumpFrames[i] * Random.Range(0.8f, 1.2f));
                        break;
                }
            }
            else
            {
                newJumpFrames[i] = currentJumpFrames[i];
            }
        }

        individual.dna.jumpFrames = newJumpFrames;
    }
}