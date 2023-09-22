using CrawlingApp;

namespace System.Windows.CrawlingApp
{
    class Program  
    {
        public static void  Main()
        {
            // Hata yönetimi için try catch blokları kullanıyorum
            try
            {   /*  
                    Fonksiyonların ve sınıf özelliklerinin tutulduğu bir sınıf oluşturdum 
                    ve constructor oluşturarak kullanacağım internet sitesini parametre olarak verdim                                             
                */
                Crawling crawling = new Crawling("https://www.arabam.com");
                // Sayfanın kaynak kodunu alan fonksiyon
                crawling.GetPage();
                // Kaynak kodun içerisinden istediğim linkleri alan fonksiyon
                crawling.GetLinks(crawling.Page);
                // Gelen linklerin kaynak kodlarından istediğim bilgileri alan fonksiyon
                crawling.GetInfo(crawling.Host, crawling.Links);
                //Aldığım bilgileri tuttuğum Dictionary nesnesinden ayırıp konsola yazan kod parçası
                foreach (var ad in crawling.Advertisements)
                {
                    Console.WriteLine($"{ad.Key,-100} || {ad.Value,-25}");
                }
                /* 
                    Bilgilerdeki fiyatların ortalamasını almak için
                    listedeki verilerin tipini değiştirip başka bir listeye atıyorum.
                */
                List<double> Prices = crawling.Advertisements.Values.Select(p => double.Parse(p.Replace("TL", "").Trim())).ToList();
                // Güzel görünmesi adına ortalamayı bir alt satıra yazmak için boşluk bıraktım
                Console.WriteLine("\n");
                /* 
                    Konsola listedeki fiyatların ortalamasını yazdırdım
                    ve 3 ondalıklı sayı ile yazdırmak için ToString ile format değiştirdim.
                */
                Console.WriteLine($"Average: {Prices.Average().ToString("N3")}");

                /*
                    Bir txt dosyası oluşturuyorum ve dosya yolunu masaüstü olarak seçiyorum
                    Yukarıda yaptığım işlemler gibi aldığım bilgileri ve ortalamasını dosyaya yazdırıyorum
                    ve dosya nesnesi kullanımını kapatıyorum.
                */
                using (StreamWriter file = File.CreateText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Result.txt"))
                {
                    foreach (var ad in crawling.Advertisements)
                    {
                        file.WriteLine($"{ad.Key,-100} || {ad.Value,-25}");
                    }
                    file.WriteLine("\n");
                    file.WriteLine($"Average: {Prices.Average().ToString("N3")}");
                    file.Close();
                }                

            }
            catch (Exception)
            {
                // Herhangi bir hata durumunda hatayı göstermesi için gereken kod parçası
                throw;
            }

        }




    }
}