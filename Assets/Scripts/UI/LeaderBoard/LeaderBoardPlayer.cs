namespace UI.LeaderBoardSystem
{
    public class LeaderBoardPlayer
    {
        public LeaderBoardPlayer(string name, int rank, int score)
        {
            Name = name;
            Rank = rank;
            Score = score;
        }

        public string Name { get; private set; }
        public int Rank { get; private set; }
        public int Score { get; private set; }
    }
}