using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    class CreateEvalutionHashTabel
    {
        int playerCount = 3;
        public void CreateHashTabelsWithAllCombinations(BitBoard inputBoard)
        {

        }
        private Dictionary<short, short> findAllCombinations(int combinationLength)
        {
            List<sbyte> testMethod = new List<sbyte>();
            testMethod.Add(0);
            testMethod.Add(1);
            testMethod.Add(2);


            IEnumerable<List<sbyte>> thisIenum = < IEnumerable <List<sbyte> > testMethod;


           var dd = GetPermutationsWithRept<IEnumerable<sbyte>>(thisIenum, 6);

            
            return returnDictionary;
        }

        static IEnumerable<IEnumerable<T>>GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
                return list.Select(t => new T[] { t });

            return GetPermutationsWithRept(list, length - 1).SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }

    class Permutations : IEnumerable
    {
        Permutations[] Items =  ;
        sbyte[] outItems;
            public IEnumerator GetEnumerator()
        {
            foreach(object o in Items )
            {
                if(o == null)
                {
                    break;
                }
                yield return 0;
            }
         }
    }
}
