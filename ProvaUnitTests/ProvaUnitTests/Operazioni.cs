
namespace ProvaUnitTests
{
    public class Operazioni
    {

        public int calcolaFattoriale(int n)
        {
            if (n > 0)
            {
                return calcolaFattoriale(n - 1) * n;
            } else
            {
                return 1;
            }
        }
    }
}
