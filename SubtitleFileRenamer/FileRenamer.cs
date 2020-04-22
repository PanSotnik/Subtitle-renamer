using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleFileRenamer
{
    class FileRenamer
    {
        static void Main(string[] args)
        {
            FileRenamer renamer = new FileRenamer();
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Ensure the application is running out of the target directory and press a key. Media and subtitle files must be located here. /");
            Console.WriteLine("Убедитесь, что программа запущена из целевой папки и нажмите любую клавишу. Файлы медиа, а также субтитров, должны находиться здесь.");
            Console.ReadLine();

            renamer.PerformOps();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit. /");
            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadLine();
        }

        private void PerformOps()
        {
            List<string> videoExtensions = new List<string> { ".MP4", ".MKV", ".AVI", ".MOV", ".WMV" };

            List<string> folderFiles = Directory.GetFiles(Directory.GetCurrentDirectory()).ToList();

            List<string> subtitleFiles = new List<string>(folderFiles.Where(f => f.EndsWith(".srt")));

            List<string> videoFiles = new List<string>(folderFiles.Where(f => videoExtensions.Any(f.ToUpper().EndsWith)));

            if (videoFiles.Count != subtitleFiles.Count)
            {
                Console.WriteLine("The number of subtitle files must be equal to the number of videos. /");
                Console.WriteLine("Количество файлов видео и субтитров должно быть равно.");
                Console.WriteLine();
                Console.WriteLine($"SRT files: {subtitleFiles.Count}");
                Console.WriteLine($"Video files: {videoFiles.Count}");
                return;
            }

            for (int i = 0; i < subtitleFiles.Count; i++)
            {
                try
                {
                    File.Move(subtitleFiles[i], $"{videoFiles[i].Substring(0, videoFiles[i].LastIndexOf(".", StringComparison.Ordinal))}.srt");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
