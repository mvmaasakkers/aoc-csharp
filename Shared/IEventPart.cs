namespace Shared
{
    
    public interface IEventPartInt
    {
        public int Compute();
    }

    
    public interface IEventPartString
    {
        public string Compute();
    }
    
    public interface IEventPartLong
    {
        public long Compute();
    }

    
    public interface IEventPart : IEventPartInt, IEventPartString, IEventPartLong { }
}