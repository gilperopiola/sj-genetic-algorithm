using UnityEngine;

public static class ReproductionHelper
{
    public static Individual[] GetChildrenIndividuals(Individual[] population, int nChildren)
    {
        Individual[] childrenIndividuals = new Individual[nChildren];

        for (int i = 0; i < nChildren; i++)
        {
            Individual parent1 = SpinRoulette(population);
            Individual parent2 = SpinRoulette(population, parent1);
            switch (Config.crossoverAlgorithm)
            {
                case Config.CrossoverAlgorithm.OneOfEach:
                    childrenIndividuals[i] = CrossoverOneOfEach(parent1, parent2);
                    break;
                case Config.CrossoverAlgorithm.HalfOfEach:
                    childrenIndividuals[i] = CrossoverHalfOfEach(parent1, parent2);
                    break;
                case Config.CrossoverAlgorithm.EachRandom:
                    childrenIndividuals[i] = CrossoverEachRandom(parent1, parent2);
                    break;
            }
        }

        return childrenIndividuals;
    }

    private static Individual CrossoverOneOfEach(Individual parent1, Individual parent2)
    {
        Individual child = new Individual();
        child.dna = new DNA();

        for (int i = 0; i < parent1.dna.jumpFrames.Length; i++)
        {
            child.dna.jumpFrames[i] = (i % 2 == 0) ? parent1.dna.jumpFrames[i] : parent2.dna.jumpFrames[i];
        }

        return child;
    }

    private static Individual CrossoverHalfOfEach(Individual parent1, Individual parent2)
    {
        Individual child = new Individual();
        child.dna = new DNA();

        for (int i = 0; i < parent1.dna.jumpFrames.Length; i++)
        {
            child.dna.jumpFrames[i] = (i < parent1.dna.jumpFrames.Length / 2) ? parent1.dna.jumpFrames[i] : parent2.dna.jumpFrames[i];
        }

        return child;
    }

    private static Individual CrossoverEachRandom(Individual parent1, Individual parent2)
    {
        Individual child = new Individual();
        child.dna = new DNA();

        for (int i = 0; i < parent1.dna.jumpFrames.Length; i++)
        {
            child.dna.jumpFrames[i] = (Random.Range(0, 10) <= 4) ? parent1.dna.jumpFrames[i] : parent2.dna.jumpFrames[i];
        }

        return child;
    }

    private static Individual SpinRoulette(Individual[] population, Individual alreadySelectedIndividual = null)
    {
        float totalFitness = FitnessHelper.GetPopulationTotalFitness(population);
        float rouletteResult = Random.Range(0, totalFitness);
        float portionCounter = 0;

        if (totalFitness == 0)
        {
            return population[Random.Range(0, population.Length)];
        }

        for (int i = 0; i < population.Length; i++)
        {
            portionCounter += population[i].fitness;

            if (portionCounter > rouletteResult)
            {
                if (alreadySelectedIndividual != null && alreadySelectedIndividual.fitness.ToString("F") == population[i].fitness.ToString("F"))
                {
                    return SpinRoulette(population, alreadySelectedIndividual);
                }
                else
                {
                    return population[i];
                }
            }
        }

        Debug.LogError("roulette error: " + totalFitness + " | " + rouletteResult);
        return null;
    }
}