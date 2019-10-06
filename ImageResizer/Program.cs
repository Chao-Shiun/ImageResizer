using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;
            CancellationTokenSource cts = new CancellationTokenSource();
            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var taskArr = imageProcess.ResizeImages(sourcePath, destinationPath, 2.0, cts);
            //cts.Cancel();
            //while (true)
            //{
            //    sw.Stop();
            //    if (sw.Elapsed.TotalMilliseconds > 0.001)
            //    {
            //        cts.Cancel();
            //        sw.Start();
            //        break;
            //    }
            //    sw.Start();
            //}
            //await Task.WhenAll(taskArr);
            var cancelList = taskArr.Where(x => x.IsCanceled).ToList();
            if (cancelList.Any())
            {
                Console.WriteLine($"已取消的Task有");
                foreach (var item in cancelList)
                {
                    Console.WriteLine(item.Id);
                }
            }

            sw.Stop();

            Console.WriteLine($"花費時間: {sw.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }
    }
}
