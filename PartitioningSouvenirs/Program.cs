using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartitioningSouvenirs
{
    class Program
    {

        
        static void Main(string[] args)
        {
            int num = Convert.ToInt32(Console.ReadLine());
            string s = Console.ReadLine();
            int[] val = new int[num];
            string[] _s = s.Split(' ');
            for (int i = 0; i < num; i++)
            {
                val[i] = Convert.ToInt32(_s[i]);
            }
            Console.WriteLine(isKPartitionPossible(val, num));
        }

        static int valueofKPartitionPossibleRec(int[] arr, int[] subsetSum, bool[] taken,
                int subset, int N, int curIdx, int limitIdx)
        {
            if (subsetSum[curIdx] == subset)
            {
                if (curIdx == 3 - 2)
                    return 1;

                // recursive call for next subsetition 
                return valueofKPartitionPossibleRec(arr, subsetSum, taken, subset,
                                                     N, curIdx + 1, N - 1);
            }

            for (int i = limitIdx; i >= 0; i--)
            {
                // if already taken, continue 
                if (taken[i])
                    continue;
                int tmp = subsetSum[curIdx] + arr[i];

                // if temp is less than subset then only include the element 
                // and call recursively 
                if (tmp <= subset)
                {
                    // mark the element and include into current partition sum 
                    taken[i] = true;
                    subsetSum[curIdx] += arr[i];
                    int nxt = valueofKPartitionPossibleRec(arr, subsetSum, taken,
                                                    subset, N, curIdx, i - 1);

                    // after recursive call unmark the element and remove from 
                    // subsetition sum 
                    taken[i] = false;
                    subsetSum[curIdx] -= arr[i];
                    if (nxt == 1)
                        return 1;
                }
            }
            return 0;
        }

        static int isKPartitionPossible(int[] arr, int N)
        {
            if (N < 3)
                return 0;
            int sum = 0;
            for (int i = 0; i < N; i++)
                sum += arr[i];
            if (sum % 3 != 0)
                return 0;

            // the sum of each subset should be subset (= sum / K) 
            int subset = sum / 3;
            int[] subsetSum = new int[3];
            bool[] taken = new bool[N];

            // Initialize sum of each subset from 0 
            for (int i = 0; i < 3; i++)
                subsetSum[i] = 0;

            // mark all elements as not taken 
            for (int i = 0; i < N; i++)
                taken[i] = false;

            // initialize first subsubset sum as last element of 
            // array and mark that as taken 
            subsetSum[0] = arr[N - 1];
            taken[N - 1] = true;

            // call recursive method to check K-substitution condition 
            return valueofKPartitionPossibleRec(arr, subsetSum, taken,
                                            subset, N, 0, N - 1);
        }
    }
}
