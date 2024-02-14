namespace QuestSystem
{
    [System.Serializable]
    public class QuestGoal
    {
        public GoalType goalType;
        public int requireAmount;
        public int currentAmount;

        public bool IsReached()
        {
            return (currentAmount >= requireAmount);
        }

        public void CustomerAttended()
        {
            if((goalType == GoalType.Kitchen))
                currentAmount++;
        }

        public void ItemCollected()
        {
            if((goalType == GoalType.Gathering))
                currentAmount++;
        }

       
    }
    
    public enum GoalType
    {
        Gathering,
        Kitchen
    }
}
