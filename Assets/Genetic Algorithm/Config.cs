public static class Config
{
    public enum CrossoverAlgorithm
    {
        OneOfEach,
        HalfOfEach,
        EachRandom,
    }

    public enum MutationAlgorithm
    {
        RandomValue,
        ModifiedValue,
    }

    //genetic algorithm
    public static int nIndividualsPerPopulation;
    public static int nElite;
    public static float mutationRate; //percentage

    public static CrossoverAlgorithm crossoverAlgorithm;
    public static MutationAlgorithm mutationAlgorithm;

    //dna
    public static int nJumps;
    public static int minJumpFrame;
    public static int maxJumpFrame;

    //misc
    public static string logFile;

    //
    //genetic algorithm
    public static void SetIndividualsPerPopulation(int nIndividuals)
    {
        nIndividualsPerPopulation = nIndividuals;
    }
    public static void SetElite(int elite)
    {
        nElite = elite;
    }
    public static void SetCrossover(CrossoverAlgorithm newCrossoverAlgorithm)
    {
        crossoverAlgorithm = newCrossoverAlgorithm;
    }

    public static void SetMutation(float newMutationRate, MutationAlgorithm newMutationAlgorithm)
    {
        mutationRate = newMutationRate;
        mutationAlgorithm = newMutationAlgorithm;
    }

    //dna
    public static void SetJumps(int jumps)
    {
        nJumps = jumps;
    }
    public static void SetMinMaxJumpFrames(int min, int max)
    {
        minJumpFrame = min;
        maxJumpFrame = max;
    }

    //misc
    public static void SetLogFile(string path)
    {
        logFile = path;
    }
}