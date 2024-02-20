namespace CodingTracker
{
    public class CodingSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Constructor for initializing a CodingSession
        public CodingSession(int id, DateTime startTime, DateTime endTime)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}