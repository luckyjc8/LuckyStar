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
                paths[Int32.Parse(inputs[1])].AddLast(Int32.Parse(inputs[0]));
            }
            
            return paths; //hasil akhir array of LinkedList<int>
        }

        public static List<string[]> SolveBulk(string path, LinkedList<int>[] paths)
        //menjawab pertanyaan beruntun Ferdiant dari membaca berkas eksternal
        {
            int instances = new int();
            List<string[]> answers = new List<string[]>(); // penampung jawaban
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
                        answers.Add(Solve(Int32.Parse(inputs[0]), Int32.Parse(inputs[1]), Int32.Parse(inputs[2]), paths, "", visited));
                    }
                }
            }
            return answers; //List of jawaban dikembalikan
        }

        public static string[] Solve(int a,int b,int c, LinkedList<int>[] paths, string temp, bool[] visited)
        //Menjawab pertanyaan dari Ferdiant
        {
            string old_temp = temp; // variasi baru dari path
            temp += c + "-";
            string[] ans = new string[paths.Length];
            if (c == b) // rumah ditemukan
            {
                ans[0] = "YA";
                ans[ans.Length] = temp;
                temp = ""; //temp direset
                return ans;
            }
            else if (c == 1 || paths[c].Count == 0) //jalan buntu, rumah tidak ditemukan
            {
                ans[0] = "TIDAK";
                ans[ans.Length] = temp;
                temp = ""; //temp direset
                return ans;
            }
            else //masih bisa jalan ke rumah berikutnya
            {

                foreach (int p in paths[c]) //semua kemungkinan rumah yang bisa dicapai
                {
                    if (!visited[p])
                    {
                        visited[p] = true;
                        if ((a == 0 && p < b) || (a == 1 && p > b)) //validasi rute sesuai syarat mendekati/menjauhi istana
                        {
                            string[] ans_temp = Solve(a, b, p, paths, temp, visited);
                            for (int i = ans.Length; i <= ans_temp.Length; i++)
                            {
                                ans[i] = ans_temp[i]; //enumerasi path ditambah.
                            }
                        }
                        if (ans[0] == "YA") // solusi ditemukan
                        {
                            break;
                        }
                    }
                }
                
            }
            temp = old_temp; //variasi selesai
            return ans; //solusi dikembalikan
        }
    }
}
