using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class PopulationMap
{
    public int nIndividualsPerPopulation;
    public int nElite;
    public List<Population> populations;
}
[Serializable]
public class Population
{
    public List<Individual> individuals;
}

public static class PopulationLogger
{

    public static PopulationMap populationMap;

    public static void Init(int nIndividualsPerPopulation, int nElite)
    {
        populationMap = new PopulationMap();
        populationMap.nIndividualsPerPopulation = nIndividualsPerPopulation;
        populationMap.nElite = nElite;
        populationMap.populations = new List<Population>();
        Log();
    }

    public static void AddPopulation(Individual[] population, int nPopulation)
    {
        Population newPopulation = new Population();
        newPopulation.individuals = new List<Individual>();
        foreach (Individual individual in population)
        {
            newPopulation.individuals.Add(individual);
        }
        populationMap.populations.Add(newPopulation);
        Log();
    }

    private static void Log()
    {
        var json = JsonUtility.ToJson(populationMap);
        Debug.Log(json);
        System.IO.File.WriteAllText("Assets/" + Config.logFile, json);
    }
}