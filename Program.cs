using System;
using System.IO;


namespace Desempenho
{

    class Program
    {
        static void Main(string[] args)
        {
            int quantInstrucao = 0;
            float tresCiclo = 0;
            float quatroCiclo = 0;
            float cincoCiclo = 0;

            Console.WriteLine("\nRelatório:");
            SeparaLinha(ref tresCiclo, ref quatroCiclo, ref cincoCiclo, ref quantInstrucao);

            Console.WriteLine("\nQuantidade de instruções: " + quantInstrucao + " (100%)");
            Console.WriteLine("Quantidade de instruções com 3 ciclos: " + tresCiclo + " (" + ((tresCiclo * 100) / quantInstrucao) + "%)");
            Console.WriteLine("Quantidade de instruções com 4 ciclos: " + quatroCiclo + " (" + ((quatroCiclo * 100) / quantInstrucao) + "%)");
            Console.WriteLine("Quantidade de instruções com 5 ciclos: " + cincoCiclo + " (" + ((cincoCiclo * 100) / quantInstrucao) + "%)");
            Console.WriteLine("Quantidade de ciclos gasto: " + ((tresCiclo * 3) + (quatroCiclo * 4) + (cincoCiclo * 5)));
            Console.WriteLine("CPI: " + (((((tresCiclo * 100) / quantInstrucao) / 100) * 3) + ((((quatroCiclo * 100) / quantInstrucao) / 100) * 4) + ((((cincoCiclo * 100) / quantInstrucao) / 100) * 5)));

        }

        private static void SomaInstrucoes(string op, string funct, ref float tresCiclo, ref float quatroCiclo, ref float cincoCiclo)
        {
            if (op == "000010" || op == "000011")
            {
                tresCiclo++;
            }
            else if (op == "000000" && (funct == "001001" || funct == "001000"))
            {
                tresCiclo++;
            }
            else if (op == "000100" || op == "000001" || op == "000111" || op == "000110" || op == "000101" || op == "010000")
            {
                tresCiclo++;
            }
            else if (op == "000000" && (funct == "001101" || funct == "001100"))
            {
                tresCiclo++;
            }
            else if (op == "100000" || op == "100100" || op == "100001" || op == "100101" || op == "100011")
            {
                cincoCiclo++;
            }
            else
            {
                quatroCiclo++;
            }
        }

        private static void SeparaLinha(ref float tresCiclo, ref float quatroCiclo, ref float cincoCiclo, ref int quantInstrucao)
        {
            string[] lines = File.ReadAllLines("Teste2.txt");
            string op = "";
            string funct = "";
            foreach (var line in lines)
            {
                op = (line[0].ToString() + line[1].ToString() + line[2].ToString() + line[3].ToString() + line[4].ToString() + line[5].ToString());
                funct = (line[26].ToString() + line[27].ToString() + line[28].ToString() + line[29].ToString() + line[30].ToString() + line[31].ToString());
                SomaInstrucoes(op, funct, ref tresCiclo, ref quatroCiclo, ref cincoCiclo);
                quantInstrucao++;

            }
        }
    }
}
