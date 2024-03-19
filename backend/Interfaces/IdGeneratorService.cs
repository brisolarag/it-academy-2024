using System.Threading;

namespace backend.Interfaces
{
    public class IdGeneratorService : IIdGeneratorService
    {
        private int _nextApostaId = 1000; // Começa a partir de 1000

        public int GenerateIdForAposta()
        {
            return Interlocked.Increment(ref _nextApostaId);
        }
    }
}