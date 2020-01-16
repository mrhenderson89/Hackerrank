using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace MatrixLayerRotation
{
    class Program
    {
        // Complete the matrixRotation function below.
        static void matrixRotation(List<List<int>> matrix, int r)
        {
            List<List<int>> output = new List<List<int>>();
            bool identicalList = true;

            if (!matrix.TrueForAll(x => x.TrueForAll(y => y.Equals(matrix[0].FirstOrDefault()))))
            {
                identicalList = false;
            }

            if (identicalList)
            {
                outputList(matrix, matrix[0].Count);
            }
            else
            {
                for (int l = 0; l < matrix[0].Count / 2; l++)
                {

                    IEnumerable<int> list = createMatrixList(matrix, l);
                    Console.WriteLine("List " + l);
                    outputSingleList(list.ToList());
                    list = rotateList(list, r);
                    Console.WriteLine("Rotated List " + l);
                    outputSingleList(list.ToList());
                    output.Add(list.ToList());

                    int k = 0;
                    //for (int i = 0; i < matrix.Count; i++)
                    //{
                    //    int innerSize = matrix.Count - 1;
                    //    if (i == 0)
                    //    {
                    //        output.Add(list.Take(matrix[i].Count).ToList());
                    //    }
                    //    else if (i < matrix.Count - 1)
                    //    {
                    //        output.Add(list.Reverse().Skip(i - 1).Take(1).ToList());
                    //        for (int j = i; j < matrix.Count - 1; j++)
                    //        {
                    //            if (j == 1)
                    //            {
                    //                output[i].AddRange(list.Skip((matrix[i].Count - 2) * (i - 1)).Take(matrix[i].Count - 2).ToList());
                    //                break;
                    //            }
                    //            else if (j < matrix.Count - 2)
                    //            {
                    //                output[i].Add(inner.Reverse().Skip(k).First());
                    //                output[i].Add(list.Skip(j).First());
                    //                k++;
                    //                break;
                    //            }
                    //            else
                    //            {
                    //                output[i].AddRange(list.Reverse().Skip(k).Take(matrix[i].Count - 2).ToList());
                    //                break;
                    //            }
                    //        }
                    //        //output[i].AddRange(inner.Skip((matrix[i].Count - 2) * (i-1)).Take(matrix[i].Count - 2).ToList());
                    //        output[i].Add(list.Skip(matrix[i].Count + (i - 1)).First());
                    //    }
                    //    else if (i == matrix.Count - 1)
                    //    {
                    //        output.Add(list.Reverse().Skip(matrix.Count - 2).Take(matrix[i].Count).ToList());
                    //    }
                    //}
                }

                outputList(output, matrix[0].Count);

                Console.ReadLine();
            }
        }

        static List<int> createMatrixList(List<List<int>> matrix, int iterator)
        {
            List<int> output = new List<int>();

            for (int i = iterator; i < matrix.Count - iterator; i++)
            {
                if (i == iterator)
                {
                    output.AddRange(matrix[i].Skip(iterator).Take(matrix[i].Count - (iterator*2)));
                }
                else if (i < matrix.Count - (iterator+1))
                {
                    output.Add(matrix[i].Skip(matrix[i].Count - (iterator + 1)).First());
                }
                else
                {
                    output.AddRange(matrix[i].OrderByDescending(x => x).Skip(iterator).Take(matrix[i].Count - (iterator *2)));
                }
            }

            for (int i = matrix.Count - (iterator + 2); i > iterator; i--)
            {
                output.Add(matrix.ElementAt(i).Skip(iterator).First());
            }

            return output;
        }

        static List<int> createOuterList(List<List<int>> matrix)
        {
            List<int> output = new List<int>();

            for (int i = 0; i < matrix.Count; i++)
            {
                if (i == 0)
                {
                    output.AddRange(matrix[i]);
                }
                else if (i < matrix.Count - 1)
                {
                    output.Add(matrix.ElementAt(i).Last());
                }
                else
                {
                    output.AddRange(matrix[i].OrderByDescending(x => x));
                }
            }

            for (int i = matrix.Count - 2; i > 0; i--)
            {
                output.Add(matrix.ElementAt(i).First());
            }

            return output;
        }

        static List<int> createInnerList(List<List<int>> matrix)
        {
            List<int> output = new List<int>();

            for (int i = 1; i < matrix.Count-1; i++)
            {
                if (i == 1)
                {
                    output.AddRange(matrix[i].Skip(1).Take(matrix[i].Count-2));
                }
                else if (i < matrix.Count - 2)
                {
                    output.Add(matrix[i].Skip(matrix[i].Count -2).First());
                }
                else
                {
                    output.AddRange(matrix[i].OrderByDescending(x => x).Skip(1).Take(matrix[i].Count - 2));
                }
            }

            for (int i = matrix.Count - 3; i > 1; i--)
            {
                output.Add(matrix.ElementAt(i).Skip(1).First());
            }

            return output;
        }

        static List<int> rotateList(IEnumerable<int> input, int rotations)
        {
            List<int> output = new List<int>();
            List<int> leadingInts = new List<int>();

            if (rotations % input.Count() == 0)
            {
                return input.ToList();
            }
            else
            {
                while (rotations > input.Count())
                {
                    rotations = rotations - input.Count();
                }
            }

            for(int i=0; i<rotations; i++)
            {
                leadingInts.Add(input.ElementAt(i));
            }

            for(int j=rotations; j<input.Count();j++)
            {
                output.Add(input.ElementAt(j));
            }

            output.AddRange(leadingInts);


            return output;
        }

        static void outputList(List<List<int>> rotatedList, int matrixWidth)
        {
            //foreach (var sublist in rotatedList)
            //{
            //    foreach (var value in sublist)
            //    {
            //        Console.Write(value);
            //        Console.Write(' ');
            //    }
            //    Console.WriteLine();
            //}

            for(int i = 0; i< rotatedList.Count; i++)
            {
                for(int j=0; j < matrixWidth; j++)
                {
                    int k = i;
                    if(j<i)
                    {
                        Console.Write(rotatedList[j].Last());
                        Console.Write(' ');
                        rotatedList[j].Remove(rotatedList[j].Last());
                    }
                    else if(j >= i && j<= (matrixWidth-i))
                    {
                        Console.Write(rotatedList[k].First());
                        Console.Write(' ');
                        rotatedList[k].RemoveAt(0);
                    }
                    else if(j >= (matrixWidth - i))
                    {
                        Console.Write(rotatedList[k].First());
                        Console.Write(' ');
                        rotatedList[k].RemoveAt(0);
                    }
                    k--;
                }
                Console.WriteLine();
            }

        }

        static void outputSingleList(List<int> list)
        {
                foreach (var value in list)
                {
                    Console.Write(value);
                    Console.Write(' ');
                }
                Console.WriteLine();
        }

        static void Main(string[] args)
        {
            //string[] mnr = Console.ReadLine().TrimEnd().Split(' ');

            //int m = Convert.ToInt32(mnr[0]);

            //int n = Convert.ToInt32(mnr[1]);

            //int r = Convert.ToInt32(mnr[2]);

            //List<List<int>> matrix = new List<List<int>>();

            //for (int i = 0; i < m; i++)
            //{
            //    matrix.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(matrixTemp => Convert.ToInt32(matrixTemp)).ToList());
            //}

            List<List<int>> matrix = new List<List<int>>();
            //matrix.Add(new List<int> { 9718805, 60013003, 5103628, 85388216, 21884498, 38021292, 73470430, 31785927 });
            //matrix.Add(new List<int> { 69999937, 71783860, 10329789, 96382322, 71055337, 30247265, 96087879, 93754371 });
            //matrix.Add(new List<int> { 79943507, 75398396, 38446081, 34699742, 1408833, 51189, 17741775, 53195748 });
            //matrix.Add(new List<int> { 79354991, 26629304, 86523163, 67042516, 54688734, 54630910, 6967117, 90198864 });
            //matrix.Add(new List<int> { 84146680, 27762534, 6331115, 5932542, 29446517, 15654690, 92837327, 91644840 });
            //matrix.Add(new List<int> { 58623600, 69622764, 2218936, 58592832, 49558405, 17112485, 38615864, 32720798 });
            //matrix.Add(new List<int> { 49469904, 5270000, 32589026, 56425665, 23544383, 90502426, 63729346, 35319547 });
            //matrix.Add(new List<int> { 20888810, 97945481, 85669747, 88915819, 96642353, 42430633, 47265349, 89653362 });
            //matrix.Add(new List<int> { 55349226, 10844931, 25289229, 90786953, 22590518, 54702481, 71197978, 50410021 });
            //matrix.Add(new List<int> { 9392211, 31297360, 27353496, 56239301, 7071172, 61983443, 86544343, 43779176 });
            matrix.Add(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 });
            matrix.Add(new List<int> { 11, 12, 13, 14, 15, 16, 17, 18 });
            matrix.Add(new List<int> { 21, 22, 23, 24, 25, 26, 27, 28 });
            matrix.Add(new List<int> { 31, 32, 33, 34, 35, 36, 37, 38 });
            matrix.Add(new List<int> { 41, 42, 43, 44, 45, 46, 47, 48 });
            matrix.Add(new List<int> { 51, 52, 53, 54, 55, 56, 57, 58 });
            matrix.Add(new List<int> { 61, 62, 63, 64, 65, 66, 67, 68 });
            matrix.Add(new List<int> { 71, 72, 73, 74, 75, 76, 77, 78 });
            matrix.Add(new List<int> { 81, 82, 83, 84, 85, 86, 87, 88 });
            matrix.Add(new List<int> { 91, 92, 93, 94, 95, 96, 97, 98 });

            int r = 0;
            matrixRotation(matrix, r);
        }
    }
}


//10 8 40
//9718805 60013003 5103628 85388216 21884498 38021292 73470430 31785927
//69999937 71783860 10329789 96382322 71055337 30247265 96087879 93754371
//79943507 75398396 38446081 34699742 1408833 51189 17741775 53195748
//79354991 26629304 86523163 67042516 54688734 54630910 6967117 90198864
//84146680 27762534 6331115 5932542 29446517 15654690 92837327 91644840
//58623600 69622764 2218936 58592832 49558405 17112485 38615864 32720798
//49469904 5270000 32589026 56425665 23544383 90502426 63729346 35319547
//20888810 97945481 85669747 88915819 96642353 42430633 47265349 89653362
//55349226 10844931 25289229 90786953 22590518 54702481 71197978 50410021
//9392211 31297360 27353496 56239301 7071172 61983443 86544343 43779176

//20 35 20
//38950343 46927501 52732087 79096784 99768969 87107645 10655095 46677242 33633183 16379998 27254248 42691669 36996828 39112247 88842074 24498867 24431906 2183515 24518860 8314921 61842591 4075781 78546289 38723163 54296259 58332834 72221222 76060312 73789550 83838565 39249266 60702778 26036952 90167124 27699771
//16890510 84763994 62683333 36330960 52963972 67006619 29122446 93405938 99753158 83562860 7054176 92077306 4345791 75012462 89936676 68000196 62575180 35614647 28391955 42040554 30306484 34913710 2884941 53777471 963349 70026715 64164289 79955530 74725282 20357033 38623679 66125006 73439290 30872072 57168246
//99414907 74527659 20102991 68221497 15715836 47037040 94668793 38183872 65506647 73462209 53396698 25954898 59140985 32091272 81005196 42056652 74616728 96357248 63835792 67447027 38683492 80044320 8263886 12287085 85600270 20185300 6188842 40727578 56937426 43100133 71397805 74205441 30524204 3263549 65617321
//12791945 56974984 19553267 43711417 81809847 38184584 58918402 73754269 19237784 3764818 33621057 29600133 68345843 28601713 84942306 11092771 29557082 87544699 23607103 20201108 55705511 61696137 85170914 10137374 45661875 94851660 37224143 28908570 2724302 48348702 44508728 98771569 76221723 17912741 57904517
//8706735 97453699 57047755 43731381 48294456 54391094 98099423 10926264 78273507 23487841 74326637 25392625 15579738 91504754 24148953 23183065 8808037 55422220 2194578 47164195 93810141 84319296 30855604 35426054 48535293 63522577 764127 83936671 11240401 18378950 68935044 90805728 86840745 32682148 9278234
//65009421 42441944 35083780 41764933 48542010 84988319 25745337 3740135 57697810 21482385 55038384 29704849 96550594 25374716 46276002 74349120 33685751 71492951 75259269 47698699 61853746 56273236 80848410 30945346 79451758 90076980 82818953 6862713 42603635 34020227 25939062 69666819 37096588 55012614 52505001
//75762177 10546334 89597290 29497431 59021632 86900651 39702792 21072044 62079995 57754642 71562357 2273815 28578645 7939247 52825694 55664954 60380224 86457800 42282778 78982370 68479663 78083813 41638747 58991300 36669712 65796279 29423523 34377500 72976303 99717277 23389311 78821178 45460956 4329732 59643645
//84321666 86194626 62491641 34285202 14392555 39290 12126244 26454124 26922815 33330680 79455933 10478125 37300277 41637573 51261012 69190192 3819479 45480086 57882634 40513196 69446833 78221446 98552569 65051316 86691725 11304763 91120738 53697639 85633000 88588317 69364683 61161903 19076056 13739959 82803519
//33841807 33366533 12483205 76302053 98605911 68562392 34944673 39739191 72825638 87466960 72434531 21627929 5388429 69451246 37698618 94812528 3443661 82044693 98013027 47231792 60277409 60949252 56448839 46225605 75981731 87432480 48216855 90975187 54478649 50981117 56209117 24405786 67978531 24647410 52686070
//23243869 3097132 60696192 42515278 22909051 41625048 34961554 17449558 62534378 83337900 29747706 30639539 54646830 88255096 47159363 65907570 93903144 1399516 87422869 7267015 19764438 24380035 10059961 10752972 39106282 15013312 19701899 42837490 96499250 28720802 82882472 77534353 52258178 63581494 87324151
//37755625 91346726 34690382 1032465 25564074 39837005 82867258 74939433 10672680 9862449 17779854 59887687 36874462 28865278 6190029 76422675 62042896 34428245 42301094 84161170 68925174 32093600 79078957 46430019 71157668 20310550 56939340 75419884 91764932 84015676 39337774 73274535 9860620 46339150 53568652
//55336990 55350739 46542796 39474174 53199594 81146709 81534392 28783468 47255908 57727656 8165819 13446816 71128103 15848500 54011009 12599333 63644237 9772340 25432869 14683063 17965691 85612722 33486691 53775941 92015724 17203398 80570802 77320611 99535960 15370209 63723500 76215634 96898348 25561726 54078615
//77626125 17225955 3502408 6230294 45890440 80426701 98863935 36120902 97543984 25419962 82907528 81554238 12750828 69136727 27632013 25549470 17328300 95799755 81169470 19489574 17069254 19541016 87002310 90381267 89211288 43589430 63032667 54531734 74671653 60062711 90617014 88560734 69061170 95822938 63385964
//27904950 80510009 11906194 11001213 72834700 7982675 6712459 43958651 82491474 95029102 56442671 62128677 28567949 30813309 50195710 31931780 61990524 2780558 41688759 64879495 18327591 53468499 44740362 23667883 50635116 57737366 45456230 41092665 32064365 16088734 90483634 61762241 14794853 54761946 37751016
//6916800 66718961 33941839 23785234 18618791 32792836 91680892 9999605 61162042 84081561 93033496 19271750 44753832 24264830 11419555 63680959 89356321 20732633 42379145 94450630 65030632 15388760 38649547 75942078 29209101 48893183 59890390 96971840 31028061 1312714 50402324 84775311 68185691 12012533 93829018
//79615390 62746400 92109555 45101941 61335591 68895313 83921794 6781796 93082789 37853418 53586262 99470136 61975664 42995983 25381115 74109108 51670577 11673865 79276139 27756850 77577467 19319390 36848871 13820636 51904093 76125493 54942002 82749303 22757777 76918850 31300901 49831132 49209530 90804743 1918495
//63012775 32526254 38743488 75927905 35549466 22702488 44425201 10621617 15816183 58973982 46641778 76539161 89754035 83171064 29811982 553637 50195617 85570690 63397526 3500645 21707681 4331694 40655128 68131471 99608922 1648704 72501480 83035711 47465250 82435779 23225179 8666688 98634485 4651809 71361633
//2716447 24326862 54305299 41254015 23825243 63167325 22727130 78655392 4973828 55390112 28875495 37184119 50895077 88565632 75238148 1526714 67454777 41154226 12322752 97989359 75189696 5200857 13776988 33331317 94054306 80129376 31855821 58770443 11915871 55864506 11254100 77633152 65450803 45404222 88248491
//2328139 68833569 84484742 91643453 19155178 22972630 89124861 92191840 37917430 26036236 50605927 17961383 87387637 84624143 51838240 64544198 67385670 94179595 9084225 81057184 83556907 17099263 78653234 5556927 71897925 37487010 19452400 56793626 66658091 12610715 69172447 63154172 77452579 72859638 74406930
//24195466 19254443 44363078 13147904 55344681 82390365 81419403 61870543 62144103 38746759 99497737 88813748 26843833 55099909 93812968 83352 67196515 24815879 13337908 25849048 44983993 35122953 35604833 10175640 60106567 58741395 53761820 59923877 64991337 95862345 44087642 15675488 73626867 95299715 29532209