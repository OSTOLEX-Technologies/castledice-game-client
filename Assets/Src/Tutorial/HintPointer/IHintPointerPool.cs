namespace Src.Tutorial.HintPointer
{
    public interface IHintPointerPool
    {
        public HintPointer Obtain();
        
        public void Reclaim(HintPointer pointer);
    }
}