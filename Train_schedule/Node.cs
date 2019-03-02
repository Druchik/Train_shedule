namespace Train_schedule
{
    public class Node
    {
        public Node(Train data) => Data = data;
        public Train Data { get; set; }
        public Node next;
    }
}
