using System;

namespace SimpleMenu
{
    class Karakter
    {
        // Encapsulation
        public string nama { get; private set; }
        public int kesehatan { get; private set; }
        public int senjata { get; private set; }
        public int healing { get; private set; }
        public int posisi { get; private set; }

        // Constructor
        public Karakter(string nama, int kesehatan, int senjata,
                        int healing, int posisi = 0)
        {
            this.nama = nama;
            this.kesehatan = kesehatan;
            this.senjata = senjata;
            this.healing = healing;
            this.posisi = posisi;
        }

        // Menampilkan informasi karakter
        public void Tampilkan()
        {
            Console.WriteLine($"Nama      : {nama}");
            Console.WriteLine($"Kesehatan : {kesehatan}");
            Console.WriteLine($"Senjata   : {senjata}");
            Console.WriteLine($"Healing   : {healing}");
            Console.WriteLine($"Posisi    : {posisi}");
            Console.WriteLine();
        }

        // Menyerang karakter lain
        public void Serang(Karakter target)
        {
            Console.WriteLine($"{nama} menyerang {target.nama}!");
            target.kesehatan -= senjata;

            if (target.kesehatan < 0)
                target.kesehatan = 0;

            Console.WriteLine(
                $"{target.nama} menerima {senjata} damage.");
            Console.WriteLine(
                $"Kesehatan {target.nama}: {target.kesehatan}");
        }

        // Menggunakan potion
        public void PakaiHealing()
        {
            kesehatan += healing;

            Console.WriteLine($"{nama} menggunakan Potion!");
            Console.WriteLine(
                $"Kesehatan sekarang: {kesehatan}");
        }

        // Perpindahan menggunakan angka
        public void PindahPosisiAngka()
        {
            while (true)
            {
                Console.WriteLine($"\nPosisi {nama}: {posisi}");
                Console.Write("Masukkan angka perpindahan (0 untuk berhenti): ");

                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int angka))
                {
                    if (angka == 0)
                        break;

                    posisi += angka;

                    Console.WriteLine(
                        $"Posisi sekarang: {posisi}");
                }
                else
                {
                    Console.WriteLine("Input harus berupa angka!");
                }
            }
        }

        // Perpindahan menggunakan keyboard
        public void PindahPosisiKeyboard()
        {
            Console.WriteLine("\nKontrol:");
            Console.WriteLine("Panah kanan = maju");
            Console.WriteLine("Panah kiri  = mundur");
            Console.WriteLine("Esc         = kembali");

            while (true)
            {
                ConsoleKeyInfo tombol = Console.ReadKey(true);

                if (tombol.Key == ConsoleKey.RightArrow)
                {
                    posisi += 2;
                    Console.WriteLine($"Maju! Posisi: {posisi}");
                }
                else if (tombol.Key == ConsoleKey.LeftArrow)
                {
                    posisi -= 1;
                    Console.WriteLine($"Mundur! Posisi: {posisi}");
                }
                else if (tombol.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Membuat objek karakter
            Karakter player = new Karakter(
                "Arlot", 100, 40, 20);

            Karakter musuh = new Karakter(
                "Yuzhong", 100, 30, 0);

            Karakter karakter3 = new Karakter(
                "Franco", 120, 35, 25);

            bool statusGame = true;

            while (statusGame)
            {
                Console.Clear();

                Console.WriteLine("===== MENU GAME =====");
                Console.WriteLine("1. Informasi Karakter");
                Console.WriteLine("2. Mainkan Permainan");
                Console.WriteLine("3. Perpindahan Angka");
                Console.WriteLine("4. Tombol Keyboard");
                Console.WriteLine("5. Keluar");
                Console.WriteLine("=====================");

                Console.Write("Pilihanmu: ");
                string pilihan = Console.ReadLine() ?? "";

                switch (pilihan)
                {
                    case "1":
                        InfoKarakter(player, musuh, karakter3);
                        break;

                    case "2":
                        MainkanGame(player, musuh);
                        break;

                    case "3":
                        player.PindahPosisiAngka();
                        break;

                    case "4":
                        player.PindahPosisiKeyboard();
                        break;

                    case "5":
                        Console.WriteLine("Keluar dari permainan!");
                        statusGame = false;
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }

                if (statusGame)
                {
                    Console.WriteLine("\nTekan tombol untuk kembali...");
                    Console.ReadKey(true);
                }
            }
        }

        // Menu informasi karakter
        static void InfoKarakter(
            Karakter player,
            Karakter musuh,
            Karakter karakter3)
        {
            Console.WriteLine("\n===== INFORMASI KARAKTER =====");

            player.Tampilkan();
            musuh.Tampilkan();
            karakter3.Tampilkan();
        }

        // Menu simulasi pertarungan
        static void MainkanGame(
            Karakter player,
            Karakter musuh)
        {
            Console.WriteLine("\n===== SIMULASI PERTARUNGAN =====");

            player.Serang(musuh);

            if (musuh.kesehatan > 0)
            {
                musuh.Serang(player);
            }

            Console.WriteLine("\n===== STATUS KARAKTER =====");
            player.Tampilkan();
            musuh.Tampilkan();

            Console.Write("Gunakan Potion? (Y/N): ");
            string pilihan = (Console.ReadLine() ?? "").ToUpper();

            if (pilihan == "Y")
            {
                player.PakaiHealing();
            }
            else
            {
                Console.WriteLine("Potion tidak digunakan.");
            }
        }
    }
}