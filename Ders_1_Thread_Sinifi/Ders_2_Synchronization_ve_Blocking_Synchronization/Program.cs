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

object locking = new();
int i = 0;
Thread thread1 = new(() =>
{
    try
    {
        Monitor.Enter(locking);
        for (i = 0; i < 10; i++)
        {
            Console.WriteLine($"Thread 1 {i}");
        }
    }
    finally
    {
        Monitor.Exit(locking);
    }
});

Thread thread2 = new(() =>
{
    try
    {
        Monitor.Enter(locking);
        for (i = 0; i < 10; i++)
        {
            Console.WriteLine($"Thread 2 {i}");
        }
    }
    finally
    {
        Monitor.Exit(locking);
    }


});

thread1.Start();
thread2.Start();
#endregion