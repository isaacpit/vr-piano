namespace Types
{
    public enum ChordType { Major, Minor, Diminished, NUM_CHORDS };
    public enum MusicalNote { C, Csharp, D, Dsharp, E, F, Fsharp, G, Gsharp, A, Asharp, B };
    public enum DifficultyType { One, Two, Three, Four, Five}; //TODO Rename these to better fit difficulty

    [System.Serializable]
    public class WeightedEnemy
    {
        public ChordType enemyType;
        public int weight;
    }

    [System.Serializable]
    public class WeightedNote
    {
        public MusicalNote noteType;
        public int weight;
    }

    [System.Serializable]
    public class DifficultySettings
    {
        public DifficultyType difficultyType;
        public DifficultyObject difficultyObject;
    }
}
