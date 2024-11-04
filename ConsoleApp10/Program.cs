using System;
using System.Collections.Generic;

// Ürün sınıfı: Ürün bilgilerini tutmak için kullanıyoruz.
class Urun
{
    public int Id; // Her ürünün benzersiz ID'si
    public string Ad; // Ürün adı
    public int Miktar; // Ürün miktarı

    public Urun(int id, string ad, int miktar)
    {
        Id = id; // Ürün ID'sini ayarlıyoruz
        Ad = ad; // Ürün adını atıyoruz
        Miktar = miktar; // Ürün miktarını belirliyoruz
    }
}

// Linked List düğümü: Ürünleri temsil eden her bir düğüm.
class LinkedListNode
{
    public Urun Urun; // Bu düğümdeki ürün bilgisi
    public LinkedListNode Sonraki; // Sonraki düğümü işaret eden referans

    public LinkedListNode(Urun urun)
    {
        Urun = urun; // Düğümün ürününü ayarlıyoruz
        Sonraki = null; // Başlangıçta sonraki düğüm yok
    }
}

// Linked List yapısı: Ürünleri saklamak için oluşturuyoruz.
class LinkedList
{
    private LinkedListNode bas; // Listenin başını tutuyoruz
    private LinkedListNode son; // Listenin sonunu tutuyoruz

    // Yeni bir ürün eklemek için bu metodu kullanıyoruz.
    public void Ekle(Urun urun)
    {
        var yeniDugum = new LinkedListNode(urun); // Yeni bir düğüm oluşturuyoruz
        if (son == null) // Eğer liste henüz boşsa
        {
            bas = son = yeniDugum; // Hem baş hem de son düğüm bu yeni düğüm olacak
        }
        else
        {
            son.Sonraki = yeniDugum; // Son düğümün sonraki düğümünü yeni düğüm yapıyoruz
            son = yeniDugum; // Şimdi son düğüm yeni düğüm
        }
    }

    // Listede bulunan ürünleri gösteren metot.
    public void Listele()
    {
        var mevcut = bas; // Baş düğümden başlıyoruz
        while (mevcut != null) // Listenin sonuna kadar gidiyoruz
        {
            Console.WriteLine($"ID: {mevcut.Urun.Id}, Ad: {mevcut.Urun.Ad}, Miktar: {mevcut.Urun.Miktar}");
            mevcut = mevcut.Sonraki; // Bir sonraki düğüme geçiyoruz
        }
    }

    // Ürünü silmek için bu metodu kullanıyoruz (başından).
    public void Sil()
    {
        if (bas == null) // Eğer liste boşsa
        {
            Console.WriteLine("Silinecek ürün yok."); // Kullanıcıya mesaj veriyoruz
            return; // İşlemi sonlandırıyoruz
        }
        bas = bas.Sonraki; // Baş düğümünü bir sonraki düğümle değiştiriyoruz
        if (bas == null) // Eğer yeni baş düğüm yoksa
            son = null; // Son düğümü de null yapıyoruz
    }

    // Ürün aramak için bu metodu kullanıyoruz.
    public void Ara(int id)
    {
        var mevcut = bas; // Baş düğümden başlıyoruz
        while (mevcut != null) // Listeyi kontrol ederek arıyoruz
        {
            if (mevcut.Urun.Id == id) // Eğer ID eşleşiyorsa
            {
                Console.WriteLine($"Bulundu! ID: {mevcut.Urun.Id}, Ad: {mevcut.Urun.Ad}, Miktar: {mevcut.Urun.Miktar}");
                return; // İşlemi sonlandırıyoruz
            }
            mevcut = mevcut.Sonraki; // Bir sonraki düğüme geçiyoruz
        }
        Console.WriteLine("Ürün bulunamadı."); // Eğer ürün yoksa kullanıcıya mesaj veriyoruz
    }

    // Ürünleri miktarlarına göre sıralamak için bu metodu kullanıyoruz.
    public void Sirala()
    {
        if (bas == null) return; // Eğer liste boşsa, işlem yapmıyoruz

        var urunler = new List<Urun>(); // Ürünleri geçici bir listeye alıyoruz
        var mevcut = bas;
        while (mevcut != null)
        {
            urunler.Add(mevcut.Urun); // Ürünleri listeye ekliyoruz
            mevcut = mevcut.Sonraki;
        }

        // Miktarlarına göre sıralıyoruz
        urunler.Sort((x, y) => x.Miktar.CompareTo(y.Miktar));

        // Sıralanmış ürünleri gösteriyoruz
        foreach (var urun in urunler)
        {
            Console.WriteLine($"ID: {urun.Id}, Ad: {urun.Ad}, Miktar: {urun.Miktar}");
        }
    }
}

// Program sınıfı: Ana uygulama
class Program
{
    static void Main(string[] args)
    {
        LinkedList kuyruk = new LinkedList(); // Yeni bir kuyruk oluşturuyoruz
        Stack<Urun> yigin = new Stack<Urun>(); // Yeni bir yığın oluşturuyoruz

        Console.WriteLine("Kuyruk (1) veya Yığın (2) seçin:");
        int secim = int.Parse(Console.ReadLine()); // Kullanıcıdan seçim alıyoruz

        while (true)
        {
            Console.WriteLine("Ekle (1), Sil (2), Ara (3), Listele (4), Sırala (5), Çıkış (0):");
            int islem = int.Parse(Console.ReadLine()); // Yapılacak işlemi alıyoruz

            switch (islem)
            {
                case 1: // Ürün ekleme işlemi
                    Console.WriteLine("Ürün ID girin:");
                    int id = int.Parse(Console.ReadLine()); // Ürün ID'si alıyoruz
                    Console.WriteLine("Ürün adı girin:");
                    string ad = Console.ReadLine(); // Ürün adını alıyoruz
                    Console.WriteLine("Ürün miktarını girin:");
                    int miktar = int.Parse(Console.ReadLine()); // Ürün miktarını alıyoruz
                    var urun = new Urun(id, ad, miktar); // Yeni bir ürün oluşturuyoruz

                    if (secim == 1) // Kuyruk seçilmişse
                    {
                        kuyruk.Ekle(urun); // Ürünü kuyruğa ekliyoruz
                    }
                    else if (secim == 2) // Yığın seçilmişse
                    {
                        yigin.Push(urun); // Ürünü yığın'a ekliyoruz
                    }
                    break;

                case 2: // Ürün silme işlemi
                    if (secim == 1) // Kuyruk ise
                    {
                        kuyruk.Sil(); // Ürünü kuyruktan siliyoruz
                    }
                    else if (secim == 2) // Yığın ise
                    {
                        if (yigin.Count > 0) // Yığın boş değilse
                        {
                            var silinen = yigin.Pop(); // Son ürünü siliyoruz
                            Console.WriteLine($"Silinen ürün: ID: {silinen.Id}, Ad: {silinen.Ad}, Miktar: {silinen.Miktar}");
                        }
                        else
                        {
                            Console.WriteLine("Yığın boş."); // Eğer yığın boşsa
                        }
                    }
                    break;

                case 3: // Ürün arama işlemi
                    Console.WriteLine("Aranacak ürün ID'sini girin:");
                    int araId = int.Parse(Console.ReadLine()); // Aranacak ID'yi alıyoruz
                    if (secim == 1) // Kuyruk seçilmişse
                    {
                        kuyruk.Ara(araId); // Ürünü kuyrukta arıyoruz
                    }
                    else if (secim == 2) // Yığın seçilmişse
                    {
                        foreach (var item in yigin) // Yığın'da arama yapıyoruz
                        {
                            if (item.Id == araId) // Eğer ID eşleşiyorsa
                            {
                                Console.WriteLine($"Bulundu! ID: {item.Id}, Ad: {item.Ad}, Miktar: {item.Miktar}");
                                break; // Arama işlemini sonlandırıyoruz
                            }
                        }
                    }
                    break;

                case 4: // Ürünleri listeleme işlemi
                    if (secim == 1) // Kuyruk seçilmişse
                    {
                        kuyruk.Listele(); // Kuyruğu listele
                    }
                    else if (secim == 2) // Yığın seçilmişse
                    {
                        foreach (var item in yigin) // Yığın'daki tüm ürünleri gösteriyoruz
                        {
                            Console.WriteLine($"ID: {item.Id}, Ad: {item.Ad}, Miktar: {item.Miktar}");
                        }
                    }
                    break;

                case 5: // Ürünleri sıralama işlemi
                    if (secim == 1) // Kuyruk seçilmişse
                    {
                        kuyruk.Sirala(); // Kuyruğu sıralıyoruz
                    }
                    else if (secim == 2)
                    {
                        Console.WriteLine("Yığın sıralanamaz."); // Yığın sıralanamaz
                    }
                    break;

                case 0: // Programdan çıkış
                    return; // Uygulamadan çıkıyoruz

                default:
                    Console.WriteLine("Geçersiz seçim."); // Kullanıcıya hatalı seçim mesajı veriyoruz
                    break;
            }
        }
    }
}
