#region Spinning
//Videoda anlatıldığı şekliyle spinning (veya busy waiting), bir iş parçacığının (thread) bir kaynağa erişmek için belirli bir koşulun gerçekleşmesini beklerken, boşa beklemek yerine sürekli döngü içerisinde aktif kalarak işlemciyi meşgul etmesi durumudur (11:33 - 12:15).

//Özetle spinning'in mantığı:

//Aktif Bekleme: Thread, koşul sağlanana kadar döngüden çıkmaz ve diğer thread'lere geçiş yapmaz (12:09 - 12:15).
//Kaynak Kullanımı: Sürekli işlemciyi yorduğu için yüksek maliyetlidir ve performans sorunlarına yol açabilir (20:27 - 20:55).
//Ne Zaman Kullanılır? Çok kısa ve kritik durumlarda tercih edilebilir; ancak uzun süreli beklemeler için önerilmez (21:13 - 21:24).

//bool threadCondition = true;
//Thread thread1 = new(() =>
//{
//    while (true)
//    {
//       if (threadCondition)
//        {


//            for (int i = 0; i < 10; i++)
//                Console.WriteLine($"Thread 1 {i}");
//                threadCondition = false;
//            break; }
//    }
//});
//Thread thread2 = new(() =>
//{
//    while (true)
//    {
//        if(!threadCondition)
//        {

//            for (int i = 10; i > 0; i--)
//                Console.WriteLine($"Thread 2 {i}");
//            break; }
//    }
//});
//thread1.Start();
//thread2.Start();
#endregion

#region Monitor.Enter ve Monitor.Exit
//Monitor.Enter ve Monitor.Exit metotları, locking mekanizmasının fonksiyonel versiyonlarıdır.
//Monitor.Enter, lock mekanizmasında olduğu gibi belirli bir nesne üzerinden kilit almaya çalışmaktadır. Eğer başka bir thread bu kilidi almışsa beklemeye alınacaktır.
//Dolayısıyla kilidin alındığı süre boyunca, diğer thread'ler bu kritik bölgeye erişemeyecektirler.  Monitor.Exit ise Monitor.Enter ile kilitlenmiş olan nesneyi serbest bırakır.

//object locking = new();
//int i = 0;
//Thread thread1 = new(() =>
//{
//    try
//    {
//        Monitor.Enter(locking);
//        for (i = 0; i < 10; i++)
//        {
//            Console.WriteLine($"Thread 1 {i}");
//        }
//    }
//    finally
//    {
//        Monitor.Exit(locking);
//    }
//});

//Thread thread2 = new(() =>
//{
//    try
//    {
//        Monitor.Enter(locking);
//        for (i = 0; i < 10; i++)
//        {
//            Console.WriteLine($"Thread 2 {i}");
//        }
//    }
//    finally
//    {
//        Monitor.Exit(locking);
//    }


//});

//thread1.Start();
//thread2.Start();
#endregion

#region lockTaken Parametresi
//Çok nadirde olsa Monitor.Enter metodunun kilitleyememe durumu söz konusu olabilmektedir. Haliyle bu durumu anlayabilmek için 'lockTaken' parametresini kullanabiliriz.
//Yani lockTaken sayesinde, Monitor.Enter ile bir kilidin başarıyla alınıp alınmadığı kontrol edilmektedir. Bu durumu kontrol etmek, thread'in kritik bölgeye güvenli bir şekilde erişip erişemediğini belirlemek için önem arz etmektedir.
//Bu özelliği genellikle birden fazla thread ile çalışırken, bir thread'in kilidi aldığını ve diğerlerinin kilidi alamadığını kontrol etmek için kullanmayı tercih ediyoruz.

//object locking = new();
//int i = 0;

//Thread thread1 = new(() =>
//{
//	try
//	{
//		bool lockTaken = false;
//		Monitor.Enter(locking, ref lockTaken);

//		if (lockTaken)
//			for (i = 0; i < 10; i++)
//				Console.WriteLine($"Thread 1 : {i}");

//	}
//	finally { Monitor.Exit(locking); }
//});
//Thread thread2 = new(() =>
//{
//    try
//    {
//        bool lockTaken = false;
//        Monitor.Enter(locking, ref lockTaken);

//        if (lockTaken)
//            for (i = 0; i < 10; i++)
//                Console.WriteLine($"Thread 2 : {i}");

//    }
//    finally { Monitor.Exit(locking); }
//});

//thread1.Start();
//thread2.Start();


#endregion

#region Monitor.TryEnter
//Monitor.TryEnter bir nesne üzerinde lock almayı deneyen ve alınıp alınmadığını kontrol eden bir C# senkronizasyon yöntemidir.
//Bu metot ile bir kodu kilitlemeye çalışırken rekabet eden diğer thread'lerle ilişkili riskler kontrol edilebilmektedir.
//Bir kodun akışı verilen milisaniye cinsinden süre zarfından kilitlenmeye çalışılmakta ve eğer başarılı olunursa kilitlenme sağlanmakta ve geriye true değeri döndürülmektedir. Aksi taktirde false döndürülecektir.
//Hele hele kritik bölgeye ulaşmada geçici bir başarısızlığa izin vermek ve ardından tekrar denemek istenilen durumlarda oldukça kullanışlıdır!
//Ancak bu konuda dikkatli olunmalıdır! Sürekli tekrar denemek yerine bir süre beklemenin daha uygun olduğu durumlar daha fazladır

//object locking = new();
//int i = 0;
//Thread thread1 = new(() =>
//{
//	var result = Monitor.TryEnter(locking, 100);
//	if (result)
//		try
//		{
//			for (i = 0; i < 10; i++)
//			{
//				Console.WriteLine($"Thread 1 : {i}");
//			}
//		}
//		finally
//		{
//			Monitor.Exit(locking);
//		}
//});
//Thread thread2 = new(() =>
//{
//    var result = Monitor.TryEnter(locking, 1);
//    if (result)
//        try
//        {
//            for (i = 0; i < 10; i++)
//            {
//                Console.WriteLine($"Thread 2 : {i}");
//            }
//        }
//        finally
//        {
//            Monitor.Exit(locking);
//        }
//});

//thread1.Start();
//thread2.Start();

#endregion

#region Mutex Sınıfı
//Mutex, locking mekanizmasına benzer şekilde bilgisayar seviyesinde process'ler arası kilitleme işlemi Yürütebilen özel bir yapılanmadır,
//Anlam olarak mutual exclusion kelimelerinin kısaltması olan Mutex, kritik bölgelere erişimi kontrol etmek için kullanılan bir senkronizasyon mekanizmasıdır.
//Bu sınıf sayesinde bizİer ortak kaynaklara yahut kritik bölgeye eş zamanlı olarak erişimi kontrol edebilmekte ve senkronizasyonu sağlayabilmekteyiz.
//Mutex mutex = new();
//Thread thread1 = new(() =>
//{
//    mutex.WaitOne();
//    for (int i = 0; i < 10; i++)
//        Console.WriteLine($"Thread 1 : {i}");
//    mutex.ReleaseMutex();
//});
//Thread thread2 = new(() =>
//{
//    mutex.WaitOne();
//    for (int i = 0; i < 10; i++)
//        Console.WriteLine($"Thread 2 : {i}");
//    mutex.ReleaseMutex();
//});

//thread1.Start();
//thread2.Start();
#endregion

#region Mutex İle Single Instance Application
//Ayrıca ilk cümlede de belirttiğimiz gibi Mutex ile process seviyesinde de kilitleme yapabilmekteyiz. Bunun en güzel örneklendirmesi Single Instance Application'dır.
//Single Instance Application ile derlenmiş bir uygulamanın sade ve sadece tek bir instance'ının çalıştırılmasını sağlayabiliriz.

//internal class Program
//{
//    static Mutex _mutex;
//    static string _programName = "Albeyazım Soft";
//    private static void Main(string[] args)
//    {
//        Mutex.TryOpenExisting(_programName, out _mutex);
//        if(_mutex is null)
//        {
//            _mutex = new Mutex(true,_programName);
//            Console.WriteLine("Program ayakta");
//            Console.Read();
//        }
//        else
//        {
//            _mutex.Close();
//            return;
//        }
//    }
//}
#endregion

#region .Net 9 Yeni Locking Referansı
//Evet, Lock sınıfı sade ve sadece locking sürecinde kullanılması için oluşturulmuştur ve bu tür muhtemelen eski yaklaşma nazaran daha hızlı ve performanslı bir davranış sunmaktadır.
Lock locking = new();
int i = 0;

Thread thread1 = new(() =>
{
	lock (locking)
	{
		for (i = 0; i < 10; i++)
		{
			Console.WriteLine($"Thread 1 : {i}");
			Thread.Sleep (1000);
		}
	}
});
Thread thread2 = new(() =>
{
	lock (locking)
	{
		for (i = 0; i < 10; i++)
		{
			Console.WriteLine($"Thread 1 : {i}");
            Thread.Sleep(1000);

        }
    }
});

thread1.Start();

#endregion