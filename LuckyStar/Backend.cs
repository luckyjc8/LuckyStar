using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LuckyStar
{
    class Backend
    {
        public static LinkedList<int>[] ReadMap(string path)
        //Membaca berkas pemetaan dan menyimpan datanya dalam bentuk array of LinkedList<int>
        {
            int houses;
            LinkedList<int>[] paths = new LinkedList<int>[100000];

            string text = File.ReadAllText(@path, Encoding.UTF8);
            string[] txt = text.Split('\n');
            bool first = true;
            foreach (string input in txt) //pembacaan tiap baris
            {
                if (first) //assignment khusus untuk baris pertama
                {
                    houses = Int32.Parse(input);
                    first = false;
                    for(int i = 0; i < houses-1; i++) //untuk sebanyak (jumlah rumah-1), lakukan :
                    {
                        paths[i] = new LinkedList<int>(); //alokasi data LinkedList<int>
                    }
                }
                else //menyalin rute antar rumah sebagai elemen LinkedList<int>
                {
                    string[] inputs = input.Split(' ');
                    paths[Int32.Parse(inputs[0])].AddLast(Int32.Parse(inputs[1]));
                }
            }
            return paths; //hasil akhir array of LinkedList<int>
        }

        public static List<bool> SolveBulk(string path, LinkedList<int>[] paths)
        //menjawab pertanyaan beruntun Ferdiant dari membaca berkas eksternal
        {
            int instances = new int();
            List<bool> answers = new List<bool>(); // penampung jawaban
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
                        answers.Add(Solve(Int32.Parse(inputs[0]), Int32.Parse(inputs[1]), Int32.Parse(inputs[2]), paths));
                    }
                }
            }
            return answers; //List of jawaban dikembalikan
        }

        public static bool Solve(int a,int b,int c, LinkedList<int>[] paths)
        //Menjawab pertanyaan dari Ferdiant
        {
            if (c == b) // rumah ditemukan
            {
                return true;
            }
            else if (c == 1 || paths[c].Count == 0) //jalan buntu, rumah tidak ditemukan
            {
                return false;
            }
            else //masih bisa jalan ke rumah berikutnya
            {
                bool found = false;
                foreach (int p in paths[c]) //semua kemungkinan rumah yang bisa dicapai
                {
                    if ((a == 0 && p < b) || (a == 1 && p > b)) //validasi rute sesuai syarat mendekati/menjauhi istana
                    {
                        found = found && Solve(a, b, p, paths); //rekursi Solve dari rumah tujuan (DFS)
                    }
                    if (found) // solusi ditemukan
                    {
                        break;
                    }
                }
                return found; //solusi dikembalikan
            }
        }
    }
}
