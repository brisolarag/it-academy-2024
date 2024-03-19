using backend.Models.Apostas;
using System;
using System.Collections.Generic;

namespace backend.Models.Sorteios
{
    public class Sorteio
    {
        public int Id { get; set; }
        public List<int> NumerosGanhadores { get; private set; }
        public DateTime DataSorteio { get; private init; }
        public ICollection<Aposta> TodasApostas { get; private set; }
        public SorteioStatus Status { get; set; }


        
        public Sorteio()
        {
            NumerosGanhadores = new List<int>();
            TodasApostas = new List<Aposta>();
            randomizarNumerosIniciais();
            DataSorteio = DateTime.Now;
            Status = SorteioStatus.ApostasEmAndamento;
        }


        


        private Random rnd = new Random();
        
        private void randomizarNumerosIniciais()
        {
            while (NumerosGanhadores.Count < 5)
            {
                AddNumero();
            }
        }

        public List<int> GetNumerosGanhadores()
        {
            return NumerosGanhadores;
        }

        public void AddNumero()
        {
            int numToAdd;
            do
            {
                numToAdd = rnd.Next(1, 50);
            } while (NumerosGanhadores.Contains(numToAdd));

            NumerosGanhadores.Add(numToAdd);
        }

        public void AddAposta(Aposta aposta)
        {
            TodasApostas.Add(aposta);
        }
    }
}