using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TesteCandidatoTriangulo
{
    public class Triangulo
    {
        /// <summary>
        ///    6
        ///   3 5
        ///  9 7 1
        /// 4 6 8 4
        /// Um elemento somente pode ser somado com um dos dois elementos da próxima linha. Como o elemento 5 na Linha 2 pode ser somado com 7 e 1, mas não com o 9.
        /// Neste triangulo o total máximo é 6 + 5 + 7 + 8 = 26
        /// 
        /// Seu código deverá receber uma matriz (multidimensional) como entrada. O triângulo acima seria: [[6],[3,5],[9,7,1],[4,6,8,4]]
        /// </summary>
        /// <param name="dadosTriangulo"></param>
        /// <returns>Retorna o resultado do calculo conforme regra acima</returns>
        public int ResultadoTriangulo(string dadosTriangulo)
        {
            int[][] triangle = JsonConvert.DeserializeObject<int[][]>(dadosTriangulo);
                        
            return MaxTotal(triangle);
        }

        public static int MaxTotal(int[][] triangle)
        {
            int n = triangle.Length;
            int maxTotal = triangle[0][0];

            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    int current = triangle[i][j];
                    if (j == 0)
                    {
                        triangle[i][j] += triangle[i - 1][j];
                    }
                    else if (j == i)
                    {
                        triangle[i][j] += triangle[i - 1][j - 1];
                    }
                    else
                    {
                        triangle[i][j] += Math.Max(triangle[i - 1][j - 1], triangle[i - 1][j]);
                    }
                    if (triangle[i][j] > maxTotal)
                    {
                        maxTotal = triangle[i][j];
                    }
                }
            }

            return maxTotal;
        }


    }
}
