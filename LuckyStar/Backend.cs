using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LuckyStar
{
    public class Backend
    {

        public static LinkedList<int>[] ReadMap(string path)

        {
            path += ".txt";
           
            string text = File.ReadAllText(@path, Encoding.UTF8); //membaca semua lines
            string[] txt = text.Split('\n'); //memecah pembacaan menjadi per baris
            int houses = Int32.Parse(txt[0]); //membaca jumlah rumah
            
            LinkedList<int>[] paths = new LinkedList<int>[houses+1]; //alokasi data sesuai jumlah rumah
            for (int i = 0; i <= houses; i++) //untuk sebanyak (jumlah rumah-1), lakukan :
            {
                paths[i] = new LinkedList<int>(); //alokasi data LinkedList<int>
            }

            paths[0].AddLast(houses);
            for (int i = 1; i <= houses-1; i++)
            {
                string[] inputs = txt[i].Split(' ');
                paths[Int32.Parse(inputs[0])].AddLast(Int32.Parse(inputs[1]));
                paths[Int32.Parse(inputs[1])].AddLast(-Int32.Parse(inputs[0]));
            }
            
            /* Untuk testing. Melihat isi paths.
            foreach(LinkedList<int> p2 in paths)
            {
                Console.WriteLine(String.Join(" ", p2));
            } */

            return paths; //hasil akhir array of LinkedList<int>
        }

        public static List<List<string>> SolveBulk(string path, LinkedList<int>[] paths)
        //menjawab pertanyaan beruntun Ferdiant dari membaca berkas eksternal
        {
            int instances = new int();
            List<List<string>> answers = new List<List<string>>(); // penampung jawaban
            string text = File.ReadAllText(@path, Encoding.UTF8);
            string[] txt = text.Split('\n');
            bool first = true;
            int data_no = 1; //merepresentasikan nomor data yang sudah diproses + 1
            foreach (string input in txt) //pembacaan tiap baris
            {
                if (first) //assignment khusus untuk baris pertama
                {
                    instances = Int32.Parse(input);
                    first = false;
                }
                else
                {
                    if (data_no > instances) //failsafe kasus pertanyaan di file eksternal > Q di baris pertama file eksternal
                    {
                        break;
                    }
                    else //kasus normal, pertanyaan dibaca dan diproses dengan fungsi Solve
                    {
                        string[] inputs = input.Split(' ');
                        bool[] visited = new bool[paths.Length];
                        for(int i = 0; i < paths.Length; i++)
                        {
                            visited[i] = false;
                        }
                        List<string> enumeration = new List<string>();
                        string ans = Solve(Int32.Parse(inputs[0]), Int32.Parse(inputs[1]), Int32.Parse(inputs[2]), paths, "", enumeration) ? "YA" : "TIDAK";
                        enumeration.Add(ans);
                        answers.Add(enumeration);
                    }
                }
            }
            return answers; //List of jawaban dikembalikan
        }

        public static bool ExistsInPath(int p, string temp)
        {
            String[] arr = temp.Split('-');
            bool found = false;
            foreach(string a in arr)
            {
                if(a == p.ToString())
                {
                    found = true; break;
                }
            }
            return found;

        }

        public static bool Solve(int a,int b,int c, LinkedList<int>[] paths, string temp,List<string> enumerations)
        //Menjawab pertanyaan dari Ferdiant
        {
            string old_temp = temp; // variasi baru dari path
            bool found = false; //penampung jawaban
            temp += c + "-";
            if (c == b) // rumah ditemukan
            {
                enumerations.Add(temp);
                temp = ""; //temp direset
                return true;
            }
            else if (c == 1 && a == 0) //jalan sampai istana, rumah tidak ditemukan
            {
                enumerations.Add(temp);
                temp = ""; //temp direset
                return false;
            }
            else //masih bisa jalan ke rumah berikutnya
            {
                int count = 0;
                foreach (int p in paths[c]) //semua kemungkinan rumah yang bisa dicapai
                {
                    int new_p = p > 0 ? p : -p;
                    if (!ExistsInPath(new_p, temp))
                    {
                        if ((a == 0 && p<0) || (a == 1 && p>0)) //validasi rute sesuai syarat mendekati/menjauhi istana
                        {
                            found = found || Solve(a, b, new_p, paths, temp, enumerations);
                            count++;
                        }
                    }
                    
                    if (found) { break; } // solusi ditemukan
                }
                if (count == 0) //jika jalan buntu, rumah tidak ditemukan
                {
                    enumerations.Add(temp);
                    temp = ""; //temp direset
                    return false;
                }
            }
            temp = old_temp; //variasi selesai
            
            return found; //solusi dikembalikan
        }
    }
}
