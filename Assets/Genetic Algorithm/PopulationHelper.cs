using UnityEngine;

public static class PopulationHelper
{
    public static Individual[] population;

    private static int generationsFinished = 0;
    private static int individualsFinished = 0;

    public static void Init()
    {
        population = new Individual[Config.nIndividualsPerPopulation];

        for (int i = 0; i < population.Length; i++)
        {
            Individual individual = new Individual();
            individual.dna = new DNA();
            population[i] = individual;
        }

        PopulationLogger.Init(Config.nIndividualsPerPopulation, Config.nElite);
    }

    public static void Advance()
    {
        if (individualsFinished >= Config.nIndividualsPerPopulation - 1)
        {
            Debug.Log("just finished generation " + generationsFinished);
            AdvanceGeneration();
        }
        else
        {
            individualsFinished++;
            StartIndividual(individualsFinished);
        }
    }


    private static void AdvanceGeneration()
    {
        Individual[] nextPopulation = new Individual[Config.nIndividualsPerPopulation];
        Individual[] orderedPopulation = FitnessHelper.GetOrderedPopulation(population);

        AddElite(nextPopulation, orderedPopulation);
        AddChildren(nextPopulation, population);

        Debug.Log("population " + generationsFinished);
        PopulationLogger.AddPopulation(orderedPopulation, generationsFinished);

        generationsFinished++;
        individualsFinished = 0;
        population = (Individual[])nextPopulation.Clone();
        StartIndividual(0);
    }

    public static void StartIndividual(int index)
    {
        GameObject.Find("Jumper").GetComponent<Jumper>().SetIndividual(population[index]);
        Globals.isPaused = false;
    }

    private static void AddElite(Individual[] nextPopulation, Individual[] orderedPopulation)
    {
        for (int i = 0; i < Config.nElite; i++)
        {
            nextPopulation[i] = orderedPopulation[i];
            orderedPopulation[i].wasElite = true;
        }
    }

    private static void AddChildren(Individual[] nextPopulation, Individual[] population)
    {
        Individual[] childrenIndividuals = ReproductionHelper.GetChildrenIndividuals(population, Config.nIndividualsPerPopulation - Config.nElite);
        MutationHelper.MutatePopulation(childrenIndividuals);

        for (int i = Config.nElite; i < childrenIndividuals.Length + Config.nElite; i++)
        {
            nextPopulation[i] = childrenIndividuals[i - Config.nElite];
        }
    }
}