

#region Thread Sınıfı
//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        Thread thread = new Thread(() =>
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                Console.WriteLine($"Worker Thread çalıştı{i}");

//            }
//        });

//        thread.Start();
//        for (int i = 0; i < 10; i++)
//        {
//            Console.WriteLine($"Ana Thread sonlandı{i}");

//        }
//    }

//}
#endregion
#region Thread Id
//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        Console.WriteLine($"Main Thread Idleri");
//        Console.WriteLine(Environment.CurrentManagedThreadId);
//        Console.WriteLine(AppDomain.GetCurrentThreadId());//Kararlı olmadığı depracted ediliştir.
//        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

//        Thread thread = new(() =>
//        {

//            Console.WriteLine($"Worker Thread Idleri");
//            Console.WriteLine(Environment.CurrentManagedThreadId);
//            Console.WriteLine(AppDomain.GetCurrentThreadId());
//            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//        });

//        thread.Start();
//    }
//}
#endregion
#region IsBackground
//int i = 10;
//Thread thread1 = new(() =>
//{
//    while (i >= 10)
//    {
//        i--;
//        Thread.Sleep(1000);
//    }
//    Console.WriteLine("Worker Thread Görevini Tamamladı.");
//});
//thread1.IsBackground=false;
//thread1.Start();
//Console.WriteLine("Main Thread görevini tamamladı.");
#endregion
#region Thread State
//Running : Thread'in çalıştığını ifade eder.
//StopRequested: Thread'in durdurulması istenmiştir.
//SuspendRequested: Thread'in askıya alınmasını istenmiştir.
//Background: Arka planda çalışan Thread'i ifade eder.
// Unstarted: Thread henüz başlatılmamıştır.
//Stopped: Thread sonlandırılmıştır.
//WaitSleepJoin: Thread ya başka thread'i beklemekte ya da uykudadır.
//Suspend: Thread askıda bekletilmektedir.
//AbortRequested: Thread'in sonlandırılması talep edilmiştir.
// Aborted: Thread aniden sonlandırılmıştır.

//int i = 10;
//Thread thread1 = new(() =>
//{
//    while (i>=0)
//    {
//        i--;
//        Thread.Sleep(1000);
//    }
//    Console.WriteLine("Worker Thread görevini tamamladı.");
//});
//thread1.Start();
//ThreadState state = ThreadState.Running;
//while (true)
//{
//    if (thread1.ThreadState == ThreadState.Stopped)
//        break;
//    if (state!=thread1.ThreadState)
//    {
//        state = thread1.ThreadState;
//    Console.WriteLine(thread1.ThreadState);

//    }

//}
//Console.WriteLine("Main Thread görevini tamamladı");
#endregion
#region Race Conditions
//Race Conditions, multithread programlama süreçlerinde iki veya daha fazla thread'in aynı kaynağa(veri, değişken, bellek alanı vs.) eş zamanlı olarak erişmesi durumunda ortaya çıkan istenmeyen durumlardır.
//Ya da bir thread henüz diğer threacfin tamamlanmamış işi üzerinde işlem yaparak da race condition durumuna sebebiyet verebilir.

//race conditions'ı onlemek içın ne yapmalıyız?
//Senkronizasyon teknikleri kullanacağız. BU teknikler sayesinde birden çok throad'i aynı anda bir kaynağa kontrollü bir şekilde eriştirebilecek ve kritik arz eden kaynaklarda yalnız bir thread'in çalışmasına izin verıyor olacağız.

#endregion
#region Thread'ler Arası Locking
//Locking mokanizmostyla kısıtlanan bölecyc tok bir thread erişirken, diğer threaffler kritik bölgenin kilidinin açılmasını beklerler.
//Locking mekanizması, genellikle kodda kritik bölge(critical section) olarak adlandırdığımız bölgede kullanılmaktadır.

//object locking = new();
//int i = 1;
//Thread thread1 = new(() =>
//{
//    lock (locking)
//    {
//        while (i < 10)
//        {
//            i++;
//            Console.WriteLine($"Thread 1 :{i}");
//        }
//    }

//});
//Thread thread2 = new(() =>
//{
//    lock (locking)
//    {
//        while (i > 0)
//        {
//            i--;
//            Console.WriteLine($"Thread 2 :{i}");
//        }
//    }
//});
//thread1.Start();
//thread2.Start();
//Console.WriteLine("Main Thread Sonlandı");
#endregion
#region Sleep
//ideal bir thread çatışmasında Thread.SIeep metodunun 0 saniye verilmişte olsa kullanılması önerilir. özellikle işlem hızı açısından bir endişe durumu söz konusuysa Thread.Sleep(O) diyerek ilgili thrcad ve CPU için bir rahatlama zamanı oluşturulabilir.
//Thread thread = new(() =>
//{
//    for (int i = 0; i < 10; i++)
//    {
//        Console.WriteLine(i);
//        Thread.Sleep(1000);
//    }
//});
//thread.Start();
#endregion
#region Join
//Bir thread'in, başka bir thread'in işleminin bitmesini beklemesi için kullanılan metottur.
//Bu boklomc 3ürccindo main thread'de bloklanacaktır.
//Thread thread1 = new(() =>
//{
//    for (global::System.Int32 i = 0; i < 10; i++)
//    {
//        Console.WriteLine($"Thread 1 {i}");   
//    }
//});
//Thread thread2 = new(() =>
//{
//    for (global::System.Int32 i = 0; i < 10; i++)
//    {
//        Console.WriteLine($"Thread 2 {i}");
//    }
//});
//thread1.Start();
//thread1.Join();
//thread2.Start();
#endregion
#region Thread İptal Etme
//Bir thread'in çalışmasını iptal etmek için eskiden Thread.Abort metodunu kullanıyordukl Ancak bu metot thread'i aniden durdurması ve bundan dolayı kaynakların düzgün bir şekilde temizlenmemesi durumuna neden olmasından dolayı deprecated edilmiştir.
//Bu nedenle artık bir thread'i durdurmak İçin daha güvenli yöntem Olan 'işaretle ve biti" (graceful shutdown) yaklaşımını kullonworuz.
//İşaretle ve bitir yaklaşımında thread içerisinde belirli bir şart yahut flag kontrol edilir bu şart gerçekleştiğinde thread'in çalışması sona erdirilir.

//bool shouldStop = false;
//int i = 0;
//Thread thread = new(()=>
//{
//    while (!shouldStop)
//    {
//        if (shouldStop) break;
//        i++;
//        Console.WriteLine("Worker Thread Görevde "+i);
//    }
//    Console.WriteLine("Worker Thread Sonlandı.");
//});
//thread.Start();
//Thread.Sleep(2000);
//shouldStop = true;

//Yandaki örneğe göz atarsamz eğer shouldStop isimli bir değişken şart ya da flag olarak kullanılmaktadır. işte bu değişken üzerinden işaretleme yapmakta ve uygun olduğu zaman da thread'i sonlandırarak bitirme işlemi uygulanmaktadır.
//Ya da benzer mantığı sonraki derslerimiz de oldukça detaylı İnceleyeceğimiz CancollationTokenSource ile do yandaki gibi kurgulayabilir ve çalışan threadleri güvonilir bir şekilde İptal edebilirz.

//int i = 0;
//Thread thread = new((cancellationToken) =>
//{
//    var cancel = (CancellationTokenSource)cancellationToken;
//    while (true)
//    {
//        if (cancel.IsCancellationRequested) break;
//        i++;
//        Console.WriteLine("Worker Thread Görevde " + i);
//    }
//    Console.WriteLine("Worker Thread Sonlandı.");
//});
//CancellationTokenSource source = new CancellationTokenSource();

//thread.Start(source);
//Thread.Sleep(2000);
//source.Cancel();

#endregion
#region Interrupt
//Bir thread'i bekleyen durumdan uyandırmak ve çalışma durumunu kesintiye uğratmak için kullanılan metottur.
//Ancak, Intertupt metodunun kullanımı dikkatlice ele alınmalıdır, çünkü uyandırılan thread hala bekleyen bir durumdayasa (Sleep ya da Wait gibi) ThreadInterruptException hatası fırlatılır.
Thread thread = new(() =>
{
	try
	{
        Thread.Sleep(Timeout.Infinite);
    }
	catch (ThreadInterruptedException ex)
	{

        Console.WriteLine(ex.Message);
	}
});
thread.Start();
thread.Interrupt();

//Intorrupt motcdu ilc bir thrcad'i boklomo durumundayken tamamlanmaya zorlayabiliriz,
//Ya da uykuda olan bif thread'i uyandırmak ve işlemlerine devam ettirmek amacıyla da kullanabiliriz.
#endregion